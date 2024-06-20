using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Muxx.Lib.Helpers;
using Muxx.Lib.Services;
using Muxx.Lib.ValueObjects.Enums;
using Muxx.UI.Windows;

namespace PDV
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public MainWindow()
      {
         InitializeComponent();
      }
      /// <summary>
      /// Extrai instalador PayGo Windows
      /// </summary>
      private static void extraiPGWin()
      {
         const string setupPayGoWindows = "SetupPayGo005.001.030.000_Update.exe";
         string resourceName;

         if (File.Exists(setupPayGoWindows))
            return;

         string directoryName = System.IO.Path.GetDirectoryName(setupPayGoWindows);
         if (string.IsNullOrEmpty(directoryName))
            directoryName = ".";
         if (!Directory.Exists(directoryName))
            Directory.CreateDirectory(directoryName);

         Assembly assembly = Assembly.GetExecutingAssembly();
         resourceName =
            assembly
            .GetManifestResourceNames()
            .FirstOrDefault(str => str.EndsWith(setupPayGoWindows, StringComparison.CurrentCultureIgnoreCase));

         using (Stream stream = assembly.GetManifestResourceStream(resourceName))
         using (FileStream fileStream = new FileStream(setupPayGoWindows, FileMode.Create))
            stream.CopyTo(fileStream);
      }
      /// <summary>
      /// Inicia acesso a documentação online
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private async void Documento_Click (object sender, RoutedEventArgs e)
      {
         string target = "https://paygodev.readme.io/docs";
         try
         {
            await Task.Run(() => Process.Start(target));
         }
         catch (System.ComponentModel.Win32Exception noBrowser)
         {
            if (noBrowser.ErrorCode == -2147467259)
               MessageBox.Show(noBrowser.Message);
         }
         catch (System.Exception other)
         {
            MessageBox.Show(other.Message);
         }
      }
      /// <summary>
      /// Ação do botão Update para liberar a aplicação da proteção
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private async void Update_Click (object sender, RoutedEventArgs e)
      {
         PGWebLib.PW_End();
         MessageBox.Show("Após atualizar a versão da automação, por favor reiniciar o PC");
         await Task.Run(() => Environment.Exit(-1));
      }
      /// <summary>
      /// Botão para instalar o PayGo Windows de forma automática e em segundo plano
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private async void Instal_Click (object sender, RoutedEventArgs e)
      {
         string cpfCnpj = "311.980.820-20";// Colocar o CNPJ do seu terminal
         string pontoDeCaptura = "999999999";   // Colocar o pdc do seu terminal
         /* Para ambiente de produção essa variável não é necessária. 
          * Por padrão  já configurado como produção*/
         string ambiente = "DEMO"; 

         extraiPGWin();
         // Configura as variáveis de ambiente para a ativação em segundo plano do PayGo Windows
         Environment.SetEnvironmentVariable("CPFCNPJ", cpfCnpj, EnvironmentVariableTarget.User);
         Environment.SetEnvironmentVariable("PontoDeCaptura", pontoDeCaptura, EnvironmentVariableTarget.User);
         Environment.SetEnvironmentVariable("AmbienteCPAY", ambiente, EnvironmentVariableTarget.User);
         await Task.Run(() => Process.Start("SetupPayGo005.001.030.000_Update.exe"));
      }
      /// <summary>
      /// Chamada de operação administrativa
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private async void Admin_Click(object sender, RoutedEventArgs e)
      {
         await NewTransacExecute(PWOPER.PWOPER_ADMIN);
      }
      /// <summary>
      /// Chamada de operação de venda
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      private async void Sale_Click(object sender, RoutedEventArgs e)
      {
         await NewTransacExecute(PWOPER.PWOPER_SALE);
      }

      private bool Cancelar()
      {
         return false;
      }
      /// <summary>
      /// Executa uma operação
      /// </summary>
      /// <param name="pwOper"></param>
      /// <returns></returns>
      private async Task NewTransacExecute(PWOPER pwOper)
      {
         Admin.IsEnabled = false;
         Sale.IsEnabled = false;
         Update.IsEnabled = false;

         Log.PrintThread("Iniciando...");

         TefWindow.Instance.TimeOut = null;
         TefWindow.Instance.BindMuxxLib();

         PGWebLib.DebugType = DebugType.Json;
         PGWebLib.DebugCallback = Log.PrintThread;

         Fluxos.CancelarOperacaoFunc = Cancelar;
         Fluxos.Clear();

         Fluxos.ParamsAdd(PWINFO.PWINFO_AUTNAME, "PDV");
         Fluxos.ParamsAdd(PWINFO.PWINFO_AUTVER, "1.0.0.0");
         Fluxos.ParamsAdd(PWINFO.PWINFO_AUTDEV, "PayGo");
         Fluxos.ParamsAdd(PWINFO.PWINFO_AUTCAP, (
            (int)PWINFO_AUTCAP.PWINFO_AUTCAP_DSP_CHECKOUT +
            (int)PWINFO_AUTCAP.PWINFO_AUTCAP_DSP_QRCODE
            ).ToString());
         //QRCode
         Fluxos.ParamsAdd(PWINFO.PWINFO_DSPQRPREF,
            ((int)PWINFO_DSPQRPREF.PWINFO_DSPQRPREF_EXIBE_CHECKOUT).ToString());

         Log.PrintThread(
            string.Format("Operação: [{0}]", pwOper.ToString()));

         bool status = await Fluxos.FluxoInitAsync();
         if (!status)
         {
            Log.PrintThread("Não foi possível inicializar a biblioteca");
            return;
         }

         PWCNF pwCnf;
         status = await Fluxos.FluxoPrincipalAsync(pwOper);
         if (status)
         {
            Log.PrintThread("Transação: realizada com sucesso");
            pwCnf = PWCNF.PWCNF_CNF_AUTO;
         }
         else
         {
            Log.PrintThread("Transação: Não foi possível concluir sua transação");
            pwCnf = PWCNF.PWCNF_REV_MANU_AUT;
         }

         if (Fluxos.PossuiPendencia())
         {
            Log.PrintThread("Existe alguma transação pendente de confirmação no PayGoWeb...");

            //Nesse exemplo estou confirmando, mas o correto é verificar o status 
            //dessa transação na sua automação, para confirmar ou desfazer a mesma.
            if (Fluxos.FluxoConfirmacaoPendencia(PWCNF.PWCNF_CNF_AUTO))
            {
               Log.PrintThread("Confirmada!!!");
            }
            else
            {
               Log.PrintThread("Não Confirmada!!!");
            }
         }

         TefWindow.Instance.BindDisplayAguarde();

         Log.PrintThread("Resultados:");

         await Fluxos.FluxoGetResultPwInfosAsync();
         foreach (var info in Fluxos.ResultsEnviadosComSucesso)
         {
            Log.PrintThread(info.ToString());
         }

         if (Fluxos.RequerConfirmacao())
         {
            Log.PrintThread("Confirmando a transação...");

            if (Fluxos.FluxoConfirmacao(pwCnf))
            {
               Log.PrintThread("Confirmada!!!");
            }
            else
            {
               Log.PrintThread("Não Confirmada!!!");
            }
         }

         Log.PrintThread("Operação Finalizada!");

         TefWindow.Instance.Hide();

         Admin.IsEnabled = true;
         Sale.IsEnabled = true;
         Update.IsEnabled = true;
      }
      /// <summary>
      /// Faz verificações de atualização ao encerar a aplicação
      /// </summary>
      /// <param name="e"></param>
      protected override void OnClosed(EventArgs e)
      {
         string existeAtualizacao = Environment.GetEnvironmentVariable("PGWebLibAtualiza");
         if (Convert.ToBoolean(existeAtualizacao))
            Environment.SetEnvironmentVariable("PGWebLibPermiteAtualiza", "TRUE", EnvironmentVariableTarget.User);
         else
            Environment.SetEnvironmentVariable("PGWebLibPermiteAtualiza", "FALSE", EnvironmentVariableTarget.User);
         Environment.Exit(-1);
      }

   }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Muxx.Lib.Helpers
{
   public static class Log
   {
      // Pasta raiz para o log
      private static string m_exePath = ".";

      public static void PrintThread(string mensagem)
      {
         string logMessage = string.Format("[Thread {0,2}][{1:HH:mm:ss.fff}] {2}", Thread.CurrentThread.ManagedThreadId.ToString(), DateTime.Now, mensagem);
         Debug.Print(logMessage);
         LogWrite(logMessage);
      }

      private static void LogWrite(string logMessage)
      {
         m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
         try
         {
            using (StreamWriter w = File.AppendText(m_exePath + "\\" + "PDV_log.txt"))
            {
               LogFile(logMessage, w);
            }
         }
         catch (Exception ex)
         {
         }
      }

      private static void LogFile (string logMessage, TextWriter txtWriter)
      {
         try
         {
            txtWriter.Write("\r\nLog Entry : ");
            txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            txtWriter.WriteLine("  :");
            txtWriter.WriteLine("  :{0}", logMessage);
            txtWriter.WriteLine("-------------------------------");
         }
         catch (Exception ex)
         {
         }
      }

      public static void PrintThread(params string[] mensagens)
      {
         mensagens = mensagens.SelectMany(m => m.Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None)).ToArray();
         PrintThread(
            string.Join(
            Environment.NewLine,
            mensagens.Select(msg => string.Format("{0}", msg))));
      }

   }
}
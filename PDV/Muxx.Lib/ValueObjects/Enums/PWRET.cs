﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muxx.Lib.ValueObjects.Enums
{
   public enum PWRET
   {
      // Erros gerais

      /// <summary>
      /// Operação bem-sucedida.
      /// </summary>
      PWRET_OK = 0,
      /// <summary>
      /// Existe uma transação pendente, é necessário
      /// confirmar ou desfazer essa transação através de
      /// PW_iConfirmation.
      /// </summary>
      PWRET_FROMHOSTPENDTRN = -2599,
      PWRET_FROMHOSTPOSAUTHERR,
      PWRET_FROMHOSTUSRAUTHERR,
      PWRET_FROMHOST,
      PWRET_TLVERR,
      PWRET_SRVINVPARAM,
      PWRET_REQPARAM,
      PWRET_HOSTCONNUNK,
      PWRET_INTERNALERR,
      PWRET_BLOCKED,
      PWRET_FROMHOSTTRNNFOUND,
      PWRET_PARAMSFILEERR,
      PWRET_NOCARDENTMODE,
      PWRET_INVALIDVIRTMERCH,
      PWRET_HOSTTIMEOUT,
      PWRET_CONFIGREQUIRED,
      PWRET_HOSTCONNERR,
      PWRET_HOSTCONNLOST,
      PWRET_FILEERR,
      PWRET_PINPADERR,
      PWRET_MAGSTRIPEERR,
      PWRET_PPCRYPTERR,
      PWRET_SSLCERTERR,
      PWRET_SSLNCONN,
      PWRET_GPRSATTACHFAILED,
      PWRET_EMVDENIEDCARD,
      PWRET_EMVDENIEDHOST,
      PWRET_NOLINE,
      PWRET_NOANSWER,
      PWRET_SYNCERROR,
      PWRET_CRCERR,
      PWRET_DECOMPERR,
      PWRET_PROTERR,
      PWRET_NOSIM,
      PWRET_SIMERROR,
      PWRET_SIMBLOCKED,
      PWRET_PPPNEGFAILED,
      PWRET_WIFICONNERR,
      PWRET_WIFINOTFOUND,
      PWRET_COMPERR,
      PWRET_INVALIDCPFCNPJ,
      PWRET_APNERROR,
      PWRET_WIFIAUTHERR,
      PWRET_QRCODEERR,
      PWRET_QRCODENOTSUPPORTED,
      PWRET_QRCODENOTFOUND,
      PWRET_DEFAULT_COMM_ERROR,
      PWRET_CTLSMAGSTRIPENOTALLOW,
      PWRET_PARAMSFILEERRSIZE,
      /* Inserir novos erros gerais somente AQUI */

      // Erros específicos da biblioteca
      PWRET_INVPARAM = -2499,
      PWRET_NOTINST,
      PWRET_MOREDATA,
      PWRET_NODATA,
      PWRET_DISPLAY,
      PWRET_INVCALL,
      PWRET_NOTHING,
      PWRET_BUFOVFLW,
      PWRET_CANCEL,
      PWRET_TIMEOUT,
      PWRET_PPNOTFOUND,
      PWRET_TRNNOTINIT,
      PWRET_DLLNOTINIT,
      PWRET_FALLBACK,
      PWRET_WRITERR,
      PWRET_PPCOMERR,
      PWRET_NOMANDATORY,
      PWRET_OFFINTERNAL,
      PWRET_OFFINVCAP,
      PWRET_OFFNOCARDENTMODE,
      PWRET_OFFINVCARDENTMODE,
      PWRET_OFFNOTABLECARDRANGE,
      PWRET_OFFNOTABLEPRODUCT,
      PWRET_OFFINVTAG,
      PWRET_OFFNOCARDFULLPAN,
      PWRET_OFFINVCARDEXPDT,
      PWRET_OFFCARDEXP,
      PWRET_OFFNOTRACKS,
      PWRET_OFFTRACKERR,
      PWRET_OFFCHIPMANDATORY,
      PWRET_OFFINVCARD,
      PWRET_OFFINVCURR,
      PWRET_OFFINVAMOUNT,
      PWRET_OFFGREATERAMNT,
      PWRET_OFFLOWERAMNT,
      PWRET_OFFGREATERINST,
      PWRET_OFFLOWERINST,
      PWRET_OFFINVCARDTYPE,
      PWRET_OFFINVFINTYPE,
      PWRET_OFFINVINST,
      PWRET_OFFGREATERINSTNUM,
      PWRET_OFFLOWERINSTNUM,
      PWRET_OFFMANDATORYCVV,
      PWRET_OFFINVLASTFOUR,
      PWRET_OFFNOAID,
      PWRET_OFFNOFALLBACK,
      PWRET_OFFNOPINPAD,
      PWRET_OFFNOAPOFF,
      PWRET_OFFTRNNEEDPP,
      PWRET_OFFCARDNACCEPT,
      PWRET_OFFTABLEERR,
      PWOFF_OFFMAXTABERR,
      PWRET_OFFINTERNAL1,
      PWRET_OFFINTERNAL2,
      PWRET_OFFINTERNAL3,
      PWRET_OFFINTERNAL4,
      PWRET_OFFINTERNAL5,
      PWRET_OFFINTERNAL6,
      PWRET_OFFINTERNAL7,
      PWRET_OFFINTERNAL8,
      PWRET_OFFINTERNAL9,
      PWRET_OFFINTERNAL10,
      PWRET_OFFINTERNAL11,
      PWRET_OFFNOPRODUCT,
      PWRET_OFFINTERNAL12,
      PWRET_OFFINTERNAL13,
      PWRET_OFFINTERNAL14,
      PWRET_NOPINPAD,
      PWRET_OFFINTERNAL15,
      PWRET_OFFINTERNAL16,
      PWRET_ABECSERRCOM,
      PWRET_OFFCFGNOCARDRANGE,
      PWRET_OFFCFGNOPRODUCT,
      PWRET_OFFCFGNOTRANSACTION,
      PWRET_OFFINTERNAL17,
      PWRET_OFFINTERNAL18,
      PWRET_PPABORT,
      PWRET_OFFINTERNAL19,
      PWRET_PPERRTREATMENT,
      PWRET_INVPAYMENTMODE,
      PWRET_OFFINVALIDOPER,
      PWRET_OFFINTERNAL20,
      PWRET_OFFINTERNAL21,
      /* Inserir novos erros de processamento local somente AQUI */
      PWRET_OFFEND,
      /* Inserir novos erros da biblioteca somente AQUI */

      #region Erros específicos da biblioteca compartilhada de PIN-pad

      PWRET_PPS_MAX = -2100,
      PWRET_PPS_MIN = PWRET_PPS_MAX - 100,

      /* Status de -2199 a -2180 : Erros de processamento de cartão com chip sem contato */
      PWRET_PPS_CTLSIFCHG = PWRET_PPS_MAX - 87,
      PWRET_PPS_CTLSEXTCVM,
      PWRET_PPS_CTLSSAPPNAUT,
      PWRET_PPS_CTLSSAPPNAV,
      PWRET_PPS_CTLSSPROBLEMS,
      PWRET_PPS_CTLSSINVALIDAT,
      PWRET_PPS_CTLSSCOMMERR,
      PWRET_PPS_CTLSSMULTIPLE,
      PWRET_PPS_CTCARDBLOCKED,

      /* Status de -2179 a -2160 : Erros de processamento de cartão com chip com contato */
      PWRET_PPS_ERRFALLBACK = PWRET_PPS_MAX - 76,
      PWRET_PPS_VCINVCURR,
      PWRET_PPS_CARDNOTEFFECT,
      PWRET_PPS_LIMITEXC,
      PWRET_PPS_NOBALANCE,
      PWRET_PPS_CARDAPPNAUT,
      PWRET_PPS_CARDAPPNAV,
      PWRET_PPS_CARDINVDATA,
      PWRET_PPS_CARDPROBLEMS,
      PWRET_PPS_CARDINVALIDAT,
      PWRET_PPS_CARDERRSTRUCT,
      PWRET_PPS_CARDEXPIRED,
      PWRET_PPS_CARDNAUTH,
      PWRET_PPS_CARDBLOCKED,
      PWRET_PPS_CARDINV,
      PWRET_PPS_ERRCARD,
      PWRET_PPS_DUMBCARD,

      /* Status de -2159 a -2150 : Erros de processamento de cartão com chip (SAM) */
      PWRET_PPS_SAMINV = PWRET_PPS_MAX - 52,
      PWRET_PPS_NOSAM,
      PWRET_PPS_SAMERR,

      /* Status de -2149 a -2140 : Erros básicos reportados pelo pinpad */
      PWRET_PPS_PINBUSY = PWRET_PPS_MAX - 44,
      PWRET_PPS_NOCARD,
      PWRET_PPS_ERRPIN,
      PWRET_PPS_MCDATAERR,
      PWRET_PPS_INTERR,

      /* Status de -2139 a -2130 : Erros de comunicação/protocolo com o pinpad */
      PWRET_PPS_COMMTOUT = PWRET_PPS_MAX - 34,
      PWRET_PPS_RSPERR,
      PWRET_PPS_UNKNOWNSTAT,
      PWRET_PPS_COMMERR,
      PWRET_PPS_PORTERR,

      /* Status de -2129 a -2110 : Erros básicos da biblioteca */
      PWRET_PPS_NOAPPLIC = PWRET_PPS_MAX - 22,
      PWRET_PPS_TABERR,
      PWRET_PPS_TABEXP,
      RESERVED,
      PWRET_PPS_NOFUNC,
      PWRET_PPS_INVMODEL,
      PWRET_PPS_EXECERR,
      PWRET_PPS_NOTOPEN,
      PWRET_PPS_ALREADYOPEN,
      PWRET_PPS_CANCEL,
      PWRET_PPS_TIMEOUT,
      PWRET_PPS_INVPARM,
      PWRET_PPS_INVCALL,

      /* Status de -2109 a -2100  : Não representam erros */
      PWRET_PPS_BACKSP = PWRET_PPS_MAX - 8,
      PWRET_PPS_F4,
      PWRET_PPS_F3,
      PWRET_PPS_F2,
      PWRET_PPS_F1,
      PWRET_PPS_NOTIFY = PWRET_PPS_MAX - 2,
      PWRET_PPS_PROCESSING,
      PWRET_PPS_OK

      #endregion
   };

}
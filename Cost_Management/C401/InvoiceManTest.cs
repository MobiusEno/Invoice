
using NLog;
using Plusmore.Einvoice.Common.Sample.Helper;
using Plusmore.Utility.EscPos.Model.WinPos;

namespace Plusmore.Einvoice.Common.Sample.Model.C0401
{
  
    public partial class InvoiceManTest
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public static Wpt810Printer Prt = new Wpt810Printer( MyConfig.PrinterPort );

        #region ClassInitialize and Cleanup

       
        public static void InvoiceManTest_ClassInit(  )
        {
            Prt.Open();
            Prt.InitializePrinter();
        }

        
        public static void InvoiceManTest_ClassCleanup()
        {
            Prt.Close();
            Prt = null;
        }

        #endregion ClassInitialize and Cleanup

    }
}

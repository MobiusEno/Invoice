//
// File: Plusmore.Utility.EscPos.Tests.UnitTest / Wpt810InvoiceTests.cs
//
// Author: Pay
// Created: 2017-01-06 14:48
//
// Modified: 2017-01-07 12:41
//

using System;

using NLog;
using Plusmore.Einvoice.Common.Sample.Helper;
using Plusmore.Utility.EscPos.Controller;
using Plusmore.Utility.EscPos.Model.WinPos;

namespace Plusmore.Einvoice.Common.Sample.Printer
{
   
    public class Wpt810InvoiceTests
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        #region Printer

        private static Wpt810Printer _printer;

        public static Wpt810Printer Printer
        {
            get
            {
                if ( _printer != null )
                {
                    return _printer;
                }

                _printer = new Wpt810Printer( MyConfig.PrinterPort )
                {
                    LogoHeight = 66
                };

                _printer.ComError += ComErrorEvent;
                _printer.ComOpened += ComOpenedEvent;
                _printer.ComClosed += ComClosedEvent;

                return _printer;
            }
            set { _printer = value; }
        }

        private static void ComErrorEvent( object o, ComMessageEventArgs e )
        {
            Logger.Error( e.Message );
        }

        private static void ComOpenedEvent( object o, EventArgs e )
        {
            Logger.Debug( "COM opened" );
        }

        private static void ComClosedEvent( object o, EventArgs e )
        {
            Logger.Debug( "COM closed" );
        }

        #endregion Printer

        #region ClassInitialize and Cleanup

      
        public static void PrinterBasicTests_init()
        {
            Printer.Open();
        }

       
        public static void PrinterBaseTest_DesInit()
        {
            Printer.Close();
            Printer = null;
        }

        #endregion ClassInitialize and Cleanup

      
        public void Wpt810InvoiceTests_Test()
        {
        }

     
        public void Wpt810InvoiceTests_PrintHelloWorld()
        {
            Printer.InitializePrinter();

            Printer.PrintHelloWorld();

            Printer.CutPaper(  );
        }
    }
}
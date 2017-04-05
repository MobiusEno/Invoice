// File: Plusmore.Einvoice.Common.Tests / CancelInvoiceManTests.cs
// 
// Created: 2016-12-31 15:56
// Modified: 2016-12-31 19:44

using System;
using Cost_Management.Models;
using System.Linq;
using NLog;
using Plusmore.Einvoice.Common.Model.C0501;

namespace Plusmore.Einvoice.Common.Sample.Model.C0501
{
   
    public class CancelInvoiceManTests
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly string _path = Environment.GetFolderPath( Environment.SpecialFolder.Desktop );

      
        public void CancelInvoiceManTests_toJson(Invoices OpenData)
        {
            var cim = new CancelInvoiceMan
            {
                InvoiceNumber = OpenData.InvoicesNum,
                InvoiceDate = Convert.ToDateTime(OpenData.InvoicesDate),
                BuyerId = OpenData.CustomerValue,
                CancelDate = DateTime.Now,
                CancelReason = OpenData.Cancel
            };

            Logger.Debug( "CancelInvoiceMan.json: {0}", cim.ToJson() );

            cim.Save( String.Format( @"{0}\delme\C0501\C0501-{1}.json", this._path, cim.InvoiceNumber ) );
        }
    }
}

using Plusmore.Einvoice.Common.Sample.Helper;

namespace Plusmore.Einvoice.Common.Sample.Model.C0401
{
    partial class InvoiceManTest
    {

        /// <summary>
        ///     一般消費者 
        /// </summary>
        
        public void InvoiceManTest_Case00_CompanyName_and_Logo()
        {
            var im = this.Sample1InvoiceMan;

            im.Main.InvoiceNumber = "TW00000001";

            // company name
            im.Print( Prt, MyConfig.CompanyName, MyConfig.AesKey);

            // logo
            im.Print( Prt, MyConfig.AesKey);
        }

    }
}
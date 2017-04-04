
using Plusmore.Einvoice.Common.Helper;

namespace Plusmore.Einvoice.Common.Sample.Helper
{
 
    public class ValidatorTests
    {
     
        public void ValidatorTests_Vat()
        {
            const string vat = "24592118";
            const string tenZero = "0000000000";
            const string wrong1 = "245921188888";
            const string wrong2 = "1234567";

         
        }

   
        public void ValidatorTests_LoveCode()
        {
            const string code1 = "123";
            const string code2 = "00132";
            const string code3 = "1234567";
            const string wrong1 = "12345678";
            const string wrong2 = "245921188888";
            const string wrong3 = "1";
            const string wrong4 = "12";

          
        }

      
        public void ValidatorTests_InvoiceNumber()
        {
            const string invoiceNumber = "AB12345678";
            const string wrong1 = "AB245921188888";
            const string wrong2 = "AB1234567";

        
        }


        public void ValidatorTests_MobilePhoneCode()
        {
            const string code1 = "/Y6G17YP";
            const string code2 = "/Y6G17Y.";
            const string code3 = "/Y6G17Y-";
            const string code4 = "/Y++--Y-";
            const string wrong1 = "Y6G17YP";
            var wrong2 = "/Y6G17YP".ToLower();
            const string wrong3 = "1";
            const string wrong4 = "12";

        }
        public void ValidatorTests_CitizenDigitalCertificate()
        {
            const string code1 = "AB12345678901234";
            const string wrong1 = "AB1234567890";
            var wrong2 = code1.ToLower();
            const string wrong3 = "AB1234567890123400000000";
            const string wrong4 = "12";

        }
    }
}
// File: Plusmore.Einvoice.Common.Tests / RandomNumberHelperTests.cs
// 
// Author: Pay
// Created: 2017-01-12 14:06
// 
// Modified: 2017-01-12 14:14


using NLog;
using Plusmore.Einvoice.Common.Helper;

namespace Plusmore.Einvoice.Common.Sample.Helper
{
  
    public class RandomNumberHelperTests
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

      
        public void RandomNumberHelperTests_Validate()
        {
            const string num = "1234";
            const string wrongNum1 = "12";
            const string wrongNum2 = "ABCD";

          
        }

       
        public void RandomNumberHelperTests_RandomNumber()
        {
            var r = RandomNumberHelper.RandomNumber();

            Logger.Debug( "random number: {0}", r );

            for ( var i = 0; i <= 1000; i++ )
            {
                r = RandomNumberHelper.RandomNumber();

              
            }
        }
    }
}
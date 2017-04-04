// File: Plusmore.Einvoice.Common.Sample / InvoiceManTest.Case04.cs
// 
// Author: Pay
// Created: 2017-01-17 16:23
// 
// Modified: 2017-01-18 14:33

using System;

using Plusmore.Einvoice.Common.Helper;
using Plusmore.Einvoice.Common.Model.General;
using Plusmore.Einvoice.Common.Sample.Helper;

namespace Plusmore.Einvoice.Common.Sample.Model.C0401
{
    partial class InvoiceManTest
    {
        /// <summary>
        ///     消費者 捐贈發票
        ///     1. Print Mark = N
        ///     2. loveCode = 愛心碼 或 社福統編
        /// </summary>
       
        public void InvoiceManTest_Case04_general_buyer_love_code()
        {
            var im = this.Sample1InvoiceMan;

            im.Main.InvoiceNumber = "TW00000006";

            // 愛心碼 
            const string loveCode = "123";

            // 驗證 愛心碼 
          

            im.Main.PrintMark = YnEnum.N;
            im.Main.LoveCode = loveCode;

            // 正式上線, 可以不用驗證 假如需要驗證, 有驗證異常的發票, 要進行異常處理程序 
            var v = im.Validate();

            if ( v.IsValid == false )
            {
                Logger.Debug( v.ToString() );
            }

          
            // 得到 上傳的 json data 
            Logger.Debug( im.ToJson() );

            // 儲存 上傳的檔案 
            im.Save( String.Format( @"{0}\C0401\C0401-{1}.json", MyConfig.Folder, im.Main.InvoiceNumber ) );

            // PrintMark = N, 無法列印 
            im.Print( Prt, MyConfig.AesKey, hasPrintList: false, reprint: false );

            // 可選擇列印明細 
            im.PrintList( Prt );
        }
    }
}
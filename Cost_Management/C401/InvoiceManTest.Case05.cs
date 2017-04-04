using System;

using NLog;
using Plusmore.Einvoice.Common.Helper;
using Plusmore.Einvoice.Common.Model.General;
using Plusmore.Einvoice.Common.Sample.Helper;

namespace Plusmore.Einvoice.Common.Sample.Model.C0401
{
    partial class InvoiceManTest
    {
        /// <summary>
        ///     消費者 持 手機條碼 + 捐贈發票
        ///     1. Print Mark = N
        ///     2. loveCode = 愛心碼 或 社福統編
        ///     3. Carrier Type = 3J0002
        ///     4. CarrierId1 = 手機條碼
        ///
        ///     消費者 持 自然人憑證 + 捐贈發票
        ///     1. Print Mark = N
        ///     2. loveCode = 愛心碼 或 社福統編
        ///     3. Carrier Type = CQ0001
        ///     4. CarrierId1 = 自然人憑證條碼
        ///  
        ///     消費者 持 會員載具 + 捐贈發票
        ///     1. Print Mark = N
        ///     2. loveCode = 愛心碼 或 社福統編
        ///     3. Carrier Type = 會員載具代碼
        ///     4. CarrierId1 = 會員載具資訊, 顯碼
        ///     5. CarrierId2 = 會員載具資訊, 隱碼
        /// </summary>
     
        public void InvoiceManTest_Case05_general_buyer_mobile_phone_code_love_code()
        {
            var im = this.Sample1InvoiceMan;

            im.Main.InvoiceNumber = "TW00000007";

            // 沒有列印出發票 
            im.Main.PrintMark = YnEnum.N;

            // 愛心碼 
            const string loveCode = "123";

            // 驗證 愛心碼 
           

            im.Main.LoveCode = loveCode;

            // 手機條碼 
            const string code = "/ABCD.+-";

            // 驗證 手機條碼 
        

            im.Main.CarrierType = "3J0002";
            im.Main.CarrierId1 = code;

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
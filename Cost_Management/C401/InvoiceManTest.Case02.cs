using System;

using Plusmore.Einvoice.Common.Model.C0401;
using Plusmore.Einvoice.Common.Sample.Helper;

namespace Plusmore.Einvoice.Common.Sample.Model.C0401
{
    partial class InvoiceManTest
    {
        /// <summary>
        ///     零元發票
        ///     1. 使用折價卷
        ///     2. 明細有可能是負的
        ///     3. 有開立發票就需要上傳
        ///     4. 零元發票 不列印 不提供給消費者
        ///     5. 零元發票 無法兌獎
        /// </summary>
       
        public void InvoiceManTest_Case02_general_buyer_zero()
        {
            var im = Sample1InvoiceMan;

            var coupon = new InvoiceMan.InvDetail.ProductItem
            {
                SequenceNumber = "003",
                RelateNumber = "GIFT-0001",
                Description = "折價卷",
                UnitPrice = -1,
                Quantity = 300,
                Amount = -300
            };

            im.Detail.ProductItems.Add( coupon );

            im.Amount.SalesAmount = 0;
            im.Amount.TotalAmount = 0;

            im.Main.InvoiceNumber = "TW00000003";

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

            // 零元發票無法列印, 會列印明細 
            im.Print( Prt, MyConfig.AesKey, hasPrintList: true, reprint: false );
        }
    }
}
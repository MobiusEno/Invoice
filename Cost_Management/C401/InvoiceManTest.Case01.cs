using System;
using System.Collections.Generic;
using Plusmore.Einvoice.Common.Helper;
using Plusmore.Einvoice.Common.Model.C0401;
using Plusmore.Einvoice.Common.Model.General;
using Plusmore.Einvoice.Common.Sample.Helper;
using Cost_Management.Models;
using System.Linq;

namespace Plusmore.Einvoice.Common.Sample.Model.C0401
{
    partial class InvoiceManTest
    {
        public PONO_DatabaseEntities db = new PONO_DatabaseEntities();
        private InvoiceTotal InvoiceData = new InvoiceTotal();
        
        public InvoiceMan Sample1InvoiceMan
        {
            get
            {
                return new InvoiceMan
                {
                    Main = new InvoiceMan.InvMain
                    {
                        InvoiceNumber = InvoiceData.Main.InvoicesNum,
                        InvoiceDate = Convert.ToDateTime(InvoiceData.Main.InvoicesDate),
                        CarrierId1= InvoiceData.Main.ToolCode,
                        LoveCode = InvoiceData.Main.LoveCode,
                        Seller = new InvRole
                        {
                            Identifier = InvoiceData.Main.Value,
                            Name = InvoiceData.Main.Store
                        },

                        PrintMark = YnEnum.Y,
                        RandomNumber = RandomNumberHelper.RandomNumber(),
                        RelateNumber = "領餐號碼-" + (InvoiceData.Main.InvoicesNum).Substring(InvoiceData.Main.InvoicesNum.Length - 3)  //訂單編號, 列印明細用, 可以省略
                    },

                    Detail = new InvoiceMan.InvDetail
                    {

                        ProductItems = new List<InvoiceMan.InvDetail.ProductItem>
                        {
                        }
                    },

                    Amount = new InvoiceMan.InvAmount
                    {
                        SalesAmount = Convert.ToInt64(InvoiceData.Main.InvoicesTotal),
                        TotalAmount = Convert.ToInt64(InvoiceData.Main.InvoicesTotal)
                    }
                };
            }
        }

        /// <summary>
        ///     一般消費者 
        /// </summary>
       
        public void InvoiceManTest_Case01_general_buyer(Invoices OpenData,bool complement)
        {
            InvoiceData.Main = db.Invoices.Find(OpenData.InvoicesNum);
            InvoiceData.Detail = db.InvoiceDetail.Where(p => p.InvoicesNum.Equals(OpenData.InvoicesNum)).ToList();
            var im = this.Sample1InvoiceMan;
           
            im.Main.InvoiceNumber = OpenData.InvoicesNum;
            for (int i = 0; i < InvoiceData.Detail.Count; i++)
            {
                im.Detail.ProductItems.Add(new InvoiceMan.InvDetail.ProductItem()
                {
                    SequenceNumber = "00" + Convert.ToString(i + 1),
                    //RelateNumber =  //產品編號, 列印明細用, 可以省略
                    Description = InvoiceData.Detail[i].ProductName,
                    UnitPrice = (float)InvoiceData.Detail[i].Price,
                    Quantity = (float)InvoiceData.Detail[i].Qty,
                    Amount = (float)InvoiceData.Detail[i].TotalPrice
                });
              }
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

            // hasPrintList=true: 消費者要求列印明細 reprint=true: 消費者因為發票破損無法辨識 qrcode, barcode...再列印一張"補印字樣"的發票, 需要記錄 補印 最好限印一次 
            im.Print(Prt, OpenData.Name, MyConfig.AesKey, hasPrintList: true, reprint: complement);
        }

        /// <summary>
        ///     打統編 
        /// </summary>
       
        public void InvoiceManTest_Case01_vat_buyer(Invoices OpenData)
        {
            InvoiceData.Main = db.Invoices.Find(OpenData.InvoicesNum);
            InvoiceData.Detail = db.InvoiceDetail.Where(p => p.InvoicesNum.Equals(OpenData.InvoicesNum)).ToList();
            var im = this.Sample1InvoiceMan;
            im.Main.InvoiceNumber = OpenData.InvoicesNum;
            for (int i = 0; i < InvoiceData.Detail.Count; i++)
            {
                im.Detail.ProductItems.Add(new InvoiceMan.InvDetail.ProductItem()
                {
                    SequenceNumber = "00" + Convert.ToString(i + 1),
                    //RelateNumber =  //產品編號, 列印明細用, 可以省略
                    Description = InvoiceData.Detail[i].ProductName,
                    UnitPrice = (float)InvoiceData.Detail[i].Price,
                    Quantity = (float)InvoiceData.Detail[i].Qty,
                    Amount = (float)InvoiceData.Detail[i].TotalPrice
                });
            }
            im.Main.Buyer = new InvRole()
            {
                Identifier = OpenData.CustomerValue,
                //Name = "福氣有限公司"  //非必填
            };

            // 打統編發票, 需要拆算 SalesAmount and TaxAmount 
            im.Amount.SalesAmount = 286;
            im.Amount.TaxAmount = 14;

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

            // 打統編的發票, 一律列印明細, 可以列印多次 不會有補印字樣, 
            im.Print( Prt, MyConfig.AesKey );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cost_Management.Models;
using Plusmore.Einvoice.Common.Sample.Model.C0401;

namespace Cost_Management.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        
        public PONO_DatabaseEntities db = new PONO_DatabaseEntities();
        #region//新增/刪除訂單內容
        public ActionResult Order()
        {
            InvoiceDetailViewModel NewData = new InvoiceDetailViewModel();
            ViewData["Receipts"] = db.Receipts.Where(p=>p.IsEnabled==true).ToList();
            ViewData["InvoiceDetail"] = db.InvoiceDetail.ToList();

            return View(NewData);
        }
        [HttpPost]
        public ActionResult Order(InvoiceDetailViewModel Data)
        {
            IQueryable<Invoices> DemandData = db.Invoices
          .Where(p => p.InvoicesNum.Equals(Data.InvoicesNum));

            Invoices NewData1 = new Invoices();
            InvoiceDetail NewData = new InvoiceDetail();
            bool hasElements = DemandData.AsQueryable().Any();
            //沒主單則新增
            if (!hasElements)
            {
                NewData1.InvoicesNum = Data.InvoicesNum;
                
                db.Invoices.Add(NewData1);
                db.SaveChanges();

                NewData.InvoicesNum = Data.InvoicesNum;
                //包包
                if (Data.ProductClass == "1")
                {
                    NewData.ProductName = Data.ProductContent1 + Data.ProductContent0 + "包";
                    NewData.Note = Data.Note;
                }
                //飲料
                else
                {
                    NewData.ProductName = Data.DrinkContent;
                    NewData.Note = Data.DrinkContent0 + "．" + Data.DrinkContent1 + "．" + Data.DrinkContent2 + "．" + Data.Note;
                }

                NewData.Qty = Data.Qty;
                Products productprice = db.Products.Where(p => p.ProductionName.Equals(NewData.ProductName)).FirstOrDefault();
                var sum = (productprice.Prices) * NewData.Qty;
                NewData.Price = productprice.Prices;
                NewData.TotalPrice = sum;
                db.InvoiceDetail.Add(NewData);
                db.SaveChanges();
            }
            //已經有主單新增明細
            else
            {
                NewData.InvoicesNum = Data.InvoicesNum;
                //包包
                if (Data.ProductClass == "1")
                {
                    NewData.ProductName = Data.ProductContent1 + Data.ProductContent0 + "包";
                    NewData.Note = Data.Note;
                }
                //飲料
                else
                {
                    NewData.ProductName = Data.DrinkContent;
                    NewData.Note = Data.DrinkContent0 + "．" + Data.DrinkContent1 + "．" + Data.DrinkContent2 + "．" + Data.Note;
                }

                NewData.Qty = Data.Qty;
                Products productprice = db.Products.Where(p => p.ProductionName.Equals(NewData.ProductName)).FirstOrDefault();
                var sum = (productprice.Prices) * NewData.Qty;
                NewData.Price = productprice.Prices;
                NewData.TotalPrice = sum;
                
                db.InvoiceDetail.Add(NewData);
                db.SaveChanges();
            }

            return RedirectToAction("Order");
        }
        public ActionResult DeleteDetail(int Id)
        {
            InvoiceDetail oldData = db.InvoiceDetail.Find(Id);
            db.InvoiceDetail.Remove(oldData);
            db.SaveChanges();
            return RedirectToAction("Order");
        }
        #endregion
        #region//儲存訂單
        public ActionResult OrderSubmit(string Invoice, int OrderSum,string orderStatus)
        {
            Invoices EditData = db.Invoices.Find(Invoice);
            StoreInformation StoreInformation = db.StoreInformation.Where(p => p.Status==true).FirstOrDefault();
            Receipts ReceiptsInformation = db.Receipts.Where(p => p.IsEnabled==true).FirstOrDefault();
            EditData.Name = StoreInformation.Name;
            EditData.Store = StoreInformation.Store;
            EditData.InvoicePeriod = ReceiptsInformation.InvoicePeriod;
            EditData.InvoicesDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            EditData.InvoicesTotal = Convert.ToString(OrderSum);
            EditData.Value = StoreInformation.Value;

            EditData.orderStatus = orderStatus;

            EditData.Note = null;
            EditData.Status = false;
            ReceiptsInformation.CurrentNum = ((Convert.ToInt32(ReceiptsInformation.CurrentNum)) + 1).ToString();
            db.SaveChanges();
            return RedirectToAction("Demand");
        }
        #endregion
        #region//訂單直開發票
        public ActionResult OrderC401(string Invoice, int OrderSum, string orderStatus)
        {
            Invoices EditData = db.Invoices.Find(Invoice);
            StoreInformation StoreInformation = db.StoreInformation.Where(p => p.Status==true).FirstOrDefault();
            Receipts ReceiptsInformation = db.Receipts.Where(p => p.IsEnabled==true).FirstOrDefault();
            EditData.Name = StoreInformation.Name;
            EditData.Store = StoreInformation.Store;
            EditData.InvoicePeriod = ReceiptsInformation.InvoicePeriod;
            EditData.InvoicesDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            EditData.InvoicesTotal = Convert.ToString(OrderSum);
            EditData.Value = StoreInformation.Value;
           
            EditData.orderStatus = orderStatus;

            EditData.Note = null;
            EditData.Status = false;
            ReceiptsInformation.CurrentNum = ((Convert.ToInt32(ReceiptsInformation.CurrentNum)) + 1).ToString();
            db.SaveChanges();
            return RedirectToAction("C401", "Invoice", new { InvoicesNum = Invoice});
        }
        #endregion
        #region//發票查詢

        public ActionResult Demand(string sortOrder, string Search, string DateSearch ,int Page = 0)
        {
            DemandViewModel Data = new DemandViewModel();
           
            //將傳入值Search(搜尋)放入頁面模型中
            if (!String.IsNullOrEmpty(DateSearch))
            {
                Data.DateSearch = DateSearch;
            }
            else
            {
                Data.DateSearch = DateSearch;
            }
            Data.Search = Search;
            Data.sortOrder = sortOrder;
            //新增頁面模型中的分頁
            if (Page == 0)
            {
                Data.Paging = new ForPaging();
            }
            else
            {
                Data.Paging = new ForPaging(Page);
            }
            Data.DataList = GetDataList(Data.Paging, Data.Search, Data.DateSearch, sortOrder);
            //將頁面資料傳入View中
            ViewData["InvoiceDetail"] = db.InvoiceDetail.ToList();
            return View(Data);
        }
        //根據分頁以及搜尋來取得資料陣列的方法
        public List<Invoices> GetDataList(ForPaging Paging, string Search, string DateSearch, string sortOrder)
        {

            //宣告要接受全部搜尋資料的物件
            IQueryable<Invoices> SearchData;
            //判斷搜尋是否為空或Null，用於決定要呼叫取得搜尋資料
             SearchData = GetAllDataList(Paging, Search, DateSearch, sortOrder);
            //先排序再根據分頁來回傳所需部分的資料陣列
              return SearchData.OrderByDescending(p => p.InvoicesDate)
              .Skip((Paging.NowPage - 1) *
                  Paging.ItemNum).Take(Paging.ItemNum).ToList();
        }
        
        //包含搜尋值的搜尋資料方法
        public IQueryable<Invoices> GetAllDataList(ForPaging Paging, string Search, string DateSearch,string sortOrder)
        {
            //三個搜尋值都不為空
            if (!String.IsNullOrEmpty(DateSearch) && !String.IsNullOrEmpty(Search) && !String.IsNullOrEmpty(sortOrder))
            {
                IQueryable<Invoices> Data = db.Invoices
               .Where((p => p.InvoicesNum.Equals(Search) && p.InvoicesDate.Contains(DateSearch) && p.Status==true));
                //計算所需的總頁數
                Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
                //重新設定正確的頁數，避免有不正確值傳入
                Paging.SetRightPage();
                //回傳搜尋資料
                return Data;
            }
            //datesearch=nuull
            else if (!String.IsNullOrEmpty(Search) && String.IsNullOrEmpty(DateSearch) && !String.IsNullOrEmpty(sortOrder))
            {
                //根據搜尋值來搜尋資料
                IQueryable<Invoices> Data = db.Invoices
                    .Where(p => p.InvoicesNum.Equals(Search) && p.Status == true);
                //計算所需的總頁數
                Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
                //重新設定正確的頁數，避免有不正確值傳入
                Paging.SetRightPage();
                //回傳搜尋資料
                return Data;
            }//sortOrder=null
            else if (!String.IsNullOrEmpty(Search) && !String.IsNullOrEmpty(DateSearch) && String.IsNullOrEmpty(sortOrder))
            {
                IQueryable<Invoices> Data = db.Invoices
              .Where(p => p.InvoicesNum.Equals(Search) && p.InvoicesDate.Contains(DateSearch)&& p.Status == false);
                //計算所需的總頁數
                Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
                //重新設定正確的頁數，避免有不正確值傳入
                Paging.SetRightPage();
                //回傳搜尋資料
                return Data;
            }//date&project=null
            else if (!String.IsNullOrEmpty(Search) && String.IsNullOrEmpty(DateSearch) && String.IsNullOrEmpty(sortOrder))
            {
                IQueryable<Invoices> Data = db.Invoices
              .Where(p => p.InvoicesNum.Equals(Search)&& p.Status == false);
                //計算所需的總頁數
                Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
                //重新設定正確的頁數，避免有不正確值傳入
                Paging.SetRightPage();
                //回傳搜尋資料
                return Data;
            }//search&project=null
            else if (String.IsNullOrEmpty(Search) && !String.IsNullOrEmpty(DateSearch) && String.IsNullOrEmpty(sortOrder))
            {
                IQueryable<Invoices> Data = db.Invoices
              .Where(p => p.InvoicesDate.Contains(DateSearch) && p.Status == false);
                //計算所需的總頁數
                Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
                //重新設定正確的頁數，避免有不正確值傳入
                Paging.SetRightPage();
                //回傳搜尋資料
                return Data;
            }//sreach&datesearch=null
            else if (String.IsNullOrEmpty(Search) && String.IsNullOrEmpty(DateSearch) && !String.IsNullOrEmpty(sortOrder))
            {
                IQueryable<Invoices> Data = db.Invoices
              .Where(p => p.Status == true);
                //計算所需的總頁數
                Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
                //重新設定正確的頁數，避免有不正確值傳入
                Paging.SetRightPage();
                //回傳搜尋資料
                return Data;
            }//search=null
            else if (String.IsNullOrEmpty(Search) && !String.IsNullOrEmpty(DateSearch) && !String.IsNullOrEmpty(sortOrder))
            {
                IQueryable<Invoices> Data = db.Invoices
              .Where(p => p.InvoicesDate.Contains(DateSearch) && p.Status==true);
                //計算所需的總頁數
                Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
                //重新設定正確的頁數，避免有不正確值傳入
                Paging.SetRightPage();
                //回傳搜尋資料
                return Data;
            }
            //無其他搜尋值,且未開立發票
            else
            {
                //宣告要回傳的搜尋資料為資料庫中的Guestbooks資料表
                IQueryable<Invoices> Data = db.Invoices
                .Where(p => p.Status == false);
                //計算所需的總頁數
                Paging.MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(Data.Count()) / Paging.ItemNum));
                //重新設定正確的頁數，避免有不正確值傳入
                Paging.SetRightPage();
                //回傳搜尋資料
                return Data;
            }


        }
        #endregion
        #region//刪除訂單
        public ActionResult DeleteOrder(string InvoicesNum)
        {
            InvoiceDetail ExitData = null;
            do
            {
                InvoiceDetail oldData = db.InvoiceDetail.Where(p => p.InvoicesNum.Equals(InvoicesNum)).FirstOrDefault();
                db.InvoiceDetail.Remove(oldData);
                db.SaveChanges();
                ExitData = db.InvoiceDetail.Where(p => p.InvoicesNum.Equals(InvoicesNum)).FirstOrDefault();
            }
            while (ExitData != null);
            Invoices oldData1 = db.Invoices.Find(InvoicesNum);
            db.Invoices.Remove(oldData1);
            db.SaveChanges();
            return RedirectToAction("Demand");
            
        }
        #endregion

        #region//開立發票
        public ActionResult C401(string InvoicesNum)
        {
            Invoices OpenData= db.Invoices.Find(InvoicesNum);
            return View(OpenData);
        }
        [HttpPost]
        public ActionResult C401(Invoices Data)
        {
            bool complement = false;
            Invoices EditData = db.Invoices.Find(Data.InvoicesNum);
            EditData.ToolCode = Data.ToolCode;
            EditData.LoveCode = Data.LoveCode;
            EditData.Tax = Data.Tax;
            EditData.PayBy = Data.PayBy;
            EditData.CustomerValue = Data.CustomerValue;
            EditData.orderStatus = Data.orderStatus;
            EditData.Status = true;
            db.SaveChanges();
            if(string.IsNullOrEmpty(EditData.CustomerValue))
            {
                Printer(Data.InvoicesNum, complement);
            }
            else
            {
                ComplementPrinter(Data.InvoicesNum);
            }
            
            return RedirectToAction("Demand");
        }
        #endregion
        #region//發票列印(含補印)
        public ActionResult Printer(string InvoicesNum,bool complement)
        {
           
            Invoices OpenData = db.Invoices.Find(InvoicesNum);
            InvoiceManTest x1 = new InvoiceManTest();
            InvoiceManTest.InvoiceManTest_ClassInit();
            if (string.IsNullOrEmpty(OpenData.CustomerValue))
            {
                x1.InvoiceManTest_Case01_general_buyer(OpenData, complement);
            }
            else
            {
                x1.InvoiceManTest_Case01_vat_buyer(OpenData);
            }

            return RedirectToAction("Demand");
        }
        #endregion
        #region//統編列印(不含補印)
        public ActionResult ComplementPrinter(string InvoicesNum)
        {
            
            Invoices OpenData = db.Invoices.Find(InvoicesNum);
            InvoiceManTest x1 = new InvoiceManTest();
            InvoiceManTest.InvoiceManTest_ClassInit();
            x1.InvoiceManTest_Case01_vat_buyer(OpenData);

            return RedirectToAction("Demand");
        }
        #endregion
    }

}

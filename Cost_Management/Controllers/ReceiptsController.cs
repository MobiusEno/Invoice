using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using LINQtoCSV;
using Cost_Management.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace Cost_Management.Controllers
{
    [Authorize]
    public class ReceiptsController : Controller
    {
        public PONO_DatabaseEntities db = new PONO_DatabaseEntities();
        #region//匯入字軌
        public ActionResult Index()
        {
            ViewData["Receipts"] = db.Receipts.ToList();
            
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            string path = null;

            List<Receipts> Receipts = new List<Receipts>();
            if (file.ContentLength > 0)
            {
                var filename = Path.GetFileName(file.FileName);
                path = AppDomain.CurrentDomain.BaseDirectory + "Upload\\" + filename;
                file.SaveAs(path);
                var csv = new CsvReader(new StreamReader(path));
                csv.Configuration.IgnoreHeaderWhiteSpace = true;
                csv.Configuration.RegisterClassMap<MyClassMap>();
                var ReceiptsList = csv.GetRecords<Receipts>();
                foreach (var c in ReceiptsList)
                {
                    Receipts ReceiptsData = new Receipts();
                    ReceiptsData.Value = c.Value;
                    ReceiptsData.InvoicePeriod = c.InvoicePeriod;
                    ReceiptsData.Title = c.Title;
                    ReceiptsData.StartNum = c.StartNum;
                    ReceiptsData.EndNum = c.EndNum;
                    ReceiptsData.CurrentNum = c.StartNum;
                    ReceiptsData.IsEnabled = false;
                    db.Receipts.Add(ReceiptsData);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
        public sealed class MyClassMap : CsvClassMap<Receipts>
        {
            public MyClassMap()
            {
                Map(m => m.Value).Index(0);
                Map(m => m.InvoicePeriod).Index(3);
                Map(m => m.Title).Index(4);
                Map(m => m.StartNum).Index(5);
                Map(m => m.EndNum).Index(6);

            }
        }
        #endregion

        //啟用字軌
        public ActionResult RP(int ID)
        {
            Receipts chechData = db.Receipts.Where(p => p.IsEnabled==true).FirstOrDefault();
            if(chechData==null)
            {
                Receipts EditData = db.Receipts.Find(ID);
                EditData.Id = EditData.Id;
                EditData.Value = EditData.Value;
                EditData.InvoicePeriod = EditData.InvoicePeriod;
                EditData.Title = EditData.Title;
                EditData.StartNum = EditData.StartNum;
                EditData.EndNum = EditData.EndNum;
                EditData.CurrentNum = EditData.CurrentNum;
                EditData.IsEnabled = true;
                db.SaveChanges();
            }
            else
            {
                TempData["Data"] = "已經有啟用中的字軌";
            }
            return RedirectToAction("Index"); 
        }
        //取消啟用
        public ActionResult RP1(int ID)
        {
            Receipts EditData = db.Receipts.Find(ID);
            EditData.Id = EditData.Id;
            EditData.Value = EditData.Value;
            EditData.InvoicePeriod = EditData.InvoicePeriod;
            EditData.Title = EditData.Title;
            EditData.StartNum = EditData.StartNum;
            EditData.EndNum = EditData.EndNum;
            EditData.CurrentNum = EditData.CurrentNum;
            EditData.IsEnabled = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}

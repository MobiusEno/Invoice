using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cost_Management.Models;

namespace Cost_Management.Controllers
{
    [Authorize]
    public class MasterController : Controller
    {
        //實作DB
        public PONO_DatabaseEntities db = new PONO_DatabaseEntities();
        public IQueryable<StoreInformation> ListData;
        public IQueryable<Products> ListData1;
        
        #region//店家資訊
        public ActionResult StoreInformation(int? Id)
        {
            StoreInformation NewData = new StoreInformation();
            ListData = db.StoreInformation;
            if (Id>0)
            {
               
                NewData = db.StoreInformation.Find(Id);
                NewData.ListData = ListData.ToList();
            }
           
            else
            {
                
                NewData.ListData = ListData.ToList();
                
            }
            
            return View(NewData);
        }
        [HttpPost]
        public ActionResult StoreInformation(StoreInformation Data)
        {
            IQueryable<StoreInformation> DemandData = db.StoreInformation
           .Where(p => p.Store.Equals(Data.Store));
            bool hasElements = DemandData.AsQueryable().Any();

            if(hasElements)
            {
                StoreInformation EditData = db.StoreInformation.Find(Data.Id);
                EditData.Name = Data.Name;
                EditData.Value = Data.Value;
                EditData.Store = Data.Store;
                EditData.StoreNumber = Data.StoreNumber;
                EditData.StoreAddress = Data.StoreAddress;
                EditData.Status = Data.Status;
                EditData.StoreCode = Data.StoreCode;
                db.SaveChanges();
                ListData = db.StoreInformation;
                EditData.ListData = ListData.ToList();
                return View(EditData);
            }
            else
            {
                StoreInformation NewData = new StoreInformation();
                NewData.Name = Data.Name;
                NewData.Value = Data.Value;
                NewData.Store = Data.Store;
                NewData.StoreNumber = Data.StoreNumber;
                NewData.StoreAddress = Data.StoreAddress;
                NewData.StoreCode = Data.StoreCode;
                NewData.Status = false;
                db.StoreInformation.Add(NewData);
                db.SaveChanges();
                ListData = db.StoreInformation;
                NewData.ListData = ListData.ToList();
                return View(NewData);
            }
            
                
        }
        public ActionResult DeleteStrore(int Id)
        {
            StoreInformation NewData = db.StoreInformation.Find(Id);
            db.StoreInformation.Remove(NewData);
            db.SaveChanges();
            return RedirectToAction("StoreInformation");
        }
        #endregion
        #region//新增-刪除產品
        public ActionResult product()
        {
            Products NewData = new Products();
            ListData1 = db.Products;
            NewData.ListData = ListData1.ToList();
            return View(NewData);
        }
       
        [HttpPost]
        public ActionResult product(Products Data)
        {
            
                string Name = "";
                Products NewData = new Products();
                NewData.ProductClass = Data.ProductClass;
                if (!string.IsNullOrEmpty(Data.ProductionContent0))
                {
                    NewData.ProductionContent0 = Data.ProductionContent0;
                }
                else
                {
                    NewData.ProductionContent0 = "";
                }
                if (!string.IsNullOrEmpty(Data.ProductionContent1))
                {
                    NewData.ProductionContent1 = Data.ProductionContent1;
                }
                else
                {
                    NewData.ProductionContent1 = "";
                }
                if (!string.IsNullOrEmpty(Data.ProductionContent2))
                {
                    NewData.ProductionContent2 = Data.ProductionContent2;
                }
                else
                {
                    NewData.ProductionContent2 = "";
                }


                if (!string.IsNullOrEmpty(NewData.ProductionContent1))
                {
                    Name += NewData.ProductionContent1;
                }
                if (!string.IsNullOrEmpty(NewData.ProductionContent1))
                {
                    if (Data.ProductClass == "包包")
                    {
                        Name += Data.ProductionContent0 + "包";
                    }
                    else
                    {
                        Name += NewData.ProductionContent0;
                    }

                }
                if (!string.IsNullOrEmpty(NewData.ProductionContent2))
                {
                    Name += NewData.ProductionContent2;
                }

                NewData.ProductionName = Name;
                NewData.Prices = Data.Prices;
                
                db.Products.Add(NewData);
                db.SaveChanges();
                ListData1 = db.Products;
               NewData.ListData = ListData1.ToList();

            return View(NewData);
        }
        public ActionResult DeleteProduct(int Id)
        {
            Products NewData = db.Products.Find(Id);
            db.Products.Remove(NewData);
            db.SaveChanges();
            return RedirectToAction("product");
        }
        #endregion
    }
}

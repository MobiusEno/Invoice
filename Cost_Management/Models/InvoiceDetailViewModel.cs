using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cost_Management.Models
{
    public class InvoiceDetailViewModel
    {
        public int Id { get; set; }
        [DisplayName("發票號碼:")]
        [Required(ErrorMessage = "請輸入發票號碼:")]
        public string InvoicesNum { get; set; }

        public string ProductName { get; set; }
        [DisplayName("數量:")]
        [Required(ErrorMessage = "請輸入數量:")]
        public Nullable<int> Qty { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> TotalPrice { get; set; }
        [DisplayName("領餐號碼:")]
        public Nullable<int> OrderNum { get; set; }

        [DisplayName("備註:")]
        public string Note { get; set; }

        public string orderStatus  { get; set; }

        public virtual Invoices Invoices { get; set; }
        [DisplayName("產品類別:")]
        public string ProductClass { get; set; }
        [DisplayName("包包麵皮:")]
        public string ProductContent0 { get; set; }
        [DisplayName("包包主餡:")]
        public string ProductContent1 { get; set; }

        [DisplayName("飲料種類:")]
        public string DrinkContent { get; set; }
        [DisplayName("冷熱:")]
        public string DrinkContent0 { get; set; }
        [DisplayName("甜度:")]
        public string DrinkContent1 { get; set; }
        [DisplayName("冰塊:")]
        public string DrinkContent2 { get; set; }
        public int OrderSum = 0;
        public int QTY = 0;
    }
    
}
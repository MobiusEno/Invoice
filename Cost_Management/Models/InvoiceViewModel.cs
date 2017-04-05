using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cost_Management.Models
{
    [MetadataType(typeof(InvoiceViewModel))]
    public partial class Invoices
    {
        private partial class InvoiceViewModel
        {
            [DisplayName("發票號碼:")]
            public string InvoicesNum { get; set; }
            public string Name { get; set; }
            public string Store { get; set; }
            public string InvoicePeriod { get; set; }
            public string InvoicesDate { get; set; }
            [DisplayName("發票總額:")]
            public string InvoicesTotal { get; set; }
            public string Value { get; set; }
            [DisplayName("附註:")]
            public string Note { get; set; }
            [DisplayName("客戶統編:")]
            public string CustomerValue { get; set; }
            [DisplayName("取餐方式:")]
           
            public string orderStatus { get; set; }
            public Nullable<bool> Status { get; set; }
            [DisplayName("載具號碼:")]
            public string ToolCode { get; set; }
            [DisplayName("愛心碼:")]
            public string LoveCode { get; set; }
            [DisplayName("稅額:")]
            public string Tax { get; set; }
            [DisplayName("付款方式:")]
           
            public string PayBy { get; set; }
            [DisplayName("作廢原因:")]
            
            public string Cancel { get; set; }

        }
    }
}
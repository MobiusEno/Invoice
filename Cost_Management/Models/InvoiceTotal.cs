using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cost_Management.Models
{
    public class InvoiceTotal
    {
        public Invoices Main { get; set; }
        public List<InvoiceDetail> Detail { get; set; }
    }
}
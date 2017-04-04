using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cost_Management.Models
{
    public class ReceiptsViewModel
    {
        public int Id { get; set; }

        public string Value { get; set; }
        public string InvoicePeriod { get; set; }
        public string Title { get; set; }
        public string StartNum { get; set; }
        public string EndNum { get; set; }
        public string CurrentNum { get; set; }
        public bool? IsEnabled { get; set; }
    }
}
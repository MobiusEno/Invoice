using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Cost_Management.Models
{
    public class DemandViewModel
    {
        [DisplayName("搜尋")]
        public string Search { get; set; }
        [DataType(DataType.Date)]
        public string DateSearch { get; set; }
        
        public ForPaging Paging { get; set; }
        public List<Invoices> DataList { get; set; }

        public int OrderSum = 0;
        public int QTY = 0;
        public string sortOrder { get; set; }
    }
}
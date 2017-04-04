using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cost_Management.Models
{
    [MetadataType(typeof(Products_formMetadata))]
    public partial class Products
    {
        private partial class Products_formMetadata
        {
            public int Id { get; set; }

            [DisplayName("產品類別:")]
            [Required(ErrorMessage = "產品類別:")]
            public string ProductClass { get; set; }

            [DisplayName("產品名稱:")]
            [Required(ErrorMessage = "產品名稱:")]
            public string ProductionName { get; set; }

            [DisplayName("產品內容:")]
            
            public string ProductionContent0 { get; set; }
            [DisplayName("產品內容:")]
           
            public string ProductionContent1 { get; set; }
            [DisplayName("產品內容:")]
          
            public string ProductionContent2 { get; set; }

            [DisplayName("產品價錢:")]
            [Required(ErrorMessage = "產品價錢:")]

            public int Prices { get; set; }
            public List<Products> ListData { get; set; }
        }
    }
}
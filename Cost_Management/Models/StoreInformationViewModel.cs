using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cost_Management.Models
{
    [MetadataType(typeof(StoreInformation_formMetadata))]
    public partial class StoreInformation
    {
        private partial class StoreInformation_formMetadata
        {

            public int Id { get; set; }
            
            public string StartNum { get; set; }
            public string EndNum { get; set; }
            [DisplayName("店家名稱:")]
            [Required(ErrorMessage = "請輸入店家名稱")]
            public string Name { get; set; }
            [DisplayName("店家統編:")]
            [Required(ErrorMessage = "店家統編")]
            public string Value { get; set; }
            [DisplayName("店家門市:")]
            [Required(ErrorMessage = "店家門市")]
            public string Store { get; set; }
            [DisplayName("門市代碼:")]
            [Required(ErrorMessage = "門市代碼")]
            public string StoreCode { get; set; }
            [DisplayName("門市電話:")]
            [Required(ErrorMessage = "門市電話")]
            public string StoreNumber { get; set; }
            [DisplayName("店家地址:")]
            [Required(ErrorMessage = "請輸入店家地址")]
            public string StoreAddress { get; set; }
            [DisplayName("是否啟用:")]
            public bool Status { get; set; }
            
            public List<StoreInformation> ListData { get; set; }
        }
    }
}
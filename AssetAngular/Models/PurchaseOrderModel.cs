using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetAngular.Models
{
    public class PurchaseOrderModel
    {

        public decimal pd_id { get; set; }
        public string pd_order_no { get; set; }
        public Nullable <decimal> pd_ad_id { get; set; }
        public string assetname { get; set; }
        public Nullable<decimal> pd_type_id { get; set; }
        public string assettype { get; set; }
        public decimal pd_qty { get; set; }
        public Nullable<decimal> pd_vendor_id { get; set; }
        public string vendorname { get; set; }
        public string pd_odatestr { get; set; }
        public string pd_pdatestr { get; set; }
        public string pd_status { get; set; }
    }
}
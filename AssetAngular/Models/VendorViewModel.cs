using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetAngular.Models
{
    public class VendorViewModel
    {
        public decimal vd_id { get; set; }
        public string vd_name { get; set; }
        public string vd_type { get; set; }
        public string vd_atype { get; set; }
        public Nullable<decimal> vd_atype_id { get; set; }
        public string vd_fromstr { get; set; }
        public string vd_tostr { get; set; }
        public string vd_addr { get; set; }
    }
}
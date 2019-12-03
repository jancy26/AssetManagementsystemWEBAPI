﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssetAngular.Models
{
    public class AssetViewModel
    {
        public decimal ad_id { get; set; }
        public string ad_name { get; set; }
        public string ad_type { get; set; }
        public Nullable<decimal> ad_type_id { get; set; }
        public string ad_class { get; set; }
    }
}
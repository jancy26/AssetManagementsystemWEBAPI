//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AssetAngular.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class asset_master
    {
        public decimal am_id { get; set; }
        public Nullable<decimal> am_atype_id { get; set; }
        public Nullable<decimal> am_make_id { get; set; }
        public Nullable<decimal> am_ad_id { get; set; }
        public string am_model { get; set; }
        public Nullable<decimal> am_snumber { get; set; }
        public Nullable<System.DateTime> am_myyear { get; set; }
        public Nullable<System.DateTime> am_pdate { get; set; }
        public string am_warranty { get; set; }
        public Nullable<System.DateTime> am_from { get; set; }
        public Nullable<System.DateTime> am_to { get; set; }
    
        public virtual asset_def asset_def { get; set; }
        public virtual asset_type asset_type { get; set; }
        public virtual vendor_creation vendor_creation { get; set; }
    }
}

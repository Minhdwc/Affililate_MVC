//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Affililate_Web.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product_Click
    {
        public int id { get; set; }
        public int id_product { get; set; }
        public Nullable<System.DateTime> click_time { get; set; }
        public string event_type { get; set; }
        public Nullable<decimal> commission { get; set; }
        public string user_ip { get; set; }
    
        public virtual Product Product { get; set; }
    }
}

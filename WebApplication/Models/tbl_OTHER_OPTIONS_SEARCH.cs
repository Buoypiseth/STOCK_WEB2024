//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_OTHER_OPTIONS_SEARCH
    {
        public long ID { get; set; }
        public string para_ReportName { get; set; }
        public Nullable<bool> SearchProduct { get; set; }
        public Nullable<bool> SearchCategory { get; set; }
        public Nullable<bool> SearchInvoice { get; set; }
        public Nullable<bool> SearchCustomer { get; set; }
        public Nullable<bool> SearchSupplier { get; set; }
        public Nullable<bool> SearchStock { get; set; }
        public Nullable<bool> SearchUser { get; set; }
        public Nullable<bool> SearchDateOnly { get; set; }
        public Nullable<bool> SearchAdvanceOnly { get; set; }
    }
}

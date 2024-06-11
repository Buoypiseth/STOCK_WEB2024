using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Supplier
    {
        public string SupplierID { get; set; }
        public string CompanyName { get; set; }
        public string ContFirstName { get; set; }
        public string ContLastName { get; set; }
        public string JobTitle { get; set; }
        public string BusPhone { get; set; }
        public string MobilePhone { get; set; }
        public string FaxNumber { get; set; }
        public string Address { get; set; }
        public string ZipPostalCode { get; set; }
        public string Region { get; set; }
        public string supCity { get; set; }
        public string supCountry { get; set; }
        public string CompEmail { get; set; }
        public string WebPage { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
        public Nullable<int> autoNo { get; set; }
        public Nullable<long> GroupID { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Customer
    {
        public string memberID { get; set; }
        public string memberTitle { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string nickName { get; set; }
        public string idType { get; set; }
        public string icpassportNo { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public string MaritalStatus { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string PostalCode { get; set; }
        public string POBox { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string eMail { get; set; }
        public Nullable<int> autono { get; set; }
        public string VAT_NO { get; set; }
        public string Water_MeterNo { get; set; }
        public Nullable<double> Water_Diameter { get; set; }
        public Nullable<bool> Water_inactive { get; set; }
        public string Water_Decription { get; set; }
        public string StreetNo { get; set; }
        public string HouseNo { get; set; }
        public string village { get; set; }
        public string communes { get; set; }
        public string district { get; set; }
        public string city_province { get; set; }
        public Nullable<System.DateTime> Passport_Issue { get; set; }
        public Nullable<System.DateTime> Passport_Expire { get; set; }
        public Nullable<System.DateTime> Visa_Expire { get; set; }
        public Nullable<long> Group_Customers_ID { get; set; }
        public Nullable<int> CodeFP { get; set; }
        public string MemberCardNo { get; set; }
        public string Sincroni_Info { get; set; }
        public string memberIDMain { get; set; }
        public string AccountType { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> DateExpire { get; set; }
        public Nullable<double> LimitePay1Day { get; set; }
        public Nullable<double> LimitePay1Time { get; set; }
    }
}
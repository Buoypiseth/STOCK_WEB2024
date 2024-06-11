using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class BuyProduct
    {
        public string MainStockID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public string unitType { get; set; }
        public float NumInOne { get; set; }
        public Nullable<double> BuyingCost { get; set; }
        public Nullable<double> transCost { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<double> TotalCost { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<double> SellingPriceUSD { get; set; }
        public string PrdNameEng { get; set; }
        public string barCode { get; set; }
        public string OrderComment { get; set; }
        public string PrdCategory { get; set; }
        public Nullable<double> Val { get; set; }
        public string Type { get; set; }
        public string BuyID { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string InvoiceNo { get; set; }
        public string SupplierID { get; set; }
        public string CompanyName { get; set; }
        public Nullable<double> Total { get; set; }
        public Nullable<double> PerDis { get; set; }
        public Nullable<double> AmtDis { get; set; }
        public Nullable<double> GrandTotal { get; set; }
        public Nullable<double> AmtPay { get; set; }
        public Nullable<double> AmtLoan { get; set; }
        public string Username { get; set; }
        public Nullable<double> usdtoKher { get; set; }
        public Nullable<double> usdtoTHB { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> DateReturn { get; set; }
        public string Description { get; set; }
        public string StockID { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Product
    {
        public string ProductID { get; set; }
        public string PrdCategID { get; set; }
        public string PrdCategory { get; set; }
        public string SupplierID { get; set; }
        public string PrdNameEng { get; set; }
        public string PrdNameKh { get; set; }
        public string UnitType { get; set; }
        public Nullable<double> totalInStock { get; set; }
        public Nullable<double> minimalStock { get; set; }
        public Nullable<double> SellingPriceUSD { get; set; }
        public Nullable<double> SellingPriceKHR { get; set; }
        public Nullable<double> SellingPriceTHB { get; set; }
        public byte[] Photo { get; set; }
        public string OrderComment { get; set; }
        public string PrdDesc { get; set; }
        public string barCode { get; set; }
        public string Delstatus { get; set; }
        public Nullable<bool> isCutStock { get; set; }
        public string ProductIDRealStock { get; set; }
        public Nullable<double> NumInOne { get; set; }
        public Nullable<bool> IsUnique { get; set; }
        public string TaxNote { get; set; }
        public string Money_Sale_Type { get; set; }
        public Nullable<double> Width { get; set; }
        public Nullable<double> Height { get; set; }
        public Nullable<double> Area_Size { get; set; }
        public string LOC_Location { get; set; }
        public string DMCode { get; set; }
        public string RPCode_Repaire { get; set; }
        public Nullable<double> NumHours { get; set; }
        public Nullable<double> Larbor_Cost { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<double> TotalCost { get; set; }
        public Nullable<double> BuyingCost { get; set; }
        //Declare variable
        [NotMapped]
        public HttpPostedFileBase ImgUpload { get; set; }
        public string Image { get; set; }
        public virtual Category Category { get; set; }
        public Nullable<double> SellingPriceUSDForThisCus { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public string StockID { get; set; }
        public string BuyID { get; set; }

        public long Product_PackageDetail_ID { get; set; }
        public long Product_PackageID { get; set; }
        public string Product_Package { get; set; }
        public string Description { get; set; }
    }
}
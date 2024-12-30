using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Sale
    {
        public string orderDetailsID { get; set; }
        public string custId { get; set; }
        public string customerOrderID { get; set; }
        public string roomtableID { get; set; }
        public string custName { get; set; }
        public string prdID { get; set; }
        public string unitType { get; set; }
        public Nullable<double> unitPrice { get; set; }
        public Nullable<double> orderQty { get; set; }
        public Nullable<double> totalAmt { get; set; }
        public Nullable<double> percDisc { get; set; }
        public Nullable<double> AmtDisc { get; set; }
        public string TaxNote { get; set; }
        public string OrderDetailDescription { get; set; }
        //////add field-----
        public string PrdNameEng { get; set; }
        public Nullable<double> SellingPriceUSD1 { get; set; }
        public Nullable<double> SellingPriceUSD { get; set; }
        public Nullable<double> SellingPriceKHR { get; set; }

        //declare variable payment
        public Nullable<double> totalAmount { get; set; }
        public Nullable<double> amtDiscount { get; set; }
        public Nullable<double> percentDiscount { get; set; }
        public Nullable<double> netAmount { get; set; }
        public Nullable<double> amtGTotalReceived { get; set; }
        public Nullable<double> amtReceived { get; set; }
        public Nullable<double> amtReturn { get; set; }
        public Nullable<double> amtOwed { get; set; }

        public Nullable<double> totalAmountRiel { get; set; }
        public Nullable<double> percentDiscountRiel { get; set; }
        public Nullable<double> netAmountRiel { get; set; }
        public Nullable<double> amtReceivedRiel { get; set; }
        public Nullable<double> amtReturnRiel { get; set; }
        public Nullable<double> amtOwedRiel { get; set; }
        public Nullable<System.DateTime> ReturnDate { get; set; }
        public Nullable<System.DateTime> orderDate { get; set; }
        public string DescripitonTerm { get; set; }
        public Nullable<double> usdTokhr { get; set; }
        public Nullable<double> usdToTHB { get; set; }
        public Nullable<double> usdToKhrChange { get; set; }
        public Nullable<double> percDiscRiel { get; set; }
        public Nullable<double> AmtDiscRiel { get; set; }
        public Nullable<double> orderQtyReturnTemp { get; set; }
        public Nullable<double> orderQtyReturn { get; set; }
        public Nullable<double> totalAmtReturn { get; set; }
        public Nullable<double> netAmountReturn { get; set; }
        public Nullable<double> totalAmtReturnTemp { get; set; }
        public string PytStatus { get; set; }
        public string statusReturn { get; set; }

        //----------declare variable temp--------------
        public string InvoiceID { get; set; }
        public string InvoiceNo { get; set; }
        public Nullable<double> Val { get; set; }
        public string Type { get; set; }

        public bool IsPrint { get; set; }
        public int NumPrint { get; set; }
        public string PaperSize { get; set; }
        public string IsType { get; set; }
        public string barCode { get; set; }
        public bool status { get; set; }
        public string PrdDesc { get; set; }
        public string PrdCategID { get;set; }
        public string Image { get; set; }
        public int rowCount { get; set; }

        public string CustomerAccountID { get; set; }
        public Nullable<double> CustomerAmount { get; set; }
        public Nullable<double> RealPriceUSD { get; set; }
        public Nullable<double> RealPriceKHR { get; set; }
        public Nullable<double> usdTokhrSaleItem { get; set; }
        public Nullable<double> usdToTHBSaleItem { get; set; }
        public Nullable<double> usdToKhrChangeSaleItem { get; set; }
    }
}
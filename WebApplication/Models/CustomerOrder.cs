using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class CustomerOrder
    {
        public string customerOrderID { get; set; }
        public Nullable<System.DateTime> orderDate { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public string customerID { get; set; }
        public string CustName { get; set; }
        public Nullable<double> totalAmount { get; set; }
        public Nullable<double> percentDiscount { get; set; }
        public Nullable<double> amtDiscount { get; set; }
        public Nullable<double> Tax { get; set; }
        public Nullable<double> netAmount { get; set; }
        public Nullable<double> voidAmt { get; set; }
        public Nullable<double> grandTotal { get; set; }
        public Nullable<double> amtReceived { get; set; }
        public Nullable<double> amtReturn { get; set; }
        public Nullable<double> amtOwed { get; set; }
        public string userID { get; set; }
        public string UserName { get; set; }
        public Nullable<double> usdTokhr { get; set; }
        public Nullable<double> usdToTHB { get; set; }
        public string RandTID { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public string InvoiceNo { get; set; }
        public string voidStatus { get; set; }
        public string openingBalanceID { get; set; }
        public Nullable<System.DateTime> ReturnDate { get; set; }
        public Nullable<int> NumCus { get; set; }
        public string DiscountType { get; set; }
        public Nullable<double> CreditPay { get; set; }
        public string Servant { get; set; }
        public string CardTypeID { get; set; }
        public Nullable<double> CardTypeAmount { get; set; }
        public Nullable<double> AccountAMT { get; set; }
        public Nullable<double> USD_FromCus { get; set; }
        public Nullable<double> KHR_FromCus { get; set; }
        public Nullable<double> THB_FromCus { get; set; }
        public string AccountType { get; set; }
        public string AccountCode { get; set; }
        public Nullable<double> AccountRealMoneyForThisInvoice { get; set; }
        public Nullable<double> WaitingNo { get; set; }
    }
}
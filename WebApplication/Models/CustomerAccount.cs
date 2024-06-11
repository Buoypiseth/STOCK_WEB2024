using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class CustomerAccount
    {
        public string CustomerAccountID { get; set; }
        public string memberID { get; set; }
        public string AccountType { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<double> RealMoneyRemain { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> DateExpire { get; set; }
        public Nullable<double> LimitePay1Day { get; set; }
        public Nullable<double> LimitePay1Time { get; set; }
        public string Description { get; set; }
        public string EditeNote { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public Nullable<bool> IsActive { get; set; }

        //customer account in/out history
        public string CustAccInOutHistoryID { get; set; }
        public Nullable<double> RealMoney { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Type { get; set; }
        public Nullable<double> BeginAmount { get; set; }
        public string customerOrderID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Username { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public class BillRepository
    {
        private DataContext context;
        public BillRepository()
        {
            this.context = new DataContext();
        }
        public List<CustomerOrder> GetForDataTable()
        {
            // Initialization.  
            List<CustomerOrder> lst = new List<CustomerOrder>();
            var customerOrders = from co in context.tbl_customerorder
                                  join u in context.tbl_useraccount on co.userID equals u.UserID
                                  join c in context.tbl_customers on co.customerID equals c.memberID
                                  orderby (co.InvoiceNo) descending
                                  select new
                                  {
                                      co.customerOrderID,
                                      co.InvoiceNo,
                                      c.fullName,
                                      co.orderDate,
                                      co.totalAmount,
                                      co.percentDiscount,
                                      co.amtDiscount,
                                      co.grandTotal,
                                      co.amtReceived,
                                      co.amtReturn,
                                      co.amtOwed,
                                      u.UserName,
                                      co.voidAmt,
                                      co.voidStatus,
                                      co.userID
                                  };
            foreach (var i in customerOrders)
            {
                CustomerOrder infoObj = new CustomerOrder();
                infoObj.customerOrderID = i.customerOrderID;
                infoObj.InvoiceNo = i.InvoiceNo;
                infoObj.CustName = i.fullName;
                infoObj.orderDate = i.orderDate;
                infoObj.totalAmount = i.totalAmount;
                infoObj.percentDiscount = i.percentDiscount;
                infoObj.amtDiscount = i.amtDiscount;
                infoObj.grandTotal = i.grandTotal;
                infoObj.amtReceived = i.amtReceived;
                infoObj.amtReturn = i.amtReturn;
                infoObj.amtOwed = i.amtOwed;
                infoObj.UserName = i.UserName;
                infoObj.voidAmt = i.voidAmt;
                infoObj.voidStatus = i.voidStatus;
                infoObj.userID = i.userID;
                lst.Add(infoObj);
            }
            // info.  
            return lst;
        }
        public List<CustomerOrder> SortByColumnWithOrder(string order, string orderDir, List<CustomerOrder> data)
        {
            // Initialization.  
            List<CustomerOrder> lst = new List<CustomerOrder>();

            try
            {
                // Sorting  
                switch (order)
                {
                    case "0":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderBy(p => p.orderDate).ToList()
                                                                                                 : data.OrderBy(p => p.orderDate).ToList();
                        break;

                    case "1":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.orderDate).ToList()
                                                                                                 : data.OrderBy(p => p.orderDate).ToList();
                        break;
                    default:

                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.orderDate).ToList()
                                                                                                 : data.OrderBy(p => p.orderDate).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                // info.  
                Console.Write(ex);
            }

            // info.  
            return lst;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
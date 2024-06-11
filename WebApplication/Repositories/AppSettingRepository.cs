using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApplication.Models;
using WebApplication.Helpers;

namespace WebApplication.Repositories
{
    public class AppSettingRepository
    {
        private DataContext context;
        private tbl_APP_Setting model;
        public AppSettingRepository()
        {
            this.context = new DataContext();
            this.model = new tbl_APP_Setting();
        }
        public float AppSetting(string name)
        {
            var data = context.tbl_APP_Setting.FirstOrDefault(x => x.Name == name);
            return data == null ? 0 :(float)data.Value;
        }
        public bool IsSaleNoEnoughStock(Sale data, string IsStatus = "AddOrder")
        {
            var IsValue = context.tbl_APP_Setting.Where(x => x.Name == "is_Sale_No_Enough_Stock")
                .Select(x => x.Value)
                .Single();
            if(IsValue == 1)
            {
                return IsTotalInStock(data, IsStatus);
            }
            return false;
        }
        public bool IsTotalInStock(Sale data, string IsStatus)
        {
            if(IsStatus == "AddOrder") {
                var product = context.tbl_products.FirstOrDefault(x => x.ProductID == data.prdID);
                var orderDetails = (from r in context.tbl_orderdetails
                                    join p in context.tbl_products on r.prdID equals p.ProductID
                                    where r.userID == IUser.Id
                                    && r.PytStatus == "Ordering"
                                    select new
                                    {
                                        ProductIDRealStock = p.ProductIDRealStock,
                                        orderQty = (r.orderQty / r.NumInCase)
                                    }).Where(x => x.ProductIDRealStock == product.ProductIDRealStock).ToList();

                var totalInOrdering = (orderDetails == null ? 0 : orderDetails.Sum(x => x.orderQty)) + (data.orderQty / product.NumInOne);
                var totalInStock = context.tblStockTypeProducts.FirstOrDefault(x => x.ProductID == product.ProductIDRealStock && x.StockID == IUser.StockID);
                if (totalInStock.Qty < totalInOrdering && data.status == false)
                {
                    return true;
                }
            }
            else
            {
                var product = context.tbl_products.FirstOrDefault(x => x.ProductID == data.prdID);
                var orderDetails = (from r in context.tbl_orderdetails
                                    join p in context.tbl_products on r.prdID equals p.ProductID
                                    where r.userID == IUser.Id
                                    && r.PytStatus == "Ordering"
                                    select new
                                    {
                                        prdID = r.prdID,
                                        ProductIDRealStock = p.ProductIDRealStock,
                                        orderQty = (r.orderQty / r.NumInCase)
                                    }).Where(x => x.ProductIDRealStock == product.ProductIDRealStock && x.prdID != data.prdID).ToList();

                var totalInOrdering = (orderDetails == null ? 0 : orderDetails.Sum(x => x.orderQty)) + (data.Val / product.NumInOne);
                var totalInStock = context.tblStockTypeProducts.FirstOrDefault(x => x.ProductID == product.ProductIDRealStock && x.StockID == IUser.StockID);
                if (totalInStock.Qty < totalInOrdering && data.status == false)
                {
                    return true;
                }
            }
            return false;
        }
        //public string AppSettingOption(string IsType)
        //{
        //    var appSetting = context.tbl_App_Setting_Options.FirstOrDefault(x => x.Type == IsType && x.First == 1);
        //    return appSetting.Name;
        //}
        public List<tbl_App_Setting_Options> GetAppSetting(string Type)
        {
            return context.tbl_App_Setting_Options.Where(x => x.Type == Type).OrderBy(x => x.First).ToList();
        }
        public List<tbl_APP_Setting> GetForDataTable()
        {
            return context.tbl_APP_Setting.ToList();
        }
        public List<tbl_APP_Setting> SortByColumnWithOrder(string order, string orderDir, List<tbl_APP_Setting> data)
        {
            // Initialization.  
            List<tbl_APP_Setting> lst = new List<tbl_APP_Setting>();

            try
            {
                // Sorting  
                switch (order)
                {
                    case "0":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderBy(p => p.ID).ToList()
                                                                                                 : data.OrderBy(p => p.ID).ToList();
                        break;

                    case "1":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Name).ToList()
                                                                                                 : data.OrderBy(p => p.Name).ToList();
                        break;
                    default:

                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Value).ToList()
                                                                                                 : data.OrderBy(p => p.Value).ToList();
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
        //public string Update(tbl_App_Setting data)
        //{
        //    try
        //    {
        //        model = context.tbl_App_Setting.Where(x => x.Name == data.Name).FirstOrDefault();
        //        model.Value = data.Value;
        //        model.Note = data.Note;
        //        context.Entry(model).State = EntityState.Modified;
        //        var _settingOption = context.tbl_App_Setting_Options.Where(x => x.Type == data.Name && x.First == 1).FirstOrDefault();
        //        if (_settingOption != null)
        //        {
        //            _settingOption.First = 2;
        //            context.Entry(_settingOption).State = EntityState.Modified;
        //            _settingOption = context.tbl_App_Setting_Options.Where(x => x.Type == data.Name && x.Name == data.Note).FirstOrDefault();
        //            _settingOption.First = 1;
        //            context.Entry(_settingOption).State = EntityState.Modified;
        //        }
        //        this.Save();
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.ToString();
        //    }
        //    return "True";
        //}
        public void Save()
        {
           context.SaveChanges();
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
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Helpers;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public class ProductPackageRepository
    {
        private DataContext context;
        private tbl_products_Package model;
        public ProductPackageRepository()
        {
            this.context = new DataContext();
            this.model = new tbl_products_Package();
        }
        public List<Product> GetForDataTable()
        {
            List<Product> lst = new List<Product>();
            var results = context.tbl_products_Package.ToList().OrderByDescending(x => x.Product_Package);
            foreach (var i in results)
            {
                Product p = new Product();
                p.Product_PackageID = i.Product_PackageID;
                p.Product_Package = i.Product_Package;
                p.Description = i.Description;
                lst.Add(p);
            }
            return lst;
        }
        public List<Product> GetProductPackageForDataTable(long id)
        {
            List<Product> lst = new List<Product>();
            var results = (from pk in context.tbl_products_Package
                           join pd in context.tbl_products_Package_Detail on pk.Product_PackageID equals pd.Product_PackageID
                           join p in context.tbl_products on pd.ProductID equals p.ProductID
                           where pd.Product_PackageID == id
                           orderby p.PrdNameEng
                           select new
                           {
                               pd.Product_PackageDetail_ID,
                               pk.Product_PackageID,
                               pk.Product_Package,
                               p.ProductID,
                               p.PrdNameEng,
                               p.UnitType,
                               p.OrderComment,
                               p.barCode,
                               pd.Qty,
                               pd.SellingPriceUSD,
                               pd.Description,
                               //tblPurchase.Quantity,
                               //tblPurchase.TotalCost
                           }).ToList();
            foreach (var i in results)
            {
                Product p = new Product();
                p.Product_PackageDetail_ID = i.Product_PackageDetail_ID;
                p.Product_PackageID = i.Product_PackageID;
                p.Product_Package = i.Product_Package;
                p.ProductID = i.ProductID;
                p.PrdNameEng = i.PrdNameEng;
                p.UnitType = i.UnitType;
                p.OrderComment = i.OrderComment;
                p.barCode = i.barCode;
                p.Quantity = i.Qty;
                p.SellingPriceUSD = i.SellingPriceUSD;
                p.Description = i.Description;
                lst.Add(p);
            }
            return lst;
        }
        public List<Product> SortByColumnWithOrder(string order, string orderDir, List<Product> data)
        {
            // Initialization.  
            List<Product> lst = new List<Product>();

            try
            {
                // Sorting  
                switch (order)
                {
                    case "0":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Product_Package).ToList()
                                                                                                 : data.OrderBy(p => p.Product_Package).ToList();
                        break;

                    case "1":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.PrdNameEng).ToList()
                                                                                                 : data.OrderBy(p => p.PrdNameEng).ToList();
                        break;
                    default:

                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Product_Package).ToList()
                                                                                                 : data.OrderBy(p => p.Product_Package).ToList();
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
        public (string status, string message) Create(tbl_products_Package data)
        {
            try
            {
                model.Product_Package = data.Product_Package;
                model.Description = data.Description;
                context.tbl_products_Package.Add(model);
                this.Save();
            }
            catch (Exception ex)
            {
                return (Resources.Alerts.Error, ex.ToString());
            }
            return ("True", Resources.Alerts.Product_Created);
        }
        public (string status, string message) Update(tbl_products_Package data)
        {
            try
            {
                model = context.tbl_products_Package.Find(data.Product_PackageID);
                model.Product_Package = data.Product_Package;
                model.Description = data.Description;
                context.Entry(model).State = EntityState.Modified;
                this.Save();
            }
            catch (Exception ex)
            {
                return (Resources.Alerts.Error, ex.ToString());
            }
            return ("True", Resources.Alerts.Product_Updated);
        }
        public (string status, string message) CreateItem(tbl_products_Package_Detail data)
        {
            try
            {
                tbl_products_Package_Detail packageDetail = new tbl_products_Package_Detail();
                packageDetail.Product_PackageID = data.Product_PackageID;
                packageDetail.ProductID = data.ProductID;
                packageDetail.Qty = data.Qty;
                packageDetail.SellingPriceUSD = data.SellingPriceUSD;
                packageDetail.Description = data.Description;
                context.tbl_products_Package_Detail.Add(packageDetail);
                this.Save();
            }
            catch (Exception ex)
            {
                return (Resources.Alerts.Error, ex.ToString());
            }
            return ("True", Resources.Alerts.Product_Created);
        }
        public (string status, string message) UpdateItem(tbl_products_Package_Detail data)
        {
            try
            {
                var packageDetail = context.tbl_products_Package_Detail.Find(data.Product_PackageDetail_ID);
                packageDetail.Product_PackageID = data.Product_PackageID;
                packageDetail.ProductID = data.ProductID;
                packageDetail.Qty = data.Qty;
                packageDetail.SellingPriceUSD = data.SellingPriceUSD;
                packageDetail.Description = data.Description;
                context.Entry(packageDetail).State = EntityState.Modified;
                this.Save();
            }
            catch (Exception ex)
            {
                return (Resources.Alerts.Error, ex.ToString());
            }
            return ("True", Resources.Alerts.Product_Updated);
        }
        public string Delete(tbl_products_Package_Detail data)
        {
            try
            {
                var packageDetail = context.tbl_products_Package_Detail.FirstOrDefault(x => x.Product_PackageDetail_ID == data.Product_PackageDetail_ID);
                context.tbl_products_Package_Detail.Remove(packageDetail);
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "True";
        }
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
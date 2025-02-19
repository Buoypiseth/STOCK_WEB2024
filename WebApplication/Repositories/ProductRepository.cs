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
    public class ProductRepository
    {
        private DataContext context;
        //private StockProductRepository stockProductRepository;
        private BuyProductRepository buyProductRepository;
        public IEnumerable<SelectListItem> TaxNote { set; get; }
        public IEnumerable<SelectListItem> Money_Sale_Type { set; get; }
        public List<Product> Products { get; set; }
        public ProductRepository()
        {
            this.context = new DataContext();
            //this.stockProductRepository = new StockProductRepository();
            this.buyProductRepository = new BuyProductRepository();
        }
        public List<Product> GetForDataTable()
        {
            List<Product> lst = new List<Product>();
            var results = (from p in context.tbl_products
                           join c in context.tbl_prdcategory on p.PrdCategID equals c.PrdCategID
                           //join sp in context.tblStockTypeProducts on p.ProductID equals sp.ProductID
                           //join purchase in context.tbl_product_purchases on p.ProductID equals purchase.ProductID into pur
                           //from tblPurchase in pur.DefaultIfEmpty()
                           //where p.isCutStock == true
                           where p.ProductID == p.ProductIDRealStock
                           orderby p.PrdNameEng
                           select new
                           {
                               p.ProductID,
                               c.PrdCategory,
                               p.PrdNameEng,
                               p.UnitType,
                               p.OrderComment,
                               p.barCode,
                               p.SellingPriceUSD,
                               p.SellingPriceKHR,
                               p.minimalStock,
                               p.totalInStock,
                               p.Image,
                               p.PrdCategID,
                               //tblPurchase.Quantity,
                               //tblPurchase.TotalCost
                               p.BuyingCost
                           }).ToList();
            foreach (var i in results)
            {
                Product p = new Product();
                p.ProductID = i.ProductID;
                p.PrdCategory = i.PrdCategory;
                p.UnitType = i.UnitType;
                p.PrdNameEng = i.PrdNameEng;
                p.OrderComment = i.OrderComment;
                p.barCode = i.barCode;
                p.SellingPriceUSD = i.SellingPriceUSD;
                p.SellingPriceKHR = i.SellingPriceKHR;
                p.minimalStock = i.minimalStock;
                p.totalInStock = i.totalInStock;
                p.Image = i.Image;
                p.PrdCategID = i.PrdCategID;
                //p.Quantity = i.Quantity;
                //p.TotalCost = i.TotalCost;
                p.BuyingCost = i.BuyingCost;
                lst.Add(p);
            }
            return lst;
        }
        public List<Product> GetChildForDataTable(string id)
        {
            List<Product> lst = new List<Product>();
            var results = (from p in context.tbl_products
                           join c in context.tbl_prdcategory on p.PrdCategID equals c.PrdCategID
                           //join sp in context.tblStockTypeProducts on p.ProductID equals sp.ProductID
                           //join purchase in context.tbl_product_purchases on p.ProductID equals purchase.ProductID into pur
                           //from tblPurchase in pur.DefaultIfEmpty()
                           where p.ProductIDRealStock == id && p.ProductID != id
                           orderby p.PrdNameEng
                           select new
                           {
                               p.ProductID,
                               c.PrdCategory,
                               p.PrdNameEng,
                               p.UnitType,
                               p.OrderComment,
                               p.barCode,
                               p.SellingPriceUSD,
                               p.SellingPriceKHR,
                               p.minimalStock,
                               p.totalInStock,
                               //tblPurchase.Quantity,
                               //tblPurchase.TotalCost
                           }).ToList();
            foreach (var i in results)
            {
                Product p = new Product();
                p.ProductID = i.ProductID;
                p.PrdCategory = i.PrdCategory;
                p.PrdNameEng = i.PrdNameEng;
                p.UnitType = i.UnitType;
                p.OrderComment = i.OrderComment;
                p.barCode = i.barCode;
                p.SellingPriceUSD = i.SellingPriceUSD;
                p.SellingPriceKHR = i.SellingPriceKHR;
                p.minimalStock = i.minimalStock;
                p.totalInStock = i.totalInStock;
                //p.Quantity = i.Quantity;
                //p.TotalCost = i.TotalCost;
                lst.Add(p);
            }
            return lst;
        }
        public List<Product> GetForPurchase()
        {
            // Initialization.  
            List<Product> lst = new List<Product>();
            var products = from p in context.tbl_products
                           join c in context.tbl_prdcategory on p.PrdCategID equals c.PrdCategID
                           where p.isCutStock == true
                           select new
                           {
                               p.ProductID,
                               p.PrdNameEng,
                               p.barCode,
                               p.UnitType,
                               p.OrderComment,
                               p.SellingPriceUSD,
                               p.SellingPriceKHR,
                               p.SellingPriceTHB,
                               p.isCutStock,
                               p.IsUnique,
                               p.ProductIDRealStock,
                               p.Image,
                               c.PrdCategory,
                               p.totalInStock,
                               p.BuyingCost
                           };
            foreach (var i in products)
            {
                Product setdata = new Product();
                setdata.ProductID = i.ProductID;
                setdata.PrdNameEng = i.PrdNameEng;
                setdata.barCode = i.barCode;
                setdata.UnitType = i.UnitType;
                setdata.OrderComment = i.OrderComment;
                setdata.SellingPriceUSD = i.SellingPriceUSD;
                setdata.SellingPriceKHR = i.SellingPriceKHR;
                setdata.SellingPriceTHB = i.SellingPriceTHB;
                setdata.isCutStock = i.isCutStock;
                setdata.IsUnique = i.IsUnique;
                setdata.ProductIDRealStock = i.ProductIDRealStock;
                setdata.Image = i.Image;
                setdata.PrdCategory = i.PrdCategory;
                setdata.totalInStock = i.totalInStock;
                setdata.BuyingCost = i.BuyingCost;
                lst.Add(setdata);
            }
            // info.  
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
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.ProductID).ToList()
                                                                                                 : data.OrderBy(p => p.ProductID).ToList();
                        break;

                    case "1":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.PrdNameEng).ToList()
                                                                                                 : data.OrderBy(p => p.PrdNameEng).ToList();
                        break;
                    case "2":
                        // Setting.  
                        //lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.OrderComment).ToList()
                        //: data.OrderBy(p => p.OrderComment).ToList();
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.UnitType).ToList()
                        : data.OrderBy(p => p.UnitType).ToList();
                        break;
                    case "3":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.PrdCategory).ToList()
                                                                                                 : data.OrderBy(p => p.PrdCategory).ToList();
                        break;
                    default:

                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.PrdNameEng).ToList()
                                                                                                 : data.OrderBy(p => p.PrdNameEng).ToList();
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
        public (string status, string message) Create(Product data, string ImgName, Byte[] photo)
        {
            try
            {
                //Byte[] bytes = null;
                //if (data.ImgUpload.FileName != null)
                //{

                //    Stream fs = data.ImgUpload.InputStream;

                //    BinaryReader br = new BinaryReader(fs);

                //    bytes = br.ReadBytes((Int32)fs.Length);
                //}
                tbl_products t = new tbl_products();
                t.ProductID = Helper.AutoID();
                t.PrdCategID = data.PrdCategID;
                t.SupplierID = "0";
                t.PrdNameEng = data.PrdNameEng;
                t.PrdNameKh = data.PrdNameEng;
                t.UnitType = data.UnitType ?? "";
                t.totalInStock = 0;
                t.minimalStock = data.minimalStock ?? 0;
                t.SellingPriceUSD = data.SellingPriceUSD ?? 0;
                t.SellingPriceKHR = Helper.ExchangeRate((float)t.SellingPriceUSD, "usdToKhr");
                t.SellingPriceTHB = Helper.ExchangeRate((float)t.SellingPriceUSD, "usdToThb");
                t.Photo = photo;
                t.OrderComment = data.OrderComment ?? "";
                t.PrdDesc = data.PrdDesc ?? "";
                t.barCode = data.barCode ?? "";
                t.Delstatus = "";
                t.isCutStock = data.isCutStock;
                t.ProductIDRealStock = data.ProductIDRealStock == null ? t.ProductID : data.ProductIDRealStock;
                t.NumInOne = data.NumInOne ?? 1;
                t.IsUnique = data.IsUnique;
                t.TaxNote = data.TaxNote;
                t.Money_Sale_Type = data.Money_Sale_Type;
                t.Image = ImgName;
                t.BuyingCost = data.BuyingCost ?? 0;
                context.tbl_products.Add(t);
                this.Save();

                tbl_products_Customers_Price customersPrice = new tbl_products_Customers_Price();
                customersPrice.ProductID = t.ProductID;
                customersPrice.memberID = "1";
                customersPrice.SellingPriceUSDForThisCus = data.SellingPriceUSDForThisCus;
                this.ProductsCustomersPrice(customersPrice);

                //tblStockTypeProduct stockProducts = new tblStockTypeProduct();
                //stockProducts.StockID = "0";
                //stockProducts.ProductID = t.ProductID;
                //stockProductRepository.CreateOrUpdate(stockProducts);

                ////Call insert  table tbl_BuyProducts
                tbl_BuyProducts buyProduct = new tbl_BuyProducts();
                buyProduct.InvoiceNo = "000001";
                buyProduct.SupplierID = "0";
                this.BuyProduct(buyProduct, data, t.ProductID);

                ////////////Call insert table main_stocks
                ////Main_Stocks _main_stock = new Main_Stocks();
                ////_main_stock.ProductID = t.ProductID;
                ////_main_stock.unitType = t.UnitType;
                ////_main_stock.BuyingCost = data.BuyingCost;
                ////_main_stock.CommentDetails = _buy_product.InvoiceNo;
                ////_main_stock.SupplierID = t.SupplierID;
                ////_mainStockRepository.Insert(_main_stock);
            }
            catch (Exception ex)
            {
                return (Resources.Alerts.Error, ex.ToString());
            }
            return ("True", Resources.Alerts.Product_Created);
        }
        public (string status, string message) Update(Product data, string ImgName)
        {
            try
            {
                tbl_products t = context.tbl_products.Find(data.ProductID);
                t.PrdCategID = data.PrdCategID;
                //t.SupplierID = "0";
                t.PrdNameEng = data.PrdNameEng;
                t.PrdNameKh = data.PrdNameEng;
                t.UnitType = data.UnitType;
                //t.totalInStock = 0;
                t.minimalStock = data.minimalStock == null ? 0 : data.minimalStock;
                t.SellingPriceUSD = data.SellingPriceUSD == null ? 0.00 : data.SellingPriceUSD;
                t.SellingPriceKHR = Helper.ExchangeRate((float)t.SellingPriceUSD, "usdToKhr");
                t.SellingPriceTHB = Helper.ExchangeRate((float)t.SellingPriceUSD, "usdToThb");
                //byte[] bytes = null;
                //t.Photo = bytes;
                t.OrderComment = data.OrderComment == null ? "" : data.OrderComment;
                t.PrdDesc = data.PrdDesc == null ? "" : data.PrdDesc;
                t.barCode = data.barCode == null ? "" : data.barCode;
                //t.Delstatus = "";
                t.isCutStock = data.isCutStock;
                //t.ProductIDRealStock = data.ProductID;
                t.NumInOne = data.NumInOne == null ? 1 : data.NumInOne;
                t.IsUnique = data.IsUnique;
                t.TaxNote = data.TaxNote;
                t.Money_Sale_Type = data.Money_Sale_Type;
                t.Image = ImgName;
                context.Entry(t).State = EntityState.Modified;
                this.Save();

                tbl_products_Customers_Price customersPrice = new tbl_products_Customers_Price();
                customersPrice.ProductID = t.ProductID;
                customersPrice.memberID = "1";
                customersPrice.SellingPriceUSDForThisCus = data.SellingPriceUSDForThisCus;
                this.ProductsCustomersPrice(customersPrice);
            }
            catch (Exception ex)
            {
                return (Resources.Alerts.Error, ex.ToString());
            }
            return ("True", Resources.Alerts.Product_Updated);
        }
        public string ProductsCustomersPrice(tbl_products_Customers_Price data)
        {
            try
            {
                var _customerPrice = context.tbl_products_Customers_Price.FirstOrDefault(x => x.ProductID == data.ProductID && x.memberID == data.memberID);
                if (_customerPrice == null)
                {
                    tbl_products_Customers_Price customersPrice = new tbl_products_Customers_Price();
                    customersPrice.ProductID = data.ProductID;
                    customersPrice.memberID = data.memberID;
                    customersPrice.SellingPriceUSDForThisCus = data.SellingPriceUSDForThisCus;
                    if(customersPrice.SellingPriceUSDForThisCus > 0)
                    context.tbl_products_Customers_Price.Add(customersPrice);
                }
                else
                {
                    _customerPrice.ProductID = data.ProductID;
                    _customerPrice.memberID = data.memberID;
                    _customerPrice.SellingPriceUSDForThisCus = data.SellingPriceUSDForThisCus;
                    context.Entry(_customerPrice).State = EntityState.Modified;
                }
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }

        public (string status, string message) UpdateProductPrice(Product data, string ImgName)
        {
            try
            {
                tbl_products t = context.tbl_products.Find(data.ProductID);
                //t.PrdCategID = data.PrdCategID;
                ////t.SupplierID = "0";
                //t.PrdNameEng = data.PrdNameEng;
                //t.PrdNameKh = data.PrdNameEng;
                t.UnitType = data.UnitType;
                ////t.totalInStock = 0;
                t.totalInStock = data.totalInStock ?? 0;
                t.BuyingCost = data.BuyingCost ?? 0;
                //t.minimalStock = data.minimalStock == null ? 0 : data.minimalStock;
                t.SellingPriceUSD = data.SellingPriceUSD ?? 0;
                t.SellingPriceKHR = Helper.ExchangeRate((float)t.SellingPriceUSD, "usdToKhr");
                t.SellingPriceTHB = Helper.ExchangeRate((float)t.SellingPriceUSD, "usdToThb");
                ////byte[] bytes = null;
                ////t.Photo = bytes;
                //t.OrderComment = data.OrderComment == null ? "" : data.OrderComment;
                //t.PrdDesc = data.PrdDesc == null ? "" : data.PrdDesc;
                //t.barCode = data.barCode == null ? "" : data.barCode;
                ////t.Delstatus = "";
                //t.isCutStock = data.isCutStock;
                ////t.ProductIDRealStock = data.ProductID;
                //t.NumInOne = data.NumInOne == null ? 1 : data.NumInOne;
                //t.IsUnique = data.IsUnique;
                //t.TaxNote = data.TaxNote;
                //t.Money_Sale_Type = data.Money_Sale_Type;
                //t.Image = ImgName;
                context.Entry(t).State = EntityState.Modified;
                this.Save();
            }
            catch (Exception ex)
            {
                return (Resources.Alerts.Error, ex.ToString());
            }
            return ("True", Resources.Alerts.Product_Updated);
        }

        public string BuyProduct(tbl_BuyProducts data, Product product,string ProductID)
        {
            try
            {
                tbl_BuyProducts buyProduct = new tbl_BuyProducts();
                buyProduct.BuyID = Helper.AutoID()+1;
                buyProduct.Date = data.Date;
                buyProduct.InvoiceNo = data.InvoiceNo;
                buyProduct.StockID = data.StockID;
                buyProduct.SupplierID = data.SupplierID;
                buyProduct.Total = data.Total;
                buyProduct.PerDis = data.PerDis;
                buyProduct.GrandTotal = data.GrandTotal;
                buyProduct.AmtPay = data.AmtPay;
                buyProduct.AmtLoan = data.AmtLoan;
                buyProduct.Status = data.Status == null ? "" : data.Status;
                buyProduct.DateReturn = data.DateReturn;
                buyProductRepository.Create(buyProduct);

                tbl_mainstock mainStock = new tbl_mainstock();
                mainStock.MainStockID = Helper.AutoID()+2;
                mainStock.ProductID = ProductID;
                mainStock.PurchaseDate = DateTime.Now;
                mainStock.StockinDate = DateTime.Now;
                mainStock.caseRetail = "";
                mainStock.unitType = product.UnitType == null ? "" : product.UnitType;
                mainStock.BuyingCost = product.BuyingCost == null ? 0 : product.BuyingCost;
                mainStock.transCost = 0;
                mainStock.tax = 0;
                mainStock.otherExp = 0;
                mainStock.Quantity = product.Quantity == null ? 0 : product.Quantity;
                mainStock.TotalCost = mainStock.Quantity * mainStock.BuyingCost;
                mainStock.PytStatus = "ទំនិញថ្មី";
                mainStock.CommentDetails = data.InvoiceNo;
                mainStock.stockInDateTime = DateTime.Now;
                mainStock.qtyInStock = mainStock.Quantity;
                mainStock.OrderNo = 0;
                mainStock.prdStatus = "ទំនិញថ្មី";
                mainStock.usdTokhr = Helper.ExchangeRate(0, "usdToKhr", "defaultRate");
                mainStock.usdTothb = Helper.ExchangeRate(0, "usdToThb", "defaultRate");
                mainStock.SupplierID = data.SupplierID == null ? "0" : data.SupplierID;
                //mainStock.ExpireDate = product.ExpireDate == null ? DateTime.Now : product.ExpireDate;
                mainStock.BuyID = buyProduct.BuyID;
                mainStock.StockID = data.StockID == null ? "0" : data.StockID;
                context.tbl_mainstock.Add(mainStock);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Helpers;
using System.Data.Entity;

namespace WebApplication.Repositories
{
    public class BuyProductRepository
    {
        private DataContext context;
        private MainStockRepository mainStockRepository;
        //private StockProductRepository stockProductRepository;
        //private ProductRepository productRepository;
        //string ProductID;
        public BuyProductRepository()
        {
            this.context = new DataContext();
            this.mainStockRepository = new MainStockRepository();
        }
        public List<BuyProduct> GetForDataTable()
        {
            List<BuyProduct> lst = new List<BuyProduct>();
            var buyProducts = from b in context.tbl_BuyProducts
                              join s in context.tbl_suppliers on b.SupplierID equals s.SupplierID
                              join u in context.tbl_useraccount on b.UserID equals u.UserID
                              where b.InvoiceNo != "000001"
                              select new
                              {
                                  b.BuyID,
                                  b.Date,
                                  b.InvoiceNo,
                                  s.CompanyName,
                                  b.Total,
                                  b.PerDis,
                                  b.AmtDis,
                                  b.GrandTotal,
                                  b.AmtPay,
                                  b.AmtLoan,
                                  u.UserName,
                                  b.Description
                              };
            foreach (var i in buyProducts)
            {
                BuyProduct setdata = new BuyProduct();
                setdata.BuyID = i.BuyID;
                setdata.Date = i.Date;
                setdata.InvoiceNo = i.InvoiceNo;
                setdata.CompanyName = i.CompanyName;
                setdata.Total = i.Total;
                setdata.PerDis = i.PerDis;
                setdata.AmtDis = i.AmtDis;
                setdata.GrandTotal = i.GrandTotal;
                setdata.AmtPay = i.AmtPay;
                setdata.AmtLoan = i.AmtLoan;
                setdata.Username = i.UserName;
                setdata.Description = i.Description;
                lst.Add(setdata);
            }

            return lst;
        }
        public List<MainStock> GetDataBuyDetail(string buyId)
        {
            List<MainStock> lst = new List<MainStock>();
            var buyProducts = from m in context.tbl_mainstock
                              join b in context.tbl_BuyProducts on m.BuyID equals b.BuyID
                              join s in context.tbl_suppliers on b.SupplierID equals s.SupplierID
                              join p in context.tbl_products on m.ProductID equals p.ProductID
                              join u in context.tbl_useraccount on b.UserID equals u.UserID
                              where m.BuyID == buyId
                              select new
                              {
                                  b.BuyID,
                                  b.Date,
                                  b.InvoiceNo,
                                  s.CompanyName,
                                  b.Total,
                                  b.PerDis,
                                  b.AmtDis,
                                  b.GrandTotal,
                                  b.AmtPay,
                                  b.AmtLoan,
                                  u.UserName,
                                  b.Description,

                                  m.MainStockID,
                                  m.ProductID,
                                  p.PrdNameEng,
                                  p.barCode,
                                  p.UnitType,
                                  p.totalInStock,
                                  p.OrderComment,
                                  m.PurchaseDate,
                                  m.BuyingCost,
                                  m.Quantity,
                                  m.TotalCost,
                                  m.ExpireDate
                              };
            foreach (var i in buyProducts)
            {
                MainStock setdata = new MainStock();
                setdata.BuyID = i.BuyID;
                setdata.Date = i.Date;
                setdata.InvoiceNo = i.InvoiceNo;
                setdata.CompanyName = i.CompanyName;
                setdata.Total = i.Total;
                setdata.PerDis = i.PerDis;
                setdata.AmtDis = i.AmtDis;
                setdata.GrandTotal = i.GrandTotal;
                setdata.AmtPay = i.AmtPay;
                setdata.AmtLoan = i.AmtLoan;
                setdata.Username = i.UserName;
                setdata.Description = i.Description;

                setdata.MainStockID = i.MainStockID;
                setdata.ProductID = i.ProductID;
                setdata.PrdNameEng = i.PrdNameEng;
                setdata.barCode = i.barCode;
                setdata.unitType = i.UnitType;
                setdata.totalInStock = i.totalInStock;
                setdata.OrderComment = i.OrderComment;
                setdata.PurchaseDate = i.PurchaseDate;
                setdata.BuyingCost = i.BuyingCost;
                setdata.Quantity = i.Quantity;
                setdata.TotalCost = i.TotalCost;
                setdata.ExpireDate = i.ExpireDate;
                lst.Add(setdata);
            }

            return lst;
        }
        public List<BuyProduct> SortByColumnWithOrder(string order, string orderDir, List<BuyProduct> data)
        {
            // Initialization.  
            List<BuyProduct> lst = new List<BuyProduct>();

            try
            {
                // Sorting  
                switch (order)
                {
                    case "0":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Date).ToList()
                                                                                                 : data.OrderBy(p => p.Date).ToList();
                        break;

                    case "1":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.InvoiceNo).ToList()
                                                                                                 : data.OrderBy(p => p.InvoiceNo).ToList();
                        break;

                    default:

                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Date).ToList()
                                                                                                 : data.OrderBy(p => p.Date).ToList();
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
        public List<MainStock> GetForMainStockOrdering(string buyId = "")
        {
            List<MainStock> lst = new List<MainStock>();
            var buyProducts = from m in context.tbl_mainstock_ordering
                              join p in context.tbl_products on m.ProductID equals p.ProductID
                              join u in context.tbl_useraccount on m.UserID equals u.UserID
                              where m.BuyID == buyId
                              select new
                              {
                                  m.MainStockID,
                                  m.ProductID,
                                  p.PrdNameEng,
                                  m.PurchaseDate,
                                  m.BuyingCost,
                                  m.Quantity,
                                  m.TotalCost,
                                  m.ExpireDate
                              };
            //if (!string.IsNullOrEmpty(buyId))
            //{
            //    buyProducts = from m in context.tbl_mainstock_ordering
            //                      join p in context.tbl_products on m.ProductID equals p.ProductID
            //                      join u in context.tbl_useraccount on m.UserID equals u.UserID
            //                      where m.BuyID == buyId
            //                      select new
            //                      {
            //                          m.MainStockID,
            //                          m.ProductID,
            //                          p.PrdNameEng,
            //                          m.PurchaseDate,
            //                          m.BuyingCost,
            //                          m.Quantity,
            //                          m.TotalCost,
            //                          m.ExpireDate
            //                      };
            //}
            foreach (var i in buyProducts)
            {
                MainStock setdata = new MainStock();
                setdata.MainStockID = i.MainStockID;
                setdata.ProductID = i.ProductID;
                setdata.PrdNameEng = i.PrdNameEng;
                setdata.PurchaseDate = i.PurchaseDate;
                setdata.BuyingCost = i.BuyingCost;
                setdata.Quantity = i.Quantity;
                setdata.TotalCost = i.TotalCost;
                setdata.ExpireDate = i.ExpireDate;
                lst.Add(setdata);
            }

            return lst;
        }
        public List<MainStock> SortByColumnWithOrdering(string order, string orderDir, List<MainStock> data)
        {
            // Initialization.  
            List<MainStock> lst = new List<MainStock>();

            try
            {
                // Sorting  
                switch (order)
                {
                    case "0":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderBy(p => p.PurchaseDate).ToList()
                                                                                                 : data.OrderByDescending(p => p.PurchaseDate).ToList();
                        break;

                    case "1":
                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderBy(p => p.PrdNameEng).ToList()
                                                                                                 : data.OrderByDescending(p => p.PrdNameEng).ToList();
                        break;

                    default:

                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderBy(p => p.PurchaseDate).ToList()
                                                                                                 : data.OrderByDescending(p => p.PurchaseDate).ToList();
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
        public string Create(MainStock data)
        {
            try
            {
                tbl_mainstock_ordering mainStockOrdering = new tbl_mainstock_ordering();
                var setProduct = (from p in context.tbl_products
                                  join c in context.tbl_prdcategory on p.PrdCategID equals c.PrdCategID
                                  select new
                                  {
                                      p.ProductID,
                                      p.UnitType,
                                      p.PrdNameEng,
                                      p.barCode,
                                      p.OrderComment,
                                      c.PrdCategory
                                  }).FirstOrDefault(x => x.ProductID == data.ProductID);
                mainStockOrdering.MainStockID = Helper.AutoID();
                mainStockOrdering.ProductID = data.ProductID;
                mainStockOrdering.unitType = setProduct.UnitType ?? "";
                mainStockOrdering.PrdNameEng = setProduct.PrdNameEng;
                mainStockOrdering.barCode = setProduct.barCode;
                mainStockOrdering.OrderComment = setProduct.OrderComment;
                mainStockOrdering.PrdCategory = setProduct.PrdCategory;
                mainStockOrdering.BuyingCost = data.BuyingCost ?? 0;
                mainStockOrdering.SellingPriceUSD = data.SellingPriceUSD ?? 0;
                mainStockOrdering.transCost = data.transCost ?? 0;
                mainStockOrdering.Quantity = data.Quantity ?? 0;
                mainStockOrdering.TotalCost = mainStockOrdering.BuyingCost * mainStockOrdering.Quantity;
                mainStockOrdering.ExpireDate = data.ExpireDate ?? DateTime.Now;
                mainStockOrdering.UserID = IUser.Id;
                mainStockOrdering.BuyID = data.BuyID ?? "0";
                mainStockOrdering.BuyStatus = "New";
                context.tbl_mainstock_ordering.Add(mainStockOrdering);
                context.SaveChanges();

                this.UpdateCostAndSellPrice(mainStockOrdering);
            }
            catch (Exception ex)
            {
                // info.  
                Console.Write(ex);
                return ex.ToString();
            }
            return "True";
        }
        public string Update(MainStock data)
        {
            try
            {
                var mainStockOrdering = context.tbl_mainstock_ordering
                    .FirstOrDefault(x => x.ProductID == data.ProductID
                    && x.UserID == IUser.Id
                    && x.BuyID == data.BuyID
                    && x.BuyStatus == "New");
                mainStockOrdering.BuyingCost = data.BuyingCost ?? 0;
                mainStockOrdering.SellingPriceUSD = data.SellingPriceUSD ?? 0;
                mainStockOrdering.Quantity = data.Quantity + mainStockOrdering.Quantity;
                mainStockOrdering.TotalCost = mainStockOrdering.BuyingCost * mainStockOrdering.Quantity;
                mainStockOrdering.ExpireDate = data.ExpireDate ?? DateTime.Now;
                mainStockOrdering.UserID = IUser.Id;
                context.Entry(mainStockOrdering).State = EntityState.Modified;
                context.SaveChanges();

                this.UpdateCostAndSellPrice(mainStockOrdering);
            }
            catch (Exception ex)
            {
                // info.  
                Console.Write(ex);
                return ex.ToString();
            }
            return "True";
        }
        public string UpdateCostAndSellPrice(tbl_mainstock_ordering data)
        {
            try
            {
                var setProduct = context.tbl_products.FirstOrDefault(x => x.ProductID == data.ProductID);
                setProduct.SellingPriceUSD = data.SellingPriceUSD;
                setProduct.SellingPriceKHR = Helper.ExchangeRate((float)data.SellingPriceUSD, "usdToKhr");
                setProduct.SellingPriceTHB = Helper.ExchangeRate((float)data.SellingPriceUSD, "usdToThb");
                setProduct.BuyingCost = data.BuyingCost;
                context.Entry(setProduct).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                // info.  
                Console.Write(ex);
                return ex.ToString();
            }
            return "True";
        }
        public string TransferToModify(MainStock data)
        {
            try
            {
                var mainStocks = context.tbl_mainstock.Where(x => x.BuyID == data.BuyID).ToList();
                mainStocks.ForEach(history =>
                {
                    //check delete data in tbl_mainstock_ordering
                    var mainStock = context.tbl_mainstock_ordering
                                      .FirstOrDefault(x => x.MainStockID == history.MainStockID);
                    if(mainStock != null)
                    {
                        context.tbl_mainstock_ordering.Remove(mainStock);
                        context.SaveChanges();
                    }
                    //create transfer to modify invoice
                    tbl_mainstock_ordering mainStockOrdering = new tbl_mainstock_ordering();
                    var setProduct = (from p in context.tbl_products
                                      join c in context.tbl_prdcategory on p.PrdCategID equals c.PrdCategID
                                      select new
                                      {
                                          p.ProductID,
                                          p.SellingPriceUSD,
                                          p.UnitType,
                                          p.PrdNameEng,
                                          p.barCode,
                                          p.OrderComment,
                                          c.PrdCategory
                                      }).FirstOrDefault(x => x.ProductID == history.ProductID);
                    mainStockOrdering.MainStockID = history.MainStockID;
                    mainStockOrdering.ProductID = setProduct.ProductID;
                    mainStockOrdering.unitType = setProduct.UnitType ?? "";
                    mainStockOrdering.PrdNameEng = setProduct.PrdNameEng;
                    mainStockOrdering.barCode = setProduct.barCode;
                    mainStockOrdering.OrderComment = setProduct.OrderComment;
                    mainStockOrdering.PrdCategory = setProduct.PrdCategory;
                    mainStockOrdering.BuyingCost = history.BuyingCost ?? 0;
                    mainStockOrdering.SellingPriceUSD = setProduct.SellingPriceUSD ?? 0;
                    mainStockOrdering.transCost = history.transCost ?? 0;
                    mainStockOrdering.Quantity = history.Quantity ?? 0;
                    mainStockOrdering.TotalCost = mainStockOrdering.BuyingCost * mainStockOrdering.Quantity;
                    mainStockOrdering.ExpireDate = data.ExpireDate ?? DateTime.Now;
                    mainStockOrdering.UserID = IUser.Id;
                    mainStockOrdering.BuyID = history.BuyID;
                    mainStockOrdering.BuyStatus = "Old";
                    mainStockOrdering.BuyReturnDate = DateTime.Now;
                    context.tbl_mainstock_ordering.Add(mainStockOrdering);
                    this.Save();
                });
            }
            catch (Exception ex)
            {
                // info.  
                Console.Write(ex);
                return ex.ToString();
            }
            return "True";
        }
        public string Delete(MainStock data)
        {
            try
            {
                var mainStockOrdering = context.tbl_mainstock_ordering.FirstOrDefault(x => x.MainStockID == data.MainStockID);
                context.tbl_mainstock_ordering.Remove(mainStockOrdering);
                context.SaveChanges();

                var mainStock = context.tbl_mainstock.FirstOrDefault(x => x.MainStockID == data.MainStockID);
                if(mainStock != null)
                {
                    context.tbl_mainstock.Remove(mainStock);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // info.  
                Console.Write(ex);
                return ex.ToString();
            }

            return "True";
        }
        public string CreatePayment(tbl_BuyProducts data)
        {
            try
            {
                tbl_BuyProducts buyProduct = new tbl_BuyProducts();
                buyProduct.BuyID = Helper.AutoID();
                buyProduct.Date = data.Date ?? DateTime.Now;
                buyProduct.InvoiceNo = data.InvoiceNo;
                buyProduct.SupplierID = data.SupplierID;
                buyProduct.Total = data.Total ?? 0;
                buyProduct.PerDis = data.PerDis ?? 0;
                buyProduct.AmtDis = data.AmtDis ?? 0;
                buyProduct.GrandTotal = data.GrandTotal ?? 0;
                buyProduct.AmtPay = data.AmtPay ?? 0;
                buyProduct.AmtLoan = data.AmtLoan ?? 0;
                buyProduct.UserID = IUser.Id;
                buyProduct.usdtoKher = Helper.ExchangeRate(0, "usdToKhr", "defaultRate");
                buyProduct.usdtoTHB = Helper.ExchangeRate(0, "usdToThb", "defaultRate");
                buyProduct.Status = data.Status;
                buyProduct.DateReturn = data.DateReturn ?? DateTime.Now;
                buyProduct.StockID = data.StockID ?? "0";
                buyProduct.Description = data.Description ?? "";
                context.tbl_BuyProducts.Add(buyProduct);

                tbl_mainstock mainStock = new tbl_mainstock();
                var mainStockOrders = context.tbl_mainstock_ordering
                    .Where(x => x.BuyID == data.BuyID)
                    .ToList();
                int i = 0;
                foreach (var row in mainStockOrders)
                {
                    i++;
                    mainStock.OrderNo = i;
                    mainStock.MainStockID = row.MainStockID;
                    mainStock.ProductID = row.ProductID;
                    mainStock.unitType = row.unitType;
                    mainStock.BuyingCost = (float)row.BuyingCost;
                    mainStock.transCost = row.transCost;
                    mainStock.Quantity = row.Quantity;
                    mainStock.ExpireDate = row.ExpireDate;
                    mainStock.StockinDate = buyProduct.Date;
                    mainStock.CommentDetails = buyProduct.InvoiceNo;
                    mainStock.SupplierID = buyProduct.SupplierID;
                    mainStock.BuyID = buyProduct.BuyID;
                    mainStock.StockID = buyProduct.StockID;
                    mainStock.caseRetail = "";
                    mainStock.qtyInStock = 0;
                    mainStockRepository.PlusMainStock(mainStock);
                    this.Remove(row.MainStockID);
                }
            }
            catch (Exception ex)
            {
                // info.  
                Console.Write(ex);
                return ex.ToString();
            }
            return "True";
        }
        public string UpdatePayment(tbl_BuyProducts data)
        {
            try
            {
                var buyProduct = context.tbl_BuyProducts.FirstOrDefault(x => x.BuyID == data.BuyID);
                buyProduct.Date = data.Date ?? DateTime.Now;
                buyProduct.InvoiceNo = data.InvoiceNo;
                buyProduct.SupplierID = data.SupplierID;
                buyProduct.Total = data.Total ?? 0;
                buyProduct.PerDis = data.PerDis ?? 0;
                buyProduct.AmtDis = data.AmtDis ?? 0;
                buyProduct.GrandTotal = data.GrandTotal ?? 0;
                buyProduct.AmtPay = data.AmtPay ?? 0;
                buyProduct.AmtLoan = data.AmtLoan ?? 0;
                buyProduct.UserID = IUser.Id;
                buyProduct.usdtoKher = Helper.ExchangeRate(0, "usdToKhr", "defaultRate");
                buyProduct.usdtoTHB = Helper.ExchangeRate(0, "usdToThb", "defaultRate");
                buyProduct.Status = data.Status;
                buyProduct.DateReturn = data.DateReturn ?? DateTime.Now;
                buyProduct.StockID = data.StockID ?? "0";
                buyProduct.Description = data.Description ?? "";
                context.Entry(buyProduct).State = EntityState.Modified;

                tbl_mainstock mainStock = new tbl_mainstock();
                var mainStockOrders = context.tbl_mainstock_ordering
                    .Where(x => x.BuyID == buyProduct.BuyID)
                    .ToList();
                int i = 0;
                foreach (var row in mainStockOrders)
                {
                    i++;
                    mainStock.OrderNo = i;
                    mainStock.MainStockID = row.MainStockID;
                    mainStock.ProductID = row.ProductID;
                    mainStock.unitType = row.unitType;
                    mainStock.BuyingCost = (float)row.BuyingCost;
                    mainStock.transCost = row.transCost;
                    mainStock.Quantity = row.Quantity;
                    mainStock.ExpireDate = row.ExpireDate;
                    mainStock.StockinDate = buyProduct.Date;
                    mainStock.CommentDetails = buyProduct.InvoiceNo;
                    mainStock.SupplierID = buyProduct.SupplierID;
                    mainStock.BuyID = buyProduct.BuyID;
                    mainStock.StockID = buyProduct.StockID;
                    mainStock.caseRetail = "";
                    mainStock.qtyInStock = 0;
                    mainStockRepository.CreateOrUpdate(mainStock);
                    this.Remove(row.MainStockID);
                }
            }
            catch (Exception ex)
            {
                // info.  
                Console.Write(ex);
                return ex.ToString();
            }
            return "True";
        }
        public string Remove(string MainStockID)
        {
            try
            {
                var mainStockOrdering = context.tbl_mainstock_ordering
                                      .FirstOrDefault(x => x.MainStockID == MainStockID);
                context.tbl_mainstock_ordering.Remove(mainStockOrdering);
                this.Save();
            }
            catch (Exception ex)
            {
                // info.  
                Console.Write(ex);
                return ex.ToString();
            }

            return "True";
        }
        public string Create(tbl_BuyProducts data)
        {
            try
            {
                tbl_BuyProducts buyProduct = new tbl_BuyProducts();
                buyProduct.BuyID = data.BuyID;
                buyProduct.Date = data.Date == null ? DateTime.Now : data.Date;
                buyProduct.InvoiceNo = data.InvoiceNo;
                buyProduct.SupplierID = data.SupplierID;
                buyProduct.Total = data.Total == null ? 0 : data.Total;
                buyProduct.PerDis = data.PerDis == null ? 0 : data.PerDis;
                float _AmtDis = ((float)buyProduct.PerDis / 100) * (float)buyProduct.Total;
                buyProduct.AmtDis = _AmtDis;
                buyProduct.GrandTotal = data.GrandTotal == null ? 0 : data.GrandTotal;
                buyProduct.AmtPay = data.AmtPay == null ? 0 : data.AmtPay;
                buyProduct.AmtLoan = data.AmtLoan == null ? 0 : data.AmtLoan;
                buyProduct.UserID = IUser.Id;
                buyProduct.usdtoKher = Helper.ExchangeRate(0, "usdToKhr", "defaultRate");
                buyProduct.usdtoTHB = Helper.ExchangeRate(0, "usdToThb", "defaultRate");
                buyProduct.Status = data.Status;
                buyProduct.DateReturn = data.DateReturn == null ? DateTime.Now : data.DateReturn;
                buyProduct.StockID = data.StockID == null ? "0" : data.StockID;
                context.tbl_BuyProducts.Add(buyProduct);
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
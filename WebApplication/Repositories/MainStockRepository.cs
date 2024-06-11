using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Helpers;
using System.Data.Entity;

namespace WebApplication.Repositories
{
    public class MainStockRepository
    {
        private DataContext context;
        private StockProductRepository stockProductRepository;
        public MainStockRepository()
        {
            this.context = new DataContext();
            this.stockProductRepository = new StockProductRepository();
        }
        string MainStockID;
        string ProductID;
        double orderQty;
        double itemCount;
        //DateTime _dateExpire;
        public string Insert(tbl_mainstock data)
        {
            try
            {
                tbl_mainstock mainStock = new tbl_mainstock();
                mainStock.MainStockID = Helper.AutoID();
                mainStock.ProductID = data.ProductID;
                mainStock.PurchaseDate = data.PurchaseDate == null ? DateTime.Now : data.PurchaseDate;
                mainStock.StockinDate = data.StockinDate == null ? DateTime.Now : data.StockinDate;
                mainStock.caseRetail = data.caseRetail;
                mainStock.unitType = data.unitType;
                mainStock.BuyingCost = data.BuyingCost == null ? 0 : data.BuyingCost;
                mainStock.transCost = data.transCost == null ? 0 : data.transCost;
                mainStock.tax = data.tax == null ? 0 : data.tax;
                mainStock.otherExp = data.otherExp == null ? 0 : data.otherExp;
                mainStock.Quantity = data.Quantity == null ? 0 : data.Quantity;
                mainStock.TotalCost = mainStock.Quantity * mainStock.BuyingCost;
                mainStock.PytStatus = "ទំនិញថ្មី";
                mainStock.CommentDetails = data.CommentDetails == null ? "000001" : data.CommentDetails;
                mainStock.stockInDateTime = DateTime.Now;
                mainStock.qtyInStock = data.Quantity == null ? 0 : data.Quantity;
                mainStock.OrderNo = data.OrderNo == null ? 0 : data.OrderNo;
                mainStock.prdStatus = "ទំនិញថ្មី";
                mainStock.usdTokhr = Helper.ExchangeRate(0, "usdToKhr", "defaultRate");
                mainStock.usdTothb = Helper.ExchangeRate(0, "usdToThb", "defaultRate");
                mainStock.SupplierID = data.SupplierID;
                mainStock.ExpireDate = data.ExpireDate == null ? DateTime.Now.Date : data.ExpireDate;
                mainStock.BuyID = data.BuyID == null ? "0" : data.BuyID;
                mainStock.StockID = data.StockID == null ? "0" : data.StockID;
                context.tbl_mainstock.Add(mainStock);
                this.Save();

                //////Call insert to table Stock_Products
                tblStockTypeProduct _stock_products = new tblStockTypeProduct();
                _stock_products.StockID = mainStock.StockID;
                _stock_products.ProductID = mainStock.ProductID;
                _stock_products.Qty = mainStock.qtyInStock;
                _stock_products.TransferDate = mainStock.stockInDateTime;
                _stock_products.LastDate = DateTime.Now;
                stockProductRepository.CreateOrUpdate(_stock_products);

                //Buy_Products _buy_product = new Buy_Products();
                //_buy_product.InvoiceNo = mainStock.CommentDetails;
                //_buy_product.SupplierID = mainStock.SupplierID;
                //_buy_product.Total = mainStock.TotalCost;
                //_buyProductRepository.Insert(_buy_product);
            }
            catch (Exception ex)
            {
                // info.  
                Console.Write(ex);
                return ex.ToString();
            }
            return "True";
        }
        //Save buy in stock 
        public string PlusMainStock(tbl_mainstock data)
        {
            try
            {
                tbl_mainstock mainStock = new tbl_mainstock();
                mainStock.MainStockID = data.MainStockID;
                mainStock.ProductID = data.ProductID;
                mainStock.PurchaseDate = data.PurchaseDate ?? DateTime.Now;
                mainStock.StockinDate = data.StockinDate ?? DateTime.Now;
                mainStock.caseRetail = data.caseRetail ?? "";
                mainStock.unitType = data.unitType;
                mainStock.BuyingCost = data.BuyingCost;
                mainStock.transCost = data.transCost;
                mainStock.tax = 0;
                mainStock.otherExp = 0;
                mainStock.Quantity = data.Quantity;
                mainStock.TotalCost = mainStock.Quantity * mainStock.BuyingCost;
                mainStock.PytStatus = "ទំនិញថ្មី";
                mainStock.CommentDetails = data.CommentDetails;
                mainStock.stockInDateTime = DateTime.Now;
                mainStock.OrderNo = data.OrderNo;
                mainStock.prdStatus = "ទំនិញថ្មី";
                mainStock.usdTokhr = Helper.ExchangeRate(0, "usdToKhr", "defaultRate");
                mainStock.usdTothb = Helper.ExchangeRate(0, "usdToThb", "defaultRate");
                mainStock.SupplierID = data.SupplierID;
                mainStock.ExpireDate = data.ExpireDate;
                mainStock.BuyID = data.BuyID;
                mainStock.StockID = data.StockID;
                mainStock.qtyInStock = data.qtyInStock ?? 0;

                //var rows = context.tbl_mainstock.Where(x => x.ProductID == mainStock.ProductID && x.qtyInStock < 0).ToList();
                //mainStock.qtyInStock = rows.Sum(x => x.qtyInStock) + mainStock.Quantity;
                //rows.ForEach(a =>
                //{
                //    a.qtyInStock = 0;
                //});
                context.tbl_mainstock.Add(mainStock);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        //Save buy in stock 
        public string CreateOrUpdate(tbl_mainstock data)
        {
            try
            {
                var setMainStock = context.tbl_mainstock.FirstOrDefault(x => x.MainStockID == data.MainStockID);
                if (setMainStock == null)
                {
                    tbl_mainstock mainStock = new tbl_mainstock();
                    mainStock.MainStockID = data.MainStockID;
                    mainStock.ProductID = data.ProductID;
                    mainStock.PurchaseDate = data.PurchaseDate ?? DateTime.Now;
                    mainStock.StockinDate = data.StockinDate ?? DateTime.Now;
                    mainStock.caseRetail = data.caseRetail ?? "";
                    mainStock.unitType = data.unitType;
                    mainStock.BuyingCost = data.BuyingCost;
                    mainStock.transCost = data.transCost;
                    mainStock.tax = 0;
                    mainStock.otherExp = 0;
                    mainStock.Quantity = data.Quantity;
                    mainStock.TotalCost = mainStock.Quantity * mainStock.BuyingCost;
                    mainStock.PytStatus = "ទំនិញថ្មី";
                    mainStock.CommentDetails = data.CommentDetails;
                    mainStock.stockInDateTime = DateTime.Now;
                    mainStock.OrderNo = data.OrderNo;
                    mainStock.prdStatus = "ទំនិញថ្មី";
                    mainStock.usdTokhr = Helper.ExchangeRate(0, "usdToKhr", "defaultRate");
                    mainStock.usdTothb = Helper.ExchangeRate(0, "usdToThb", "defaultRate");
                    mainStock.SupplierID = data.SupplierID;
                    mainStock.ExpireDate = data.ExpireDate;
                    mainStock.BuyID = data.BuyID;
                    mainStock.StockID = data.StockID;
                    mainStock.qtyInStock = data.qtyInStock ?? 0;
                    context.tbl_mainstock.Add(mainStock);
                    context.SaveChanges();
                }
                else
                {
                    setMainStock.ProductID = data.ProductID;
                    setMainStock.PurchaseDate = data.PurchaseDate ?? DateTime.Now;
                    setMainStock.StockinDate = data.StockinDate ?? DateTime.Now;
                    setMainStock.caseRetail = data.caseRetail ?? "";
                    setMainStock.unitType = data.unitType;
                    setMainStock.BuyingCost = data.BuyingCost;
                    setMainStock.transCost = data.transCost;
                    setMainStock.tax = 0;
                    setMainStock.otherExp = 0;
                    setMainStock.Quantity = data.Quantity;
                    setMainStock.TotalCost = setMainStock.Quantity * setMainStock.BuyingCost;
                    setMainStock.PytStatus = "ទំនិញថ្មី";
                    setMainStock.CommentDetails = data.CommentDetails;
                    setMainStock.stockInDateTime = DateTime.Now;
                    setMainStock.OrderNo = data.OrderNo;
                    setMainStock.prdStatus = "ទំនិញថ្មី";
                    setMainStock.usdTokhr = Helper.ExchangeRate(0, "usdToKhr", "defaultRate");
                    setMainStock.usdTothb = Helper.ExchangeRate(0, "usdToThb", "defaultRate");
                    setMainStock.SupplierID = data.SupplierID;
                    setMainStock.ExpireDate = data.ExpireDate;
                    setMainStock.BuyID = data.BuyID;
                    setMainStock.StockID = data.StockID;
                    setMainStock.qtyInStock = data.qtyInStock ?? 0;
                    context.Entry(setMainStock).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        public string Update(tbl_mainstock data)
        {
            try
            {
                tbl_mainstock mainStock = context.tbl_mainstock.FirstOrDefault(x => x.MainStockID == data.MainStockID);
                context.Entry(mainStock).State = EntityState.Modified;
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }

        public void SubtractMainStock(tbl_mainstock data, string orderDetailsID, string customerOrderID)
        {
            orderQty = (double)data.Quantity;
            //var items = context.tbl_mainstock.Where(x => x.ProductID == data.ProductID && x.StockID == IUser.StockID).ToList();
            var items = context.tbl_mainstock.Where(x => x.ProductID == data.ProductID).ToList();
            tblStockTypeProduct stockProduct = new tblStockTypeProduct();
            foreach (var item in items)
            {
                var mainStock = context.tbl_mainstock.FirstOrDefault(x => x.MainStockID == item.MainStockID);
                MainStockID = mainStock.MainStockID;
                ProductID = item.ProductID;
                //_dateExpire = mainStock.ExpireDate == null ? DateTime.Now : (DateTime)mainStock.ExpireDate;
                itemCount = (double)item.qtyInStock;
                if (orderQty > itemCount)
                {
                    //go to update qtyInstock 0
                    orderQty = orderQty - itemCount;
                    mainStock.qtyInStock = 0;
                    context.Entry(mainStock).State = EntityState.Modified;
                    this.Save();



                    /////////Update Subtract table Stock_Products
                    //_stock_product.StockID = _StockID;
                    //_stock_product.ProductID = (long)item.ProductID;
                    //_stock_product.Qty = mainStock.qtyInStock;
                    //_stockProductRepository.SubtractProduct(_stock_product);
                }
                else
                {
                    //go to update qtyInstock Positive
                    itemCount = itemCount - orderQty;
                    mainStock.qtyInStock = Math.Round(itemCount,15);
                    context.Entry(mainStock).State = EntityState.Modified;
                    this.Save();



                    /////////Update Subtract table Stock_Products
                    //_stock_product.StockID = _StockID;
                    //_stock_product.ProductID = (long)item.ProductID;
                    //_stock_product.Qty = mainStock.qtyInStock;
                    //_stockProductRepository.SubtractProduct(_stock_product);

                    orderQty = 0;
                    if (orderQty == 0)
                    {
                        break;
                    }
                }
            }

            if (orderQty != 0)
            {
                //go to update qtyInstock Negative
                var mainStock = context.tbl_mainstock.FirstOrDefault(x => x.MainStockID == MainStockID);
                orderQty = (double)mainStock.qtyInStock - orderQty;
                mainStock.qtyInStock = Math.Round(orderQty,15);
                context.Entry(mainStock).State = EntityState.Modified;
                this.Save();

                /////////Update Subtract table Stock_Products
                //_stock_product.StockID = _StockID;
                //_stock_product.ProductID = _ProductID;
                //_stock_product.Qty = mainStock.qtyInStock;
                //_stockProductRepository.SubtractProduct(_stock_product);
            }
            //if(orderCount == 0)
            //{
            //    Message Ok
            //}
            //else
            //{
            //    Message No
            //}
            tbl_orderdetails ordersDetail = new tbl_orderdetails();
            ordersDetail.orderDetailsID = orderDetailsID;
            ordersDetail.customerOrderID = customerOrderID;
            //_orders_detail.dateExpire = _dateExpire;
            this.UpdatePayStatus(ordersDetail);
        }

        public string UpdatePayStatus(tbl_orderdetails data)
        {
            try
            {
                tbl_orderdetails ordersDetail = context.tbl_orderdetails.FirstOrDefault(x => x.orderDetailsID == data.orderDetailsID && x.userID == IUser.Id);
                ordersDetail.customerOrderID = data.customerOrderID;
                ordersDetail.PytStatus = "Paid";
                //_orders_detail.dateExpire = data.dateExpire;
                context.Entry(ordersDetail).State = EntityState.Modified;
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }

        public void PlusReturnToMainStock(tbl_mainstock data)
        {
            float orderCount;
            float itemCount;
            orderCount = (float)data.Quantity;
            var items = context.tbl_mainstock.Where(x => x.ProductID == data.ProductID && x.StockID == IUser.Id).OrderByDescending(x => x.MainStockID).ToList();
            foreach (var item in items)
            {
                var mainStock = context.tbl_mainstock.FirstOrDefault(x => x.MainStockID == item.MainStockID);
                MainStockID = mainStock.MainStockID;
                orderCount = (float)item.qtyInStock + orderCount;
                itemCount = (float)item.Quantity;
                if (orderCount > itemCount)
                {
                    //go to update qtyInstock plus
                    orderCount = orderCount - itemCount;
                    mainStock.qtyInStock = Math.Round((double)mainStock.Quantity,15);
                    context.Entry(mainStock).State = EntityState.Modified;
                    this.Save();
                }
                else
                {
                    //go to update qtyInstock plus
                    mainStock.qtyInStock = Math.Round(orderCount,15);
                    context.Entry(mainStock).State = EntityState.Modified;
                    this.Save();

                    orderCount = 0;
                    if (orderCount == 0)
                    {
                        break;
                    }
                }
            }

            if (orderCount != 0)
            {
                //go to update qtyInstock Negative
                var mainStock = context.tbl_mainstock.Where(x => x.MainStockID == MainStockID).FirstOrDefault();
                orderCount = (float)mainStock.Quantity == 0 ? (float)orderCount : (float)mainStock.Quantity;
                mainStock.qtyInStock = Math.Round(orderCount,15);
                context.Entry(mainStock).State = EntityState.Modified;
                this.Save();
            }
            ////if(orderCount == 0)
            ////{
            ////    Message Ok
            ////}
            ////else
            ////{
            ////    Message No
            ////}
        }

        public string Delete(tbl_mainstock data)
        {
            try
            {
                tbl_mainstock mainStock = context.tbl_mainstock.FirstOrDefault(x => x.MainStockID == data.MainStockID);
                context.tbl_mainstock.Remove(mainStock);
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
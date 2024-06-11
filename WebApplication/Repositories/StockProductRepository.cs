using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Helpers;

namespace WebApplication.Repositories
{
    public class StockProductRepository
    {
        private DataContext context;
        double orderQty;
        public StockProductRepository()
        {
            this.context = new DataContext();
        }
        public string CreateOrUpdate(tblStockTypeProduct data)
        {
            try
            {
                bool stockProduct = context.tblStockTypeProducts.Any(x => x.ProductID == data.ProductID && x.StockID == data.StockID);
                if (stockProduct)
                {
                    this.Update(data);
                }
                else
                {
                    this.Create(data);
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        public string Create(tblStockTypeProduct data)
        {
            try
            {
                tblStockTypeProduct stockProducts = new tblStockTypeProduct();
                stockProducts.StockID = data.StockID;
                stockProducts.ProductID = data.ProductID;
                stockProducts.Qty = data.Qty == null ? 0 : data.Qty;
                stockProducts.TransferDate = data.TransferDate == null ? DateTime.Now : data.TransferDate;
                context.tblStockTypeProducts.Add(stockProducts);
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }

        public string Update(tblStockTypeProduct data)
        {
            try
            {
                tblStockTypeProduct stockProduct = context.tblStockTypeProducts.FirstOrDefault(x => x.StockID == data.StockID && x.ProductID == data.ProductID);
                orderQty = (double)stockProduct.Qty + (double)data.Qty;
                stockProduct.Qty = (double)Math.Round(orderQty, 15);
                //stockProduct.Qty = stockProduct.Qty + data.Qty;
                stockProduct.TransferDate = data.TransferDate;
                context.Entry(stockProduct).State = EntityState.Modified;
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }

        public string SubtractProduct(tblStockTypeProduct data)
        {
            try
            {
                //tblStockTypeProduct stockProducts = context.tblStockTypeProducts.FirstOrDefault(x => x.StockID == IUser.StockID && x.ProductID == data.ProductID);
                var stockProducts = context.tblStockTypeProducts.FirstOrDefault(x => x.ProductID == data.ProductID);

                //stockProducts.Qty = Math.Round((double)(stockProducts.Qty - (double)data.Qty), 8);
                orderQty = (double)stockProducts.Qty - (double)data.Qty;
                stockProducts.Qty = (double)Math.Round(orderQty, 15);
                stockProducts.LastDate = data.LastDate;
                context.Entry(stockProducts).State = EntityState.Modified;
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        public string Delete(tblStockTypeProduct data)
        {
            try
            {
                tblStockTypeProduct _stock_products = context.tblStockTypeProducts.FirstOrDefault(x => x.StockID == data.StockID);
                context.tblStockTypeProducts.Remove(_stock_products);
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
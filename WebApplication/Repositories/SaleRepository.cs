using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Helpers;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication.Repositories
{
    public class SaleRepository
    {
        private DataContext context;
        private DBConnect dBConnect;
        private MainStockRepository mainStockRepository;
        private StockProductRepository stockProductRepository;
        float unitPrice;
        float percentDiscount;
        string customerOrderID;
        string AutoId;
        public SaleRepository()
        {
            this.context = new DataContext();
            this.dBConnect = new DBConnect();
            this.mainStockRepository = new MainStockRepository();
            this.stockProductRepository = new StockProductRepository();
        }
        public List<Sale> GetForSale()
        {
            // Initialization.
            List<Sale> lst = new List<Sale>();
            try
            {
                //var products = (from p in context.tbl_products
                //                    //join c in context.tbl_prdcategory on pg.PrdCategID equals c.PrdCategID
                //                //where (p.isSale == true)
                //                group p by p.PrdNameEng into g
                //                select new
                //                {
                //                    ProductID = g.Min(x => x.ProductID),
                //                    PrdCategID = g.Min(x => x.PrdCategID),
                //                    barCode = g.Min(x => x.barCode),
                //                    PrdNameEng = g.Key,
                //                    Image = g.Min(x => x.Image),
                //                    rowCount = g.Count(),
                //                    SellingPriceUSD = g.Min(x => x.SellingPriceUSD),
                //                    PrdDesc = g.Min(x => x.PrdDesc)
                //                }).ToList()
                //                .OrderBy(x => x.barCode);

                var products = (from p in context.tbl_products
                                select new
                                {
                                    ProductID = p.ProductID,
                                    PrdCategID = p.PrdCategID,
                                    barCode = p.barCode,
                                    PrdNameEng = p.PrdNameEng,
                                    Image = p.Image,
                                    SellingPriceUSD = p.SellingPriceUSD,
                                    PrdDesc = p.PrdDesc
                                }).ToList()
                            .OrderBy(x => x.barCode);
                foreach (var i in products)
                {
                    Sale p = new Sale();
                    p.prdID = i.ProductID;
                    p.PrdCategID = i.PrdCategID;
                    p.barCode = i.barCode;
                    p.PrdNameEng = i.PrdNameEng;
                    p.PrdDesc = i.PrdDesc;
                    p.Image = i.Image;
                    p.SellingPriceUSD = i.SellingPriceUSD;
                    lst.Add(p);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return lst;
        }
        public List<Sale> GetForDataTable(string customerOrderId)
        {
            // Initialization.  
            List<Sale> lst = new List<Sale>();
            try
            {
                var products = from orderdetail in context.tbl_orderdetails
                               join product in context.tbl_products on orderdetail.prdID equals product.ProductID
                               join category in context.tbl_prdcategory on product.PrdCategID equals category.PrdCategID
                               where (orderdetail.PytStatus == "Ordering" 
                               && orderdetail.userID == IUser.Id)
                               orderby orderdetail.orderDate
                               select new
                               {
                                   orderdetail.orderDetailsID,
                                   orderdetail.customerOrderID,
                                   product.ProductID,
                                   product.PrdNameEng,
                                   product.OrderComment,
                                   product.barCode,
                                   product.PrdDesc,
                                   orderdetail.unitType,
                                   product.SellingPriceUSD,
                                   orderdetail.unitPrice,
                                   orderdetail.orderQty,
                                   orderdetail.totalAmt,
                                   orderdetail.AmtDisc,
                                   orderdetail.percDisc,
                                   orderdetail.TaxNote,
                                   orderdetail.OrderDetailDescription,
                                   orderdetail.PytStatus,
                               };
                if (customerOrderId != "0")
                {
                    products = from orderdetail in context.tbl_orderdetails
                                   join product in context.tbl_products on orderdetail.prdID equals product.ProductID
                                   join category in context.tbl_prdcategory on product.PrdCategID equals category.PrdCategID
                                   where (orderdetail.customerOrderID == customerOrderId
                                   && orderdetail.userID == IUser.Id)
                                   orderby orderdetail.orderDate
                                   select new
                                   {
                                       orderdetail.orderDetailsID,
                                       orderdetail.customerOrderID,
                                       product.ProductID,
                                       product.PrdNameEng,
                                       product.OrderComment,
                                       product.barCode,
                                       product.PrdDesc,
                                       orderdetail.unitType,
                                       product.SellingPriceUSD,
                                       orderdetail.unitPrice,
                                       orderdetail.orderQty,
                                       orderdetail.totalAmt,
                                       orderdetail.AmtDisc,
                                       orderdetail.percDisc,
                                       orderdetail.TaxNote,
                                       orderdetail.OrderDetailDescription,
                                       orderdetail.PytStatus,
                                   };
                }
                foreach (var i in products.ToList())
                {
                    Sale setSale = new Sale();
                    setSale.orderDetailsID = i.orderDetailsID;
                    setSale.customerOrderID = i.customerOrderID;
                    setSale.prdID = i.ProductID;
                    setSale.PrdNameEng = i.PrdNameEng;
                    setSale.unitType = i.unitType;
                    setSale.orderQty = i.orderQty;
                    setSale.unitPrice = i.unitPrice;
                    setSale.percDisc = i.percDisc;
                    setSale.AmtDisc = i.AmtDisc;
                    setSale.totalAmt = i.totalAmt;
                    setSale.PrdDesc = i.PrdDesc;
                    lst.Add(setSale);
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
        //Sort
        public List<Sale> SortByColumnWithOrder(string order, string orderDir, List<Sale> data)
        {
            // Initialization.  
            List<Sale> lst = new List<Sale>();

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
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.orderQty).ToList()
                                                                                                 : data.OrderBy(p => p.orderQty).ToList();
                        break;
                    default:

                        // Setting.  
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.SellingPriceUSD).ToList()
                                                                                                 : data.OrderBy(p => p.SellingPriceUSD).ToList();
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
        public string CreateScanBarcode(Sale data, Product product)
        {
            try
            {
                tbl_orderdetails ordersDetail = new tbl_orderdetails();
                ordersDetail.orderDetailsID = Helper.AutoID();
                ordersDetail.customerOrderID = data.customerOrderID ?? "0";
                ordersDetail.prdID = product.ProductID;
                ordersDetail.orderDate = DateTime.Now;
                ordersDetail.unitType = product.UnitType;
                ordersDetail.orderQty = data.orderQty;
                if (data.custId == "0")
                {
                    ordersDetail.unitPrice = data.unitPrice ?? product.SellingPriceUSD;
                }
                else
                {
                    var priceByCust = context.tbl_products_Customers_Price.Where(x =>
                    x.memberID == data.custId
                    && x.ProductID == product.ProductID).FirstOrDefault();
                    ordersDetail.unitPrice = data.unitPrice ?? priceByCust.SellingPriceUSDForThisCus;
                }
                ordersDetail.totalAmt = ordersDetail.unitPrice * ordersDetail.orderQty;
                ordersDetail.PytStatus = ordersDetail.customerOrderID == "0" ? "Ordering" : "Paid";
                ordersDetail.roomtableID = "0";
                ordersDetail.ReservationID = "0";
                ordersDetail.NumPrinted = 0;
                ordersDetail.NumPrintedInv = 0;
                ordersDetail.percDisc = 0;
                ordersDetail.AmtDisc = 0;
                ordersDetail.orderTime = DateTime.Now;
                ordersDetail.VoidDate = DateTime.Now;
                ordersDetail.BuyingTotal = 0;
                ordersDetail.userID = IUser.Id;
                ordersDetail.TaxNote = product.TaxNote;
                ordersDetail.OrderDetailDescription = data.OrderDetailDescription ?? product.PrdDesc;
                ordersDetail.orderQtyReturn = 0;
                ordersDetail.totalAmtReturn = 0;
                ordersDetail.orderQtyReturnTemp = 0;
                ordersDetail.totalAmtReturnTemp = 0;
                ordersDetail.NumInCase = product.NumInOne;
                context.tbl_orderdetails.Add(ordersDetail);
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        public string UpdateScanBarcode(Sale data)
        {
            try
            {
                //get product id by barcode
                var product = context.tbl_products.FirstOrDefault(x => x.barCode == data.barCode);
                //update sale new invoice
                var ordersDetail = context.tbl_orderdetails.Where(x => x.prdID == product.ProductID
                       && x.userID == IUser.Id
                       && x.PytStatus == "Ordering")
                   .FirstOrDefault();
                //update sale old invoice
                if (data.customerOrderID != "0")
                {
                    ordersDetail = context.tbl_orderdetails.Where(x => x.prdID == product.ProductID
                            && x.userID == IUser.Id
                            && x.customerOrderID == data.customerOrderID)
                        .FirstOrDefault();
                    ordersDetail.dateReturn = DateTime.Now;
                }
                unitPrice = data.unitPrice == null ? (float)ordersDetail.unitPrice : (float)data.unitPrice;
                ordersDetail.unitPrice = unitPrice;
                ordersDetail.orderQty = ordersDetail.orderQty + data.orderQty;
                ordersDetail.totalAmt = ordersDetail.orderQty * unitPrice;
                //ordersDetail.orderTime = DateTime.Now;
                //ordersDetail.VoidDate = DateTime.Now;
                context.Entry(ordersDetail).State = EntityState.Modified;
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        public string Create(Sale data)
        {
            try
            {
                tbl_orderdetails ordersDetail = new tbl_orderdetails();
                var product = context.tbl_products.FirstOrDefault(x => x.ProductID == data.prdID);
                ordersDetail.orderDetailsID = Helper.AutoID();
                ordersDetail.customerOrderID = data.customerOrderID ?? "0";
                ordersDetail.prdID = data.prdID;
                ordersDetail.orderDate = DateTime.Now;
                ordersDetail.unitType = product.UnitType;
                ordersDetail.orderQty = data.orderQty;
                if(data.custId == "0")
                {
                    ordersDetail.unitPrice = data.unitPrice ?? product.SellingPriceUSD;
                }
                else
                {
                    float unitPrice = 0;
                    var priceByCust = context.tbl_products_Customers_Price.Where(x =>
                    x.memberID == data.custId
                    && x.ProductID == data.prdID).FirstOrDefault();
                    if(priceByCust != null)
                    {
                        unitPrice = (float)priceByCust.SellingPriceUSDForThisCus;
                    }
                    else
                    {
                        unitPrice = (float)product.SellingPriceUSD;
                    }
                    ordersDetail.unitPrice = unitPrice;
                }
                ordersDetail.totalAmt = ordersDetail.unitPrice * ordersDetail.orderQty;
                ordersDetail.PytStatus = ordersDetail.customerOrderID == "0" ? "Ordering": "Paid";
                ordersDetail.roomtableID = "0";
                ordersDetail.ReservationID = "0";
                ordersDetail.NumPrinted = 0;
                ordersDetail.NumPrintedInv = 0;
                ordersDetail.percDisc = 0;
                ordersDetail.AmtDisc = 0;
                ordersDetail.orderTime = DateTime.Now;
                ordersDetail.VoidDate = DateTime.Now;
                ordersDetail.BuyingTotal = 0;
                ordersDetail.userID = IUser.Id;
                ordersDetail.TaxNote = product.TaxNote;
                ordersDetail.OrderDetailDescription = data.OrderDetailDescription ?? product.PrdDesc;
                ordersDetail.orderQtyReturn = 0;
                ordersDetail.totalAmtReturn = 0;
                ordersDetail.orderQtyReturnTemp = 0;
                ordersDetail.totalAmtReturnTemp = 0;
                ordersDetail.NumInCase = product.NumInOne;
                context.tbl_orderdetails.Add(ordersDetail);
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        public string Update(Sale data)
        {
            try
            {
                //update sale new invoice
                var ordersDetail = context.tbl_orderdetails.Where(x => x.prdID == data.prdID 
                        && x.userID == IUser.Id 
                        && x.PytStatus == "Ordering")
                    .FirstOrDefault();
                //update sale old invoice
                if(data.customerOrderID != "0")
                {
                    ordersDetail = context.tbl_orderdetails.Where(x => x.prdID == data.prdID
                            && x.userID == IUser.Id
                            && x.customerOrderID == data.customerOrderID)
                        .FirstOrDefault();
                    ordersDetail.dateReturn = DateTime.Now;
                }
                unitPrice = data.unitPrice == null ? (float)ordersDetail.unitPrice : (float)data.unitPrice;
                ordersDetail.unitPrice = unitPrice;
                ordersDetail.orderQty = ordersDetail.orderQty + data.orderQty;
                ordersDetail.totalAmt = ordersDetail.orderQty * unitPrice;
                //ordersDetail.orderTime = DateTime.Now;
                //ordersDetail.VoidDate = DateTime.Now;
                context.Entry(ordersDetail).State = EntityState.Modified;
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        public string StoreItemSale(Sale data)
        {
            try
            {
                tbl_orderdetails ordersDetail = new tbl_orderdetails();
                var product = context.tbl_products.FirstOrDefault(x => x.ProductID == data.prdID);
                ordersDetail.orderDetailsID = Helper.AutoID();
                ordersDetail.customerOrderID = data.customerOrderID ?? "0";
                ordersDetail.prdID = data.prdID;
                ordersDetail.orderDate = DateTime.Now;
                ordersDetail.unitType = product.UnitType;
                ordersDetail.unitPrice = data.RealPriceUSD ?? 0;
                ordersDetail.orderQty = data.orderQty ?? 0;
                //if (data.custId == "0")
                //{
                //    ordersDetail.unitPrice = data.RealPriceUSD ?? product.SellingPriceUSD;
                //}
                //else
                //{
                //    float unitPrice = 0;
                //    var priceByCust = context.tbl_products_Customers_Price.Where(x =>
                //    x.memberID == data.custId
                //    && x.ProductID == data.prdID).FirstOrDefault();
                //    if (priceByCust != null)
                //    {
                //        unitPrice = (float)priceByCust.SellingPriceUSDForThisCus;
                //    }
                //    else
                //    {
                //        unitPrice = (float)data.RealPriceUSD;
                //    }
                //    ordersDetail.unitPrice = unitPrice;
                //}
                ordersDetail.PytStatus = ordersDetail.customerOrderID == "0" ? "Ordering" : "Paid";
                ordersDetail.roomtableID = "0";
                ordersDetail.ReservationID = "0";
                ordersDetail.NumPrinted = 0;
                ordersDetail.NumPrintedInv = 0;
                ordersDetail.percDisc = data.percDisc ?? 0;
                ordersDetail.AmtDisc = data.AmtDisc ?? 0;
                ordersDetail.totalAmt = data.totalAmt ?? 0;
                ordersDetail.orderTime = DateTime.Now;
                ordersDetail.VoidDate = DateTime.Now;
                ordersDetail.BuyingTotal = 0;
                ordersDetail.userID = IUser.Id;
                ordersDetail.TaxNote = product.TaxNote;
                ordersDetail.OrderDetailDescription = data.OrderDetailDescription ?? product.PrdDesc;
                ordersDetail.orderQtyReturn = 0;
                ordersDetail.totalAmtReturn = 0;
                ordersDetail.orderQtyReturnTemp = 0;
                ordersDetail.totalAmtReturnTemp = 0;
                ordersDetail.NumInCase = product.NumInOne;
                context.tbl_orderdetails.Add(ordersDetail);
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }

        public string UpdateItemSale(Sale data)
        {
            try
            {
                //update sale new invoice
                var ordersDetail = context.tbl_orderdetails.Where(x => x.prdID == data.prdID
                        && x.userID == IUser.Id
                        && x.PytStatus == "Ordering")
                    .FirstOrDefault();
                //update sale old invoice
                if (data.customerOrderID != "0")
                {
                    ordersDetail = context.tbl_orderdetails.Where(x => x.prdID == data.prdID
                            && x.userID == IUser.Id
                            && x.customerOrderID == data.customerOrderID)
                        .FirstOrDefault();
                    ordersDetail.dateReturn = DateTime.Now;
                }
                ordersDetail.unitPrice = data.RealPriceUSD ?? 0;
                ordersDetail.orderQty = data.orderQty ?? 0;
                ordersDetail.percDisc = data.percDisc ?? 0;
                ordersDetail.AmtDisc = data.AmtDisc ?? 0;
                ordersDetail.totalAmt = data.totalAmt ?? 0;
                //ordersDetail.orderTime = DateTime.Now;
                //ordersDetail.VoidDate = DateTime.Now;
                context.Entry(ordersDetail).State = EntityState.Modified;
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }

        public string UpdateOrder(Sale data)
        {
            try
            {
                var ordersDetail = context.tbl_orderdetails.FirstOrDefault(x => x.prdID == data.prdID && x.userID == IUser.Id && x.PytStatus == "Ordering");
                if (data.Type == "orderQty")
                {
                    ordersDetail.orderQty = data.Val;
                    ordersDetail.totalAmt = (ordersDetail.unitPrice - ordersDetail.AmtDisc) * ordersDetail.orderQty;
                }
                else if (data.Type == "unitPrice")
                {
                    ordersDetail.unitPrice = data.Val;
                    ordersDetail.totalAmt = (ordersDetail.unitPrice - ordersDetail.AmtDisc) * ordersDetail.orderQty;
                }
                else if (data.Type == "AmtDisc")
                {
                    ordersDetail.AmtDisc = data.Val;
                    percentDiscount = ((float)ordersDetail.AmtDisc / (float)ordersDetail.unitPrice) * 100;
                    ordersDetail.percDisc = Math.Round(percentDiscount);
                    ordersDetail.totalAmt = (ordersDetail.unitPrice - ordersDetail.AmtDisc) * ordersDetail.orderQty;
                }
                else
                {
                    ordersDetail = context.tbl_orderdetails.FirstOrDefault(x => x.orderDetailsID == data.orderDetailsID);
                    if (ordersDetail.orderQtyReturnTemp > 0)
                    {
                        ordersDetail.orderQtyReturn = ordersDetail.orderQtyReturn - ordersDetail.orderQtyReturnTemp;
                        ordersDetail.orderQtyReturnTemp = 0;
                    }
                    ordersDetail.orderQtyReturnTemp = data.Val;
                    ordersDetail.totalAmtReturnTemp = (ordersDetail.unitPrice - ordersDetail.AmtDisc) * ordersDetail.orderQtyReturnTemp;

                    ordersDetail.orderQtyReturn = ordersDetail.orderQtyReturn + ordersDetail.orderQtyReturnTemp;
                    ordersDetail.totalAmtReturn = (ordersDetail.unitPrice - ordersDetail.AmtDisc) * ordersDetail.orderQtyReturn;
                    if(ordersDetail.orderQtyReturnTemp == 0 && ordersDetail.orderQtyReturn > 0)
                    {
                        ordersDetail.statusReturn = "Returned";
                    }
                    else if(ordersDetail.orderQtyReturnTemp == 0)
                    {
                        ordersDetail.statusReturn = null;
                    }
                    else
                    {
                        ordersDetail.statusReturn = "Returning";
                    }

                        ordersDetail.dateReturn = DateTime.Now;
                }
                context.Entry(ordersDetail).State = EntityState.Modified;
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        public string CreateHold(Sale data)
        {
            try
            {
                var ordersDetails = context.tbl_orderdetails.Where(x => x.userID == IUser.Id && x.PytStatus == "Ordering").ToList();
                if(ordersDetails.Count == 0)
                {
                    return "No data available!";
                }
                tblOrderDetailsTemp setHold = new tblOrderDetailsTemp();
                setHold.roomtableID = Helper.AutoID();
                setHold.CustomerName = data.custName;
                setHold.Date = DateTime.Now;
                context.tblOrderDetailsTemps.Add(setHold);
                context.SaveChanges();
                ordersDetails.ForEach(x =>
                {
                    x.roomtableID = setHold.roomtableID;
                    x.PytStatus = "Hold";
                    context.Entry(x).State = EntityState.Modified;
                    context.SaveChanges();
                });
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        public string OpenHoldToOrdering(Sale data)
        {
            try
            {
                var ordersDetails = context.tbl_orderdetails.Where(x => x.roomtableID == data.roomtableID).ToList();
                var dataHold = context.tblOrderDetailsTemps.Where(x => x.roomtableID == data.roomtableID).FirstOrDefault();
                context.tblOrderDetailsTemps.Remove(dataHold);
                context.SaveChanges();
                ordersDetails.ForEach(x =>
                {
                    x.roomtableID = "0";
                    x.PytStatus = "Ordering";
                    context.Entry(x).State = EntityState.Modified;
                    context.SaveChanges();
                });
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        //create customer payment
        public (string status, string id) CreatePayment(Sale data)
        {
            try
            {
                tbl_customerorder customerOrder = new tbl_customerorder();
                customerOrderID = Helper.AutoID();
                customerOrder.customerOrderID = customerOrderID;
                customerOrder.customerID = data.custId;
                customerOrder.orderDate = data.orderDate == null ? DateTime.Now : data.orderDate;
                customerOrder.Time = DateTime.Now;
                customerOrder.totalAmount = data.totalAmount ?? 0;
                customerOrder.amtDiscount = data.amtDiscount ?? 0;
                //percentDiscount = ((float)customerOrder.amtDiscount / (float)customerOrder.totalAmount) * 100;
                //customerOrder.percentDiscount = Math.Round(percentDiscount);
                customerOrder.percentDiscount = data.percentDiscount ?? 0;
                customerOrder.amtOrderExpend = 0;
                customerOrder.netAmount = data.netAmount ?? 0;
                customerOrder.voidAmt = 0;
                customerOrder.grandTotal = customerOrder.netAmount;
                customerOrder.amtReceived = data.amtGTotalReceived ?? 0;
                customerOrder.amtReturn = data.amtReturn ?? 0;
                customerOrder.amtOwed = data.amtOwed ?? 0;
                customerOrder.userID = IUser.Id;
                customerOrder.usdTokhr = data.usdTokhr ?? Helper.ExchangeRate(0, "usdToKhr", "defaultRate");
                customerOrder.usdToTHB = data.usdToTHB ?? Helper.ExchangeRate(0, "usdToThb", "defaultRate");
                customerOrder.RandTID = "0";
                customerOrder.StartTime = DateTime.Now;
                customerOrder.InvoiceNo = this.InvoiceNo(data.IsType);
                customerOrder.voidStatus = "";
                customerOrder.openingBalanceID = "0";
                customerOrder.ReturnDate = data.ReturnDate == null ? DateTime.Now : data.ReturnDate;
                customerOrder.usdTokhrChange = data.usdToKhrChange ?? Helper.ExchangeRate(0, "usdToKhrChange", "defaultRate");
                customerOrder.OwnerReceive = false;
                customerOrder.SaleID = "0";
                customerOrder.TotalOwed = 0;
                customerOrder.DescripitonTerm = data.DescripitonTerm;
                customerOrder.Note = "";
                customerOrder.ShipDate = DateTime.Now;
                customerOrder.FOBPOINT = "";
                customerOrder.ID_Ship = "0";
                customerOrder.netAmountReturn = 0;
                context.tbl_customerorder.Add(customerOrder);
                var ordersDetail = context.tbl_orderdetails.Where(x => x.userID == IUser.Id && x.PytStatus == "Ordering").ToList();
                ordersDetail.ForEach(x =>
                {
                    x.customerOrderID = customerOrder.customerOrderID;
                    x.PytStatus = "Paid";
                    context.Entry(x).State = EntityState.Modified;
                    context.SaveChanges();
                });
                //var ordersDetail = (from r in context.tbl_orderdetails
                //                    join p in context.tbl_products on r.prdID equals p.ProductID
                //                    where r.userID == IUser.Id
                //                    && r.PytStatus == "Ordering"
                //                    select new
                //                    {
                //                        p.ProductIDRealStock,
                //                        r.NumInCase,
                //                        r.orderQty,
                //                        r.orderDetailsID,
                //                        r.PytStatus,
                //                        r.customerOrderID
                //                    }).ToList();
                //int i = 0;
                //tbl_mainstock mainStock = new tbl_mainstock();
                //tblStockTypeProduct stockProduct = new tblStockTypeProduct();
                //foreach (var row in ordersDetail)
                //{
                //    i++;
                //    mainStock.ProductID = row.ProductIDRealStock;
                //    //orderQty = Math.Round(row.orderQty / row.NumInOne, 8);
                //    mainStock.Quantity = row.orderQty / row.NumInCase;
                //    //Math.Round((double)(row.orderQty / row.NumInOne),4, MidpointRounding.AwayFromZero);
                //    mainStockRepository.SubtractMainStock(mainStock, row.orderDetailsID, customerOrder.customerOrderID);

                //    //Update Subtract table Stock_Products
                //    stockProduct.ProductID = row.ProductIDRealStock;
                //    stockProduct.Qty = mainStock.Quantity;
                //    stockProductRepository.SubtractProduct(stockProduct);
                //}
                //this.Save();
            }
            catch (Exception ex)
            {
                return (ex.ToString(), customerOrderID);
            }
            return ("True", customerOrderID);
        }
        //public (string status, string id) CreateOrUpdatePayment(Sale data)
        //{
        //    try
        //    {
        //        tbl_customerorder customerOrder = new tbl_customerorder();
        //        customerOrderID = Helper.AutoID();
        //        customerOrder.customerOrderID = customerOrderID;
        //        customerOrder.customerID = data.customerOrderID;
        //        customerOrder.orderDate = data.orderDate == null ? DateTime.Now : data.orderDate;
        //        customerOrder.Time = DateTime.Now;
        //        customerOrder.totalAmount = data.totalAmount == null ? 0 : data.totalAmount;
        //        customerOrder.amtDiscount = data.AmtDisc == null ? 0 : data.AmtDisc;
        //        percentDiscount = ((float)customerOrder.amtDiscount / (float)customerOrder.totalAmount) * 100;
        //        customerOrder.percentDiscount = Math.Round(percentDiscount);
        //        customerOrder.amtOrderExpend = 0;
        //        customerOrder.netAmount = data.netAmount == null ? 0 : data.netAmount;
        //        customerOrder.voidAmt = 0;
        //        customerOrder.grandTotal = customerOrder.netAmount;
        //        customerOrder.amtReceived = data.amtReceived == null ? 0 : data.amtReceived;
        //        customerOrder.amtReturn = data.amtReturn == null ? 0 : data.amtReturn;
        //        customerOrder.amtOwed = data.amtOwed == null ? 0 : data.amtOwed;
        //        customerOrder.userID = IUser.Id;
        //        customerOrder.usdTokhr = data.usdTokhr == null ? Helper.ExchangeRate(0, "usdToKhr", "defaultRate") : data.usdTokhr;
        //        customerOrder.usdToTHB = data.usdToTHB == null ? Helper.ExchangeRate(0, "usdToThb", "defaultRate") : data.usdToTHB;
        //        customerOrder.RandTID = "0";
        //        customerOrder.StartTime = DateTime.Now;
        //        customerOrder.InvoiceNo = this.InvoiceNo(data.IsType);
        //        customerOrder.voidStatus = "";
        //        customerOrder.openingBalanceID = "0";
        //        customerOrder.ReturnDate = data.ReturnDate == null ? DateTime.Now : data.ReturnDate;
        //        customerOrder.usdTokhrChange = data.usdToKhrChange == null ? Helper.ExchangeRate(0, "usdToKhrChange", "defaultRate") : data.usdToKhrChange;
        //        customerOrder.OwnerReceive = false;
        //        customerOrder.SaleID = "0";
        //        customerOrder.TotalOwed = 0;/////total owed old
        //        customerOrder.DescripitonTerm = data.DescripitonTerm;
        //        customerOrder.Note = "";
        //        customerOrder.ShipDate = DateTime.Now;
        //        customerOrder.FOBPOINT = "";
        //        customerOrder.ID_Ship = "0";
        //        customerOrder.netAmountReturn = 0;
        //        if (data.InvoiceID == "0")
        //        {
        //            context.tbl_customerorder.Add(customerOrder);
        //        }
        //        else
        //        {
        //            customerOrder.customerOrderID = data.customerOrderID;
        //            customerOrderID = customerOrder.customerOrderID;
        //            this.UpdatePayment(data);
        //        }

        //        //var ordersDetails = context.tbl_orderdetails.Where(x => x.customerOrderID == data.customerOrderID && x.userID == IUser.Id && x.PytStatus == "Ordering").ToList();
        //        var ordersDetails = from r in context.tbl_orderdetails
        //                            join p in context.tbl_products on r.prdID equals p.ProductID
        //                            //where r.customerOrderID == data.customerOrderID 
        //                            //&& r.userID == IUser.Id
        //                            //&& r.PytStatus == "Ordering"
        //                            where r.userID == IUser.Id
        //                            && r.PytStatus == "Ordering"
        //                            select new
        //                       {
        //                           p.ProductIDRealStock,
        //                           r.NumInCase,
        //                           r.orderQty,
        //                           r.orderDetailsID,
        //                       };
        //        int i = 0;
        //        tbl_mainstock mainStock = new tbl_mainstock();
        //        tblStockTypeProduct stockProduct = new tblStockTypeProduct();
        //        foreach (var row in ordersDetails)
        //        {
        //            i++;
        //            mainStock.ProductID = row.ProductIDRealStock;
        //            //orderQty = Math.Round(row.orderQty / row.NumInOne, 8);
        //            mainStock.Quantity = row.orderQty / row.NumInCase;
        //            //Math.Round((double)(row.orderQty / row.NumInOne),4, MidpointRounding.AwayFromZero);
        //            mainStockRepository.SubtractMainStock(mainStock, row.orderDetailsID, customerOrder.customerOrderID);

        //            /////////Update Subtract table Stock_Products
        //            stockProduct.ProductID = row.ProductIDRealStock;
        //            stockProduct.Qty = mainStock.Quantity;
        //            stockProductRepository.SubtractProduct(stockProduct);
        //        }
        //        this.Save();
        //    }
        //    catch (Exception ex)
        //    {
        //        return (ex.ToString(), customerOrderID);
        //    }
        //    return ("True", customerOrderID);
        //}

        //update customer payment
        public (string status, string id) UpdatePayment(Sale data)
        {
            try
            {
                var customerOrder = context.tbl_customerorder.FirstOrDefault(x => x.customerOrderID == data.customerOrderID);
                customerOrder.customerID = data.custId;
                customerOrder.orderDate = data.orderDate == null ? DateTime.Now : data.orderDate;
                customerOrder.Time = DateTime.Now;
                customerOrder.totalAmount = data.totalAmount ?? 0;
                customerOrder.amtDiscount = data.amtDiscount ?? 0;
                customerOrder.percentDiscount = data.percentDiscount ?? 0;
                customerOrder.amtOrderExpend = 0;
                customerOrder.netAmount = data.netAmount ?? 0;
                customerOrder.voidAmt = 0;
                customerOrder.grandTotal = customerOrder.netAmount;
                customerOrder.amtReceived = data.amtGTotalReceived ?? 0;
                customerOrder.amtReturn = data.amtReturn ?? 0;
                customerOrder.amtOwed = data.amtOwed ?? 0;
                customerOrder.userID = IUser.Id;
                customerOrder.usdTokhr = data.usdTokhr ?? Helper.ExchangeRate(0, "usdToKhr", "defaultRate");
                customerOrder.usdToTHB = data.usdToTHB ?? Helper.ExchangeRate(0, "usdToThb", "defaultRate");
                customerOrder.RandTID = "0";
                customerOrder.StartTime = DateTime.Now;
                customerOrder.voidStatus = "";
                customerOrder.openingBalanceID = "0";
                customerOrder.ReturnDate = data.ReturnDate == null ? DateTime.Now : data.ReturnDate;
                customerOrder.usdTokhrChange = data.usdToKhrChange ?? Helper.ExchangeRate(0, "usdToKhrChange", "defaultRate");
                customerOrder.OwnerReceive = false;
                customerOrder.SaleID = "0";
                customerOrder.TotalOwed = 0;
                customerOrder.DescripitonTerm = data.DescripitonTerm;
                customerOrder.Note = "";
                customerOrder.ShipDate = DateTime.Now;
                customerOrder.FOBPOINT = "";
                customerOrder.ID_Ship = "0";
                customerOrder.netAmountReturn = 0;
                context.Entry(customerOrder).State = EntityState.Modified;
                this.Save();
            }
            catch (Exception ex)
            {
                return (ex.ToString(), data.customerOrderID);
            }
            return ("True", data.customerOrderID);
        }
        //Block Return Sale
        public List<Sale> GetDataReturnForDataTable(string customerOrderID)
        {
            // Initialization.  
            List<Sale> lst = new List<Sale>();
            try
            {
                var _product = from orderdetail in context.tbl_orderdetails
                               join product in context.tbl_products on orderdetail.prdID equals product.ProductID
                               join category in context.tbl_prdcategory on product.PrdCategID equals category.PrdCategID
                               where (orderdetail.customerOrderID == customerOrderID && orderdetail.customerOrderID != "0" && orderdetail.userID == IUser.Id)
                               orderby orderdetail.orderDate
                               select new
                               {
                                   orderdetail.orderDetailsID,
                                   orderdetail.customerOrderID,
                                   product.ProductID,
                                   product.PrdNameEng,
                                   product.OrderComment,
                                   product.barCode,
                                   orderdetail.unitType,
                                   product.SellingPriceUSD,
                                   orderdetail.unitPrice,
                                   orderdetail.orderQty,
                                   orderdetail.totalAmt,
                                   orderdetail.AmtDisc,
                                   orderdetail.percDisc,
                                   orderdetail.TaxNote,
                                   orderdetail.OrderDetailDescription,
                                   orderdetail.PytStatus,
                                   orderdetail.statusReturn,
                                   orderdetail.orderQtyReturn,
                                   orderdetail.totalAmtReturn,
                                   orderdetail.orderQtyReturnTemp,
                                   orderdetail.totalAmtReturnTemp,
                               };
                foreach (var i in _product.ToList())
                {
                    Sale infoObj = new Sale();
                    infoObj.orderDetailsID = i.orderDetailsID;
                    infoObj.customerOrderID = i.customerOrderID;
                    infoObj.prdID = i.ProductID;
                    infoObj.PrdNameEng = i.PrdNameEng;
                    infoObj.unitType = i.unitType;
                    infoObj.orderQty = i.orderQty;
                    infoObj.unitPrice = i.unitPrice;
                    infoObj.percDisc = i.percDisc;
                    infoObj.AmtDisc = i.AmtDisc;
                    infoObj.totalAmt = i.totalAmt;
                    infoObj.orderQtyReturn = i.orderQtyReturn;
                    infoObj.totalAmtReturn = i.totalAmtReturn;
                    infoObj.orderQtyReturnTemp = i.orderQtyReturnTemp;
                    infoObj.totalAmtReturnTemp = i.totalAmtReturnTemp;
                    infoObj.statusReturn = i.statusReturn;
                    infoObj.PytStatus = i.PytStatus;
                    lst.Add(infoObj);
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
        public (string status, string id) PayReturnToCustomer(Sale data)
        {
            try
            {
                this.UpdatePayment(data);
                //var _orders_detail = context.tbl_orderdetails.Where(x => x.customerOrderID == data.InvoiceID && x.userID == IUser.Id).ToList();
                var ordersDetails = from r in context.tbl_orderdetails
                                    join p in context.tbl_products on r.prdID equals p.ProductID
                                    where r.customerOrderID == data.InvoiceID
                                    && r.userID == IUser.Id
                                    select new
                                    {
                                        p.ProductIDRealStock,
                                        r.NumInCase,
                                        r.orderDetailsID,
                                        r.orderQtyReturnTemp
                                    };
                int i = 0;
                tbl_mainstock mainStock = new tbl_mainstock();
                tblStockTypeProduct stockProduct = new tblStockTypeProduct();
                foreach (var row in ordersDetails)
                {
                    i++;
                    mainStock.ProductID = row.ProductIDRealStock;
                    mainStock.Quantity = row.orderQtyReturnTemp/row.NumInCase;
                    mainStockRepository.PlusReturnToMainStock(mainStock);

                    //////Call update to table Stock_Products
                    stockProduct.StockID = IUser.Id;
                    stockProduct.ProductID = mainStock.ProductID;
                    stockProduct.Qty = mainStock.Quantity;
                    stockProduct.TransferDate = DateTime.Now;
                    stockProductRepository.CreateOrUpdate(stockProduct);
                    //this.UpdateStatusReturn(row.orderDetailsID);
                }
            }
            catch (Exception ex)
            {
                return (ex.ToString(), data.InvoiceID);
            }
            return ("True", data.InvoiceID);
        }
        public string UpdateStatusReturn(string orderDetailsID)
        {
            try
            {
                var ordersDetail = context.tbl_orderdetails.Where(x => x.orderDetailsID == orderDetailsID && x.userID == IUser.Id).FirstOrDefault();
                ordersDetail.statusReturn = "Returned";
                ordersDetail.orderQtyReturnTemp = 0;
                ordersDetail.totalAmtReturnTemp = 0;
                context.Entry(ordersDetail).State = EntityState.Modified;
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "True";
        }
        //End Return Sale
        //Invoice Number
        public string InvoiceNo(string IsType)
        {
            //string IsMonth = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            //var resutl = context.Database.SqlQuery<string>("select dbo.SF_Get_InvoiceNo_by_Type('" + IsMonth + "," + IsType + "')").Single();
            //return resutl;
            string InvoiceNo;
            using (SqlConnection conn = new SqlConnection(dBConnect.Getconnectionstring("SqlConnection")))
            {
                SqlCommand command = new SqlCommand("Pro_Get_InvoiceNo_by_Type", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Type", SqlDbType.VarChar).Value = IsType;
                try
                {
                    conn.Open();
                    InvoiceNo = (string)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                conn.Close();
            }
            return InvoiceNo;
        }
        //Delete Order
        public string Delete(Sale data)
        {
            try
            {
                tbl_orderdetails _orders_detail = context.tbl_orderdetails.Where(x => x.orderDetailsID == data.orderDetailsID && x.userID == IUser.Id).FirstOrDefault();
                context.tbl_orderdetails.Remove(_orders_detail);
                this.Save();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "True";
        }
        public string Remove(string orderDetailsID)
        {
            try
            {
                tbl_orderdetails _orders_detail = context.tbl_orderdetails.Where(x => x.orderDetailsID == orderDetailsID && x.userID == IUser.Id).FirstOrDefault();
                context.tbl_orderdetails.Remove(_orders_detail);
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
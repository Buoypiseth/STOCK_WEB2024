using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Repositories;
using WebApplication.Helpers;
using System.Data.Entity;
using System.Xml.Linq;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.Data;

namespace WebApplication.Controllers
{
    [Authorize]
    public class SaleController : Controller
    {
        // GET: Sale
        private DataContext context;
        private DBConnect dBConnect;
        private AutoPrint autoPrint;
        private SaleRepository saleRepository;
        private AppSettingRepository appSettingRepository;
        string rptName;
        string response;
        int rowCount;
        public SaleController()
        {
            this.context = new DataContext();
            this.dBConnect = new DBConnect();
            this.autoPrint = new AutoPrint();
            this.saleRepository = new SaleRepository();
            this.appSettingRepository = new AppSettingRepository();
        }
        //GET:GetData
        public ActionResult GetData(BaseDatatable param)
        {
            string customerOrderId = Request["customerOrderId"];
            var data = saleRepository.GetForDataTable(customerOrderId);
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => x.PrdNameEng.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }
            //code sort column
            var sortColumnIndex = HttpContext.Request.QueryString["iSortCol_0"];
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            data = saleRepository.SortByColumnWithOrder(sortColumnIndex, sortDirection, data);

            var displayResult = data.Skip(param.iDisplayStart)
                                .Take(param.iDisplayLength).ToList();
            var totalRecords = data.Count();
            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            }, JsonRequestBehavior.AllowGet);
        }
        //GET:Index
        public ActionResult Index(string slug = "")
        {
            this.GetOptionInvoice();
            this.GetAccountTypes();
            ViewBag.Categories = context.tbl_prdcategory
                           .OrderBy(x => x.CategoryOrder)
                           .ToList();
            var getForSale = saleRepository.GetForSale();
            ViewBag.Products = getForSale;
            ViewBag.prdCount = getForSale.Count();
            var customer = context.tbl_customers.FirstOrDefault(x => x.memberID == "0");
            ViewBag.IsReturn = false;
            ViewBag.customerOrderId = "0";
            ViewBag.invoiceNo = "";
            ViewBag.custId = customer.memberID;
            ViewBag.custName = customer.fullName;
            //Sale setSale = new Sale();
            //setSale.amtDiscount = 0;
            //setSale.percentDiscount = 0;
            //setSale.amtGTotalReceived = 0;
            //setSale.amtReceived = 0;

            if (!string.IsNullOrEmpty(slug))
            {
                var customerOrdered = context.tbl_customerorder.FirstOrDefault(x => x.customerOrderID == slug);
                customer = context.tbl_customers.FirstOrDefault(x => x.memberID == customerOrdered.customerID);
                ViewBag.IsReturn = true;
                ViewBag.customerOrderId = customerOrdered.customerOrderID;
                ViewBag.invoiceNo = customerOrdered.InvoiceNo;
                ViewBag.custId = customer.memberID;
                ViewBag.custName = customer.fullName;

                //setSale.amtDiscount = customerOrdered.amtDiscount;
                //setSale.percentDiscount = customerOrdered.percentDiscount;
                //setSale.amtGTotalReceived = customerOrdered.amtReceived - customerOrdered.amtReturn;
                //setSale.amtReceived = customerOrdered.amtReceived - customerOrdered.amtReturn;
            }
            return View();
        }
        //GET:Details
        public ActionResult Details(string slug = "")
        {
            ViewBag.Categories = context.tbl_prdcategory
                    .OrderBy(x => x.CategoryOrder)
                    .ToList();
            ViewBag.IsReturn = false;

            ViewBag.Products = context.tbl_products.Where(x => x.ProductID == slug).ToList();
            return View();
        }
        [Route("Sale/GetSearch")]
        [HttpGet]
        public ActionResult GetSearch(string data)
        {
            var getForSale = saleRepository.GetForSale()
                .Where(x => x.PrdNameEng.ToLower().Contains(data.ToLower())
                || x.barCode.ToLower().Contains(data.ToLower()))
            .OrderBy(x => x.PrdCategID)
            .OrderBy(x => x.PrdNameEng);
            ViewBag.Products = getForSale;
            ViewBag.prdCount = getForSale.Count();

            if (String.IsNullOrEmpty(data))
            {
                getForSale = saleRepository.GetForSale()
                .OrderBy(x => x.PrdCategID)
                .OrderBy(x => x.PrdNameEng);
                ViewBag.Products = getForSale;
                ViewBag.prdCount = getForSale.Count();
            }

            return PartialView("Partials/_Product");
        }
        [Route("Sale/GetProduct")]
        [HttpGet]
        public ActionResult GetProduct(string id)
        {
            var getForSale = saleRepository.GetForSale()
                                            .OrderBy(x => x.PrdCategID)
                                            .OrderBy(x => x.PrdNameEng);

            ViewBag.Products = getForSale;
            ViewBag.prdCount = getForSale.Count();
            if (id != "All")
            {
                getForSale = saleRepository.GetForSale()
                    .Where(x => x.PrdCategID == id)
                    .OrderBy(x => x.PrdCategID)
                    .OrderBy(x => x.PrdNameEng);
                ViewBag.Products = getForSale;
                ViewBag.prdCount = getForSale.Count();
            }
            return PartialView("Partials/_Product");
        }
        public void GetOptionInvoice()
        {
            List<SelectListItem> listProduct = new List<SelectListItem>();
            ViewBag.Products = listProduct;
            ViewBag.PaperSizes = new SelectList(appSettingRepository.GetAppSetting("PaperSize"), "Name", "Name");
            ViewBag.InvoiceTypes = new SelectList(appSettingRepository.GetAppSetting("InvoiceType"), "Name", "Name");
        }
        public void GetAccountTypes()
        {
            var accountTypes = context.tbl_customers_account.Where(x => x.CustomerAccountID == "0").ToList();
            ViewBag.AccountTypes = new SelectList(accountTypes, "CustomerAccountID", "AccountType");
        }
        // POST: Sale/SalePackage
        [Route("Sale/SalePackage")]
        [HttpPost]
        public JsonResult SalePackage(Sale data)
        {
            long ProductPackageId = long.Parse(data.prdID);
            var packages = context.tbl_products_Package_Detail.Where(x => x.Product_PackageID == ProductPackageId).ToList();
            if (packages.Count == 0)
            {
                return Json(new { status = response, message = "The package haven't products." }, JsonRequestBehavior.AllowGet);
            }
            Sale objSale = new Sale();
            foreach (var i in packages)
            {
                objSale.prdID = i.ProductID;
                objSale.orderQty = (data.orderQty * i.Qty);
                //objSale.SellingPriceUSD = i.SellingPriceUSD;
                //objSale.OrderDetailDescription = i.Description;
                objSale.status = data.status;
                if (appSettingRepository.IsSaleNoEnoughStock(objSale))
                {
                    return Json(new { status = "No_Enough", message = Resources.Alerts.Is_Sale_No_Enough_Stock }, JsonRequestBehavior.AllowGet);
                }
            }
            foreach (var i in packages)
            {
                rowCount++;
                objSale.orderDetailsID = rowCount.ToString();
                objSale.customerOrderID = data.customerOrderID;
                objSale.prdID = i.ProductID;
                objSale.orderQty = (data.orderQty * i.Qty);
                objSale.unitPrice = i.SellingPriceUSD;
                objSale.status = data.status;
                objSale.OrderDetailDescription = i.Description;
                var ordersDetail = context.tbl_orderdetails.Where(x => x.userID == IUser.Id && x.PytStatus == "Ordering").ToList();
                if (ordersDetail.Where(x => x.prdID == objSale.prdID).ToList().Count > 0)
                {
                    response = saleRepository.Update(objSale);
                }
                //else if (ordersDetail.Where(x => x.customerOrderID != data.customerOrderID).ToList().Count > 0)
                //{
                //    return Json(new { status = "Warning", message = Resources.Alerts.Sale_Warning_Order }, JsonRequestBehavior.AllowGet);
                //}
                else
                {
                    response = saleRepository.Create(objSale);
                }
            }
            return Json(new { status = response, message = Resources.Alerts.Sale_Successfully_Order }, JsonRequestBehavior.AllowGet);
        }
        // POST: Sale/AddOrder
        [Route("Sale/AddOrder")]
        [HttpPost]
        public JsonResult AddOrder(Sale data)
        {
            if (appSettingRepository.IsSaleNoEnoughStock(data))
            {
                return Json(new { status = "No_Enough", message = Resources.Alerts.Is_Sale_No_Enough_Stock }, JsonRequestBehavior.AllowGet);
            }
            //update sale new invoice
            bool ordersDetail = context.tbl_orderdetails.Any(x => x.prdID == data.prdID
            && x.userID == IUser.Id 
            && x.PytStatus == "Ordering");
            //update sale old invoice
            if (data.customerOrderID != "0")
            {
                ordersDetail = context.tbl_orderdetails.Any(x => x.prdID == data.prdID 
                && x.userID == IUser.Id 
                && x.customerOrderID == data.customerOrderID);
            }
            if (ordersDetail)
            {
                return Json(new { status = saleRepository.Update(data), message = Resources.Alerts.Sale_Successfully_Order }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = saleRepository.Create(data), message = Resources.Alerts.Sale_Successfully_Order }, JsonRequestBehavior.AllowGet);
        }
        // POST: Sale/ScanBarcode
        [Route("Sale/ScanBarcode")]
        [HttpPost]
        public JsonResult ScanBarcode(Sale data)
        {
            var product = context.tbl_products.FirstOrDefault(x => x.barCode == data.barCode);
            if (product == null)
            {
                return Json(new { status = "Warning", message = "The product barcode isn't available!" }, JsonRequestBehavior.AllowGet);
            }
            Sale objSale = new Sale();
            objSale.prdID = product.ProductID;
            objSale.orderQty = data.orderQty;
            objSale.status = data.status;
            if (appSettingRepository.IsSaleNoEnoughStock(objSale))
            {
                return Json(new { status = "No_Enough", message = Resources.Alerts.Is_Sale_No_Enough_Stock }, JsonRequestBehavior.AllowGet);
            }
            //update sale new invoice
            bool ordersDetail = context.tbl_orderdetails.Any(x => x.prdID == product.ProductID
               && x.userID == IUser.Id
               && x.PytStatus == "Ordering");
            //update sale old invoice
            if (data.customerOrderID != "0")
            {
                ordersDetail = context.tbl_orderdetails.Any(x => x.prdID == product.ProductID
                && x.userID == IUser.Id
                && x.customerOrderID == data.customerOrderID);
            }
            if (ordersDetail)
            {
                return Json(new { status = saleRepository.UpdateScanBarcode(data), message = "Scan update order by product barcode." }, JsonRequestBehavior.AllowGet);
            }
            Product setProduct = new Product();
            setProduct.ProductID = product.ProductID;
            setProduct.UnitType = product.UnitType;
            setProduct.SellingPriceUSD = product.SellingPriceUSD;
            setProduct.TaxNote = product.TaxNote;
            setProduct.PrdDesc = product.PrdDesc;
            setProduct.NumInOne = product.NumInOne;
            return Json(new { status = saleRepository.CreateScanBarcode(data, setProduct), message = "Scan order by product barcode." }, JsonRequestBehavior.AllowGet);
        }
        [Route("Sale/GetHoldOrder")]
        [HttpGet]
        public ActionResult GetHoldOrder(string id)
        {
            var getCustHold = context.tblOrderDetailsTemps.ToList();
            ViewBag.GetCustHold = getCustHold;
            ViewBag.holdCount = getCustHold.Count();
            return PartialView("Partials/_HoldOrder");
        }
        // POST: Sale/CreateHold
        [Route("Sale/CreateHold")]
        [HttpPost]
        public JsonResult CreateHold(Sale data)
        {
            return Json(saleRepository.CreateHold(data), JsonRequestBehavior.AllowGet);
        }
        // POST: Sale/ShowHold
        [Route("Sale/ShowHold")]
        [HttpPost]
        public JsonResult OpenHoldToOrdering(Sale data)
        {
            return Json(saleRepository.OpenHoldToOrdering(data), JsonRequestBehavior.AllowGet);
        }
        //// POST: Sale/GetListHold
        //[Route("Sale/GetListHold")]
        //[HttpGet]
        //public ActionResult GetListHold()
        //{
        //    var getCustHold = context.tblOrderDetailsTemps.ToList();
        //    ViewBag.GetCustHold = getCustHold;
        //    ViewBag.holdCount = getCustHold.Count();
        //    return PartialView("Partials/_ListHold");
        //}
        // GET: Sale/Payment
        [Route("Sale/GetPayment")]
        [HttpPost]
        public JsonResult GetPayment(Sale data)
        {
            var ordersDetail = context.tbl_orderdetails.Where(x => x.userID == IUser.Id && x.PytStatus == "Ordering").ToList();
            if (data.customerOrderID != "0")
            {
                ordersDetail = context.tbl_orderdetails.Where(x => x.customerOrderID == data.customerOrderID).ToList();
            }
            Sale setPayment = new Sale();
            var usdToKhr = Helper.ExchangeRate(0, "usdToKhr", "defaultRate");
            var usdToThb = Helper.ExchangeRate(0, "usdToThb", "defaultRate");
            var usdToKhrChange = Helper.ExchangeRate(0, "usdToKhrChange", "defaultRate");
            setPayment.usdTokhr = usdToKhr;
            setPayment.usdToTHB = usdToThb;
            setPayment.usdToKhrChange = usdToKhrChange;

            setPayment.totalAmount = ordersDetail.Sum(x => x.totalAmt);
            setPayment.totalAmountRiel = setPayment.totalAmount * usdToKhr;
            setPayment.netAmount = setPayment.totalAmount;
            setPayment.netAmountRiel = setPayment.totalAmountRiel;
            setPayment.amtOwed = setPayment.totalAmount;
            setPayment.amtOwedRiel = setPayment.totalAmountRiel;
            return Json(setPayment, JsonRequestBehavior.AllowGet);
        }
        // POST: Sale/Payment
        [Route("Sale/Payment")]
        [HttpPost]
        public JsonResult Payment(Sale data)
        {
            //bool ordersDetail = context.tbl_orderdetails.Any(x => x.userID == IUser.Id && x.PytStatus == "Ordering");
            bool IsOrdering = true;
            if (data.customerOrderID != "0")
            {
                IsOrdering = context.tbl_orderdetails.Any(x => x.customerOrderID == data.customerOrderID);
            }
            if (!IsOrdering)
            {
                return Json(new { status = "No data available!", id = "" }, JsonRequestBehavior.AllowGet);
            }
            if (data.amtOwed > 0 && data.custId == "0")
            {
                return Json(new { status = "Please select customer owed.", id = "" }, JsonRequestBehavior.AllowGet);
            }
            //#save data to xml.
            this.XmlReport(data);
            //#save payment.
            if(data.customerOrderID == "0")
            {
                var resCreate = saleRepository.CreatePayment(data);
                //call auto print Store View_Receipt
                if (data.IsPrint && data.PaperSize == "Small")
                {
                    this.GetAutoPrint(resCreate.id);
                    return Json(new { resCreate.status, resCreate.id }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { status = "Preview", resCreate.id }, JsonRequestBehavior.AllowGet);
            }

            //#update payment.
            var resUpdate = saleRepository.UpdatePayment(data);
            //call auto print Store View_Receipt
            if (data.IsPrint && data.PaperSize == "Small")
            {
                this.GetAutoPrint(resUpdate.id);
                return Json(new { resUpdate.status, resUpdate.id }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = "Preview", resUpdate.id }, JsonRequestBehavior.AllowGet);
        }
        // POST: Sale/Delete/5
        [Route("Sale/Delete")]
        [HttpPost]
        public JsonResult Delete(Sale data)
        {
            return Json(saleRepository.Delete(data), JsonRequestBehavior.AllowGet);
        }
        public void XmlReport(Sale data)
        {
            var xmlPath = Request.MapPath(Request.ApplicationPath) + @"xml\\Reports.xml";
            XDocument document = XDocument.Load(xmlPath);
            var xmlData = document.Descendants("Report").FirstOrDefault();
            xmlData.Element("IsPrint").SetValue(data.IsPrint);
            xmlData.Element("NumPrint").SetValue(data.NumPrint);
            xmlData.Element("PaperSize").SetValue(data.PaperSize);
            xmlData.Element("IsType").SetValue(data.IsType);
            document.Save(xmlPath);
        }
        public void GetAutoPrint(string slug)
        {
            using (SqlConnection conn = new SqlConnection(dBConnect.Getconnectionstring("SqlConnection")))
            {
                SqlCommand command = new SqlCommand("Pro_View_Receipt", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID", SqlDbType.VarChar).Value = slug;
                try
                {
                    var xmlPath = Request.MapPath(Request.ApplicationPath) + @"xml\\Reports.xml";
                    var xmlDoc = XDocument.Load(xmlPath);
                    var xmlReport = xmlDoc.Descendants("Report").Select(x => new
                    {
                        IsPrint = x.Element("IsPrint").Value,
                        NumPrint = x.Element("NumPrint").Value,
                        PaperSize = x.Element("PaperSize").Value,
                        IsType = x.Element("IsType").Value
                    }).FirstOrDefault();
                    if (xmlReport.PaperSize == "Small")
                    {
                        rptName = "rptReceipt_Small.rdlc";
                    }
                    else if (xmlReport.PaperSize == "A5")
                    {
                        rptName = "rptReceipt_A5.rdlc";
                    }
                    else
                    {
                        rptName = "rptReceipt_A4.rdlc";
                    }
                    int countPrint = int.Parse(xmlReport.NumPrint);
                    conn.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        LocalReport report = new LocalReport();
                        ReportDataSource rds = new ReportDataSource("DataSet1", dataReader);
                        report.ReportPath = Request.MapPath(Request.ApplicationPath) + @"ReportInside/" + xmlReport.IsType + "/" + xmlReport.PaperSize + "/" + rptName;
                        report.DataSources.Add(rds);
                        for (int i = 0; i < countPrint; i++)
                        {
                            autoPrint.SetPrint(report, autoPrint.PrinterName());
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                conn.Close();
            }

        }
        [Route("Sale/Receipt")]
        [HttpGet]
        public ActionResult Receipt(string slug)
        {
            var xmlPath = Request.MapPath(Request.ApplicationPath) + @"xml\\Reports.xml";
            var xmlDoc = XDocument.Load(xmlPath);
            var xmlReport = xmlDoc.Descendants("Report").Select(x => new
            {
                IsPrint = x.Element("IsPrint").Value,
                NumPrint = x.Element("NumPrint").Value,
                PaperSize = x.Element("PaperSize").Value,
                IsType = x.Element("IsType").Value
            }).FirstOrDefault();
            if (xmlReport.PaperSize == "Small")
            {
                rptName = "rptReceipt_Small.rdlc";
            }
            else if (xmlReport.PaperSize == "A5")
            {
                rptName = "rptReceipt_A5.rdlc";
            }
            else
            {
                rptName = "rptReceipt_A4.rdlc";
            }
            using (SqlConnection conn = new SqlConnection(dBConnect.Getconnectionstring("SqlConnection")))
            {
                SqlCommand command = new SqlCommand("Pro_View_Receipt", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ID", SqlDbType.VarChar).Value = slug;
                try
                {
                    conn.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        ReportViewer reportViewer = new ReportViewer();
                        reportViewer.SizeToReportContent = true;
                        reportViewer.ProcessingMode = ProcessingMode.Local;
                        reportViewer.SizeToReportContent = true;
                        reportViewer.Width = Unit.Percentage(100);
                        reportViewer.Height = Unit.Percentage(100);
                        reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"ReportInside/" + xmlReport.IsType + "/" + xmlReport.PaperSize + "/" + rptName;
                        ReportDataSource rds = new ReportDataSource("DataSet1", dataReader);
                        reportViewer.LocalReport.DataSources.Add(rds);

                        Warning[] warnings;
                        string[] streamIds;
                        string mimeType = string.Empty;
                        string encoding = string.Empty;
                        string extension = string.Empty;
                        byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                        byte[] file = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "inline;filename=" + rptName + ".pdf");
                        Response.Buffer = true;
                        Response.Clear();
                        Response.BinaryWrite(file);
                        Response.End();
                    }
                }
                catch (Exception ex)
                {
                    return View(ex.Message);
                }
                conn.Close();
            }
            return View();
        }

        [HttpGet]
        public ActionResult GetAccountTypeByCustomer(string id)
        {
            var dataJson = context.tbl_customers_account.Where(x => x.memberID == id).ToList();
            return Json(dataJson, JsonRequestBehavior.AllowGet);
        }
    }
}
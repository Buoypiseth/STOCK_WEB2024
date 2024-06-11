using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Repositories;
using WebApplication.Helpers;
using Microsoft.Reporting.WebForms;

namespace WebApplication.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private DataContext context;
        private ReportRepository reportRepository;
        private SaleRepository salesRepository;
        public ReportController()
        {
            this.context = new DataContext();
            this.reportRepository = new ReportRepository();
            this.salesRepository = new SaleRepository();
        }
        long UserID = long.Parse(IUser.Id);
        string PathReportName;
        string ParaReportName;
        string[] Items;
        string fromDate;
        string toDate;
        public ActionResult Index(string dir, string slug)
        {
            ViewBag.PathReportName = dir;
            ViewBag.ParaReportName = slug;
            var row = context.tbl_OTHER_OPTIONS.FirstOrDefault(x => x.para_ReportName == slug);
            if (row == null)
            {
                DropDownListFor();
                return View();
            }
            this.OptionSearch(slug);
            if (row.para08_0_TabMonthly == 1)
            {
                ViewBag.TabMonthly = "show active";
            }
            else if (row.para09_0_TabDate == 1)
            {
                ViewBag.TabDaily = "show active";
            }
            else if (row.para10_0_TabDate_From_To == 1)
            {
                ViewBag.TabFromToDate = "show active";
            }
            else if (row.para11_0_TabYearly == 1)
            {
                ViewBag.TabYearly = "show active";
            }
            DropDownListFor();
            return View();
        }
        public void OptionSearch(string key)
        {
            var row = context.tbl_OTHER_OPTIONS_SEARCH.FirstOrDefault(x => x.para_ReportName == key);
            ViewBag.SearchProduct = row.SearchProduct;
            ViewBag.SearchCategory = row.SearchCategory;
            ViewBag.SearchCustomer = row.SearchCustomer;
            ViewBag.SearchSupplier = row.SearchSupplier;
            ViewBag.SearchInvoice = row.SearchInvoice;
            ViewBag.SearchUser = row.SearchUser;
            ViewBag.SearchStock = row.SearchStock;
            ViewBag.SearchDateOnly = row.SearchDateOnly;
            if (row.SearchCategory == true ||
               row.SearchCustomer == true ||
               row.SearchSupplier == true)
            {
                ViewBag.Col1 = true;
            }
            if (row.SearchProduct == true ||
               row.SearchInvoice == true ||
               row.SearchStock == true ||
               row.SearchUser == true)
            {
                ViewBag.Col2 = true;
            }
        }
        // POST: Report/Create
        [Route("Report/Create")]
        [HttpPost]
        public ActionResult Create(FormCollection data)
        {
            PathReportName = data["PathReportName"];
            ParaReportName = data["ParaReportName"];
            reportRepository.DeleteOtherOptionEmployeeSet();
            var isSearchDate = context.tbl_OTHER_OPTIONS_SEARCH.Where(x => x.para_ReportName == ParaReportName).Any(x => x.SearchDateOnly == false);
            if (isSearchDate)
            {
                this.CheckSearch(data);
            }
            else
            {
                tbl_OTHER_OPTIONS_Employee_Set dt = new tbl_OTHER_OPTIONS_Employee_Set();
                dt.EmployeeID = 0;
                dt.UserID = UserID;
                dt.RunID = "0";
                reportRepository.InsertOtherOptionEmployeeSet(dt);
            }

            tbl_OTHER_OPTIONS_FILTER _option_filter = new tbl_OTHER_OPTIONS_FILTER();
            var row = context.tbl_OTHER_OPTIONS.FirstOrDefault(x => x.para_ReportName == ParaReportName);
            if (row.para08_0_TabMonthly == 1)
            {
                ViewBag.TabMonthly = "show active";
                _option_filter.para08_1_cboMonthly = Helper.getMonth(data["MonthlyDate"]);
                _option_filter.para08_2_Monthly_cboFrom = Helper.getMonth(data["MonthlyDate"]);
                _option_filter.para08_3_Monthly_cboTo = Helper.getLastDayInMonth(data["MonthlyDate"]);
            }
            else if (row.para09_0_TabDate == 1)
            {
                ViewBag.TabDaily = "show active";
                _option_filter.para09_1_Date = Helper.getDate(data["DailyDate"]);
            }
            else if (row.para10_0_TabDate_From_To == 1)
            {
                ViewBag.TabFromToDate = "show active";
                //fromDate = data["FromToDate"].Substring(0, 10);
                //toDate = data["FromToDate"].Substring(13, 10);
                fromDate = data["FromDate"];
                toDate = data["ToDate"];
                _option_filter.para10_1_Date_From_To_cboFrom = Helper.getDate(fromDate);
                _option_filter.para10_2_Date_From_To_cboTo = Helper.getDate(toDate);
            }
            else if (row.para11_0_TabYearly == 1)
            {
                ViewBag.TabYearly = "show active";
                _option_filter.para11_1_Yearly_cboYear = Helper.getYear(data["YearlyDate"]);
            }
            _option_filter.para_ReportName = ParaReportName;
            return Json(reportRepository.InsertOtherOptionFilter(_option_filter), JsonRequestBehavior.AllowGet);
        }
        public void CheckSearch(FormCollection data)
        {
            if (data["PrdCategID"] != null)
            {
                if (data["PrdCategID"] == "All")//filter all item
                {
                    Items = context.tbl_prdcategory.Select(x => x.PrdCategID).OrderBy(x => x).ToArray();
                }
                else
                {
                    Items = data["PrdCategID"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            else if (data["ProductID"] != null)
            {
                if (data["ProductID"] == "All")
                {
                    Items = context.tbl_products.Select(x => x.ProductID).OrderBy(x => x).ToArray();
                }
                else
                {
                    Items = data["ProductID"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            else if (data["InvoiceNo"] != null)
            {
                if (data["InvoiceNo"] == "All")
                {
                    Items = context.tbl_customerorder.Select(x => x.customerOrderID).OrderBy(x => x).ToArray();
                }
                else
                {
                    Items = data["InvoiceNo"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            else if (data["UserID"] != null)
            {
                if (data["UserID"] == "All")
                {
                    Items = context.tbl_useraccount.Select(x => x.UserID.ToString()).OrderBy(x => x).ToArray();
                }
                else
                {
                    Items = data["UserID"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            else if (data["memberID"] != null)
            {
                if (data["memberID"] == "All")
                {
                    Items = context.tbl_customers.Select(x => x.memberID).OrderBy(x => x).ToArray();
                }
                else
                {
                    Items = data["memberID"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            else if (data["SupplierID"] != null)
            {
                if (data["SupplierID"] == "All")
                {
                    Items = context.tbl_suppliers.Select(x => x.SupplierID).OrderBy(x => x).ToArray();
                }
                else
                {
                    Items = data["SupplierID"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            else if (data["StockID"] != null)
            {
                if (data["StockID"] == "All")
                {
                    Items = context.tblStockTypes.Select(x => x.StockID).OrderBy(x => x).ToArray();
                }
                else
                {
                    Items = data["StockID"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            foreach (var item in Items)
            {
                tbl_OTHER_OPTIONS_Employee_Set dt = new tbl_OTHER_OPTIONS_Employee_Set();
                dt.EmployeeID = long.Parse(item);
                dt.UserID = UserID;
                dt.RunID = "0";
                reportRepository.InsertOtherOptionEmployeeSet(dt);
            }
        }
        private void DropDownListFor()
        {
            var categories = context.tbl_prdcategory.ToList();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            categoryList.Add(new SelectListItem { Text = "All", Value = "All" });
            foreach (var item in categories)
            {
                categoryList.Add(new SelectListItem { Text = item.PrdCategory, Value = item.PrdCategID });
            }
            ViewBag.Categories = categoryList;

            var products = context.tbl_products.ToList();
            List<SelectListItem> productList = new List<SelectListItem>();
            productList.Add(new SelectListItem { Text = "All", Value = "All" });
            foreach (var item in products)
            {
                productList.Add(new SelectListItem { Text = item.PrdNameEng, Value = item.ProductID });
            }
            ViewBag.Products = productList;

            var invoices = context.tbl_customerorder.ToList();
            List<SelectListItem> invoiceList = new List<SelectListItem>();
            invoiceList.Add(new SelectListItem { Text = "All", Value = "All" });
            foreach (var item in invoices)
            {
                invoiceList.Add(new SelectListItem { Text = item.InvoiceNo, Value = item.customerOrderID });
            }
            ViewBag.Invoices = invoiceList;

            var customers = context.tbl_customers.ToList();
            List<SelectListItem> customerList = new List<SelectListItem>();
            customerList.Add(new SelectListItem { Text = "All", Value = "All" });
            foreach (var item in customers)
            {
                customerList.Add(new SelectListItem { Text = item.fullName, Value = item.memberID });
            }
            ViewBag.Customers = customerList;

            var supplies = context.tbl_suppliers.ToList();
            List<SelectListItem> suppliesList = new List<SelectListItem>();
            suppliesList.Add(new SelectListItem { Text = "All", Value = "All" });
            foreach (var item in supplies)
            {
                suppliesList.Add(new SelectListItem { Text = item.CompanyName, Value = item.SupplierID });
            }
            ViewBag.Supplies = suppliesList;

            var stocks = context.tblStockTypes.ToList();
            List<SelectListItem> stockList = new List<SelectListItem>();
            stockList.Add(new SelectListItem { Text = "All", Value = "All" });
            foreach (var item in stocks)
            {
                stockList.Add(new SelectListItem { Text = item.StockName, Value = item.StockID });
            }
            ViewBag.Stocks = stockList;

            var users = context.tbl_useraccount.Where(x => x.UserID != "0").ToList();
            List<SelectListItem> userList = new List<SelectListItem>();
            userList.Add(new SelectListItem { Text = "All", Value = "All" });
            foreach (var item in users)
            {
                userList.Add(new SelectListItem { Text = item.UserName, Value = item.UserID });
            }
            ViewBag.Users = userList;
        }
        [Route("Report/Viewer")]
        [HttpGet]
        public ActionResult Viewer(string dir, string slug)
        {
            var row = context.tbl_OTHER_OPTIONS.FirstOrDefault(x => x.para_ReportName == slug);
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.SizeToReportContent = true;
            reportViewer.ZoomPercent = 120;
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\" + dir + "\\" + row.para_ReportName + ".rdlc";
            tbl_OTHER_OPTIONS_FILTER data = new tbl_OTHER_OPTIONS_FILTER();
            data.ID = row.ID;
            data.UserID = UserID;
            data.EmployeeID = -1;
            data.RunID = "-1";
            data.para_Stored_Pro_Name = row.para_Stored_Pro_Name;
            ReportDataSource rds = new ReportDataSource("DataSet1", reportRepository.dataReader(data));
            reportViewer.LocalReport.DataSources.Add(rds);
            ViewBag.ReportViewer = reportViewer;
            return View();
        }
        [Route("Report/PdfViewer")]
        [HttpGet]
        public ActionResult PdfViewer(string dir, string slug)
        {
            //var dataReader = context.tbl_customerorder.Where(x => x.openingBalanceID == "20210601020007");
            //ReportViewer reportViewer = new ReportViewer();
            //reportViewer.ProcessingMode = ProcessingMode.Local;
            //reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\\rpQuotation.rdlc";
            //ReportDataSource rds = new ReportDataSource("DataSet1", dataReader.ToList());
            //reportViewer.LocalReport.DataSources.Add(rds);

            var row = context.tbl_OTHER_OPTIONS.FirstOrDefault(x => x.para_ReportName == slug);
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.SizeToReportContent = true;
            reportViewer.ZoomPercent = 120;
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\" + dir + "\\" + row.para_ReportName + ".rdlc";
            tbl_OTHER_OPTIONS_FILTER data = new tbl_OTHER_OPTIONS_FILTER();
            data.ID = row.ID;
            data.UserID = UserID;
            data.EmployeeID = -1;
            data.RunID = "-1";
            data.para_Stored_Pro_Name = row.para_Stored_Pro_Name;
            ReportDataSource rds = new ReportDataSource("DataSet1", reportRepository.dataReader(data));
            reportViewer.LocalReport.DataSources.Add(rds);
            ViewBag.ReportViewer = reportViewer;

            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            byte[] bytes = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            byte[] file = reportViewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "inline;filename=" + slug + ".pdf");
            Response.Buffer = true;
            Response.Clear();
            Response.BinaryWrite(file);
            Response.End();
            return View();
        }
    }
}
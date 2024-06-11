using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Repositories;
using WebApplication.Helpers;

namespace WebApplication.Controllers
{
    [Authorize]
    public class BuyProductController : Controller
    {
        private DataContext context;
        private BuyProductRepository buyProductRepository;
        private SupplierRepository supplierRepository;
        public BuyProductController()
        {
            this.context = new DataContext();
            this.buyProductRepository = new BuyProductRepository();
            this.supplierRepository = new SupplierRepository();
        }
        // GET: BuyProduct
        public ActionResult Index()
        {
            DropDownListFor();
            return View();
        }
        public ActionResult GetData(BaseDatatable param)
        {
            var data = buyProductRepository.GetForDataTable();
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => x.InvoiceNo.Contains(param.sSearch)).ToList();
            }
            //Block show column
            var sortColumnIndex = HttpContext.Request.QueryString["iSortCol_0"];
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            data = buyProductRepository.SortByColumnWithOrder(sortColumnIndex, sortDirection, data);

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
        //GET:BuyProduct/GetMainStockOrdering
        public ActionResult GetMainStockOrdering(BaseDatatable param)
        {
            string buyId = Request["buyId"] ?? "0";
            var data = buyProductRepository.GetForMainStockOrdering(buyId);
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => x.PrdNameEng.Contains(param.sSearch)).ToList();
            }
            //Block show column
            var sortColumnIndex = HttpContext.Request.QueryString["iSortCol_0"];
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            data = buyProductRepository.SortByColumnWithOrdering(sortColumnIndex, sortDirection, data);

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
        // GET: BuyProduct/AddOrder
        [Route("BuyProduct/AddOrder")]
        [HttpPost]
        public JsonResult AddOrder(MainStock data)
        {
            var mainStock = context.tbl_mainstock_ordering
                .Any(x => x.ProductID == data.ProductID 
                && x.UserID == IUser.Id
                && x.BuyID == data.BuyID
                && x.BuyStatus == "New");
            if (mainStock)
            {
                return Json(buyProductRepository.Update(data), JsonRequestBehavior.AllowGet);
            }
            return Json(buyProductRepository.Create(data), JsonRequestBehavior.AllowGet);
        }
        // GET: BuyProduct/Delete/5
        [Route("BuyProduct/Delete")]
        [HttpPost]
        public JsonResult Delete(MainStock data)
        {
            return Json(buyProductRepository.Delete(data), JsonRequestBehavior.AllowGet);
        }
        // GET: BuyProduct/Create
        public ActionResult Create()
        {
            ViewBag.InvoiceNo = "";
            ViewBag.buyId = "0";
            DropDownListFor();
            return View();
        }
        // POST: BuyProduct/Create
        [HttpPost]
        public ActionResult Create(tbl_BuyProducts data)
        {
            try
            {
                bool hasItem = context.tbl_mainstock_ordering.Any(x => x.BuyID == data.BuyID);
                if (!hasItem)
                {
                    TempData["flashError"] = "The payment was error invalid data, try again.";
                    if(data.BuyID == "0")
                    {
                        return RedirectToAction("Create");
                    }
                    return RedirectToAction("Edit", new { slug = data.BuyID });
                }
                //update payment to supplier
                if (data.BuyID != "0")
                {
                    buyProductRepository.UpdatePayment(data);
                    TempData["flashSuccess"] = "The payment was successfully updated.";
                    return RedirectToAction("Index");
                }
                //create payment to supplier
                bool hasInvoiceNo = context.tbl_BuyProducts.Any(x => x.InvoiceNo == data.InvoiceNo);
                if (hasInvoiceNo)
                {
                    TempData["flashError"] = "You've already entered that invoice number, try again.";
                    return RedirectToAction("Create");
                }
                buyProductRepository.CreatePayment(data);
                TempData["flashSuccess"] = "The payment was successfully created.";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public JsonResult TransferToModify(MainStock data)
        {
            return Json(new { status = buyProductRepository.TransferToModify(data), message = "" }, JsonRequestBehavior.AllowGet);
        }
        // GET: BuyProduct/Edit
        public ActionResult Edit(string slug)
        {
            var buyProduct = context.tbl_BuyProducts.FirstOrDefault(x => x.BuyID == slug);
            ViewBag.InvoiceNo = buyProduct.InvoiceNo;
            ViewBag.buyId = slug;
            DropDownListFor();
            return View();
        }
        // GET: BuyProduct/BuyDetail
        public ActionResult BuyDetail(string slug)
        {
            ViewBag.buyId = slug;
            return View();
        }
        // GET: BuyProduct/GetDataBuyDetail
        public ActionResult GetDataBuyDetail(BaseDatatable param)
        {
            string buyId = Request["buyId"];
            var data = buyProductRepository.GetDataBuyDetail(buyId);
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => x.PrdNameEng.Contains(param.sSearch)).ToList();
            }
            //Block show column
            var sortColumnIndex = HttpContext.Request.QueryString["iSortCol_0"];
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            data = buyProductRepository.SortByColumnWithOrdering(sortColumnIndex, sortDirection, data);

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
        // POST: Customer/CreateSupplier
        [Route("Customer/CreateSupplier")]
        [HttpPost]
        public ActionResult CreateSupplier(Supplier data)
        {
            bool supplier = context.tbl_suppliers.Any(x => x.CompanyName == data.CompanyName);
            if (supplier)
            {
                TempData["flashError"] = "The supplier was error created, try again.";
                return RedirectToAction("Create");
            }

            supplierRepository.Create(data);
            TempData["flashSuccess"] = "The supplier was successfully created.";
            return RedirectToAction("Create");
        }
        public void DropDownListFor()
        {
            var stockTypes = context.tblStockTypes.ToList();
            ViewBag.StockTypes = new SelectList(stockTypes, "StockID", "StockName");

            var suppliers = context.tbl_suppliers.ToList();
            ViewBag.Suppliers = new SelectList(suppliers, "SupplierID", "CompanyName");
        }
    }
}
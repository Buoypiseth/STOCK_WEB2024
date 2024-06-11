using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Controllers
{
    [Authorize]
    public class ProductPackageController : Controller
    {
        private DataContext context;
        private ProductPackageRepository productPackageRepository;
        public ProductPackageController()
        {
            this.context = new DataContext();
            this.productPackageRepository = new ProductPackageRepository();
        }
        // GET: ProductPackage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData(BaseDatatable param)
        {
            //string paraId = Request["PrdCategID"];
            var data = productPackageRepository.GetForDataTable();
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => x.Product_Package.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }
            var sortColumnIndex = HttpContext.Request.QueryString["iSortCol_0"];
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            data = productPackageRepository.SortByColumnWithOrder(sortColumnIndex, sortDirection, data);

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
        // POST: ProductPackage/Create
        [Route("ProductPackage/Create")]
        [HttpPost]
        public JsonResult Create(tbl_products_Package data)
        {
            if (data.Product_PackageID != 0)
            {
                var resUpdate = productPackageRepository.Update(data);
                return Json(new { resUpdate.status, resUpdate.message }, JsonRequestBehavior.AllowGet);
            }
            bool packageName = context.tbl_products_Package.Any(x => x.Product_Package == data.Product_Package);
            if (packageName)
            {
                return Json(new { status = "Warning", message = "Your\'ve already entered that Package." }, JsonRequestBehavior.AllowGet);
            }
            var resCreate = productPackageRepository.Create(data);
            return Json(new { resCreate.status, resCreate.message }, JsonRequestBehavior.AllowGet);
        }
        // POST: ProductPackage/Edit/5
        [Route("ProductPackage/Edit")]
        [HttpPost]
        public JsonResult Edit(tbl_products_Package data)
        {

            var productPackage = context.tbl_products_Package.FirstOrDefault(x => x.Product_PackageID == data.Product_PackageID);
            return Json(productPackage, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Show(long id)
        {
            var package = context.tbl_products_Package.FirstOrDefault(x => x.Product_PackageID == id);
            ViewBag.PackageID = package.Product_PackageID;
            ViewBag.PackageName = package.Product_Package;
            DropDownListFor();
            return View();
        }
        public ActionResult GetShowForDataTable(BaseDatatable param)
        {
            //System.Diagnostics.Debug.Write("Hello " + Request["ProductID"]);
            var data = productPackageRepository.GetProductPackageForDataTable(long.Parse(Request["Product_PackageID"]));
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => x.PrdNameEng.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }
            var sortColumnIndex = HttpContext.Request.QueryString["iSortCol_0"];
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            data = productPackageRepository.SortByColumnWithOrder(sortColumnIndex, sortDirection, data);

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
        public void DropDownListFor()
        {
            var categories = context.tbl_prdcategory.ToList();
            List<SelectListItem> listCategories = new List<SelectListItem>();
            listCategories.Add(new SelectListItem { Text = "--categories--", Value = "" });
            foreach (var item in categories)
            {
                listCategories.Add(new SelectListItem { Text = item.PrdCategory, Value = item.PrdCategID });
            }
            ViewBag.Categories = listCategories;
            //ViewData["Categories"] = new SelectList(categories, "PrdCategID", "PrdCategory");

            var stockTypes = context.tblStockTypes.ToList();
            ViewBag.StockTypes = new SelectList(stockTypes, "StockID", "StockName");

            var suppliers = context.tbl_suppliers.ToList();
            ViewBag.Suppliers = new SelectList(suppliers, "SupplierID", "CompanyName");

            //var _product = _context.Products.Include(t => t.Category).Include(t => t.Supplier);
            //var products = from sp in context.tblStockTypeProducts
            //               join product in context.tbl_products on sp.ProductID equals product.ProductID
            //               join category in context.tbl_prdcategory on product.PrdCategID equals category.PrdCategID
            //               where (sp.StockID == IUser.StockID)
            var products = from product in context.tbl_products
                           join category in context.tbl_prdcategory on product.PrdCategID equals category.PrdCategID
                           select new
                           {
                               product.ProductID,
                               product.PrdNameEng,
                               product.OrderComment,
                               category.PrdCategory,
                               //sp.StockID,
                               //sp.Qty
                           };
            List<SelectListItem> listProduct = new List<SelectListItem>();
            //listProduct.Add(new SelectListItem { Text = "--select products--", Value = "" });
            //foreach (var item in products)
            //{
            //    listProduct.Add(new SelectListItem { Text = item.OrderComment == "" ? item.PrdNameEng : item.PrdNameEng + "|" + item.OrderComment, Value = item.ProductID });
            //}
            ViewBag.Products = listProduct;
            //ViewData["Products"] = new SelectList((from p in products
            //                                          select new
            //                                          {
            //                                              ProductID = p.ProductID,
            //                                              OrderComment = p.PrdNameEng + " | " + p.OrderComment// + " | " + p.Qty + " | " + p.PrdCategory
            //                                          }), "ProductID", "OrderComment", null);
        }
        // POST: ProductPackage/ScanBarcode
        //[Route("BuyProduct/ScanBarcode")]
        //[HttpPost]
        //public JsonResult ScanBarcode(tbl_products_Package_Detail data)
        //{
        //    if (data.Status == "createIme" && data.barCode == null)
        //    {
        //        return Json("The IME not null.", JsonRequestBehavior.AllowGet);
        //    }
        //    var barCode = context.tbl_products.Where(x => x.barCode == data.barCode).Count();
        //    if (barCode > 0)
        //    {
        //        return Json("Your\'ve already entered that Barcode.", JsonRequestBehavior.AllowGet);
        //    }
        //    return Json(buyProductRepository.UpdateOrCreateIme(data), JsonRequestBehavior.AllowGet);
        //}
        // POST: ProductPackage/AddItem
        [Route("ProductPackage/AddItem")]
        [HttpPost]
        public JsonResult AddItem(tbl_products_Package_Detail data)
        {
            if (data.Product_PackageDetail_ID != 0)
            {
                var resUpdate = productPackageRepository.UpdateItem(data);
                return Json(new { resUpdate.status, resUpdate.message }, JsonRequestBehavior.AllowGet);
            }
            var resCreate = productPackageRepository.CreateItem(data);
            return Json(new { resCreate.status, resCreate.message }, JsonRequestBehavior.AllowGet);
        }
        // POST: ProductPackage/EditItem/5
        [Route("ProductPackage/EditItem")]
        [HttpPost]
        public JsonResult EditItem(tbl_products_Package_Detail data)
        {

            var packageDetail = context.tbl_products_Package_Detail.FirstOrDefault(x => x.Product_PackageDetail_ID == data.Product_PackageDetail_ID);
            var product = context.tbl_products.FirstOrDefault(x => x.ProductID == packageDetail.ProductID);
            Product dataEdit = new Product();
            dataEdit.Product_PackageDetail_ID = packageDetail.Product_PackageDetail_ID;
            dataEdit.PrdCategory = product.PrdCategID;
            dataEdit.ProductID = packageDetail.ProductID;
            dataEdit.Quantity = packageDetail.Qty;
            dataEdit.SellingPriceUSD = packageDetail.SellingPriceUSD;
            dataEdit.Description = packageDetail.Description;
            return Json(dataEdit, JsonRequestBehavior.AllowGet);
        }
        // POST: ProductPackage/Delete/5
        [Route("ProductPackage/Delete")]
        [HttpPost]
        public JsonResult Delete(tbl_products_Package_Detail data)
        {
            return Json(productPackageRepository.Delete(data), JsonRequestBehavior.AllowGet);
        }
    }
}
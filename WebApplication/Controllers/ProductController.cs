using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Repositories;
using WebApplication.Helpers;
using System.IO;
using System.Text;

namespace WebApplication.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private DataContext context;
        private DBConnect dBConnect;
        private ProductRepository productRepository;
        public ProductController()
        {
            this.context = new DataContext();
            this.dBConnect = new DBConnect();
            this.productRepository = new ProductRepository();
        }
        // GET: Product
        public ActionResult Index()
        {
            DropDownListFor();
            return View();
        }
        public ActionResult GetData(BaseDatatable param)
        {
            //string paraId = Request["PrdCategID"];
            var data = productRepository.GetForDataTable();
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => x.PrdNameEng.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }
            var sortColumnIndex = HttpContext.Request.QueryString["iSortCol_0"];
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            data = productRepository.SortByColumnWithOrder(sortColumnIndex, sortDirection, data);

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
        // GET: Product/ForSale
        public ActionResult GetForSale(BaseDatatable param)
        {
            string prdCatId = Request["prdCatId"];
            var data = productRepository.GetForDataTable()
                .Where(x => x.PrdCategID == prdCatId)
                .ToList();
            if(prdCatId == "All")
            {
                data = productRepository.GetForDataTable();
            }
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => x.PrdNameEng.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }
            var sortColumnIndex = HttpContext.Request.QueryString["iSortCol_0"];
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            data = productRepository.SortByColumnWithOrder(sortColumnIndex, sortDirection, data);

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
        // GET: GetForPurchase
        public ActionResult GetForPurchase(BaseDatatable param)
        {
            var data = productRepository.GetForPurchase();
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => x.barCode.ToLower().Contains(param.sSearch.ToLower())
                || x.PrdNameEng.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }
            //Block show column
            var sortColumnIndex = HttpContext.Request.QueryString["iSortCol_0"];
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            data = productRepository.SortByColumnWithOrder(sortColumnIndex, sortDirection, data);

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
        // GET: Product/Child
        public ActionResult Child(string id)
        {
            ViewBag.ParentID = id;
            DropDownListFor();
            return View();
        }
        public ActionResult GetChildData(BaseDatatable param)
        {
            //System.Diagnostics.Debug.Write("Hello " + Request["ProductID"]);
            var data = productRepository.GetChildForDataTable(Request["ProductID"]);
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => x.PrdNameEng.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }
            var sortColumnIndex = HttpContext.Request.QueryString["iSortCol_0"];
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            data = productRepository.SortByColumnWithOrder(sortColumnIndex, sortDirection, data);

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
        // POST: Product/Create
        [Route("Product/Create")]
        [HttpPost]
        public JsonResult Create(Product data)
        {
            Byte[] photo = Encoding.UTF8.GetBytes(Server.MapPath(""));
            string ImgName = "/Images/img-default.jpg";
            string fileName;
            string extension;
            if (data.ProductID != null)
            {
                if (data.ImgUpload != null)
                {
                    fileName = Path.GetFileNameWithoutExtension(data.ImgUpload.FileName);
                    extension = Path.GetExtension(data.ImgUpload.FileName);
                    fileName = "img-" + DateTime.Now.ToString("yymmssff") + extension;
                    ImgName = "/Images/" + fileName;
                    data.ImgUpload.SaveAs(Path.Combine(Server.MapPath("/Images/"), fileName));
                    FileInfo fileInfo = new FileInfo(Server.MapPath(data.Image));
                    if (fileInfo.Exists)
                    {
                        fileInfo.Delete();
                    }
                }
                else
                {
                    ImgName = data.Image;
                }
                var resUpdate = productRepository.Update(data, ImgName);
                return Json(new { resUpdate.status, resUpdate.message }, JsonRequestBehavior.AllowGet);
               // return Json(productRepository.Update(data, ImgName), JsonRequestBehavior.AllowGet);
            }
            var barCode = context.tbl_products.Where(x => x.barCode == data.barCode && x.barCode != null).Count();
            if (barCode > 0)
            {
                return Json(new { status = "Warning", message = "Your\'ve already entered that Barcode." }, JsonRequestBehavior.AllowGet);
            }
            if (data.ImgUpload != null)
            {
                fileName = Path.GetFileNameWithoutExtension(data.ImgUpload.FileName);
                extension = Path.GetExtension(data.ImgUpload.FileName);
                fileName = "img-" + DateTime.Now.ToString("yymmssff") + extension;
                ImgName = "/Images/" + fileName;
                data.ImgUpload.SaveAs(Path.Combine(Server.MapPath("/Images/"), fileName));
            }
            var resCreate = productRepository.Create(data, ImgName, photo);
            return Json(new { resCreate.status, resCreate.message }, JsonRequestBehavior.AllowGet);
            //return Json(productRepository.Create(data, ImgName, photo), JsonRequestBehavior.AllowGet);
        }
        //// POST: Product/Edit/5
        [Route("Product/Edit")]
        [HttpPost]
        public JsonResult Edit(Product data)
        {
            List<Product> listProduct = new List<Product>();
            tbl_products p = context.tbl_products.FirstOrDefault(x => x.ProductID == data.ProductID);
            var customersPrice = context.tbl_products_Customers_Price.FirstOrDefault(x => x.ProductID == data.ProductID && x.memberID == "1");
            listProduct.Add(new Product
            {
                ProductID = p.ProductID,
                PrdNameEng = p.PrdNameEng,
                PrdCategID = p.PrdCategID,
                OrderComment = p.OrderComment,
                barCode = p.barCode,
                UnitType = p.UnitType,
                PrdDesc = p.PrdDesc,
                //BuyingCost = _product.BuyingCost,
                SellingPriceUSD = p.SellingPriceUSD,
                SellingPriceUSDForThisCus = customersPrice == null ? 0 : customersPrice.SellingPriceUSDForThisCus,
                minimalStock = p.minimalStock,
                TaxNote = p.TaxNote,
                Money_Sale_Type = p.Money_Sale_Type,
                IsUnique = p.IsUnique,
                isCutStock = p.isCutStock,
                Image = p.Image,
                NumInOne = p.NumInOne,
            });
            return Json(listProduct.FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }
        // POST: Product/GetProduct
        [Route("Product/GetProduct")]
        [HttpPost]
        public JsonResult GetProduct(string id)
        {
            ////var products = context.tbl_products.Where(x => x.PrdCategID == id).ToList();
            //var products = from sp in context.tblStockTypeProducts
            //               join product in context.tbl_products on sp.ProductID equals product.ProductID
            //               join category in context.tbl_prdcategory on product.PrdCategID equals category.PrdCategID
            //               where (product.PrdCategID == id)
            var products = from product in context.tbl_products
                           join category in context.tbl_prdcategory on product.PrdCategID equals category.PrdCategID
                           where product.PrdCategID == id && product.ProductID == product.ProductIDRealStock
                           orderby product.PrdNameEng
                           select new
                           {
                               product.ProductID,
                               product.PrdNameEng,
                               product.OrderComment,
                               category.PrdCategory,
                               product.UnitType,
                               //sp.StockID,
                               //sp.Qty
                           };
            List<SelectListItem> listProducts = new List<SelectListItem>();
            if (id != "")
            {
                foreach (var item in products)
                {
                    listProducts.Add(new SelectListItem { Text = item.OrderComment == "" ? item.PrdNameEng : item.PrdNameEng + " - " + item.UnitType + " - " + item.OrderComment, Value = item.ProductID });
                }
                return Json(listProducts, JsonRequestBehavior.AllowGet);
            }

            var allProducts = from product in context.tbl_products
                           join category in context.tbl_prdcategory on product.PrdCategID equals category.PrdCategID
                           select new
                           {
                               product.ProductID,
                               product.PrdNameEng,
                               product.OrderComment,
                               category.PrdCategory,
                               product.UnitType,
                               //sp.StockID,
                               //sp.Qty
                           };
            //listProducts.Add(new SelectListItem { Text = "", Value = "" });
            //foreach (var item in allProducts)
            //{
            //    listProducts.Add(new SelectListItem { Text = item.OrderComment == "" ? item.PrdNameEng : item.PrdNameEng + "|" + item.OrderComment, Value = item.ProductID });
            //}
            return Json(listProducts, JsonRequestBehavior.AllowGet);
        }
        // POST: Product/GetProduct
        [Route("Product/GetProduct")]
        [HttpPost]
        public JsonResult GetProductForSale(string id)
        {
            ////var products = context.tbl_products.Where(x => x.PrdCategID == id).ToList();
            //var products = from sp in context.tblStockTypeProducts
            //               join product in context.tbl_products on sp.ProductID equals product.ProductID
            //               join category in context.tbl_prdcategory on product.PrdCategID equals category.PrdCategID
            //               where (product.PrdCategID == id)
            var products = from product in context.tbl_products
                           join category in context.tbl_prdcategory on product.PrdCategID equals category.PrdCategID
                           where product.PrdCategID == id
                           orderby product.PrdNameEng
                           select new
                           {
                               product.ProductID,
                               product.PrdNameEng,
                               product.OrderComment,
                               category.PrdCategory,
                               product.UnitType,
                               //sp.StockID,
                               //sp.Qty
                           };
            List<SelectListItem> listProducts = new List<SelectListItem>();
            if (id != "")
            {
                foreach (var item in products)
                {
                    listProducts.Add(new SelectListItem { Text = item.OrderComment == "" ? item.PrdNameEng : item.PrdNameEng + " - " + item.UnitType + " - " + item.OrderComment, Value = item.ProductID });
                }
                return Json(listProducts, JsonRequestBehavior.AllowGet);
            }

            var allProducts = from product in context.tbl_products
                              join category in context.tbl_prdcategory on product.PrdCategID equals category.PrdCategID
                              select new
                              {
                                  product.ProductID,
                                  product.PrdNameEng,
                                  product.OrderComment,
                                  category.PrdCategory,
                                  product.UnitType,
                                  //sp.StockID,
                                  //sp.Qty
                              };
            //listProducts.Add(new SelectListItem { Text = "", Value = "" });
            //foreach (var item in allProducts)
            //{
            //    listProducts.Add(new SelectListItem { Text = item.OrderComment == "" ? item.PrdNameEng : item.PrdNameEng + "|" + item.OrderComment, Value = item.ProductID });
            //}
            return Json(listProducts, JsonRequestBehavior.AllowGet);
        }
        private void DropDownListFor()
        {
            productRepository.TaxNote = new[]
                    {
                          new SelectListItem { Value = "Tax", Text = "Tax" },
                          new SelectListItem { Value = "No_Tax", Text = "No Tax" },
                    };
            ViewBag.TaxNotes = new SelectList(productRepository.TaxNote, "Value", "Text");
            productRepository.Money_Sale_Type = new[]
                    {
                          new SelectListItem { Value = "$", Text = "$" },
                          new SelectListItem { Value = "R", Text = "R" },
                    };
            ViewBag.MoneySaleTypes = new SelectList(productRepository.Money_Sale_Type, "Value", "Text");
            var cagetories = context.tbl_prdcategory.ToList();
            ViewBag.Cagetories = new SelectList(cagetories, "PrdCategID", "PrdCategory");
            var suppliers = context.tbl_suppliers.ToList();
            ViewBag.Suppliers = new SelectList(suppliers, "SupplierID", "CompanyName");
        }
        // POST: Product/VerifyBarCode/5
        [Route("Product/VerifyBarCode")]
        [HttpGet]
        public JsonResult VerifyBarCode(string key)
        {
            var barCode = context.tbl_products.Where(x => x.barCode == key).Count();
            if (barCode > 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}

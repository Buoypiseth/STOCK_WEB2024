using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Controllers
{
    public class ProductListController : Controller
    {
        private DataContext context;
        private DBConnect dBConnect;
        private ProductRepository productRepository;
        public ProductListController()
        {
            this.context = new DataContext();
            this.dBConnect = new DBConnect();
            this.productRepository = new ProductRepository();
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData(BaseDatatable param)
        {
            //string paraId = Request["PrdCategID"];
            var data = productRepository.GetForDataTable();
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => 
                x.PrdNameEng.ToLower().Contains(param.sSearch.ToLower())
                || x.UnitType.ToLower().Contains(param.sSearch.ToLower())).ToList();
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

        // POST: ProductList/Create
        [Route("ProductList/Create")]
        [HttpPost]
        public JsonResult Create(Product data)
        {
            Byte[] photo = Encoding.UTF8.GetBytes(Server.MapPath(""));
            string ImgName = "/Images/img-default.jpg";
            string fileName;
            string extension;
            //if (data.ProductID != null)
            //{
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
                var resUpdate = productRepository.UpdateProductPrice(data, ImgName);
                return Json(new { resUpdate.status, resUpdate.message }, JsonRequestBehavior.AllowGet);
                // return Json(productRepository.Update(data, ImgName), JsonRequestBehavior.AllowGet);
            //}
            //var barCode = context.tbl_products.Where(x => x.barCode == data.barCode && x.barCode != null).Count();
            //if (barCode > 0)
            //{
            //    return Json(new { status = "Warning", message = "Your\'ve already entered that Barcode." }, JsonRequestBehavior.AllowGet);
            //}
            //if (data.ImgUpload != null)
            //{
            //    fileName = Path.GetFileNameWithoutExtension(data.ImgUpload.FileName);
            //    extension = Path.GetExtension(data.ImgUpload.FileName);
            //    fileName = "img-" + DateTime.Now.ToString("yymmssff") + extension;
            //    ImgName = "/Images/" + fileName;
            //    data.ImgUpload.SaveAs(Path.Combine(Server.MapPath("/Images/"), fileName));
            //}
            //var resCreate = productRepository.Create(data, ImgName, photo);
            //return Json(new { resCreate.status, resCreate.message }, JsonRequestBehavior.AllowGet);
            ////return Json(productRepository.Create(data, ImgName, photo), JsonRequestBehavior.AllowGet);
        }
        //// POST: Product/Edit/5
        [Route("ProductList/Edit")]
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
                totalInStock = p.totalInStock,
                BuyingCost = p.BuyingCost,
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
    }
}
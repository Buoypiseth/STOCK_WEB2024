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
    public class CategoryController : BaseController
    {
        private DataContext context;
        private CategoryRepository categoryRepository;
        public CategoryController()
        {
            this.context = new DataContext();
            this.categoryRepository = new CategoryRepository();
        }
        // GET: Category
        public ActionResult Index()
        {
            ViewBag.PrdCategIDMain = new SelectList(context.tbl_prdcategory, "PrdCategID", "PrdCategory");
            return View();
        }
        public ActionResult GetData(BaseDatatable param)
        {
            string paraId = Request["PrdCategID"];
            var data = categoryRepository.GetForDataTable();
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => x.PrdCategory.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }
            var sortColumnIndex = HttpContext.Request.QueryString["iSortCol_0"];
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            data = categoryRepository.SortByColumnWithOrder(sortColumnIndex, sortDirection, data);

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
        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        // POST: Category/Create
        [Route("Category/Create")]
        [HttpPost]
        public JsonResult Create(tbl_prdcategory data)
        {
            if (!string.IsNullOrEmpty(data.PrdCategID))
            {
                var resUpdate = categoryRepository.Update(data);
                return Json(new { resUpdate.status, resUpdate.message }, JsonRequestBehavior.AllowGet);
            }
            var resCreate = categoryRepository.Create(data);
            return Json(new { resCreate.status, resCreate.message }, JsonRequestBehavior.AllowGet);
        }

        //// POST: Category/Edit/5
        [Route("Category/Edit")]
        [HttpPost]
        public JsonResult Edit(tbl_prdcategory data)
        {
            var dataJson = context.tbl_prdcategory.FirstOrDefault(x => x.PrdCategID == data.PrdCategID);
            return Json(dataJson, JsonRequestBehavior.AllowGet);
        }

        // GET: Category/Delete/5
        [Route("Category/Delete")]
        [HttpPost]
        public JsonResult Delete(tbl_prdcategory data)
        {
            return Json(categoryRepository.Delete(data), JsonRequestBehavior.AllowGet);
        }
    }
}

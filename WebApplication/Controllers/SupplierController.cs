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
    public class SupplierController : Controller
    {
        // GET: Supplier
        private DataContext context;
        private SupplierRepository supplierRepository;
        public SupplierController()
        {
            this.context = new DataContext();
            this.supplierRepository = new SupplierRepository();
        }
        // GET: Customer
        public ActionResult Index()
        {
            //DropDownListFors();
            return View();
        }
        public ActionResult GetData(BaseDatatable param)
        {
            var data = supplierRepository.GetForDataTable();
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => x.CompanyName.ToLower().Contains(param.sSearch.ToLower())
                || x.JobTitle.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }
            var sortColumnIndex = HttpContext.Request.QueryString["iSortCol_0"];
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            data = supplierRepository.SortByColumnWithOrder(sortColumnIndex, sortDirection, data);

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
        // POST: Supplier/Create
        [Route("Supplier/Create")]
        [HttpPost]
        public JsonResult Create(Supplier data)
        {
            if (data.SupplierID == null)
            {
                return Json(supplierRepository.Create(data), JsonRequestBehavior.AllowGet);
            }
            return Json(supplierRepository.Update(data), JsonRequestBehavior.AllowGet);
        }
        //// POST: Supplier/Edit/5
        [Route("Supplier/Edit")]
        [HttpPost]
        public JsonResult Edit(tbl_suppliers data)
        {
            var supplier = context.tbl_suppliers.FirstOrDefault(x => x.SupplierID == data.SupplierID);
            return Json(supplier, JsonRequestBehavior.AllowGet);
        }
        // GET: Supplier/Delete/5
        [Route("Supplier/Delete")]
        [HttpPost]
        public JsonResult Delete(tbl_suppliers data)
        {
            if (data.SupplierID == "0")
            {
                return Json("Can't delete.", JsonRequestBehavior.AllowGet);
            }
            return Json(supplierRepository.Delete(data), JsonRequestBehavior.AllowGet);
        }
    }
}
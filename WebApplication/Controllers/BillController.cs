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
    public class BillController : Controller
    {
        // GET: Bill
        private DataContext context;
        private BillRepository billRepository;
        public BillController()
        {
            this.context = new DataContext();
            this.billRepository = new BillRepository();
        }
        // GET: CustomerOrder
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData(BaseDatatable param)
        {
            var data = billRepository.GetForDataTable();
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => x.InvoiceNo.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }
            //Block show column
            var sortColumnIndex = HttpContext.Request.QueryString["iSortCol_0"];
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            data = billRepository.SortByColumnWithOrder(sortColumnIndex, sortDirection, data);

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
    }
}
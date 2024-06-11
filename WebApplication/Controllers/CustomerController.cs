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
    public class CustomerController : Controller
    {
        private DataContext context;
        private CustomerRepository customerRepository;
        public CustomerController()
        {
            this.context = new DataContext();
            this.customerRepository = new CustomerRepository();
        }
        // GET: Customer
        public ActionResult Index()
        {
            DropDownListFors();
            var accountTypes = context.tbl_customers_account_Cards_Type.ToList();
            ViewBag.AccountTypes = new SelectList(accountTypes, "Type", "Type", "Member");
            return View();
        }
        public ActionResult GetData(BaseDatatable param)
        {
            var data = customerRepository.GetForDataTable();
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => x.fullName.ToLower().Contains(param.sSearch.ToLower())
                || x.icpassportNo.ToLower().Contains(param.sSearch.ToLower())
                || x.Mobile.ToLower().Contains(param.sSearch.ToLower()))
                 .ToList();
            }
            var sortColumnIndex = HttpContext.Request.QueryString["iSortCol_0"];
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            data = customerRepository.SortByColumnWithOrder(sortColumnIndex, sortDirection, data);

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
        // POST: Customer/Create
        [Route("Customer/Create")]
        [HttpPost]
        public JsonResult Create(Customer data)
        {
            if (!string.IsNullOrEmpty(data.memberID))
            {
                return Json(customerRepository.Update(data), JsonRequestBehavior.AllowGet);
            }
            bool hasCustomer = context.tbl_customers.Any(x => x.icpassportNo == data.icpassportNo && x.icpassportNo != null);
            if (hasCustomer)
            {
                return Json("The customer idcard already exists. Please try again!", JsonRequestBehavior.AllowGet);
            }
            return Json(customerRepository.Create(data), JsonRequestBehavior.AllowGet);
        }
        //// POST: Customer/Edit/5
        [Route("Customer/Edit")]
        [HttpPost]
        public JsonResult Edit(Customer data)
        {
            var setCustomer = context.tbl_customers.Where(x => x.memberID == data.memberID).FirstOrDefault();
            return Json(setCustomer, JsonRequestBehavior.AllowGet);
        }
        // GET: Customer/Delete/5
        [Route("Customer/Delete")]
        [HttpPost]
        public JsonResult Delete(Customer data)
        {
            if (data.memberID == "0")
            {
                return Json("The customer can not delete.", JsonRequestBehavior.AllowGet);
            }
            return Json(customerRepository.Delete(data), JsonRequestBehavior.AllowGet);
        }
        // GET: Customer/Account
        [HttpGet]
        public ActionResult Account(string slug = "")
        {
            var cust = context.tbl_customers.FirstOrDefault(x => x.memberID == slug);
            ViewBag.CustId = slug;
            ViewBag.CustName = cust.fullName;
            var accountTypes = context.tbl_customers_account_Cards_Type.ToList();
            ViewBag.AccountTypes = new SelectList(accountTypes, "Type", "Type");
            return View();
        }
        // GET: Customer/GetDataAccount
        public ActionResult GetDataAccount(BaseDatatable param)
        {
            var custId = Request["custId"];
            var data = customerRepository.GetForAccount(custId);
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => x.AccountType.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }
            var sortColumnIndex = HttpContext.Request.QueryString["iSortCol_0"];
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            //data = customerRepository.SortByColumnWithOrder(sortColumnIndex, sortDirection, data);

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
        [HttpPost]
        public ActionResult StoreAccount(CustomerAccount data)
        {
            var hasAccount = context.tbl_customers_account.Any(x => x.AccountType == data.AccountType && x.memberID == data.memberID);
            if (hasAccount)
            {
                return Json("The account type already exists. Please try again!", JsonRequestBehavior.AllowGet);
            }
            return Json(customerRepository.StoreAccount(data), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult StoreDeposited(CustomerAccount data)
        {
            return Json(customerRepository.StoreDeposited(data), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult StoreCashouts(CustomerAccount data)
        {
            return Json(customerRepository.StoreCashouts(data), JsonRequestBehavior.AllowGet);
        }
        // GET: Customer/Transaction
        [HttpGet]
        public ActionResult Transaction(string slug = "")
        {
            var cust = context.tbl_customers.FirstOrDefault(x => x.memberID == slug);
            ViewBag.CustId = slug;
            ViewBag.CustName = cust.fullName;
            var accountTypes = context.tbl_customers_account_Cards_Type.ToList();
            ViewBag.AccountTypes = new SelectList(accountTypes, "Type", "Type");
            return View();
        }
        // GET: Customer/GetDataTransaction
        public ActionResult GetDataTransaction(BaseDatatable param)
        {
            var custId = Request["custId"];
            var tranType = Request["tranType"];
            var data = customerRepository.GetDataTransaction(custId, tranType);
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => x.AccountType.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }
            var sortColumnIndex = HttpContext.Request.QueryString["iSortCol_0"];
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            //data = customerRepository.SortByColumnWithOrder(sortColumnIndex, sortDirection, data);

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
        // POST: Customer/getAccount
        [HttpPost]
        public ActionResult getAccount(CustomerAccount data)
        {
            var custAcc = context.tbl_customers_account.FirstOrDefault(x => x.CustomerAccountID == data.CustomerAccountID);
            return Json(custAcc, JsonRequestBehavior.AllowGet);
        }
        // POST: Customer/UpdateAccount
        [HttpPost]
        public ActionResult UpdateAccount(CustomerAccount data)
        {
            return Json(customerRepository.UpdateAccount(data), JsonRequestBehavior.AllowGet);
        }
        private void DropDownListFors()
        {
            customerRepository.memberTitle = new[]
            {
                          new SelectListItem { Value = "Mr.", Text = "Mr." },
                          new SelectListItem { Value = "Ms.", Text = "Ms." },
                           new SelectListItem { Value = "Mrs.", Text = "Mrs." },
            };
            ViewBag.Genders = new SelectList(customerRepository.memberTitle, "Value", "Text");
            customerRepository.Nationality = new[]
                          {
                          new SelectListItem { Value = "Khmer", Text = "Khmer" },
                          new SelectListItem { Value = "Chinese", Text = "Chinese" },
                          new SelectListItem { Value = "French", Text = "French" },
                          new SelectListItem { Value = "Other", Text = "Other" },
                    };
            ViewBag.Nationalities = new SelectList(customerRepository.Nationality, "Value", "Text", "Other");
            customerRepository.Gender = new[]
                    {
                          new SelectListItem { Value = "Male", Text = "Male" },
                          new SelectListItem { Value = "Female", Text = "Female" },
                    };
            ViewBag.Genders = new SelectList(customerRepository.Gender, "Value", "Text");
            customerRepository.idType = new[]
            {
                              new SelectListItem { Value = "ID CARD", Text = "ID CARD" },
                              new SelectListItem { Value = "VIP", Text = "VIP" },
                              new SelectListItem { Value = "APP", Text = "APP" },
            };
            ViewBag.IdTypes = new SelectList(customerRepository.idType, "Value", "Text");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
using System;
using System.Linq;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private DataContext context;
        public HomeController()
        {
            this.context = new DataContext();
        }
        //public int IsStrongestAvenger()
        //{
        //    return context.Database.SqlQuery<int>("select dbo.FunctionName()").Single();
        //    //return context.IsStrongestAvenger.FromSqlInterpolated($"select   dbo.IsStrogestAvenger () as IsStrongestAvenger").FirstOrDefault().IsStrongestAvenger;
        //}
        public ActionResult Index()
        {
            //var IsTest = context.Database.SqlQuery<string>("select dbo.SF_Get_InvoiceNo()").Single();
            //var IsTest = context.Database.SqlQuery<int>("select dbo.FunctionName()").Single();
            //ViewData["EmployeeList2"]

            //foreach (var item in _userRoleProvider.GetRolesForUser())
            //{
            //    System.Diagnostics.Debug.WriteLine(item);
            //}IsInRole
            //System.Diagnostics.Debug.WriteLine(User.IsInRole("Admin"));
            //var InvoiceNo = context.Database.SqlQuery<string>("select dbo.SF_Get_InvoiceNo('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "')").Single();
            //var InvoiceNo = context.Database.SqlQuery<int>("select dbo.FunctionName()").Single();
            //ViewBag.InvoiceNo = InvoiceNo;
            return View();
        }
        //public ActionResult EmployeeList()
        //{
        //    var employeeModel = new List<EmployeeModel>();
        //    employeeModel.Add(new EmployeeModel() { EmpID = 101, EmpFirstName = "Surya", EmpLastName = "Kranthi" });
        //    employeeModel.Add(new EmployeeModel() { EmpID = 102, EmpFirstName = "Aditya", EmpLastName = "Roy" });
        //    employeeModel.Add(new EmployeeModel() { EmpID = 103, EmpFirstName = "Ravi", EmpLastName = "Kanth" });
        //    employeeModel.Add(new EmployeeModel() { EmpID = 104, EmpFirstName = "Anshuman", EmpLastName = "Sagara" });
        //    employeeModel.Add(new EmployeeModel() { EmpID = 105, EmpFirstName = "Jhansi", EmpLastName = "Naari" });

        //    //Example 1 Using ViewBag
        //    ViewBag.EmployeeList1 = employeeModel;

        //    //Example 2 Using ViewData
        //    ViewData["EmployeeList2"] = employeeModel;

        //    //Example 3 Using TempData
        //    TempData["EmployeeList3"] = employeeModel;

        //    return View();
        //}
        public ActionResult ChangeLanguage(string lang, string returnUrl)
        {
            Session["lang"] = lang;
            //var myUrl = returnUrl;
            return Redirect(returnUrl);
            //return RedirectToAction("Index", "Employees", new { language = lang });
        }
    }
}
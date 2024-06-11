using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Repositories;
using WebApplication.Helpers;
using System.Net;
using System.Data.Entity;

namespace WebApplication.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private DataContext context;
        private UserModel userModel;
        public RoleController()
        {
            this.context = new DataContext();
            this.userModel = new UserModel();
        }
        string UserID;
        string UserLevel;
        // GET: Role
        public ActionResult Index()
        {
            var users = context.tbl_useraccount.Where(x => x.UserID != "1").ToList();
            return View(users);
        }
        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_useraccount user = context.tbl_useraccount.FirstOrDefault(x => x.UserID == id);
            var RoleId = context.UserRoleMappings.Where(x => x.UserID == id).Select(x => x.RoleId).OrderBy(x => x).ToArray();
            var roles = context.Roles.ToList();
            ViewBag.Roles = new MultiSelectList(roles, "Id", "RoleName", RoleId);
            DropDownListForEdit(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        // POST: Role/Create
        [Route("Role/Create")]
        [HttpGet]
        public ActionResult Create()
        {
            DropDownListFor();
            return View();
        }
        // POST: Role/Create
        [Route("Role/Create")]
        [HttpPost]
        public ActionResult Create(FormCollection formValues)
        {
            UserID = formValues["UserId"];
            UserLevel = formValues["UserLevel"];
            if (formValues["UserLevel"] == "Admin")
            {
                var _userLevel = context.Roles.Where(x => x.Id == 1).FirstOrDefault();
                UserRoleMapping _t = new UserRoleMapping();
                _t.UserID = UserID;
                _t.RoleId = _userLevel.Id;
                context.UserRoleMappings.Add(_t);
                context.SaveChanges();
            }
            tbl_useraccount userModel = new tbl_useraccount();
            userModel.UserID = UserID;
            //userModel.UserLevel = UserLevel;
            this.UpdateUserLevel(userModel);

            string[] location = formValues["RoleId"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in location)
            {
                UserRoleMapping _t = new UserRoleMapping();
                _t.UserID = UserID;
                _t.RoleId = int.Parse(item);
                context.UserRoleMappings.Add(_t);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DropDownListForEdit(id);
            return View();
        }
        [HttpPost]
        public ActionResult Edit(FormCollection formValues)
        {
            if (ModelState.IsValid)
            {
                UserID = formValues["UserId"];
                UserLevel = formValues["UserLevel"];
                var roles = context.UserRoleMappings.Where(x => x.UserID == UserID).ToList();
                if(roles != null)
                {
                    context.UserRoleMappings.RemoveRange(roles);
                    context.SaveChanges();
                }

                tbl_useraccount userModel = new tbl_useraccount();
                userModel.UserID = UserID;
                //userModel.UserLevel = UserLevel;
                this.UpdateUserLevel(userModel);

                if (UserLevel == "Admin")
                {
                    var _userLevel = context.Roles.FirstOrDefault(x => x.Id == 2);
                    UserRoleMapping _t = new UserRoleMapping();
                    _t.UserID = UserID;
                    _t.RoleId = _userLevel.Id;
                    context.UserRoleMappings.Add(_t);
                    context.SaveChanges();
                }

                string[] location = formValues["RoleId"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in location)
                {
                    UserRoleMapping _t = new UserRoleMapping();
                    _t.UserID = UserID;
                    _t.RoleId = int.Parse(item);
                    context.UserRoleMappings.Add(_t);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }
        public void UpdateUserLevel(tbl_useraccount data)
        {
            tbl_useraccount user = context.tbl_useraccount.Find(data.UserID);
            //user.UserLevel = data.UserLevel;
            //user.StockID = data.StockID;
            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();
        }
        private void DropDownListForEdit(string id)
        {
            //var users = context.tbl_useraccount.Where(x => x.UserID != "1").ToList();
            var users = context.tbl_useraccount.ToList();
            ViewBag.Users = new SelectList(users, "Id", "Username");

            var RoleId = context.UserRoleMappings.Where(x => x.UserID == id).Select(x => x.RoleId).OrderBy(x => x).ToArray();
            var roles = context.Roles.Where(x => x.Id > 2).ToList();
            ViewBag.id = id;
            ViewBag.Roles = new MultiSelectList(roles, "Id", "RoleName", RoleId);

            userModel.UserLevel = new[]
            {
                new SelectListItem { Value = "Admin", Text = "Admin" },
                new SelectListItem { Value = "Normal", Text = "Normal" },
                //new SelectListItem { Value = "Seller", Text = "Seller" },
            };
            var user = context.tbl_useraccount.Where(x => x.UserID == id).Single();
            ViewBag.UserLevels = new SelectList(userModel.UserLevel, "Value", "Text", user.UserLevel == 1 ? "Admin" : "Normal");

            var stocks = context.tblStockTypes.ToList();
            ViewBag.Stocks = new SelectList(stocks, "StockID", "StockName", user.StockID);
        }
        private void DropDownListFor()
        {
            var users = context.tbl_useraccount.ToList();
            ViewBag.Users = new SelectList(users, "Id", "Username");

            userModel.UserLevel = new[]
            {
                new SelectListItem { Value = "Admin", Text = "Admin" },
                new SelectListItem { Value = "User", Text = "User" },
            };
            ViewBag.UserLevels = new SelectList(userModel.UserLevel, "Value", "Text");

            var roles = context.Roles.Where(x => x.Id > 2).ToList();
            ViewBag.Roles = new SelectList(roles, "Id", "RoleName");
        }
    }
}
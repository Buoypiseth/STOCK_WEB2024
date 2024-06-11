using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Repositories;
using WebApplication.Helpers;
using System;
using System.Collections.Generic;

namespace WebApplication.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private DataContext context;
        private UserRepository userRepository;
        public UserController()
        {
            this.context = new DataContext();
            this.userRepository = new UserRepository();
        }
        // GET: Users
        [Authorize(Roles = "User")]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData(BaseDatatable param)
        {
            var data = userRepository.GetForDataTable();
            if (!string.IsNullOrEmpty(param.sSearch) &&
                !string.IsNullOrWhiteSpace(param.sSearch))
            {
                data = data.Where(x => x.UserName.ToLower().Contains(param.sSearch.ToLower())).ToList();
            }
            var sortColumnIndex = HttpContext.Request.QueryString["iSortCol_0"];
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            data = userRepository.SortByColumnWithOrder(sortColumnIndex, sortDirection, data);

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
        // POST: User/Create
        [Authorize(Roles = "User")]
        [Route("User/Create")]
        [HttpPost]
        public JsonResult Create(UserModel data)
        {
            if(data.Password != data.ConfirmPassword)
            {
                return Json(data.Password + "" + data.ConfirmPassword, JsonRequestBehavior.AllowGet);
            }
            if (!string.IsNullOrEmpty(data.UserID))
            {
                return Json(userRepository.Update(data), JsonRequestBehavior.AllowGet);
            }
            var IsHas = context.tbl_useraccount.FirstOrDefault(x => x.UserName == data.Username);
            if (IsHas != null)
            {
                return Json("Your\'ve already entered that Username.", JsonRequestBehavior.AllowGet);
            }
            return Json(userRepository.Create(data), JsonRequestBehavior.AllowGet);
        }
        // POST: User/Edit/5
        [Authorize(Roles = "User")]
        [Route("User/Edit")]
        [HttpPost]
        public JsonResult Edit(UserModel data)
        {
            var user = context.tbl_useraccount.Find(data.UserID);
            UserModel info = new UserModel();
            info.UserID = user.UserID;
            info.FirstName = user.FirstName;
            info.LastName = user.LastName;
            info.Username = user.UserName;
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        // POST: User/ChangePassword
        [Route("User/ChangePassword")]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            var user = context.tbl_useraccount.FirstOrDefault(x => x.UserID == IUser.Id);
            UserModel model = new UserModel();
            model.UserID = user.UserID;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Username = user.UserName;
            return View(model);
        }
        // POST: User/ChangePassword
        [Route("User/ChangePassword")]
        [HttpPost]
        public JsonResult ChangePassword(UserModel data)
        {
            if (data.Password != data.ConfirmPassword)
            {
                return Json(data.Password + "" + data.ConfirmPassword, JsonRequestBehavior.AllowGet);
            }
            bool oldPassword = context.tbl_useraccount.Any(x => x.UserPwd == data.OldPassword && x.UserID == data.UserID);
            if (!oldPassword)
            {
                return Json("The password confirmation does not match.", JsonRequestBehavior.AllowGet);
            }
            return Json(userRepository.ChangePassword(data), JsonRequestBehavior.AllowGet);
        }
        private void DropDownListFor()
        {
            userRepository.UserLevel = new[]
                    {
                          new SelectListItem { Value = "Admin", Text = "Admin" },
                          new SelectListItem { Value = "User", Text = "User" },
                    };
            ViewBag.UserLevels = new SelectList(userRepository.UserLevel, "Value", "Text");
        }

        //// GET: Users/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = _context.Users.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        //// POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    User user = _context.Users.Find(id);
        //    _context.Users.Remove(user);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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

using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebApplication.Helpers;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class LoginController : Controller
    {
        private DataContext context;
        private UserModel user;
        public LoginController()
        {
            this.context = new DataContext();
            this.user = new UserModel();
        }
        // GET: Account
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(UserModel user)
        {
            try
            {
                var userLogin = context.tbl_useraccount.Where(u => u.UserName.ToLower() == user.Email.ToLower()
                                && u.UserPwd == user.Password)
                                .Select(x => new {
                                    Id = x.UserID,
                                    Name = x.UserName,
                                    UserLevel = x.UserLevel,
                                    LangCode = x.LangCode
                                }).FirstOrDefault();
                if (userLogin != null)
                {
                    FormsAuthentication.SetAuthCookie(userLogin.Name, false);
                    Session["lang"] = userLogin.LangCode;
                    CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                    serializeModel.Id = userLogin.Id;
                    serializeModel.Name = userLogin.Name;
                    serializeModel.UserLevel = (int)userLogin.UserLevel;

                    JavaScriptSerializer serializer = new JavaScriptSerializer();

                    string userData = serializer.Serialize(serializeModel);

                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                                                1,
                                                                userLogin.Name,
                                                                DateTime.Now,
                                                                DateTime.Now.AddDays(1),
                                                                false,
                                                                userData);

                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    cookie.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.Cookies.Add(cookie);
                    return RedirectToAction("Index", "Home");
                }
                TempData["flashError"] = "These credentials do not match our records.";
                return View();
            }
            catch (Exception e)
            {
                TempData["flashError"] = e;
                return View();
            }
        }
        public ActionResult SignUp()
        {
            DropDownListFor();
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SignUp([Bind(Include = "Id,Email,Password,Username")] tblUserAccount user)
        //{
        //    if (UserHelper.EmailExists(user.Email))
        //    {
        //        ViewBag.ErrorMessage = "Email address already registered.";
        //        return View();
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var keyNew = UserHelper.GeneratePassword(10);
        //            var password = UserHelper.EncodePassword(user.Password, keyNew);
        //            //user.Password = UserHelper.EncodePasswordMd5(user.Password);
        //            user.Password = password;
        //            user.CreateDate = DateTime.Now;
        //            user.ModifyDate = DateTime.Now;
        //            user.VCode = keyNew;
        //            //user.StockID = "0"
        //            context.Users.Add(user);
        //            context.SaveChanges();
        //            ModelState.Clear();
        //            return RedirectToAction("SignIn");
        //        }
        //        catch (Exception e)
        //        {
        //            ViewBag.ErrorMessage = "Some exception occured" + e;
        //            return View();
        //        }
        //    }
        //    return View();
        //}

        private void DropDownListFor()
        {
            user.UserLevel = new[]
                    {
                          new SelectListItem { Value = "Admin", Text = "Admin" },
                          new SelectListItem { Value = "Sale", Text = "Sale" },
                    };
            ViewBag.UserLevels = new SelectList(user.UserLevel, "Value", "Text");
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn");
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
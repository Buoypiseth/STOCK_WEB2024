using System;
using System.Linq;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Helpers;
using System.Data.Entity;

namespace WebApplication.Controllers
{
    [Authorize]
    public class LangController : Controller
    {
        private DataContext context;
        public LangController()
        {
            this.context = new DataContext();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChangeLanguage(string lang, string returnUrl)
        {
            try
            {
               var _model = context.tbl_useraccount.FirstOrDefault(x => x.UserID == IUser.Id);
                _model.LangCode = lang == "km" ? "en" : "km";
                context.Entry(_model).State = EntityState.Modified;
                context.SaveChanges();
                Session["lang"] = lang == "km" ? "en" : "km";
                var myUrl = returnUrl;
                return Redirect(returnUrl);
            }
            catch (Exception)
            {
                return Redirect(returnUrl);
            }
        }
    }
}
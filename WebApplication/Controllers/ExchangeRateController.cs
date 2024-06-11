using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Helpers;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Authorize]
    public class ExchangeRateController : Controller
    {
        private DataContext context;
        public ExchangeRateController()
        {
            this.context = new DataContext();
        }
        // GET: ExchangeRate
        public ActionResult Index()
        {
            return View();
        }
        // POST: ExchangeRate/GetExchangeRate/5
        [Route("ExchangeRate/GetExchangeRate")]
        [HttpGet]
        public JsonResult GetExchangeRate()
        {
            List<ExchangeRate> _getRate = new List<ExchangeRate>();
            var usdToKhr = Helper.ExchangeRate(0, "usdToKhr", "defaultRate");
            var usdToThb = Helper.ExchangeRate(0, "usdToThb", "defaultRate");
            var usdToKhrChange = Helper.ExchangeRate(0, "usdToKhrChange", "defaultRate");
            _getRate.Add(new ExchangeRate
            {
                usdTokhr = usdToKhr,
                usdToKhrChange = usdToKhrChange,
                usdToTHB = usdToThb
            });
            return Json(_getRate.FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }
    }
}
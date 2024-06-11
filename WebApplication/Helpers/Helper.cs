using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication.Models;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Security.Claims;

namespace WebApplication.Helpers
{
    [Authorize]
    public static class Helper
    {
        public static MvcHtmlString IsActive(this HtmlHelper html,string control,string action)
        {
            var routeData = html.ViewContext.RouteData;
            var routeAction = (string)routeData.Values["action"];
            var routeControl = (string)routeData.Values["controller"];

            // both must match
            //var returnActive = control == routeControl;
            var returnActive = control == routeControl && action == routeAction;

            return new MvcHtmlString(returnActive ? "nav-link active" : "nav-link inactive");
        }
        public static string IsReport(string param)
        {
            return param;
        }
        public static MvcHtmlString IsActionActive(this HtmlHelper html,string control,string action)
        {
            var routeData = html.ViewContext.RouteData;
            var routeAction = (string)routeData.Values["action"];
            var routeControl = (string)routeData.Values["controller"];

            // both must match
            //var returnActive = control == routeControl;
            var returnActive = control == routeControl && action == routeAction;

            return new MvcHtmlString(returnActive ? "nav-link active" : "nav-link inactive");
        }
        public static string AutoID()
        {
            return DateTime.Now.ToString("yyyyMMddhhmmss");
        }
        public static DateTime getDate(string val, string format = "yyyy-MM-dd")
        {
            if (val == "")
            {
                val = DateTime.Now.ToString(format);
            }
            return DateTime.ParseExact(val, format, null);
        }
        public static DateTime getMonth(string val, string format = "yyyy-MM")
        {
            if (val == "")
            {
                val = DateTime.Now.ToString(format);
            }
            return DateTime.ParseExact(val, format, null);
        }
        public static DateTime getYear(string val, string format = "yyyy")
        {
            if (val == "")
            {
                val = DateTime.Now.ToString(format);
            }
            return DateTime.ParseExact(val, format, null);
        }
        public static DateTime getLastDayInMonth(string val, string format = "yyyy-MM")
        {
            if (val == "")
            {
                val = DateTime.Now.ToString(format);
            }
            DateTime _DateTime = DateTime.ParseExact(val, format, null);
            string _Month = _DateTime.Month.ToString();
            string _Year = _DateTime.Year.ToString();
            int _Day = DateTime.DaysInMonth(int.Parse(_Year), int.Parse(_Month));
            DateTime lastDay = new DateTime(int.Parse(_Year), int.Parse(_Month), _Day, 0, 0, 0);
            return lastDay;
        }
        public static float ExchangeRate(float val, string type, string defaultRate = "")
        {
            using (var _context = new DataContext())
            {
                var dataRate = _context.tbl_exchangerates.Where(x => x.exchID == type).FirstOrDefault();
                float usdToPrice = 0;
                if (defaultRate == "defaultRate")
                {
                    usdToPrice = (float)dataRate.rates;
                    return usdToPrice;
                }
                usdToPrice = (float)dataRate.rates * val;
                return usdToPrice;
            }
        }
    }
}
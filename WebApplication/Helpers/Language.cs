using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Helpers
{
    public static class Language
    {
        public static string Lang()
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["lang"] == null || context.Session["lang"].ToString() == "km")
            {
                return "EN";
            }
            return "KH";
        }
        public static string LangCode()
        {
            HttpContext context = HttpContext.Current;
            return context.Session["lang"] == null ? "km" : context.Session["lang"].ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Globalization;
using System.Threading;
using WebApplication.Controllers;
using System.Web.Security;
using System.Web.Script.Serialization;
using WebApplication.Helpers;

namespace WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        //protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        //{
        //    HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

        //    if (authCookie != null)
        //    {
        //        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

        //        JavaScriptSerializer serializer = new JavaScriptSerializer();

        //        CustomPrincipalSerializeModel serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(authTicket.UserData);

        //        CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
        //        newUser.Id = serializeModel.Id;
        //        newUser.Name = serializeModel.Name;
        //        newUser.Type = serializeModel.Type;
        //        HttpContext.Current.User = newUser;
        //    }
        //}
        protected void Application_AcquireRequestState(Object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            //var languageSession = "en";
            var languageSession = "km";
            if (context != null && context.Session != null)
            {
                //languageSession = context.Session["lang"] != null ? context.Session["lang"].ToString() : "en";
                languageSession = context.Session["lang"] != null ? context.Session["lang"].ToString() : "km";
            }
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageSession);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(languageSession);
        }
    }
}

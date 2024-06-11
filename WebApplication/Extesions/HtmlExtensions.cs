using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace WebApplication.Extesions
{
    public static class HtmlExtensions
    {
        public static HtmlString BuildBreadcrumbNavigation(this HtmlHelper helper)
        {
            // optional condition: I didn't wanted it to show on home and account controller
            //if (helper.ViewContext.RouteData.Values["controller"].ToString() == "Home" ||
            //    helper.ViewContext.RouteData.Values["controller"].ToString() == "Account")
            //{
            //    return new HtmlString(string.Empty);
            //}
            if (helper.ViewContext.RouteData.Values["controller"].ToString() == "Account")
            {
                return new HtmlString(string.Empty);
            }

            StringBuilder breadcrumb = new StringBuilder("<ol class='breadcrumb float-right'><li class='breadcrumb-item'>").Append(helper.ActionLink("Home", "Index", "Home").ToHtmlString()).Append("</li>");

            if (helper.ViewContext.RouteData.Values["action"].ToString() != "Index")
            {
                breadcrumb.Append("<li class='breadcrumb-item'>");
                breadcrumb.Append(helper.ActionLink(helper.ViewContext.RouteData.Values["controller"].ToString(),
                                                   "Index",
                                                   helper.ViewContext.RouteData.Values["controller"].ToString()));
                breadcrumb.Append("</li>");

                breadcrumb.Append("<li class='breadcrumb-item'>");
                breadcrumb.Append(helper.ViewContext.RouteData.Values["action"].ToString());
                breadcrumb.Append("</li>");
            }
            else
            {
                breadcrumb.Append("<li class='breadcrumb-item'>");
                breadcrumb.Append(helper.ViewContext.RouteData.Values["controller"].ToString());
                breadcrumb.Append("</li>");
            }

            //if (helper.ViewContext.RouteData.Values["action"].ToString() != "Index")
            //{
            //    breadcrumb.Append("<li class='breadcrumb-item'>");
            //    breadcrumb.Append(helper.ActionLink(helper.ViewContext.RouteData.Values["action"].ToString(),
            //                                        helper.ViewContext.RouteData.Values["action"].ToString(),
            //                                        helper.ViewContext.RouteData.Values["controller"].ToString()));
            //    breadcrumb.Append("</li>");
            //}
            return new HtmlString(breadcrumb.Append("</ol>").ToString());
        }
    }
}
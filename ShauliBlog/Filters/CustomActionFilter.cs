using ShauliBlog.Utils;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace ShauliBlog.Filters
{
    public class CustomActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
                filterContext.HttpContext.Trace.Write("(Logging Filter)Exception thrown");

            filterContext.Controller.ViewBag.IsAdmin = ((ClaimsIdentity)filterContext.HttpContext.User.Identity).Claims
                                                        .Where(c => c.Type == ClaimTypes.Role)
                                                        .Select(c => c.Value)
                                                        .Contains(Consts.ADMIN_ROLE);
            base.OnActionExecuted(filterContext);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DatVeXemPhim2023.Models.Authentication
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("Username") == null)
            {
                if (context.HttpContext.Session.GetString("LoaiUser") == "khach")
                {
                    context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller", "Access" },
                        {"Action", "Login" }
                    }
                );
                }
                else if (context.HttpContext.Session.GetString("LoaiUser") == "admin")
                {
                    context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controllers", "HomeAdmin" },
                        {"Action", "Login" }
                    }
                );
                }
            }
        }
    }
}

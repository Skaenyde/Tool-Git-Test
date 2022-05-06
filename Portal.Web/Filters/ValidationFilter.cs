using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Portal.Business;
using Portal.Common.Interfaces;
using Portal.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Portal.Web.Filters
{
    public class ValidationFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var _userContextLogic = context.HttpContext.RequestServices.GetService<IUserContextLogic>();
            // Module property needs to be declared on the controller. Null reference error otherwise.
            var module = context.Controller.GetType().GetProperty("Module").GetValue(context.Controller);


            List<Module> AvailableModules = _userContextLogic.GetRoleAvailableModules(context.HttpContext.User.FindFirstValue(ClaimTypes.Role)).Result;
            var accessDenied = new RedirectToRouteResult(new
            {
                action = "AccessDenied",
                controller = "Error"
            });

            if (!AvailableModules.Any(x => x.ModuleId == (int)module))
                context.Result = accessDenied;
        }
    }
}

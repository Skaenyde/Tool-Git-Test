using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Common.Enums;
using Portal.Common.Models;
using SmartBreadcrumbs.Attributes;
using System.Security.Claims;
using Portal.Common.Interfaces;

namespace Portal.Web.Controllers
{
    public class BaseController : Controller
    {

        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly IUserContextLogic UserContextLogic;

        public BaseController(IHttpContextAccessor httpContextAccessor, IUserContextLogic userContextLogic)
        {
            HttpContextAccessor = httpContextAccessor;
            UserContextLogic = userContextLogic;
        }

        public bool RoleHasAccessToModule(AppModule module)
        {
            List<Module> AvailableModules =  UserContextLogic.GetRoleAvailableModules(HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role)).Result;

            if (AvailableModules.Any(x => x.ModuleId == (int)module))
                return true;
            else
                return false;
        }


    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.Common.ViewModels;
using SmartBreadcrumbs.Attributes;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Portal.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using Portal.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
//Test Comment
namespace Portal.Web.Controllers
{
    [Authorize]
    [DefaultBreadcrumb("Dashboard")]
    public class HomeController : Controller 
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContextLogic _userContextLogic;

        public HomeController(IHttpContextAccessor httpContextAccessor, IUserContextLogic userContextLogic)
        {
            _httpContextAccessor = httpContextAccessor;
            _userContextLogic = userContextLogic;
        }

        //[DefaultBreadcrumb("Home")]
        public async Task<IActionResult> Index()
        {
            var homeVM = new HomeViewModel()
            {
                AvailableModules = await _userContextLogic.GetRoleAvailableModules(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role))
            };
            return View(homeVM);
        }

    }
}

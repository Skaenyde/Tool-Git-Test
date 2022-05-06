using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portal.Common.Models;
using Portal.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ViewComponentSample.ViewComponents
{
    public class AsideViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContextLogic _userContextLogic;

        public AsideViewComponent(IHttpContextAccessor httpContextAccessor, IUserContextLogic userContextLogic)
        {
            _httpContextAccessor = httpContextAccessor;
            _userContextLogic = userContextLogic;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var modules = await _userContextLogic.GetRoleAvailableModules(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role));

            return View(modules);
        }

    }
}
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
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContextLogic _userContextLogic;

        public HeaderViewComponent(IHttpContextAccessor httpContextAccessor, IUserContextLogic userContextLogic)
        {
            _httpContextAccessor = httpContextAccessor;
            _userContextLogic = userContextLogic;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userPassport = new UserPassport()
            {
                UserName = _httpContextAccessor.HttpContext.User.Identities.ToList()[0].Claims.ToList()[1].Value,
                RoleDesc = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role)
            };

            return View(userPassport);
        }

    }
}
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
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Common.Enums;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net.Http;
using Portal.Web.Filters;
//Test Comment
namespace Portal.Web.Controllers
{
    [Authorize]
    [Breadcrumb("Permissions")]

    public class PermissionController : Controller 
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContextLogic _userContextLogic;
        private readonly IPermissionLogic _permissionLogic;

        public AppModule Module { get; set; } = AppModule.Permission;


        public PermissionController(IHttpContextAccessor httpContextAccessor, IUserContextLogic userContextLogic, IPermissionLogic permissionLogic)
        {
            _httpContextAccessor = httpContextAccessor;
            _userContextLogic = userContextLogic;
            _permissionLogic = permissionLogic;
        }

        [ValidationFilter]
        public async Task<IActionResult> Index()
        {
            var viewModel = new PermissionViewModel()
            {
                SelectRoles = await _permissionLogic.GetUnassignedRolesRCA(),
                SelectPermissions = await _permissionLogic.GetModulePermissionTypes()

            };
            return View(viewModel);
        }

        //public async Task<IActionResult> GetRolePermissions(string roleId)
        //{
        //    var viewModel = new PermissionViewModel()
        //    {
        //        SpecialtyPermissionId = await _permissionLogic.GetRolePermissionByModule(roleId, (int)AppModule.Specialty),
        //        PharmacyPermissionId = await _permissionLogic.GetRolePermissionByModule(roleId, (int)AppModule.Pharmacy)
        //    };

        //    return new PartialViewResult
        //    {
        //        ViewName = "partials/_dropdowns",
        //        ViewData = new ViewDataDictionary<PermissionViewModel>(ViewData, viewModel)
        //    };
        //}

        //public async Task<IActionResult> SaveRolePermissions(int roleId, int specialtyPermissionTypeId, int pharmacyPermissionTypeId)
        //{
        //    var permissions = new List<RolePermission>
        //    {
        //        new RolePermission() { PermissionTypeId = specialtyPermissionTypeId, ModuleId = (int)AppModule.Specialty },
        //        new RolePermission() { PermissionTypeId = pharmacyPermissionTypeId, ModuleId = (int)AppModule.Pharmacy }
        //    };



        //    //Initialize response variable.
        //    _permissionLogic.SaveRolePermissions(roleId, permissions);
        //    return Json(Ok());

        //    //if (response.Success)
        //    //    return Json(Ok());
        //    //else
        //    //{
        //    //    //This message will be caught by the exception middleware at Startup.cs
        //    //    //Which will transform into a JSON response to be received at Ajax call.
        //    //    throw new HttpRequestException(response.Message);
        //    //}

        //}

        public async Task<IActionResult> CreateNewRoleCompanyAccess(int roleId, string roleName, bool chkMMMFL, bool chkMMMMH, bool chkMMM, bool chkPMC)
        {

            _permissionLogic.CreateNewRoleCompanyAccess(roleId, roleName, chkMMMFL, chkMMMMH, chkMMM, chkPMC);

            return Json(Ok());

        }

        public async Task<IActionResult> CreateNewRoleModuleAccess(string newRoleName, int radProviderDirectory, int radPharmacyDirectory, int radDirectorySectionConfig)
        {

            _permissionLogic.CreateNewRoleModuleAccess(newRoleName, radProviderDirectory, radPharmacyDirectory, radDirectorySectionConfig);

            return Json(Ok());

        }

        public async Task<IActionResult> UpdateRoleCompanyAccess(int roleCompanyAccessId, bool chkRoleCompanyAccessIsActive)
        {

            _permissionLogic.UpdateRoleCompanyAccess(roleCompanyAccessId, chkRoleCompanyAccessIsActive);

            return Json(Ok());
        }

        public async Task<IActionResult> UpdateRoleModuleAccess(int rolePermissionId, int permissionTypeId)
        {

           await _permissionLogic.UpdateRoleModuleAccess(rolePermissionId, permissionTypeId);

            return Json(Ok());
        }

        public async Task<DataTableResponse<List<RoleCompanyAccess>>> GetRoleCompanyAccessList()
        {
            var dataTable = new DataTableResponse<List<RoleCompanyAccess>>
            {
                data = await _permissionLogic.LoadRoleCompanyAccessList(),
            };

            return dataTable;
        }


        public async Task<DataTableResponse<List<RolePermission>>> GetRoleModuleAccessList()
        {
            var dataTable = new DataTableResponse<List<RolePermission>>
            {
                data = await _permissionLogic.LoadRoleModuleAccessList(),
            };
            return dataTable;
        }
    }
}

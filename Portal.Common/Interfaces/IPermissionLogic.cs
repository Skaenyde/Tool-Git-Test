using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Portal.Common.ViewModels;

namespace Portal.Common.Interfaces
{
    public interface IPermissionLogic
    {
        Task<int> GetRolePermissionByModule(string roleId, int moduleId);
        //void SaveRolePermissions(int roleId, List<RolePermission> permissions);
        Task<List<RoleCompanyAccess>> LoadRoleCompanyAccessList();
        void CreateNewRoleCompanyAccess(int roleId, string roleName, bool chkMMMFL, bool chkMMMMH, bool chkMMM, bool chkPMC);
        //Task<List<SelectListItem>> GetRoles();
        Task<List<SelectListItem>> GetUnassignedRolesRCA();
        void UpdateRoleCompanyAccess(int roleCompanyAccessId, bool chkRoleCompanyAccessIsActive);
        Task<List<RolePermission>> LoadRoleModuleAccessList();
        Task<List<SelectListItem>> GetModulePermissionTypes();
        Task UpdateRoleModuleAccess(int rolePermissionId, int permissionTypeId);
        void CreateNewRoleModuleAccess(string newRoleName, int radProviderDirectory, int radPharmacyDirectory, int radDirectorySectionConfig);
    }
}

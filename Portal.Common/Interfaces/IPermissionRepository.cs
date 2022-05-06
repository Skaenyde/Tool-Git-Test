using Portal.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Common.Interfaces
{
    public interface IPermissionRepository
    {
        Task<RolePermission> GetRolePermissionByModule(string roleId, int moduleId);
        //Task<GenericResponse<int>>UpdateRolePermission(int roleId, int moduleId, int permissionTypeId);
        Task<List<RoleCompanyAccess>> GetRoleCompanyAccessList();
        //Task<List<Role>> GetRoles();

        Task<List<Role>> GetUnassignedRolesRCA();

        Task<GenericResponse<int>> PostRoleCompanyAccess(RoleCompanyAccess newRecord);
        Task PutRoleCompanyAccess(int roleCompanyAccessId, string recordActionType);
        Task<List<RolePermission>> GetRoleModuleAccessList();
        Task<List<PermissionType>> GetModulePermissionTypes();
        Task PutBiadminToolRolePermission(int rolePermissionId, int permissionTypeId);
        Task<GenericResponse<int>> PostRoleModuleAccess(string newRoleName, int radProviderDirectory, int radPharmacyDirectory, int radDirectorySectionConfig);
    }
}

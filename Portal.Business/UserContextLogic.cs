using System;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Common.Interfaces;
using Portal.Common.Models;
using Portal.Common.Enums;
using Portal.Data;


namespace Portal.Business
{
    public class UserContextLogic : IUserContextLogic
    {

        private readonly IUserContextRepository _userContextRepository;


        public UserContextLogic(IUserContextRepository userContextRepo)
        {
            _userContextRepository = userContextRepo;
        }


        public async Task<List<Module>> GetRoleAvailableModules(string role)
        {
            var rolePermissions = await _userContextRepository.GetRolePermissions(role);
            var modules = new List<Module>();

            foreach (var rolePermission in rolePermissions)
            {
                if (!modules.Exists(x => x.ModuleDesc == rolePermission.ModuleDesc) && rolePermission.PermissionTypeDesc != "None")
                {
                    modules.Add(new Module() { ModuleId = rolePermission.ModuleId, ModuleDesc = rolePermission.ModuleDesc });
                }
            }

            return modules;
        }

        public async Task<List<RolePermission>> GetRolePermissions(string role)
        {
            return await _userContextRepository.GetRolePermissions(role);
        }

        public async Task<Common.Enums.PermissionType> GetRoleModulePermission(string role, AppModule module)
        {
            var permissions = await _userContextRepository.GetRolePermissions(role);
            var modulePermission = new Common.Enums.PermissionType();
            
            if (permissions.Exists(x => x.ModuleId == (int)module && x.PermissionTypeId == (int)Common.Enums.PermissionType.edit))
                modulePermission = Common.Enums.PermissionType.edit;
            else if (permissions.Exists(x => x.ModuleId == (int)module && x.PermissionTypeId == (int)Common.Enums.PermissionType.read))
                modulePermission = Common.Enums.PermissionType.read;
            else
                modulePermission = Common.Enums.PermissionType.none;
            return modulePermission;
        }
    }
}

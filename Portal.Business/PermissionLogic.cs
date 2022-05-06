using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Common.Interfaces;
using Portal.Common.Models;
using Portal.Common.ViewModels;
using Portal.Data;

namespace Portal.Business
{
    public class PermissionLogic : IPermissionLogic
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IGeneralRepository _generalRepository;
        private readonly IUserContextRepository _userContextRepository;


        public PermissionLogic(IPermissionRepository permissionRepo, IGeneralRepository generalRepo, IUserContextRepository userContextRepo)
        {
            _permissionRepository = permissionRepo;
            _generalRepository = generalRepo;
            _userContextRepository = userContextRepo;
        }

        public async Task<int> GetRolePermissionByModule (string roleId, int moduleId)
        {
            var  permission = await _permissionRepository.GetRolePermissionByModule(roleId, moduleId);

            try
            {
                return permission.PermissionTypeId;

            }
            catch (NullReferenceException e)
            {
                return 0;
            }
        }

        //public async Task<List<SelectListItem>> GetRoles()
        //{
        //    var roles = await _permissionRepository.GetRoles();
        //    var options = new List<SelectListItem>();

        //    foreach (var role in roles)
        //    {
        //        var item = new SelectListItem { Value = role.RoleId.ToString(), Text = role.RoleDesc };
        //        options.Add(item);
        //    }
        //    return options;
        //}


        public async Task<List<SelectListItem>> GetUnassignedRolesRCA()
        {
            var roles = await _permissionRepository.GetUnassignedRolesRCA();
            var options = new List<SelectListItem>();

            foreach (var role in roles)
            {
                var item = new SelectListItem { Value = role.RoleId.ToString(), Text = role.RoleDesc };
                options.Add(item);
            }
            return options;
        }


        public async Task<List<SelectListItem>> GetModulePermissionTypes()
        {
            var permissions = await _permissionRepository.GetModulePermissionTypes();
            var options = new List<SelectListItem>();

            foreach (var permission in permissions)
            {
                var item = new SelectListItem { Value = permission.PermissionTypeId.ToString(), Text = permission.PermissionTypeDesc};
                options.Add(item);
            }
            return options;
        }


        public async Task<List<RoleCompanyAccess>> LoadRoleCompanyAccessList()
        {
            var results = await _permissionRepository.GetRoleCompanyAccessList();


            return results;
        }

        public async Task<List<RolePermission>> LoadRoleModuleAccessList()
        {
            var results = await _permissionRepository.GetRoleModuleAccessList();


            return results;
        }

        public async void CreateNewRoleCompanyAccess(int roleId, string roleName, bool chkMMMFL, bool chkMMMMH, bool chkMMM, bool chkPMC)
        {
            //todo: Aquí tengo que crear una lista donde contenga los records por cada checkbox y así enviarlos a la tabla de permisos en la base de datos



            //MMMFL
            var MMMFL = new RoleCompanyAccess
            {
                RoleAccessDesc = roleName + " -  MMMFL" ,
                RoleId = roleId,
                CompanyId = 2,
                RecordActionType = chkMMMFL ? "I" : "D" 
            };

            var MMMFL_result= await _permissionRepository.PostRoleCompanyAccess(MMMFL);

            //MMMMH
            var MMMMH = new RoleCompanyAccess
            {
                RoleAccessDesc = roleName + " -  MMMMH",
                RoleId = roleId,
                CompanyId = 3,
                RecordActionType = chkMMMMH ? "I" : "D"
            };

            var MMMMH_result = await _permissionRepository.PostRoleCompanyAccess(MMMMH);

            //MMM
            var MMM = new RoleCompanyAccess
            {
                RoleAccessDesc = roleName + " -  MMM",
                RoleId = roleId,
                CompanyId = 4,
                RecordActionType = chkMMM ? "I" : "D"
            };

            var MMM_result = await _permissionRepository.PostRoleCompanyAccess(MMM);

            //PMC
            var PMC = new RoleCompanyAccess
            {
                RoleAccessDesc = roleName + " -  PMC",
                RoleId = roleId,
                CompanyId = 5,
                RecordActionType = chkPMC ? "I" : "D"
            };

            var PMC_result = await _permissionRepository.PostRoleCompanyAccess(PMC);
        }


        public async void CreateNewRoleModuleAccess(string newRoleName, int radProviderDirectory, int radPharmacyDirectory, int radDirectorySectionConfig)
        {

            var response =  await _permissionRepository.PostRoleModuleAccess(newRoleName, radProviderDirectory, radPharmacyDirectory, radDirectorySectionConfig);
        }

        //public async void SaveRolePermissions(int roleId, List<RolePermission> permissions)
        //{
            
        //        foreach (var permission in permissions)
        //        {
        //            await _permissionRepository.UpdateRolePermission(roleId, permission.ModuleId, permission.PermissionTypeId);
        //        }
                
        //}

        public async void UpdateRoleCompanyAccess(int roleCompanyAccessId, bool chkRoleCompanyAccessIsActive)
        {
            var recordActionType = chkRoleCompanyAccessIsActive ? "I" : "D";

            await _permissionRepository.PutRoleCompanyAccess(roleCompanyAccessId, recordActionType);
        }


        public async Task UpdateRoleModuleAccess(int rolePermissionId, int permissionTypeId)
        {
            //Something

            await _permissionRepository.PutBiadminToolRolePermission(rolePermissionId, permissionTypeId);
        }
    }
}

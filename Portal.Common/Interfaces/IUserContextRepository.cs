using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Common.Interfaces
{
    public interface IUserContextRepository
    {

        //List of all permissions for an user.
        Task<List<RolePermission>> GetRolePermissions(string Role);

        //Task<bool> ModulePermission(ModuleEnum module);


    }
}

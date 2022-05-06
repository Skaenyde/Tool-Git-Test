using Portal.Common.Enums;
using Portal.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portal.Common.Interfaces
{
    public interface IUserContextLogic
    {
        //Task<UserPassport> GetUserPassport(string userName);
        Task<List<Module>> GetRoleAvailableModules(string role);
        Task<List<RolePermission>> GetRolePermissions(string role);
        Task<Enums.PermissionType> GetRoleModulePermission(string role, AppModule module);
    }
}

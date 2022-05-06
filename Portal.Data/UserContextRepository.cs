using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Portal.Common.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using Portal.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Portal.Data
{
    public class UserContextRepository : BaseRepository, IUserContextRepository
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextRepository(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }



        /// <summary>
        /// Get module permissions for the current user role.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<List<RolePermission>> GetRolePermissions(string role)
        {
            try
            {
                IEnumerable<RolePermission> rows;

                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<RolePermission>("BiAdminToolRolePermission_GetBy_Role",
                        new { role },
                        commandType: CommandType.StoredProcedure);
                }
                return rows.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
    }
}

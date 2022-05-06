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
using System.Security.Claims;
using Portal.Common.Enums;


namespace Portal.Data
{
    public class BaseRepository
    {

        #region Initialization region

        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public BaseRepository(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        public string CurrentUser => _httpContextAccessor.HttpContext.User.Identities.ToList()[0].Claims.ToList()[1].Value;

        public string CurrentRole
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            }
        }

        public List<Company> CompaniesForUser
        {
            get
            {
                return GetCompanies().GetAwaiter().GetResult();
            }
        }

        public string CurrentUserCompanyAccessId
        {
            get
            {
                return GetRoleCompanyAccessIds(CurrentRole).GetAwaiter().GetResult();
            }
        }

        // This both method works as a Logic layer which transforms the data before assigning it to a property.
        // This is done here because it will be needed across all the application requests to the DB.
        // Yes. It is braking the code sepatarion of concerns for simplicity.
        private async Task<string> GetRoleCompanyAccessIds(string CurrentRole)
        {
            try
            {
                if (CurrentRole == "Admin")
                {
                    var companies = await GetCompanies();
                    string commaStr = string.Empty;

                    foreach (var company in companies)
                    {
                        commaStr = commaStr + company.CompanyId.ToString() + ',';
                    }

                    commaStr = commaStr + "-1";

                    //commaStr = commaStr.Remove(commaStr.Length - 1, 1);

                    return commaStr;


                }
                else if ((CurrentRole == "Provider_Group") || (CurrentRole == "Provider_Group_Admin"))
                {
                    var companies = await GetRoleCompanyAccess(CurrentRole);
                    string commaStr = string.Empty;

                    foreach (var company in companies)
                    {
                        commaStr = commaStr + company.CompanyId.ToString() + ',';
                    }
                    //commaStr = commaStr.Remove(commaStr.Length - 1, 1);
                    commaStr = commaStr + "-1";

                    return commaStr;

                }
                else
                {
                    var companies = await GetRoleCompanyAccess(CurrentRole);
                    string commaStr = string.Empty;

                    foreach (var company in companies)
                    {
                        commaStr = commaStr + company.CompanyId.ToString() + ',';
                    }
                    commaStr = commaStr.Remove(commaStr.Length - 1, 1);
                    return commaStr;

                }
            }
            catch (Exception e)
            {
                var test = "t";
                return test;
            }

        }

        public async Task<List<RoleCompanyAccess>> GetRoleCompanyAccess(string role)
        {
            try
            {
                IEnumerable<RoleCompanyAccess> rows;

                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<RoleCompanyAccess>("BiAdminToolRoleCompanyAccess_GetBy_Role",
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

        public async Task<List<Company>> GetCompanies()
        {
            try
            {
                IEnumerable<Company> rows;
                if (CurrentRole == "Admin")
                {

                    var parameters = new DynamicParameters();
                    parameters.Add("@CurrentUserRole", CurrentRole, DbType.String, ParameterDirection.Input);
                    parameters.Add("@CurrentUserCompanyAccessId", String.Empty, DbType.String, ParameterDirection.Input);

                    using (var connection = Connection)
                    {
                        connection.Open();
                        rows = await connection.QueryAsync<Company>("Company_GetFor_Select",
                                        parameters,
                                        commandType: CommandType.StoredProcedure);
                    }
                }
                else
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@CurrentUserRole", CurrentRole, DbType.String, ParameterDirection.Input);
                    parameters.Add("@CurrentUserCompanyAccessId", CurrentUserCompanyAccessId, DbType.String, ParameterDirection.Input);


                    using (var connection = Connection)
                    {
                        connection.Open();

                        rows = await connection.QueryAsync<Company>("Company_GetFor_Select",
                                        parameters,
                                        commandType: CommandType.StoredProcedure);
                    }
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

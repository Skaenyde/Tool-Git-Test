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
    public class PermissionRepository : BaseRepository, IPermissionRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;


        public PermissionRepository(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;

        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public async Task<List<RoleCompanyAccess>> GetRoleCompanyAccessList()
        {
            try
            {
                IEnumerable<RoleCompanyAccess> rows;

                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<RoleCompanyAccess>("BiAdminToolRoleCompanyAccess_GetAll",
                                    null,
                                    commandType: CommandType.StoredProcedure);
                }
                return rows.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
        public async Task<List<RolePermission>> GetRoleModuleAccessList()
        {
            try
            {
                IEnumerable<RolePermission> rows;

                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<RolePermission>("BiAdminToolRolePermission_GetAll",
                                    null,
                                    commandType: CommandType.StoredProcedure);
                }
                return rows.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public async Task<List<RolePermission>> GetAllRolePermissions()
        {
            try
            {
                IEnumerable<RolePermission> rows;

                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<RolePermission>("BiAdminToolRolePermission_GetAll",
                                    new { CurrentUserCompanyAccessId },
                                    commandType: CommandType.StoredProcedure);
                }
                return rows.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public async Task<RolePermission> GetRolePermissionByModule(string roleId, int moduleId)
        {
            try
            {
                var result = new RolePermission();
                var parameters = new DynamicParameters();
                parameters.Add("@RoleId", roleId, DbType.String, ParameterDirection.Input);
                parameters.Add("@ModuleId", moduleId, DbType.Int32, ParameterDirection.Input);

                using (var connection = Connection)
                {
                    connection.Open();

                    result = await connection.QueryFirstOrDefaultAsync<RolePermission>("BiAdminToolRolePermission_GetBy_RoleAndModule",
                                    parameters,
                                    commandType: CommandType.StoredProcedure);
                }
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        //public async Task<GenericResponse<int>> UpdateRolePermission(int roleId, int moduleId, int permissionTypeId)
        //{
        //    var response = new GenericResponse<int>(0);

        //    try
        //    {
        //        using (var connection = Connection)
        //        {
        //            connection.Open();

        //            DynamicParameters parameter = new DynamicParameters();

        //            parameter.Add("@RoleId", roleId, DbType.Int32, ParameterDirection.Input);
        //            parameter.Add("@ModuleId", moduleId, dbType: DbType.Int32, direction: ParameterDirection.Input);
        //            parameter.Add("@PermissionTypeId", permissionTypeId, dbType: DbType.Int32, direction: ParameterDirection.Input);
        //            parameter.Add("@UpdateBy", CurrentUser, dbType: DbType.String, direction: ParameterDirection.Input);



        //            response.Result = connection.Execute("BiAdminToolRolePermission_UpdateBy_RoleModuleAndPermission", parameter, commandType: CommandType.StoredProcedure);
        //            response.Success = true;
        //            return response;
        //        }
        //    }
        //    catch (SqlException e)
        //    {
        //        response.Success = false;
        //        response.Message = e.Message;

        //        return response;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message, e.InnerException);
        //    }
        //}

        //public async Task<List<Role>> GetRoles()


        //{
        //    try
        //    {
        //        IEnumerable<Role> rows;

        //        using (var connection = Connection)

        //        {
        //            connection.Open();

        //            rows = await connection.QueryAsync<Role>("BiAdminToolRole_GetAll",
        //                            null,
        //                            commandType: CommandType.StoredProcedure);
        //        }
        //        return rows.ToList();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message, e.InnerException);
        //    }
        //}

        public async Task<List<Role>> GetUnassignedRolesRCA()


        {
            try
            {
                IEnumerable<Role> rows;

                using (var connection = Connection)

                {
                    connection.Open();

                    rows = await connection.QueryAsync<Role>("BiAdminToolRole_GetUnassignedRoleAccess",
                                    null,
                                    commandType: CommandType.StoredProcedure);
                }
                return rows.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public async Task<List<PermissionType>> GetModulePermissionTypes()


        {
            try
            {
                IEnumerable<PermissionType> rows;

                using (var connection = Connection)

                {
                    connection.Open();

                    rows = await connection.QueryAsync<PermissionType>("BiAdminToolPermissionType_ForSelect_GetPermissions",
                                    null,
                                    commandType: CommandType.StoredProcedure);
                }
                return rows.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public async Task<GenericResponse<int>> PostRoleCompanyAccess(RoleCompanyAccess newRecord)
        {
            var response = new GenericResponse<int>(0);
            try
            {
                using (var connection = Connection)
                {
                    connection.Open();

                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@RoleAccessDesc", newRecord.RoleAccessDesc, DbType.String, ParameterDirection.Input);
                    parameter.Add("@RoleId", newRecord.RoleId, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@CompanyId", newRecord.CompanyId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    parameter.Add("@RecordActiontype", newRecord.RecordActionType, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@CreatedBy", CurrentUser, dbType: DbType.String, direction: ParameterDirection.Input);



                    connection.Execute("BiAdminToolRoleCompanyAccess_Insert", parameter, commandType: CommandType.StoredProcedure);
                    response.Success = true;
                    return response;
                }
            }
            catch (SqlException e)
            {
                response.Success = false;
                response.Message = e.Message;

                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public async Task<GenericResponse<int>> PostRoleModuleAccess(string newRoleName, int radProviderDirectory, int radPharmacyProvider, int radDirectorySectionConfig)
        {
            var response = new GenericResponse<int>(0);
            try
            {
                using (var connection = Connection)
                {
                    connection.Open();

                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@NewRoleName", newRoleName, DbType.String, ParameterDirection.Input);
                    parameter.Add("@ProviderDirectoryPermissionId", radProviderDirectory, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@PharmacyDirectoryPermissionId", radPharmacyProvider, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@DirectorySectionConfigPermissionId", radDirectorySectionConfig, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@User", CurrentUser, DbType.String, ParameterDirection.Input);



                    connection.Execute("BiAdminToolRoleModuleAccess_Create", parameter, commandType: CommandType.StoredProcedure);
                    response.Success = true;
                    return response;
                }
            }
            catch (SqlException e)
            {
                response.Success = false;
                response.Message = e.Message;

                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }


        public async Task PutRoleCompanyAccess(int roleCompanyAccessId, string recordActionType)
        {
            

            try
            {
                using (var connection = Connection)
                {
                    connection.Open();

                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@RoleCompanyAccessId", roleCompanyAccessId, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@RecordActionType", recordActionType, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@UpdatedBy", CurrentUser, dbType: DbType.String, direction: ParameterDirection.Input);




                    connection.Execute("BiAdminToolRoleCompanyAccess_UpdateBy_ID", parameter, commandType: CommandType.StoredProcedure);
                    //response.Success = true;
                    //return response;
                }
            }
            catch (SqlException e)
            {
                throw new Exception();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public async Task PutBiadminToolRolePermission(int rolePermissionId, int permissionTypeId)
        {
            try
            {
                using (var connection = Connection)
                {
                    connection.Open();

                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@RolePermissionId", rolePermissionId, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@PermissionTypeId", permissionTypeId, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@UpdateBy", CurrentUser, DbType.String, ParameterDirection.Input);

                     connection.Execute("BiAdminToolRolePermission_UpdateBy_Id", parameter, commandType: CommandType.StoredProcedure);
                    //response.Success = true;
                    //return response;
                }
            }
            catch (SqlException e)
            {
                throw new Exception();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
    } 
}

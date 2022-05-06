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
    public class SpecialtyRepository : BaseRepository, ISpecialtyRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public SpecialtyRepository(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
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

        public async Task<List<SpecialtyCrosswalk>> GetAll()
        {
            try
            {
                IEnumerable<SpecialtyCrosswalk> rows;

                using (var connection = Connection)
                {
                    connection.Open();
                    
                    rows = await connection.QueryAsync<SpecialtyCrosswalk>("SpecialtyCrosswalk_GetAll",
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

        public async Task<List<DirectorySection>> GetAllDirectorySections()
        {
            try
            {
                IEnumerable<DirectorySection> rows;

                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<DirectorySection>("DirectorySection_GetAll",
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
        public async Task<List<DirectoryType>> GetDirectoryTypes()
        {
            try
            {
                IEnumerable<DirectoryType> rows;

                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<DirectoryType>("DirectoryType_GetFor_Select",
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

        public async Task<Specialty> GetSpecialtyById(string id)
        {
            try
            {
                var result = new Specialty();

                using (var connection = Connection)
                {
                    connection.Open();

                    result = await connection.QueryFirstOrDefaultAsync<Specialty>("Specialty_GetBy_ID", new { id }, commandType: CommandType.StoredProcedure);
                
                }
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public async Task<List<SpecialtyDirectory>> GetSpecialtyDirectories(DataTableParameters<SpecialtyDirectory> model)
        {
            try
            {
                IEnumerable<SpecialtyDirectory> rows;
                var parameters = new DynamicParameters();
                parameters.Add("@SpecialtyId", model.query.SpecialtyId.ToString(), DbType.String, ParameterDirection.Input);
                parameters.Add("@CompanyId", model.query.CompanyId.ToString(), DbType.String, ParameterDirection.Input);
                parameters.Add("@LobId", model.query.LineOfBusinessId.ToString(), DbType.String, ParameterDirection.Input);
                parameters.Add("@DirectoryTypeId", model.query.DirectoryTypeId.ToString(), DbType.Int32, ParameterDirection.Input);
                parameters.Add("@UserCompanyAccessId", CurrentUserCompanyAccessId, DbType.String, ParameterDirection.Input);



                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<SpecialtyDirectory>("SpecialtyDirectory_Search",
                                    parameters,
                                    commandType: CommandType.StoredProcedure);
                }
                return rows.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public async Task<SpecialtyDirectory> GetSpecialtyDirectoryById(string SpecialtyDirectoryId)
        {
            try
            {
                var result = new SpecialtyDirectory();

                using (var connection = Connection)
                {
                    connection.Open();

                    result = await connection.QueryFirstOrDefaultAsync<SpecialtyDirectory>("SpecialtyDirectory_GetBy_ID", new { SpecialtyDirectoryId }, commandType: CommandType.StoredProcedure);

                }
                return result; 
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public async Task<GenericResponse<int>> PostSpecialtyCrosswalk(SpecialtyCrosswalk specialtyCrosswalk)
        {
            var response = new GenericResponse<int>(0);
            
            try
            {
                using (var connection = Connection)
                {
                    connection.Open();

                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@LineOfBusinessId", specialtyCrosswalk.LineOfBusinessId, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@CompanyId", specialtyCrosswalk.CompanyId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    parameter.Add("@EffectiveDate", specialtyCrosswalk.EffectiveDate, dbType: DbType.Date, direction: ParameterDirection.Input);
                    parameter.Add("@EndDate", specialtyCrosswalk.EndDate, dbType: DbType.Date, direction: ParameterDirection.Input);
                    parameter.Add("@LanguageId", specialtyCrosswalk.LanguageId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    parameter.Add("@LanguageName", specialtyCrosswalk.LanguageName, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@SpecialtyId", specialtyCrosswalk.SpecialtyId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    parameter.Add("@SpecialtyCode", specialtyCrosswalk.SpecialtyCode, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@SpecialtyName", specialtyCrosswalk.SpecialtyName, dbType: DbType.String, direction: ParameterDirection.Input);
                    
                    response.Result = await connection.QueryFirstAsync<int>("SpecialtyCrosswalk_Insert", parameter, commandType: CommandType.StoredProcedure);
                    response.Success = true;
                    return response ;
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

        public async Task<GenericResponse<int>> UpdateSpecialtyCrosswalk(SpecialtyCrosswalk specialtyCrosswalk)
        {
            var response = new GenericResponse<int>(0);
            try
            {
                using (var connection = Connection)
                {
                    connection.Open();

                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@EffectiveDate", specialtyCrosswalk.EffectiveDate, dbType: DbType.Date, direction: ParameterDirection.Input);
                    parameter.Add("@EndDate", specialtyCrosswalk.EndDate, dbType: DbType.Date, direction: ParameterDirection.Input);
                    parameter.Add("@SpecialtyName", specialtyCrosswalk.SpecialtyName, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@UpdateBy", CurrentUser, dbType: DbType.String, direction: ParameterDirection.Input);


                    connection.Execute("SpecialtyCrosswalk_UpdateBy_ID", parameter, commandType: CommandType.StoredProcedure);
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

        public async Task<GenericResponse<int>> PutSpecialtyDirectory(SpecialtyDirectory record)
        {
            var response = new GenericResponse<int>(0);
            try
            {
                using (var connection = Connection)
                {
                    connection.Open();

                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@SpecialtyId", record.SpecialtyId, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@SpecialtyDirectoryId", record.SpecialtyDirectoryId, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@DirectorySectionId", record.DirectorySectionId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    parameter.Add("@CompanyId", record.CompanyId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    parameter.Add("@LineOfBusinessId", record.LineOfBusinessId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    parameter.Add("@EndDate", record.EndDate, dbType: DbType.Date, direction: ParameterDirection.Input);
                    parameter.Add("@UpdateBy", CurrentUser, dbType: DbType.String, direction: ParameterDirection.Input);


                    connection.Execute("SpecialtyDirectory_UpdateBy_ID", parameter, commandType: CommandType.StoredProcedure);
                    response.Success = true;
                    //response.Result = specialtyCrosswalk.SpecialtyCrosswalkId;
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

        public async Task<GenericResponse<int>> PostSpecialtyDirectory(SpecialtyDirectory newRecord)
        {
            var response = new GenericResponse<int>(0);
            try
            {
                using (var connection = Connection)
                {
                    connection.Open();

                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@SpecialtyId", newRecord.SpecialtyId, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@DirectorySectionId", newRecord.DirectorySectionId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    parameter.Add("@CompanyId", newRecord.CompanyId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    parameter.Add("@LineOfBusinessId", newRecord.LineOfBusinessId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    parameter.Add("@DirectoryTypeId", newRecord.DirectoryTypeId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    parameter.Add("@EffectiveDate", newRecord.EffectiveDate     , dbType: DbType.Date, direction: ParameterDirection.Input);
                    parameter.Add("@EndDate", newRecord.EndDate, dbType: DbType.Date, direction: ParameterDirection.Input);
                    parameter.Add("@CreatedBy", CurrentUser, dbType: DbType.String, direction: ParameterDirection.Input);



                    connection.Execute("SpecialtyDirectory_Insert", parameter, commandType: CommandType.StoredProcedure);
                    response.Success = true;
                    //response.Result = specialtyCrosswalk.SpecialtyCrosswalkId;
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

        public async Task<List<SpecialtyLanguage>> GetSpecialtyLanguages(DataTableParameters<SpecialtyLanguage> model)
        {
            try
            {
                IEnumerable<SpecialtyLanguage> rows;
                var parameters = new DynamicParameters();
                parameters.Add("@SpecialtyId", model.query.SpecialtyID.ToString(), DbType.String, ParameterDirection.Input);
                //parameters.Add("@CompanyId", model.query.CompanyId.ToString(), DbType.String, ParameterDirection.Input);
                parameters.Add("@CompanyId", model.columns[3].search.value?.ToString(), DbType.String, ParameterDirection.Input);
                parameters.Add("@LobId", model.columns[2].search.value?.ToString(), DbType.String, ParameterDirection.Input);
                parameters.Add("@LanguageId", model.columns[1].search.value?.ToString(), DbType.String, ParameterDirection.Input);
                parameters.Add("@UserCompanyAccessId", CurrentUserCompanyAccessId, DbType.String, ParameterDirection.Input);


                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<SpecialtyLanguage>("SpecialtyLanguage_Search",
                                    parameters,
                                    commandType: CommandType.StoredProcedure);
                }
                return rows.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public async Task<SpecialtyLanguage> GetSpecialtyLanguageById(string specialtyLanguageId)
        {
            try
            {
                var result = new SpecialtyLanguage();

                using (var connection = Connection)
                {
                    connection.Open();

                    result = await connection.QueryFirstOrDefaultAsync<SpecialtyLanguage>("SpecialtyLanguage_GetBy_Id", new { specialtyLanguageId }, commandType: CommandType.StoredProcedure);

                }
                return result;
            }
            catch (SqlException sql)
            {
                throw new Exception(sql.Message, sql.InnerException);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public async Task<GenericResponse<int>> PutSpecialtyLanguage(SpecialtyLanguage record)
        {
            var response = new GenericResponse<int>(0);
            try
            {
                using (var connection = Connection)
                {
                    connection.Open();

                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@SpecialtyId", record.SpecialtyID, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@SpecialtyLanguageId", record.SpecialtyLanguageId, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@LanguageId", record.LanguageId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    parameter.Add("@CompanyId", record.CompanyId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    parameter.Add("@LineOfBusinessId", record.LineOfBusinessId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    parameter.Add("@EndDate", record.EndDate, dbType: DbType.Date, direction: ParameterDirection.Input);
                    parameter.Add("@DisplayName", record.DisplayName, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@UpdateBy", CurrentUser, dbType: DbType.String, direction: ParameterDirection.Input);

                    //parameter.Add("@EffectiveDate", record.EffectiveDate, dbType: DbType.Date, direction: ParameterDirection.Input);



                    connection.Execute("SpecialtyLanguage_UpdateBy_ID", parameter, commandType: CommandType.StoredProcedure);
                    response.Success = true;
                    //response.Result = specialtyCrosswalk.SpecialtyCrosswalkId;
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

        public async Task<GenericResponse<int>> PutDirectorySection(DirectorySection record)
        {
            var response = new GenericResponse<int>(0);
            try
            {
                using (var connection = Connection)
                {
                    connection.Open();

                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@DirectorySectionId", record.DirectorySectionId, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@DirectorySectionName", record.DirectorySectionName, DbType.String, ParameterDirection.Input);
                    parameter.Add("@DirectorySectionDescription", record.DirectorySectionDescription, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@DirectorySectionCode", record.DirectorySectionCode, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@UpdateBy", CurrentUser, dbType: DbType.String, direction: ParameterDirection.Input);


                    connection.Execute("DirectorySection_UpdateBy_ID", parameter, commandType: CommandType.StoredProcedure);
                    response.Success = true;
                    //response.Result = specialtyCrosswalk.SpecialtyCrosswalkId;
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

        public async Task<GenericResponse<int>> PostSpecialtyLanguage(SpecialtyLanguage newRecord)
        {
            var response = new GenericResponse<int>(0);
            try
            {
                using (var connection = Connection)
                {
                    connection.Open();

                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@SpecialtyId", newRecord.SpecialtyID, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@LanguageId", newRecord.LanguageId, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@CompanyId", newRecord.CompanyId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    parameter.Add("@LineOfBusinessId", newRecord.LineOfBusinessId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    parameter.Add("@DisplayName", newRecord.DisplayName, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@EffectiveDate", newRecord.EffectiveDate, dbType: DbType.Date, direction: ParameterDirection.Input);
                    parameter.Add("@EndDate", newRecord.EndDate, dbType: DbType.Date, direction: ParameterDirection.Input);
                    parameter.Add("@CreatedBy", CurrentUser, dbType: DbType.String, direction: ParameterDirection.Input);



                    connection.Execute("SpecialtyLanguage_Insert", parameter, commandType: CommandType.StoredProcedure);
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

        public async Task<GenericResponse<int>> PostDirectorySection(DirectorySection newRecord)
        {
            var response = new GenericResponse<int>(0);
            try
            {
                using (var connection = Connection)
                {
                    connection.Open();

                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@DirectorySectionName", newRecord.DirectorySectionName, DbType.String, ParameterDirection.Input);
                    parameter.Add("@DirectorySectionDescription", newRecord.DirectorySectionDescription, DbType.String, ParameterDirection.Input);
                    parameter.Add("@DirectorySectionCode", newRecord.DirectorySectionCode, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@CreatedBy", CurrentUser, dbType: DbType.String, direction: ParameterDirection.Input);



                    var result = await connection.QueryAsync<int>("DirectorySection_Insert", parameter, commandType: CommandType.StoredProcedure);

                    //Capture ID of inserted record.
                    response.Result = result.Single();
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

        public async Task<List<DirectorySection>> GetDirectorySectionByDirectoryType(int directoryType)
        {
            try
            {
                IEnumerable<DirectorySection> rows;

                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<DirectorySection>("DirectorySection_GetBy_DirectoryType",
                        new { directoryType },
                        commandType: CommandType.StoredProcedure);
                }
                return rows.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public async Task<GenericResponse<int>> PostDirectorySectionType(List<DirectorySectionType> records)
        {
            var response = new GenericResponse<int>(0);


            try
            {
                using (var connection = Connection)
                {
                    connection.Open();


                    foreach (var record in records)
                    {
                        DynamicParameters parameter = new DynamicParameters();

                        parameter.Add("@DirectorySectionId", record.DirectorySectionId, DbType.Int16, ParameterDirection.Input);
                        parameter.Add("@DirectoryTypeId", record.DirectoryTypeId, DbType.Int16, ParameterDirection.Input);
                        parameter.Add("@CreatedBy", CurrentUser, dbType: DbType.String, direction: ParameterDirection.Input);
                        parameter.Add("@RecordActionType", record.RecordActionType, DbType.String, direction: ParameterDirection.Input);

                        var result = await connection.QueryAsync<int>("DirectorySectionType_Insert", parameter, commandType: CommandType.StoredProcedure);
                    }
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
        public async Task<GenericResponse<int>> PutDirectorySectionType(List<DirectorySectionType> records)
        {
            var response = new GenericResponse<int>(0);


            try
            {
                using (var connection = Connection)
                {
                    connection.Open();


                    foreach (var record in records)
                    {
                        DynamicParameters parameter = new DynamicParameters();

                        parameter.Add("@DirectorySectionId", record.DirectorySectionId, DbType.Int16, ParameterDirection.Input);
                        parameter.Add("@DirectoryTypeId", record.DirectoryTypeId, DbType.Int16, ParameterDirection.Input);
                        parameter.Add("@UpdateBy", CurrentUser, dbType: DbType.String, direction: ParameterDirection.Input);
                        parameter.Add("@RecordActionType", record.RecordActionType, DbType.String, direction: ParameterDirection.Input);

                        var result = await connection.QueryAsync<int>("DirectorySectionType_UpdateBy_Id", parameter, commandType: CommandType.StoredProcedure);
                    }
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
    } 
}

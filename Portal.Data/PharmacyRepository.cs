using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Portal.Common.Models;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Dapper.Contrib;
using System.Linq;
using Dapper.Contrib.Extensions;
using Portal.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Portal.Data
{
    public class PharmacyRepository : BaseRepository, IPharmacyRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;

        public PharmacyRepository(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
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

        //No se está usando y no creo que se va a usar. Lo borro pronto.
        public async Task<List<Pharmacy>> GetAll()
        {
            try
            {
                IEnumerable<Pharmacy> rows;

                using (var connection = Connection)
                {
                    connection.Open();
                    
                    rows = await connection.QueryAsync<Pharmacy>("Pharmacy_GetAll",
                                    null,
                                    commandType: CommandType.StoredProcedure);
                }
                return rows.ToList();
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<Pharmacy> GetPharmacyById(string id)
        {
            //var result = new Pharmacy();

            using (var connection = Connection)
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<Pharmacy>("Pharmacy_GetBy_Id", new { id }, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<PharmacyStatus> GetPharmacyStatusById(string id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<PharmacyStatus>("PharmacyStatus_GetBy_Id", new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<GenericResponse<int>> UpdatePharmacyStatus(PharmacyStatus pharmacyStatus)
        {
            var response = new GenericResponse<int>(0);

            try
            {
                using (var connection = Connection)
                {
                    connection.Open();

                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@PharmacyStatusId", pharmacyStatus.PharmacyStatusId, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@PharmacyId", pharmacyStatus.PharmacyId, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    parameter.Add("@RetailFlag", pharmacyStatus.RetailFlag, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@RetailExtSupplyFlag", pharmacyStatus.RetailExtSupplyFlag, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@HifFlag", pharmacyStatus.HifFlag, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@LtcFlag", pharmacyStatus.LtcFlag, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@IhsFlag", pharmacyStatus.IhsFlag, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@MailFlag", pharmacyStatus.MailFlag, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@MailPreferredFlag", pharmacyStatus.MailPreferredFlag, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@PreferredFlag", pharmacyStatus.PreferredFlag, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@LimitedAccessFlag", pharmacyStatus.LimitedAccessFlag, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@eRxFlag", pharmacyStatus.eRxFlag, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@TtyFlag", pharmacyStatus.TtyFlag, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@Hours24Flag", pharmacyStatus.Hours24Flag, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@ContractedFlag", pharmacyStatus.ContractedFlag, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@PartBFlag", pharmacyStatus.PartBFlag, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@SpecialtyFlag", pharmacyStatus.SpecialtyFlag, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@AdherenceFlag", pharmacyStatus.AdherenceFlag, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@DirectoryDisplayFlag", pharmacyStatus.DirectoryDisplayFlag, dbType: DbType.String, direction: ParameterDirection.Input);
                    parameter.Add("@UpdateBy", CurrentUser, dbType: DbType.String, direction: ParameterDirection.Input);


                    connection.Execute("PharmacyStatus_UpdateBy_ID", parameter, commandType: CommandType.StoredProcedure);
                    response.Success = true;
                    response.Result = pharmacyStatus.PharmacyStatusId;
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

        public async Task<List<Company>> GetCompanies()
        {
            try
            {
                IEnumerable<Company> rows;

                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<Company>("PharmacyList_Get_Companies",
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

        public async Task<List<LineOfBusiness>> GetLineOfBusinesses()
        {
            try
            {
                IEnumerable<LineOfBusiness> rows;

                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<LineOfBusiness>("PharmacyList_Get_Lobs",
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

        public async Task<List<County>> GetCounties()
        {
            try
            {
                IEnumerable<County> rows;

                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<County>("PharmacyList_Get_Counties",
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

        public async Task<List<State>> GetStates()
        {
            try
            {
                IEnumerable<State> rows;

                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<State>("PharmacyList_Get_States",
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

        public async Task<List<City>> GetCities()
        {
            try
            {
                IEnumerable<City> rows;

                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<City>("PharmacyList_Get_Cities",
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

        public async Task<List<Pharmacy>> SearchPharmacies(string searchString, int lobId, int companyId, string StateCode, string City, string County)
        {
            try
            {
                IEnumerable<Pharmacy> rows;
                var parameters = new DynamicParameters();
                parameters.Add("@SearchString", searchString, DbType.String, ParameterDirection.Input);
                parameters.Add("@LobId", lobId.ToString(), DbType.String, ParameterDirection.Input);
                parameters.Add("@CompanyId", companyId.ToString(), DbType.String, ParameterDirection.Input);
                parameters.Add("@StateCode", StateCode, DbType.String, ParameterDirection.Input);
                parameters.Add("@City", City, DbType.String, ParameterDirection.Input);
                parameters.Add("@County", County, DbType.String, ParameterDirection.Input);
                parameters.Add("@CurrentUserCompanyAccessId", CurrentUserCompanyAccessId, DbType.String, ParameterDirection.Input);


                using (var connection = Connection)
                {
                    connection.Open();
                    rows = await connection.QueryAsync<Pharmacy>("PharmacyList_Search", 
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

        public async Task<List<County>> GetCountyByString(string county)
        {
            try
            {
                IEnumerable<County> rows;

                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<County>("PharmacyList_Get_Counties",
                        new { county },
                        commandType: CommandType.StoredProcedure);
                }
                return rows.ToList();
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }
        }

        public Task<List<State>> GetStateByString(string state)
        {
            throw new NotImplementedException();
        }

        public async Task<List<City>> GetCityByString(string city)
        {
            try
            {
                IEnumerable<City> rows;

                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<City>("PharmacyList_Get_Cities",
                        new { city },
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

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
    public class GeneralRepository : BaseRepository,  IGeneralRepository
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public GeneralRepository(IConfiguration config, IHttpContextAccessor httpContextAccessor) : base(config, httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _config = config;
        }



        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }



        public async Task<List<Language>> GetLanguages()
        {
            try
            {
                IEnumerable<Language> rows;

                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<Language>("Language_GetFor_SpecialtyCrossWalk",
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

        public async Task<List<Specialty>> GetSpecialties()
        {
            try
            {
                IEnumerable<Specialty> rows;

                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<Specialty>("Specialty_GetFor_SpecialtyCrossWalk",
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

                    rows = await connection.QueryAsync<LineOfBusiness>("LineOfBusiness_GetFor_SpecialtyCrossWalk",
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

        public async Task<List<DirectorySection>> GetDirectorySections()
        {
            try
            {
                IEnumerable<DirectorySection> rows;

                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<DirectorySection>("DirectorySection_GetFor_Select",
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

        public async Task<List<Company>> GetCompanyByLob(int lobId)
        {
            try
            {
                IEnumerable<Company> rows;

                using (var connection = Connection)
                {
                    connection.Open();

                    rows = await connection.QueryAsync<Company>("Company_GetBy_Lob",
                        new { lobId },
                        commandType: CommandType.StoredProcedure);
                }
                return rows.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        //public List<Company> GetCompanies() => CompaniesForUser;
    }
}

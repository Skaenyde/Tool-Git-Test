using Portal.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Common.Interfaces
{
    public interface IPharmacyRepository
    {
        Task<List<Pharmacy>> GetAll();

        Task<List<Pharmacy>> SearchPharmacies(string searchString, int lobId, int companyId, string StateCode, string City, string County);

        Task<Pharmacy> GetPharmacyById(string id);
        Task<PharmacyStatus> GetPharmacyStatusById(string id);

        Task<GenericResponse<int>> UpdatePharmacyStatus(PharmacyStatus pharmacy);

        Task<List<Company>> GetCompanies();
        Task<List<LineOfBusiness>> GetLineOfBusinesses();
        Task<List<County>> GetCounties();
        Task<List<State>> GetStates();
        Task<List<City>> GetCities();
        Task<List<County>> GetCountyByString(string county);
        Task<List<State>> GetStateByString(string state);
        Task<List<City>> GetCityByString(string city);
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Portal.Common.ViewModels;

namespace Portal.Common.Interfaces
{
    public interface IPharmacyLogic
    {
        Task<List<Pharmacy>> GetPharmacies(DataTableParameters<PharmacyDataTableQuery> model);

        Task<Pharmacy> GetPharmacyById(string id);

        Task<PharmacyStatus> GetPharmacyStatusById(string id);

        Task<PharmacyViewModel> LoadDropdowns();

        Task<GenericResponse<int>> UpdatePharmacyStatus(PharmacyStatus pharmacyStatus);
        Task<List<County>> GetCountyByString(string county);
        Task<List<State>> GetStateByString(string state);
        Task<List<City>> GetCityByString(string city);
    }
}

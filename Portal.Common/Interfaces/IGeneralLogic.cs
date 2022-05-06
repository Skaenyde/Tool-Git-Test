using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Common.Interfaces
{
    public interface IGeneralLogic
    {
        Task<List<SelectListItem>> GetCompanies();

        Task<List<SelectListItem>> GetCompaniesByName();

        //Task<List<SelectListItem>> GetLineOfBusinesses();

        //Task<List<SelectListItem>> GetSpecialties();

        //Task<List<SelectListItem>> GetLanguages();
    }
}

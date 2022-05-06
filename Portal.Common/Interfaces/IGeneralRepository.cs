using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Common.Interfaces
{
    public interface IGeneralRepository
    {
        Task<List<Language>> GetLanguages();
        Task<List<Specialty>> GetSpecialties();
        Task<List<Company>> GetCompanies();
        Task<List<LineOfBusiness>> GetLineOfBusinesses();
        Task<List<DirectorySection>> GetDirectorySections();
        Task<List<Company>> GetCompanyByLob(int lobId);
        
        
    }
}

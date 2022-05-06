using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Common.Interfaces
{
    public interface ISpecialtyLogic
    {
        Task<List<SpecialtyCrosswalk>> Load();

        Task<Specialty> GetSpecialtyById(string id);

        Task<List<SelectListItem>> GetCompaniesByName();

        Task<List<SelectListItem>> GetLineOfBusinesses();

        Task<List<SelectListItem>> GetSpecialties();

        Task<List<SelectListItem>> GetLanguages();
        //Task<GenericResponse<int>> PostSpecialtyCrosswalk(SpecialtyCrosswalk specialtyCrosswalk);

        //Task<GenericResponse<int>> UpdateSpecialtyCrosswalk(SpecialtyCrosswalk specialtyCrosswalk);
        Task<List<SpecialtyDirectory>> GetSpecialtyDirectories(DataTableParameters<SpecialtyDirectory> model);
        Task<SpecialtyDirectory> GetSpecialtyDirectoryById(string SpecialtyDirectoryId);
        Task<List<SelectListItem>> GetDirectorySections();
        Task<List<SelectListItem>> GetDirectoryTypes();
        Task<GenericResponse<int>> UpdateSpecialtyDirectory(SpecialtyDirectory record);
        Task<GenericResponse<int>> CreateSpecialtyDirectory(SpecialtyDirectory newRecord);
        Task<List<SpecialtyLanguage>> GetSpecialtyLanguages(DataTableParameters<SpecialtyLanguage> model);
        Task<SpecialtyLanguage> GetSpecialtyLanguageById(string specialtyLanguageId);
        Task<GenericResponse<int>> UpdateSpecialtyLanguage(SpecialtyLanguage record);
        Task<GenericResponse<int>> CreateSpecialtyLanguage(SpecialtyLanguage newRecord);
        Task<List<Company>> GetCompanyByLob(int lobId);
        Task<List<DirectorySection>> GetDirectorySectionByDirectoryType(int directoryType);
        Task<List<DirectorySection>> LoadDirectorySections();
        Task<GenericResponse<int>> UpdateDirectorySection(DirectorySection directorySection);
        Task<GenericResponse<int>> CreateDirectorySection(DirectorySection newDirectorySection);
        Task <GenericResponse<int>> CreateDirectorySectionType(int directorySectionId, bool paper, bool online);
        Task <GenericResponse<int>> UpdateDirectorySectionTypeById(int directorySectionId, bool online, bool paper);
    }
}

using Portal.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Common.Interfaces
{
    public interface ISpecialtyRepository
    {
        Task<List<SpecialtyCrosswalk>> GetAll();

        Task<Specialty> GetSpecialtyById(string id);
        Task<GenericResponse<int>> PostSpecialtyCrosswalk(SpecialtyCrosswalk specialtyCrosswalk);

        Task<GenericResponse<int>> UpdateSpecialtyCrosswalk(SpecialtyCrosswalk specialtyCrosswalk);
        Task<List<SpecialtyDirectory>> GetSpecialtyDirectories(DataTableParameters<SpecialtyDirectory> model);
        Task<SpecialtyDirectory> GetSpecialtyDirectoryById(string id);
        Task<GenericResponse<int>> PutSpecialtyDirectory(SpecialtyDirectory record);
        Task<GenericResponse<int>> PostSpecialtyDirectory(SpecialtyDirectory newRecord);
        Task<List<SpecialtyLanguage>> GetSpecialtyLanguages(DataTableParameters<SpecialtyLanguage> model);
        Task<SpecialtyLanguage> GetSpecialtyLanguageById(string specialtyLanguageId);
        Task<GenericResponse<int>> PutSpecialtyLanguage(SpecialtyLanguage record);
        Task<GenericResponse<int>> PostSpecialtyLanguage(SpecialtyLanguage newRecord);
        Task<List<DirectorySection>> GetDirectorySectionByDirectoryType(int directoryType);
        Task<List<DirectorySection>> GetAllDirectorySections();
        Task<GenericResponse<int>> PutDirectorySection(DirectorySection record);
        Task<List<DirectoryType>> GetDirectoryTypes();
        Task<GenericResponse<int>> PostDirectorySection(DirectorySection newRecord);
        Task<GenericResponse<int>> PostDirectorySectionType(List<DirectorySectionType> records);
        Task<GenericResponse<int>> PutDirectorySectionType(List<DirectorySectionType> records);
    }
}

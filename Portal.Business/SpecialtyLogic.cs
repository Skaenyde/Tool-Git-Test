using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Common.Interfaces;
using Portal.Common.Models;
using Portal.Data;
using Portal.Common.Enums;


namespace Portal.Business
{
    public class SpecialtyLogic :  ISpecialtyLogic
    {

        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IGeneralRepository _generalRepository;
        private readonly IUserContextRepository _userContextRepository;


        public SpecialtyLogic(ISpecialtyRepository specialtyRepo, IGeneralRepository generalRepo, IUserContextRepository userContextRepo)
        {
            _specialtyRepository = specialtyRepo;
            _generalRepository = generalRepo;
            _userContextRepository = userContextRepo;
        }


        public async Task<Specialty> GetSpecialtyById(string id)
        {
            var result = await _specialtyRepository.GetSpecialtyById(id);
            //result.SelectSpecialtyValue = result.SpecialtyId + "_" + result.SpecialtyCode.Trim();
            //result = ConvertFlags(result);
            return result;
        }

        public async Task<List<SpecialtyCrosswalk>> Load()
        {
            var results = await _specialtyRepository.GetAll();


            return results;
        }

        public async Task<List<DirectorySection>> LoadDirectorySections()
        {
            var results = await _specialtyRepository.GetAllDirectorySections();


            return results;
        }

        public async Task<List<SelectListItem>> GetLanguages()
        {
            var languages = await _generalRepository.GetLanguages();
            var options = new List<SelectListItem>();
            foreach (var language in languages)
            {
                var item = new SelectListItem { Value = language.LanguageId.ToString(), Text = language.LanguageName };
                options.Add(item);
            }
            return options;
        }

        public async Task<List<SelectListItem>> GetCompaniesByName()
        {
            var companies = await _generalRepository.GetCompanies();
            var options = new List<SelectListItem>();
            foreach (var company in companies)
            {
                var item = new SelectListItem { Value = company.CompanyName, Text = company.CompanyName };
                options.Add(item);
            }
            return options;
        }

        public async Task<List<SelectListItem>> GetLineOfBusinesses()
        {
            var lineOfBusinesses = await _generalRepository.GetLineOfBusinesses();
            var options = new List<SelectListItem>();
            foreach (var lob in lineOfBusinesses)
            {
                var item = new SelectListItem { Value = lob.LineOfBusinessId.ToString(), Text = lob.LineOfBusinessName };
                options.Add(item);
            }
            return options;
        }

        public async Task<List<SelectListItem>> GetSpecialties()
        {
            var specialties = await _generalRepository.GetSpecialties();
            return specialties.Select(specialty => new SelectListItem {Value = specialty.SpecialtyId + "_" + specialty.SpecialtyCode, Text = specialty.SpecialtyName}).ToList();
        }
        private static SpecialtyCrosswalk ConvertFlags(SpecialtyCrosswalk item)
        {
            //Get all properties from SpecialtyCrosswalk into a Array.
            var properties = item?.GetType().GetProperties();

            //Cycle though properties which contain "Flag" in their name,
            //then update their value accordingly.
            foreach (var property in properties)
            {
                if (!property.Name.ToLower().Contains("flag")) continue;
                var caseSwitch = property.GetValue(item);
                switch (caseSwitch)
                {
                    case "N":
                        property.SetValue(item, "false");
                        break;
                    case "Y":
                        property.SetValue(item, "true");
                        break;
                    case "true":
                        property.SetValue(item, "Y");
                        break;
                    case "false":
                        property.SetValue(item, "N");
                        break;
                    default:
                        break;
                }
            }
            return item;
        }

        //public async Task<GenericResponse<int>> UpdateSpecialtyCrosswalk(SpecialtyCrosswalk specialtyCrosswalk)
        //{
        //    ConvertFlags(specialtyCrosswalk);
        //    return await  _specialtyRepository.UpdateSpecialtyCrosswalk(specialtyCrosswalk);
        //}

        //public async Task<GenericResponse<int>> PostSpecialtyCrosswalk(SpecialtyCrosswalk specialtyCrosswalk)
        //{
        //    ConvertFlags(specialtyCrosswalk);

        //    return await _specialtyRepository.PostSpecialtyCrosswalk(specialtyCrosswalk);

        //}

        public async Task<List<SpecialtyDirectory>> GetSpecialtyDirectories(DataTableParameters<SpecialtyDirectory> model)
        {
            var rows = await _specialtyRepository.GetSpecialtyDirectories(model);
            var activeRows = new List<SpecialtyDirectory>();
            var today = DateTime.Today;
      
            //Setting record status
            foreach (var row in rows)
            {
                if (row.EffectiveDate <= today && row.EndDate >= today)
                {
                    row.DisplayFlag = "Y";
                }
                else
                {
                    row.DisplayFlag = "N";
                }
            }

            //filtering only for active records
            if (model.query.Active)
            {
                foreach (var row in rows)
                {
                    if (row.EffectiveDate <= today && row.EndDate >= today)
                    {
                        activeRows.Add(row);
                    }
                }
                return activeRows;
            }
            else return rows;
        }

        public async Task<SpecialtyDirectory> GetSpecialtyDirectoryById(string SpecialtyDirectoryId) => await _specialtyRepository.GetSpecialtyDirectoryById(SpecialtyDirectoryId);

        public async Task<List<SelectListItem>> GetDirectorySections()
        {
            var sections = await _generalRepository.GetDirectorySections();
            var options = new List<SelectListItem>();
            foreach (var section in sections)
            {
                var item = new SelectListItem { Value = section.DirectorySectionId.ToString(), Text = section.DirectorySectionName };
                options.Add(item);
            }
            return options;
        }

        public async Task<List<SelectListItem>> GetDirectoryTypes()
        {          
            var options = new List<SelectListItem>();
            List<Common.Models.DirectoryType> types = await _specialtyRepository.GetDirectoryTypes();
            foreach (var type in types)
            {
                var item = new SelectListItem { Value = type.DirectoryTypeId.ToString(), Text = type.DirectoryTypeName };
                options.Add(item);
            }
            //options.Add(new SelectListItem { Value = "O", Text = "Online" });
            //options.Add(new SelectListItem { Value = "P", Text = "Paper" });
            return options;
        }

        public async Task<GenericResponse<int>> UpdateSpecialtyDirectory(SpecialtyDirectory record) => await _specialtyRepository.PutSpecialtyDirectory(record);

        public async Task<GenericResponse<int>> CreateSpecialtyDirectory(SpecialtyDirectory newRecord) => await _specialtyRepository.PostSpecialtyDirectory(newRecord);

        public async Task<List<SpecialtyLanguage>> GetSpecialtyLanguages(DataTableParameters<SpecialtyLanguage> model)
        {
            var rows = await _specialtyRepository.GetSpecialtyLanguages(model);
            var activeRows = new List<SpecialtyLanguage>();
            var today = DateTime.Today;
            bool onlyActiveEntries = Convert.ToBoolean(model.columns[7].search.value);

            if (onlyActiveEntries)
            {
                foreach (var row in rows)
                {
                    if (row.EffectiveDate <= today && row.EndDate >= today)
                    {
                        activeRows.Add(row);
                    }
                }
                return activeRows;
            }
            else return rows;
        }

        public async Task<SpecialtyLanguage> GetSpecialtyLanguageById(string specialtyLanguageId) => await _specialtyRepository.GetSpecialtyLanguageById(specialtyLanguageId);

        public async Task<GenericResponse<int>> UpdateSpecialtyLanguage(SpecialtyLanguage record) => await _specialtyRepository.PutSpecialtyLanguage(record);

        public async Task<GenericResponse<int>> CreateSpecialtyLanguage(SpecialtyLanguage newRecord) => await _specialtyRepository.PostSpecialtyLanguage(newRecord);

        public async Task<GenericResponse<int>> CreateDirectorySection(DirectorySection newRecord) => await _specialtyRepository.PostDirectorySection(newRecord);

        public async Task<GenericResponse<int>> CreateDirectorySectionType(int directorySectionId, bool paper, bool online)
        {
            var response = new GenericResponse<int>(0);
            var records = new List<DirectorySectionType>
            {
                new DirectorySectionType
                {
                    DirectorySectionId = directorySectionId,
                    DirectoryTypeId =  (int)Common.Enums.DirectoryType.Online,
                    RecordActionType = online ? "I" : "D"
                },
                new DirectorySectionType
                {
                    DirectorySectionId = directorySectionId,
                    DirectoryTypeId = (int)Common.Enums.DirectoryType.Paper,
                    RecordActionType = paper ? "I" : "D"
                }
            };

                response = await _specialtyRepository.PostDirectorySectionType(records);
            
            return response;
        }


        public async Task<GenericResponse<int>> UpdateDirectorySectionTypeById(int directorySectionId, bool online, bool paper)
        {
            var response = new GenericResponse<int>(0);
            var records = new List<DirectorySectionType>
            {
                new DirectorySectionType
                {
                    DirectorySectionId = directorySectionId,
                    DirectoryTypeId =  (int)Common.Enums.DirectoryType.Online,
                    RecordActionType = online ? "U" : "D"
                },
                new DirectorySectionType
                {
                    DirectorySectionId = directorySectionId,
                    DirectoryTypeId = (int)Common.Enums.DirectoryType.Paper,
                    RecordActionType = paper ? "U" : "D"
                }
            };

            response = await _specialtyRepository.PutDirectorySectionType(records);

            return response;
        }

        public async Task<List<Company>> GetCompanyByLob(int lobId) => await _generalRepository.GetCompanyByLob(lobId);

        public async Task<List<DirectorySection>> GetDirectorySectionByDirectoryType(int directoryType) => await _specialtyRepository.GetDirectorySectionByDirectoryType(directoryType);

        public async Task<GenericResponse<int>> UpdateDirectorySection(DirectorySection record) => await _specialtyRepository.PutDirectorySection(record);

    }
}

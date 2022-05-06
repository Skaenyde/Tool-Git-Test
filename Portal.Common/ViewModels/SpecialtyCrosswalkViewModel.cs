using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Portal.Common.Enums;
using Portal.Common.Models;

namespace Portal.Common.ViewModels
{
    public class SpecialtyCrosswalkViewModel
    {
        //public UserContext User {get; set;} with permissions, ID, Name, etc.
        public ActionType ActionType { get; set; }

        public UserPassport User { get; set; }
        //Specialty/List
        public List<SpecialtyCrosswalk> SpecialtyCrosswalks { get; set; }
        //Specialty/Details/{id}
        public SpecialtyCrosswalk SpecialtyCrosswalk { get; set; }

        public Specialty SpecialtyDemographics { get; set; }

        //Dropdowns and DB lookup values for Editing view.
        public List<Language> Languages { get; set; }
        public List<Company> Companies { get; set; }
        public List<LineOfBusiness> LineOfBusinesses { get; set; }
        public List<Specialty> Specialties { get; set; }
        public List<SelectListItem> SelectLanguages { get; set; }
        public List<SelectListItem> SelectCompanies { get; set; }
        public List<SelectListItem> SelectLineOfBusinesses { get; set; }
        public List<SelectListItem> SelectSpecialties { get; set; }

        public List<SelectListItem> SelectDirectoryTypes { get; set; }

        //-------- List search params
        [JsonProperty("SearchString")]
        public string SearchString { get; set; }

        [JsonProperty("SearchLanguage")]
        public string[] SearchLanguage { get; set; }

        [JsonProperty("SearchCompany")]
        public string[] SearchCompany { get; set; }

        [JsonProperty("SearchSpecialty")]
        public string[] SearchSpecialty { get; set; }

        [JsonProperty("SearchLoB")]
        public string[] SearchLoB { get; set; }

    }
}
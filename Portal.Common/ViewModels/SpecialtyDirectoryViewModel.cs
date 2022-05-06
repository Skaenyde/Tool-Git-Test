using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Portal.Common.Enums;
using Portal.Common.Models;

namespace Portal.Common.ViewModels
{
    public class SpecialtyDirectoryViewModel
    {
        
        public ActionType ActionType { get; set; }

        public UserPassport User { get; set; }
        
        public SpecialtyDirectory SpecialtyDirectory { get; set; }
        public SpecialtyCrosswalk SpecialtyDemographics { get; set; }

        //Dropdowns and DB lookup values for Editing view.
        public List<Language> Languages { get; set; }
        public List<Company> Companies { get; set; }
        public List<LineOfBusiness> LineOfBusinesses { get; set; }
        public List<Specialty> Specialties { get; set; }
        public List<SelectListItem> SelectLanguages { get; set; }
        public List<SelectListItem> SelectCompanies { get; set; }
        public List<SelectListItem> SelectLineOfBusinesses { get; set; }
        public List<SelectListItem> SelectSpecialties { get; set; }
        public List<SelectListItem> SelectDirectorySection { get; set; }
        public List<SelectListItem> SelectDirectoryTypes { get; set; }


    }
}
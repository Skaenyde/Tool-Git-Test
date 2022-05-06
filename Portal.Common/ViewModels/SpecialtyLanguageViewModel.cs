using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Portal.Common.Enums;
using Portal.Common.Models;

namespace Portal.Common.ViewModels
{
    public class SpecialtyLanguageViewModel
    {
        
        public ActionType ActionType { get; set; }

        public UserPassport User { get; set; }

        public SpecialtyLanguage SpecialtyLanguage { get; set; }

        public string ErrorMessage { get; set; }
        //Dropdowns
        #region
        public List<SelectListItem> SelectLanguages { get; set; }
        public List<SelectListItem> SelectCompanies { get; set; }
        public List<SelectListItem> SelectLineOfBusinesses { get; set; }
        public List<SelectListItem> SelectDirectorySection { get; set; }
        #endregion  
    }
}
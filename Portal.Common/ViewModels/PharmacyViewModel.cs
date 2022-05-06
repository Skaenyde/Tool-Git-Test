using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Common.Enums;
using Portal.Common.Models;

namespace Portal.Common.ViewModels
{
    public class PharmacyViewModel
    {
        //public UserContext User {get; set;} with permissions, ID, Name, etc.
        public ActionType ActionType { get; set; }

        public UserPassport User { get; set; }
        //Specialty/List
        public List<Pharmacy> Pharmacies { get; set; }
        //Specialty/Details/{id}
        public Pharmacy Pharmacy{ get; set; }

        public PharmacyStatus PharmacyStatus { get; set; }

        //DropDowns
        public List<SelectListItem> SelectCompanies { get; set; }
        public List<SelectListItem> SelectLineOfBusinesses { get; set; }
        public List<SelectListItem> SelectCounty { get; set; }
        public List<SelectListItem> SelectState { get; set; }
        public List<SelectListItem> SelectCity{ get; set; }

    }
}
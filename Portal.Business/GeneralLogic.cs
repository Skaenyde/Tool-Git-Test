using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Common.Interfaces;
using Portal.Common.Models;
using Portal.Data;


namespace Portal.Business
{
    public class GeneralLogic : IGeneralLogic
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IUserContextRepository _userContextRepository;


        public GeneralLogic(IGeneralRepository generalRepo, IUserContextRepository userContextRepo)
        {
            _generalRepository = generalRepo;
            _userContextRepository = userContextRepo;
        }





        //public async Task<List<SelectListItem>> GetLanguages()
        //{
        //    var languages = await _generalRepository.GetLanguages();
        //    var options = new List<SelectListItem>();
        //    foreach (var language in languages)
        //    {
        //        var item = new SelectListItem { Value = language.LanguageId.ToString(), Text = language.LanguageName };
        //        options.Add(item);
        //    }
        //    return options;
        //}


        public async Task<List<SelectListItem>> GetCompanies()
        {
            var companies = await _generalRepository.GetCompanies();
            var options = new List<SelectListItem>();
            foreach (var company in companies)
            {
                var item = new SelectListItem { Value = company.CompanyId.ToString(), Text = company.CompanyCode };
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



        //public async Task<List<SelectListItem>> GetLineOfBusinesses()
        //{
        //    var lineOfBusinesses = await _generalRepository.GetLineOfBusinesses();
        //    var options = new List<SelectListItem>();
        //    foreach (var lob in lineOfBusinesses)
        //    {
        //        var item = new SelectListItem { Value = lob.LineOfBusinessId.ToString(), Text = lob.LineOfBusinessName };
        //        options.Add(item);
        //    }
        //    return options;
        //}

    }
}

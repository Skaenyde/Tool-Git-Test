using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Common.Interfaces;
using Portal.Common.Models;
using Portal.Common.ViewModels;
using Portal.Data;

namespace Portal.Business
{
    public class PharmacyLogic : IPharmacyLogic
    {
        private readonly IPharmacyRepository _pharmacyRepository;
        private readonly IGeneralRepository _generalRepository;
        private readonly IUserContextRepository _userContextRepository;


        public PharmacyLogic(IPharmacyRepository specialtyRepo, IGeneralRepository generalRepo, IUserContextRepository userContextRepo)
        {
            _pharmacyRepository = specialtyRepo;
            _generalRepository = generalRepo;
            _userContextRepository = userContextRepo;
        }


        public async Task<Pharmacy> GetPharmacyById(string id)
        {
            var result = await _pharmacyRepository.GetPharmacyById(id);
            return result;
        }

        public async Task<PharmacyViewModel> LoadDropdowns()
        {
            var pharmacyViewModel = new PharmacyViewModel();

            var companies = await _generalRepository.GetCompanies();
            pharmacyViewModel.SelectCompanies = companies.Select(company => new SelectListItem {Value = company.CompanyId.ToString(), Text = company.CompanyName}).ToList();

            var lobs = await _pharmacyRepository.GetLineOfBusinesses();
            pharmacyViewModel.SelectLineOfBusinesses = lobs.Select(lob => new SelectListItem {Value = lob.LineOfBusinessId.ToString(), Text = lob.LineOfBusinessName}).ToList();

            //var counties = await _pharmacyRepository.GetCounties();
            //pharmacyViewModel.SelectCounty = counties.Select(county => new SelectListItem {Text = county.CountyName}).ToList();

            var states = await _pharmacyRepository.GetStates();
            pharmacyViewModel.SelectState = states.Select(state => new SelectListItem {Value =state.StateCode, Text = state.StateName}).ToList();

            //var cities = await _pharmacyRepository.GetCities();
            //pharmacyViewModel.SelectCity = cities.Select(city => new SelectListItem {Text = city.CityName}).ToList();

            return pharmacyViewModel;
        }

        public async Task<PharmacyStatus> GetPharmacyStatusById(string id)
        {
            var result = await _pharmacyRepository.GetPharmacyStatusById(id);
            
            return ConvertFlags(result);
        }

        public async Task<List<Pharmacy>> GetPharmacies(DataTableParameters<PharmacyDataTableQuery> model)
        {
            var searchString = string.Empty;
            var lobId = 0;
            var companyId = 0;
            var stateCode = string.Empty;
            var county = string.Empty;
            var city = string.Empty;
            if (model.query != null)
            {
                searchString = string.IsNullOrEmpty(model.query.SearchString) ? null : model.query.SearchString;
                lobId = model.query.LineOfBusinessId;
                companyId = model.query.CompanyId;
                stateCode = model.query.StateCode == null ? null : string.Join(",", model.query.StateCode);
                city = model.query.City == null ? null : string.Join(",", model.query.City);
                county = model.query.County == null ? null : string.Join(",", model.query.County);
            }
           
            return await _pharmacyRepository.SearchPharmacies(searchString, lobId, companyId ,stateCode, city, county);
        }

        private PharmacyStatus ConvertFlags(PharmacyStatus item)
        {
            //Get all properties from SpecialtyCrosswalk into a Array.
            PropertyInfo[] properties = item?.GetType().GetProperties();

            //Cycle though properties which contain "Flag" in their name,
            //then update their value accordingly.
            foreach (var property in properties)
            {
                if (property.Name.ToLower().Contains("flag"))
                {
                    var caseSwitch = property.GetValue(item);
                    switch (caseSwitch)
                    {
                        case "N":
                            property.SetValue(item, "false");
                            break;
                        case " ":
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
            }
            return item;
        }

        public Task<GenericResponse<int>> UpdatePharmacyStatus(PharmacyStatus pharmacyStatus)
        {
            ConvertFlags(pharmacyStatus);
            return  _pharmacyRepository.UpdatePharmacyStatus(pharmacyStatus);
        }

        public async Task<List<County>> GetCountyByString(string county) => await _pharmacyRepository.GetCountyByString(county);
        //{
        //    var counties = await _pharmacyRepository.GetCountyByString(county);
        //    foreach (var item in counties)
        //    {
        //        item.Id = county.IndexOf(item.CountyName).ToString();
        //    }
        //    return counties;
        //}

        public async Task<List<State>> GetStateByString(string state) => await _pharmacyRepository.GetStateByString(state);

        public async Task<List<City>> GetCityByString(string city) => await _pharmacyRepository.GetCityByString(city);
    }
}

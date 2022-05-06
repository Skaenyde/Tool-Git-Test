using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.Common.ViewModels;
using Portal.Business;
using Portal.Common.Interfaces;
using Portal.Data;
using Portal.Common.Models;
using Newtonsoft.Json;
using Portal.Common.Enums;
using SmartBreadcrumbs.Attributes;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using SmartBreadcrumbs.Nodes;

namespace Portal.Web.Controllers
{
    [Breadcrumb("Pharmacy Directory")]
    public class PharmacyController : BaseController
    {
        private readonly IPharmacyLogic _pharmacyLogic;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContextLogic _userContextLogic;

        #region Properties
        //private readonly AppModule _module = AppModule.Pharmacy;
        public AppModule Module { get; set; } = AppModule.Pharmacy;
        #endregion


        public PharmacyController(IPharmacyLogic pharmacyLogic, IHttpContextAccessor httpContextAccessor, IUserContextLogic userContextLogic) : base(httpContextAccessor, userContextLogic)
        {
            _pharmacyLogic = pharmacyLogic;
            _httpContextAccessor = httpContextAccessor;
            _userContextLogic = userContextLogic;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }
        //As soon as the List view is loaded, GetListData() gets called.
        [Breadcrumb("List")]
        public async Task<IActionResult> List()
        {
               
            return View(await _pharmacyLogic.LoadDropdowns());
        }

        public async Task<DataTableResponse<List<Pharmacy>>> GetListData(DataTableParameters<PharmacyDataTableQuery> model)
        {
            if (model.query == null || (IsNullOrEmpty(model.query.County) 
                                        && IsNullOrEmpty(model.query.StateCode) 
                                        && IsNullOrEmpty(model.query.City) 
                                        && model.query.CompanyId == 0 
                                        && model.query.LineOfBusinessId == 0)
                                        && string.IsNullOrEmpty(model.query.SearchString))
            {
                return new DataTableResponse<List<Pharmacy>>
                {
                    data = new List<Pharmacy>(),
                    draw = 1
                    
                };
            }
            var dataTable = new DataTableResponse<List<Pharmacy>>
            {
                data = await _pharmacyLogic.GetPharmacies(model),
            };

            //dataTable.data = dataTable.data.OrderBy(c => c.QnxtSpecialtyName)
            //    .ThenBy(c => c.CompanyName)
            //    .ThenBy(l => l.LineOfBusinessName)
            //    .ThenBy(l => l.LanguageName)
            //    .ThenBy(d => d.SpecialtyName)
            //    .ToList();
            return dataTable;
        }

        
        public async Task<IActionResult> Details(string id)
        {
            var viewModel = new PharmacyViewModel()
            {
                ActionType = ActionType.View,
                PharmacyStatus = await _pharmacyLogic.GetPharmacyStatusById(id),
                User = new UserPassport()
                {
                    ModulePermission = await _userContextLogic.GetRoleModulePermission(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role), AppModule.Pharmacy)
                }
            };

            viewModel.Pharmacy = await _pharmacyLogic.GetPharmacyById(viewModel.PharmacyStatus.PharmacyId.ToString());

            //Breadcrumb construction
            var childNode1 = new MvcBreadcrumbNode("Index", "Pharmacy", "Pharmacy Directory", false, null, null);
            var childNode2 = new MvcControllerBreadcrumbNode("Controller", viewModel.PharmacyStatus.PharmacyStatusId.ToString())
            {
                OverwriteTitleOnExactMatch = true,
                Parent = childNode1
            };

            ViewData["BreadcrumbNode"] = childNode2;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Reposts page to make an editable form based on isEdit property.
        public async Task<IActionResult> Edit(PharmacyViewModel viewModel)
        {

            viewModel.ActionType = ActionType.Edit;
            viewModel.Pharmacy = await _pharmacyLogic.GetPharmacyById(viewModel.Pharmacy.PharmacyId.ToString());
            viewModel.PharmacyStatus =
                await _pharmacyLogic.GetPharmacyStatusById(viewModel.PharmacyStatus.PharmacyStatusId.ToString());
            viewModel.User = new UserPassport()
            {
                ModulePermission = await _userContextLogic.GetRoleModulePermission(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role), AppModule.Pharmacy),
                RoleDesc = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role)
            };


            return View("Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Posts the form for update once it has been edited.
        public async Task<IActionResult> Save(PharmacyViewModel viewModel)
        {

            //Initialize response variable.
            viewModel.PharmacyStatus.PharmacyId = viewModel.Pharmacy.PharmacyId;
            //viewModel.PharmacyStatus.PharmacyStatusId = Convert.ToInt32(viewModel.Pharmacy.PharmacyStatusId);
            GenericResponse<int> response;

            
            response = await _pharmacyLogic.UpdatePharmacyStatus(viewModel.PharmacyStatus);

            if (response.Success)
                return RedirectToAction(response.Result.ToString(), "Pharmacy/Details");

            else
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View("Edit", viewModel);
            }
        }

        public async Task<List<County>> GetCounty(string county)
        {
            var test = await _pharmacyLogic.GetCountyByString(county);
            return test;
        }

        public async Task<List<State>> GetState(string state) => await _pharmacyLogic.GetStateByString(state);

        public async Task<List<City>> GetCity(string city)
        {
            var test = await _pharmacyLogic.GetCityByString(city);
            return test;
        }

        private static bool IsNullOrEmpty(string[] value)
        {
            return value == null || value.Length == 0 || value[0] == null;
        }
    }
}

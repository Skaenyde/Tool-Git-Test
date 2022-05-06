using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.Common.ViewModels;
using Portal.Common.Interfaces;
using Portal.Common.Models;
using Portal.Common.Enums;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net.Http;
using SmartBreadcrumbs.Attributes;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using SmartBreadcrumbs.Nodes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Web.Filters;

namespace Portal.Web.Controllers
{
    [Breadcrumb("Provider Directory")]

    public class SpecialtyController : BaseController
    {
        #region Fields
        private readonly ISpecialtyLogic _specialtyLogic;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContextLogic _userContextLogic;
        private readonly IGeneralLogic _generalLogic;
        //private AppModule _module = AppModule.Specialty;

        #endregion

        #region Properties
        //public AppModule Module { get { return _module; } set { _module = value; } }
        public AppModule Module { get; set; } = AppModule.Specialty;
        #endregion


        public SpecialtyController(ISpecialtyLogic specialtyLogic, IHttpContextAccessor httpContextAccessor, IUserContextLogic userContextLogic, IGeneralLogic generalLogic) : base(httpContextAccessor, userContextLogic)
        {
            _specialtyLogic = specialtyLogic;
            _httpContextAccessor = httpContextAccessor;
            _userContextLogic = userContextLogic;
            _generalLogic = generalLogic;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        [Breadcrumb("Specialties")]
        [ValidationFilter]
        public async Task<IActionResult> List()
        {
            var viewModel = new SpecialtyCrosswalkViewModel()
            {
                SelectCompanies = await _specialtyLogic.GetCompaniesByName(),
                SelectLineOfBusinesses = await _specialtyLogic.GetLineOfBusinesses(),
                SelectSpecialties = await _specialtyLogic.GetSpecialties(),
                SelectLanguages = await _specialtyLogic.GetLanguages()
                
            };

            viewModel.SelectSpecialties.Insert(0, new SelectListItem { Value = "", Text = "All" });
            return View(viewModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = new SpecialtyCrosswalkViewModel()
            {
                ActionType = ActionType.Add,
                SelectCompanies = await _generalLogic.GetCompanies(),
                SelectLineOfBusinesses = await _specialtyLogic.GetLineOfBusinesses(),
                SpecialtyDemographics = await _specialtyLogic.GetSpecialtyById(id),
                SelectDirectoryTypes = await _specialtyLogic.GetDirectoryTypes(),
                SelectSpecialties = await _specialtyLogic.GetSpecialties(),
                SelectLanguages = await _specialtyLogic.GetLanguages(),
                User = new UserPassport()
                {
                    ModulePermission = await _userContextLogic.GetRoleModulePermission(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role), AppModule.Specialty)
                }
            };

            var childNode1 = new MvcBreadcrumbNode("Index", "Specialty", "Provider Directory", false, null, null);

            var childNode2 = new MvcBreadcrumbNode("Index", "Specialty", "Specialties", false, null, null) {
                Parent = childNode1
            };

            var childNode3 = new MvcControllerBreadcrumbNode("Controller", viewModel.SpecialtyDemographics.SpecialtyName)
            {
                OverwriteTitleOnExactMatch = true,
                Parent = childNode2
            };

            ViewData["BreadcrumbNode"] = childNode3;

            return View(viewModel);
        }



        public async Task<DataTableResponse<List<SpecialtyCrosswalk>>> GetListData(DataTableParameters<SpecialtyCrosswalkViewModel> model)
        {
            var dataTable = new DataTableResponse<List<SpecialtyCrosswalk>>
            {
                data = await _specialtyLogic.Load(),
            };

            dataTable.data = dataTable.data.OrderBy(c => c.SpecialtyName)
                .ThenBy(c => c.CompanyName)
                .ThenBy(l => l.LineOfBusinessName)
                .ThenBy(l => l.LanguageName)
                .ThenBy(d => d.SpecialtyName)
                .ToList();
            
            return dataTable;
        }

        public async Task<DataTableResponse<List<SpecialtyDirectory>>> GetSpecialtyDirectoryData(DataTableParameters<SpecialtyDirectory> model)
        {
           
            var dataTable = new DataTableResponse<List<SpecialtyDirectory>>
            {
                data = await _specialtyLogic.GetSpecialtyDirectories(model),
            };

            return dataTable;
        }

        public async Task<DataTableResponse<List<SpecialtyLanguage>>> GetSpecialtyLanguageData(DataTableParameters<SpecialtyLanguage> model)
        {

            var dataTable = new DataTableResponse<List<SpecialtyLanguage>>
            {
                data = await _specialtyLogic.GetSpecialtyLanguages(model),
            };

            dataTable.draw = model.draw;
            return dataTable;
        }

        //public async Task<IActionResult> GetSpecialtyLanguageData([FromBody] DtParameters)
        //{

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Reposts page to make an editable form based on isEdit property.
        public async Task<IActionResult> Edit(SpecialtyCrosswalkViewModel viewModel)
        {
            viewModel = await ReloadSelectValues(viewModel, ActionType.Edit);
            ModelState.Clear();
            return View("Details", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Posts the form for update once it has been edited.
        public async Task<IActionResult> Save(SpecialtyCrosswalkViewModel viewModel)
        {
            #region variables

            //Temporary values while UserContext is implemented.
            viewModel.SpecialtyCrosswalk.UpdatedBy = "DevUser";
            viewModel.SpecialtyCrosswalk.CreatedBy = "DevUser";
            GenericResponse<int> response = new GenericResponse<int>(0);
            //var specialtyCrosswalkId = viewModel.SpecialtyCrosswalk.SpecialtyCrosswalkId;
            #endregion

            return View("Details");



            ////Setting up model values.
            //if (specialtyCrosswalkId == 0)
            //{
            //    viewModel.SpecialtyCrosswalk.SpecialtyId = int.Parse(viewModel.SpecialtyCrosswalk.SelectSpecialtyValue.Split("_")[0]);

            //}
            //else
            //{
            //    ModelState.Remove("SpecialtyCrosswalk.SpecialtyId");
            //    ModelState.Remove("SpecialtyCrosswalk.SpecialtyCode");
            //    ModelState.Remove("SpecialtyCrosswalk.CompanyId");
            //    ModelState.Remove("SpecialtyCrosswalk.LineOfBusinessId");
            //    ModelState.Remove("SpecialtyCrosswalk.LanguageId");
            //}

            ////Validation of model class members by data attributes specified on the model.
            //if (!ModelState.IsValid)
            //{
            //    viewModel = await ReloadSelectValues(viewModel, ActionType.Add);
            //    return View("Add", viewModel);
            //}



            ////Saving or Updating the record.
            //if (specialtyCrosswalkId == 0) 
            //{
            //    //Response returns the object after being validated and inserted on DB.
            //    //Will return an ID if successful or a fail message.
            //    response = await _specialtyLogic.PostSpecialtyCrosswalk(viewModel.SpecialtyCrosswalk);
            //    viewModel.SpecialtyCrosswalk.SpecialtyCrosswalkId = response.Result;
            //}
            //else //Edit
            //    response = await _specialtyLogic.UpdateSpecialtyCrosswalk(viewModel.SpecialtyCrosswalk);

            ////Evaluating Server Response.
            //if (response.Success)
            //    return RedirectToAction(viewModel.SpecialtyCrosswalk.SpecialtyCrosswalkId.ToString(), "Specialty/Details");
            //else
            //{
            //    viewModel = await ReloadSelectValues(viewModel, ActionType.Add);
            //    ModelState.AddModelError(string.Empty, response.Message);
            //    return View("Add", viewModel);

            //}
        }

        public async Task<IActionResult> LoadSpecialtyDirectory(string SpecialtyDirectoryId)
        {
            var viewModel = new SpecialtyDirectoryViewModel()
            {
                ActionType = ActionType.Edit,
                SelectCompanies = await _generalLogic.GetCompanies(),
                SelectLineOfBusinesses = await _specialtyLogic.GetLineOfBusinesses(),
                SelectSpecialties = await _specialtyLogic.GetSpecialties(),
                SelectLanguages = await _specialtyLogic.GetLanguages(),
                SelectDirectorySection = await _specialtyLogic.GetDirectorySections(),
                SelectDirectoryTypes = await _specialtyLogic.GetDirectoryTypes(),
                SpecialtyDirectory = await _specialtyLogic.GetSpecialtyDirectoryById(SpecialtyDirectoryId),
                User = new UserPassport()
                {
                    ModulePermission = await _userContextLogic.GetRoleModulePermission(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role), AppModule.Specialty)
                }
            };

            var ta = new PartialViewResult
            {
                ViewName = "partials/_specialtydirectory",
                ViewData = new ViewDataDictionary<SpecialtyDirectoryViewModel>(ViewData, viewModel)
            };

            return ta;
        }

        public async Task<IActionResult> LoadSpecialtyLanguage(string specialtyLanguageId)
        {
            var viewModel = new SpecialtyLanguageViewModel()
            {
                ActionType = ActionType.Edit,
                SelectCompanies = await _generalLogic.GetCompanies(),
                SelectLineOfBusinesses = await _specialtyLogic.GetLineOfBusinesses(),
                SelectLanguages = await _specialtyLogic.GetLanguages(),
                SpecialtyLanguage = await _specialtyLogic.GetSpecialtyLanguageById(specialtyLanguageId),
                User = new UserPassport()
                {
                    ModulePermission = await _userContextLogic.GetRoleModulePermission(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role), AppModule.Specialty)
                }

            };

            var ta = new PartialViewResult
            {
                ViewName = "partials/_specialtylanguage",
                ViewData = new ViewDataDictionary<SpecialtyLanguageViewModel>(ViewData, viewModel)
            };

            return ta;
            //return PartialView("_specialtydirectory-edit", viewModel);
        }

        public async Task<IActionResult> AddNewDirectorySection()
        {
            var viewModel = new SpecialtyDirectoryViewModel()
            {
                ActionType = ActionType.Add,
                SelectCompanies = await _generalLogic.GetCompanies(),
                SelectLineOfBusinesses = await _specialtyLogic.GetLineOfBusinesses(),
                SelectSpecialties = await _specialtyLogic.GetSpecialties(),
                //SelectDirectorySection = await _specialtyLogic.GetDirectorySections(),
                SelectDirectoryTypes = await _specialtyLogic.GetDirectoryTypes(),
                User = new UserPassport()
                {
                    ModulePermission = await _userContextLogic.GetRoleModulePermission(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role), AppModule.Specialty)
                }
            };

            return new PartialViewResult
            {
                ViewName = "partials/_specialtydirectory",
                ViewData = new ViewDataDictionary<SpecialtyDirectoryViewModel>(ViewData, viewModel)
            };
        }

        public async Task<IActionResult> AddNewSpecialtyLanguage()
        {
            var viewModel = new SpecialtyLanguageViewModel()
            {
                ActionType = ActionType.Add,
                SelectCompanies = await _generalLogic.GetCompanies(),
                SelectLineOfBusinesses = await _specialtyLogic.GetLineOfBusinesses(),
                SelectLanguages = await _specialtyLogic.GetLanguages(),
                User = new UserPassport()
                {
                    ModulePermission = await _userContextLogic.GetRoleModulePermission(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role), AppModule.Specialty)
                }

            };

            return new PartialViewResult
            {
                ViewName = "partials/_specialtylanguage",
                ViewData = new ViewDataDictionary<SpecialtyLanguageViewModel>(ViewData, viewModel)
            };
        }

        public async Task<IActionResult> CreateSpecialtyDirectory(int specialtyId, int directorySectionId, int companyId, int lineOfBusinessId, int directoryTypeId, string endDate, string effectiveDate)
        {
            var newRecord = new SpecialtyDirectory()
            {
                SpecialtyId = specialtyId,
                DirectorySectionId = directorySectionId,
                CompanyId = companyId,
                LineOfBusinessId = lineOfBusinessId,
                DirectoryTypeId = directoryTypeId,
                EndDate = Convert.ToDateTime(endDate),
                EffectiveDate = Convert.ToDateTime(effectiveDate)
            };

            //Initialize response variable.
            var response = await _specialtyLogic.CreateSpecialtyDirectory(newRecord);

            if (response.Success)
            {
                return Ok();
                //var model = new DataTableParameters<SpecialtyDirectory>() {
                //    query = new SpecialtyDirectory()
                //    {
                //        SpecialtyId = specialtyId
                //    }
                //};

                //return PartialView("partials/_specialtydirectory-list", model);
            }
            else
            {
                //This message will be caught by the exception middleware at Startup.cs
                //Which will transform into a JSON response to be received at Ajax call.
                throw new HttpRequestException(response.Message);
            }
        }

        public async Task<IActionResult> CreateSpecialtyLanguage(int specialtyId, int languageId, int companyId, int lineOfBusinessId, string effectiveDate, string endDate, string specialtyDisplayName)
        {
            var newRecord = new SpecialtyLanguage()
            {
                SpecialtyID = specialtyId,
                LanguageId = languageId,
                CompanyId = companyId,
                LineOfBusinessId = lineOfBusinessId,
                EffectiveDate = Convert.ToDateTime(effectiveDate),
                EndDate = Convert.ToDateTime(endDate),
                DisplayName = specialtyDisplayName
            };

            //Initialize response variable.
            var response = await _specialtyLogic.CreateSpecialtyLanguage(newRecord);


            if (response.Success)
                return Json(Ok());
            else
            {
                //This message will be caught by the exception middleware at Startup.cs
                //Which will transform into a JSON response to be received at Ajax call.
                throw new HttpRequestException(response.Message);
            }


        }

        public async Task<IActionResult> UpdateSpecialtyDirectory(int specialtyId, int specialtyDirectoryId, int directorySectionId, int companyId, int lineOfBusinessId, string endDate)
        {
            var record = new SpecialtyDirectory()
            {
                SpecialtyId = specialtyId,
                SpecialtyDirectoryId = specialtyDirectoryId,
                DirectorySectionId = directorySectionId,
                CompanyId = companyId,
                LineOfBusinessId = lineOfBusinessId,
                EndDate = Convert.ToDateTime(endDate)
            };

            //Initialize response variable.
            var response = await _specialtyLogic.UpdateSpecialtyDirectory(record);

            if (response.Success)
                return Json(Ok());
            else
            {
                //This message will be caught by the exception middleware at Startup.cs
                //Which will transform into a JSON response to be received at Ajax call.
                throw new HttpRequestException(response.Message);
            }

        }

        public async Task<IActionResult> UpdateSpecialtyLanguage(int specialtyId, int specialtyLanguageId, int languageId, int companyId, int lineOfBusinessId, string endDate, string specialtyDisplayName)
        {
            //Create record object
            var record = new SpecialtyLanguage()
            {
                SpecialtyID = specialtyId,
                SpecialtyLanguageId = specialtyLanguageId,
                LanguageId = languageId,
                CompanyId = companyId,
                LineOfBusinessId = lineOfBusinessId,
                EndDate = Convert.ToDateTime(endDate),
                DisplayName = specialtyDisplayName
            };

            //Initialize and fill response variable.
            var response = await _specialtyLogic.UpdateSpecialtyLanguage(record);

            if (response.Success)
                return Json(Ok());
            else
            {
                //This message will be caught by the exception middleware at Startup.cs
                //Which will transform into a JSON response to be received at Ajax call.
                throw new HttpRequestException(response.Message);
            }
        }

        private async Task<SpecialtyCrosswalkViewModel> ReloadSelectValues(SpecialtyCrosswalkViewModel viewModel, ActionType action)
        {
            viewModel.ActionType = action;
            //viewModel.SpecialtyCrosswalk = (action == ActionType.Edit) ? 
            //    await _specialtyLogic.GetSpecialtyById(viewModel.SpecialtyCrosswalk.SpecialtyCrosswalkId.ToString()) 
            //    : new SpecialtyCrosswalk();
            viewModel.SelectLanguages = await _specialtyLogic.GetLanguages();
            viewModel.SelectCompanies = await _generalLogic.GetCompanies();
            viewModel.SelectSpecialties = await _specialtyLogic.GetSpecialties();
            viewModel.SelectLineOfBusinesses = await _specialtyLogic.GetLineOfBusinesses();

            return viewModel;
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<List<Company>> GetCompanyByLob(int lobId) => await _specialtyLogic.GetCompanyByLob(lobId);

        public async Task<List<DirectorySection>> GetDirectorySectionByDirectoryType(int directoryType) => await _specialtyLogic.GetDirectorySectionByDirectoryType(directoryType);

 

    }
}

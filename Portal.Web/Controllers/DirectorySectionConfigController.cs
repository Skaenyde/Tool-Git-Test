using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.Common.ViewModels;
using SmartBreadcrumbs.Attributes;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Portal.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using Portal.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Common.Enums;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net.Http;
using Portal.Web.Filters;
//Test Comment
namespace Portal.Web.Controllers
{
    [Authorize]
    [Breadcrumb("Directory Section Config")]

    public class DirectorySectionConfigController : Controller 
    {
        private readonly ISpecialtyLogic SpecialtyLogic;

        public AppModule Module { get; set; } = AppModule.DirectorySectionConfig;


        public DirectorySectionConfigController(ISpecialtyLogic specialtyLogic)
        {
            SpecialtyLogic = specialtyLogic;

        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<DataTableResponse<List<DirectorySection>>> GetDirectorySectionConfigData(DataTableParameters<DirectorySectionViewModel> model)
        {
            var dataTable = new DataTableResponse<List<DirectorySection>>
            {
                data = await SpecialtyLogic.LoadDirectorySections(),
            };


            return dataTable;
        }

        public async Task<IActionResult> UpdateDirectorySection(int directorySectionId, string directorySectionName, string directorySectionCode, string directorySectionDescription, bool disable, bool online, bool paper)
        {

            var directorySection = new DirectorySection
            {
                DirectorySectionId = directorySectionId,
                DirectorySectionName = directorySectionName,
                DirectorySectionDescription = directorySectionDescription,
                DirectorySectionCode = directorySectionCode
            };

            await SpecialtyLogic.UpdateDirectorySection(directorySection);

            await SpecialtyLogic.UpdateDirectorySectionTypeById(directorySectionId, online, paper);

            return Json(Ok());
        }

        public async Task<IActionResult> CreateDirectorySection(string directorySectionName, string directorySectionDescription, string directorysectionCode, bool paper, bool online)
        {
            //1st: Create the new Directory Section Record at the provider.DirectorySection table.
            var newDirectorySection = new DirectorySection()
            {
                DirectorySectionName = directorySectionName,
                DirectorySectionDescription = directorySectionDescription,
                DirectorySectionCode = directorysectionCode

            };

            var directorySectionResponse = await SpecialtyLogic.CreateDirectorySection(newDirectorySection);
            var directorySectionId = directorySectionResponse.Result;

            //2nd: Create whether the Directory Section is Online/Paper or both at the provider.DirectorySectionType table.
            var directorySectionTypeResponse = await SpecialtyLogic.CreateDirectorySectionType(directorySectionId, paper, online);


            return Json(Ok());

            //if (response.Success)
            //    return Json(Ok());
            //else
            //{
            //    //This message will be caught by the exception middleware at Startup.cs
            //    //Which will transform into a JSON response to be received at Ajax call.
            //    throw new HttpRequestException(response.Message);
            //}


        }

    }
}

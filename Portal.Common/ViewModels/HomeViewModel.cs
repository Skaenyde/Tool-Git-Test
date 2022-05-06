using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Common.Enums;
using Portal.Common.Models;

namespace Portal.Common.ViewModels
{
    public class HomeViewModel
    {
        //public UserContext User {get; set;} with permissions, ID, Name, etc.
        public List<Module> AvailableModules { get; set; }
        //Specialty/List


    }
}
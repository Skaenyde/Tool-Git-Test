using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Portal.Common.Enums;
using Portal.Common.Models;

namespace Portal.Common.ViewModels
{
    public class DirectorySectionViewModel
    {
        //public UserContext User {get; set;} with permissions, ID, Name, etc.
        public ActionType ActionType { get; set; }

        public UserPassport User { get; set; }

        public List<DirectorySection> DirectorySections { get; set; }
        
        public DirectorySection DirectorySection { get; set; }

        [JsonProperty("chkDisable")]
        public bool chkDisable { get; set; }

        [JsonProperty("chkNewPaper")]
        public bool chkNewPaper { get; set; }

        [JsonProperty("chkNewOnline")]
        public bool chkNewOnline { get; set; }

        [JsonProperty("chkEditPaper")]
        public bool chkEditPaper { get; set; }

        [JsonProperty("chkEditOnline")]
        public bool chkEditOnline { get; set; }
    }
}
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Portal.Common.Enums;
using Portal.Common.Models;

namespace Portal.Common.ViewModels
{
    public class PermissionViewModel
    {
        [JsonProperty("SpecialtyPermissionId")]
        public int SpecialtyPermissionId { get; set; }

        [JsonProperty("PharmacyPermissionId")]
        public int PharmacyPermissionId { get; set; }

        [JsonProperty("RoleCompanyAccessDesc")]
        public string RoleCompanyAccessDesc { get; set; }

        [JsonProperty("chkMMMFL")]
        public bool chkMMMFL { get; set; }

        [JsonProperty("chkMMMMH")]
        public bool chkMMMMH { get; set; }

        [JsonProperty("chkMMM")]
        public bool chkMMM { get; set; }

        [JsonProperty("chkPMC")]
        public bool chkPMC { get; set; }


        public List<SelectListItem> SelectRoles { get; set; }

        [JsonProperty("RoleCompanyAccessId")]
        public int RoleCompanyAccessId { get; set; }

        [JsonProperty("RolePermissionId")]
        public int RolePermissionId { get; set; }

        [JsonProperty("chkRoleCompanyAccessIsActive")]
        public bool chkRoleCompanyAccessIsActive { get; set; }

        [JsonProperty("chkRoleModuleAccess")]
        public bool chkRoleModuleAccess { get; set; }

        public List<SelectListItem> SelectPermissions { get; set; }

    }
}
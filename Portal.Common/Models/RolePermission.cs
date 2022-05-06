using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Common.Models
{
    public class RolePermission
    {

        [JsonProperty("RolePermissionId")]
        public int RolePermissionId { get; set; }

        [JsonProperty("RoleId")]
        public int RoleId { get; set; }

        [JsonProperty("RoleDesc")]
        public string RoleDesc { get; set; }

        [JsonProperty("ModuleId")]
        public int ModuleId { get; set; }

        [JsonProperty("ModuleDesc")]
        public string ModuleDesc { get; set; }

        [JsonProperty("PermissionTypeId")]
        public int PermissionTypeId { get; set; }

        [JsonProperty("PermissionTypeDesc")]
        public string PermissionTypeDesc { get; set; }

        [JsonProperty("UpdateDate")]
        public DateTime? UpdateDate { get; set; }

        [JsonProperty("UpdateBy")]
        public string UpdateBy { get; set; }

    }
}

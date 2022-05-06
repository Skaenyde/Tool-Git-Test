using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Common.Models
{
    public class RoleCompanyAccess
    {

        [JsonProperty("RoleCompanyAccessId")]
        public int RoleCompanyAccessId { get; set; }

        [JsonProperty("RoleAccessDesc")]
        public string RoleAccessDesc { get; set; }

        [JsonProperty("RoleId")]
        public int RoleId { get; set; }

        [JsonProperty("CompanyId")]
        public int CompanyId { get; set; }

        [JsonProperty("RecordActionType")]
        public string RecordActionType { get; set; }

        [JsonProperty("CreateDate")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("CreatedBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("UpdateDate")]
        public DateTime? UpdateDate { get; set; }

        [JsonProperty("UpdatedBy")]
        public string UpdatedBy { get; set; }

        [JsonProperty("CompanyName")]
        public string CompanyName { get; set; }

        [JsonProperty("CompanyCode")]
        public string CompanyCode { get; set; }

        //Comes from dbo.BiAdminToolRole
        [JsonProperty("RoleDesc")]
        public string RoleDesc { get; set; }

    }
}

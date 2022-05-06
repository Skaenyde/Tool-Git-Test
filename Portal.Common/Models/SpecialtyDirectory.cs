using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Common.Models
{
    public class SpecialtyDirectory
    {
        
        [JsonProperty("SpecialtyDirectoryId")]
        [HiddenInput]
        public int SpecialtyDirectoryId { get; set; }

        [JsonProperty("DirectorySectionId")]
        public int DirectorySectionId { get; set; }

        [JsonProperty("LineOfBusinessId")]
        public int LineOfBusinessId { get; set; }

        [JsonProperty("CompanyId")]
        public int CompanyId { get; set; }

        [JsonProperty("SpecialtyId")]
        public int SpecialtyId { get; set; }

        [JsonProperty("DirectoryTypeId")]
        public int DirectoryTypeId { get; set; }

        [JsonProperty("EffectiveDate")]
        public DateTime EffectiveDate { get; set; }

        [JsonProperty("EndDate")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("CreateDate")]
        public DateTime? CreateDate { get; set; }

        [JsonProperty("CreatedBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("UpdateDate")]
        public DateTime? UpdateDate { get; set; }

        [JsonProperty("UpdatedBy")]
        public string UpdatedBy { get; set; }

        [JsonProperty("DataSourceKey")]
        public string DataSourceKey { get; set; }

        [JsonProperty("DataSourceTable")]
        public string DataSourceTable { get; set; }

        [JsonProperty("SourceSystemId")]
        public int? SourceSystemId { get; set; }

        [JsonProperty("RecordActionType")]
        public string RecordActionType { get; set; }

        [JsonProperty("ETLExecutionLogId")]
        public int? ETLExecutionLogId { get; set; }

        [JsonProperty("ETLBatchLogId")]
        public int? ETLBatchLogId { get; set; }

        [JsonProperty("ETLLastUpdateDatetime")]
        public DateTime? ETLLastUpdateDatetime { get; set; }

        [JsonProperty("ETLFingerprint")]
        public string ETLFingerprint { get; set; }


        //Additional fielts other than entity.

        [JsonProperty("DirectorySectionDescription")]
        public string DirectorySectionDescription { get; set; }

        [JsonProperty("LineOfBusinessName")]
        public string LineOfBusinessName { get; set; }

        [JsonProperty("Company")]
        public string Company { get; set; }


        [JsonProperty("Active")]
        public bool Active { get; set; }

        [JsonProperty("DisplayFlag")]
        public string DisplayFlag { get; set; }

    }
}

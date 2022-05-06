using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Portal.Common.Models
{
   public class SpecialtyLanguage
    {
        [HiddenInput]
        [JsonProperty("SpecialtyLanguageId")]
        public int SpecialtyLanguageId { get; set; }

        [JsonProperty("SpecialtyID")]
        public int SpecialtyID { get; set; }

        [Required(ErrorMessage = "Please select a Language.")]
        [JsonProperty("LanguageId")]
        public int LanguageId { get; set; }

        [JsonProperty("CompanyId")]
        [Required(ErrorMessage = "Please select a company.")]
        public int CompanyId { get; set; }

        [JsonProperty("LineOfBusinessId")]
        [Required(ErrorMessage = "Please select a line of business.")]
        public int LineOfBusinessId { get; set; }

        [JsonProperty("DisplayName")]
        [Required(ErrorMessage = "Please enter a value for display name.")]
        public string DisplayName { get; set; }

        [JsonProperty("EffectiveDate")]
        public DateTime EffectiveDate { get; set; }

        [JsonProperty("EndDate")]
        public DateTime EndDate { get; set; }

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


        //Additionally created properties of model besides the entity.
        [JsonProperty("Language")]
        public string Language { get; set; }

        [JsonProperty("Company")]
        public string Company { get; set; }

        [JsonProperty("Lob")]
        public string Lob { get; set; }


        [JsonProperty("Active")]
        public bool Active{ get; set; }
    }
}

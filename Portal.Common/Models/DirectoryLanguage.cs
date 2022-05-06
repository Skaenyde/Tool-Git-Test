using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Common.Models
{
    public class DirectoryLanguage
    {
        [JsonProperty("SpecialtyDirectoryLanguageId")]
        public int SpecialtyDirectoryLanguageId { get; set; }

        [JsonProperty("LanguageId")]
        public int LanguageId { get; set; }

        [JsonProperty("SpecialtyDirectoryId")]
        public int SpecialtyDirectoryId { get; set; }

        [JsonProperty("EffectiveDate")]
        public DateTime EffectiveDate { get; set; }

        [JsonProperty("EndDate")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }

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
    }
}

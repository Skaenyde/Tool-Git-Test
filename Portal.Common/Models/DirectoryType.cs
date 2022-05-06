using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Common.Models
{
    public class DirectoryType
    {
        [JsonProperty("DirectoryTypeId")]
        public int DirectoryTypeId { get; set; }

        [JsonProperty("DirectoryTypeCode")]
        public string DirectoryTypeCode { get; set; }

        [JsonProperty("DirectoryTypeName")]
        public string DirectoryTypeName { get; set; }

        [JsonProperty("DirectoryTypeDesc")]
        public string DirectoryTypeDesc { get; set; }

        [JsonProperty("RecordActionType")]
        public string RecordActionType { get; set; }

        [JsonProperty("CreateDate")]
        public DateTime? CreateDate { get; set; }

        [JsonProperty("CreatedBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("UpdateDate")]
        public DateTime? UpdateDate { get; set; }

        [JsonProperty("UpdatedBy")]
        public string UpdatedBy { get; set; }
    }
}

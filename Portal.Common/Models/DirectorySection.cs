using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Common.Models
{
    public class DirectorySection
    {
        [JsonProperty("DirectorySectionId")]
        public int DirectorySectionId { get; set; }

        [JsonProperty("DirectorySectionCode")]
        public string DirectorySectionCode { get; set; }

        [JsonProperty("DirectorySectionName")]
        public string DirectorySectionName { get; set; }

        [JsonProperty("DirectorySectionDescription")]
        public string DirectorySectionDescription { get; set; }

        [JsonProperty("CreateDate")]
        public DateTime? CreateDate { get; set; }

        [JsonProperty("CreatedBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("UpdateDate")]
        public DateTime? UpdateDate { get; set; }

        [JsonProperty("UpdatedBy")]
        public string UpdatedBy { get; set; }

        [JsonProperty("RecordActionType")]
        public string RecordActionType { get; set; }

        //Extended Properties
        [JsonProperty("DirectoryTypeId")]
        public int DirectoryTypeId { get; set; }

        [JsonProperty("DirectoryTypeName")]
        public string DirectoryTypeName { get; set; }

        [JsonProperty("Disable")]
        public bool Disable { get; set; }

        [JsonProperty("Paper")]
        public string Paper { get; set; }

        [JsonProperty("Online")]
        public string Online { get; set; }
    }
}

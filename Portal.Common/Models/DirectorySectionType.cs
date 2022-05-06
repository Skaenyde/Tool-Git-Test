using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Common.Models
{
    public class DirectorySectionType
    {
        [JsonProperty("DirectoryTypeSectionId")]
        public int DirectoryTypeSectionId { get; set; }

        [JsonProperty("DirectorySectionId")]
        public int DirectorySectionId { get; set; }

        [JsonProperty("DirectoryTypeId")]
        public int DirectoryTypeId { get; set; }

        [JsonProperty("CreateDate")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("CreatedBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("UpdateDate")]
        public string UpdateDate { get; set; }

        [JsonProperty("UpdatedBy")]
        public string UpdatedBy { get; set; }

        [JsonProperty("RecordActionType")]
        public string RecordActionType { get; set; }

    }
}

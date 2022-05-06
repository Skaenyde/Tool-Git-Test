using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Portal.Common.Models
{
    public class PharmacyStatus
    {
        [JsonProperty("PharmacyStatusId")]
        public int PharmacyStatusId { get; set; }

        [JsonProperty("PharmacyId")]
        public int PharmacyId { get; set; }

        [JsonProperty("RetailFlag")]
        public string RetailFlag { get; set; }

        [JsonProperty("RetailExtSupplyFlag")]
        public string RetailExtSupplyFlag { get; set; }

        [JsonProperty("HifFlag")]
        public string HifFlag { get; set; }

        [JsonProperty("LtcFlag")]
        public string LtcFlag { get; set; }

        [JsonProperty("IhsFlag")]
        public string IhsFlag { get; set; }

        [JsonProperty("MailFlag")]
        public string MailFlag { get; set; }

        [JsonProperty("MailPreferredFlag")]
        public string MailPreferredFlag { get; set; }

        [JsonProperty("PreferredFlag")]
        public string PreferredFlag { get; set; }

        [JsonProperty("LimitedAccessFlag")]
        public string LimitedAccessFlag { get; set; }

        [JsonProperty("eRxFlag")]
        public string eRxFlag { get; set; }

        [JsonProperty("TtyFlag")]
        public string TtyFlag { get; set; }

        [JsonProperty("Hours24Flag")]
        public string Hours24Flag { get; set; }

        [JsonProperty("ContractedFlag")]
        public string ContractedFlag { get; set; }

        [JsonProperty("PartBFlag")]
        public string PartBFlag { get; set; }

        [JsonProperty("SpecialtyFlag")]
        public string SpecialtyFlag { get; set; }

        [JsonProperty("AdherenceFlag")]
        public string AdherenceFlag { get; set; }

        [JsonProperty("DirectoryDisplayFlag")]
        public string DirectoryDisplayFlag { get; set; }
    }

}
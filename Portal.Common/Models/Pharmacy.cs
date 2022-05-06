using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace Portal.Common.Models
{
    public class Pharmacy
    {
        
        [JsonProperty("PharmacyId")]
        public int PharmacyId { get; set; }

        [JsonProperty("NCPDP")]
        public string NCPDP { get; set; }

        [JsonProperty("NPI")]
        public string NPI { get; set; }

        [JsonProperty("PharmacyName")]
        public string PharmacyName { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("CityName")]
        public string CityName { get; set; }

        [JsonProperty("County")]
        public string County { get; set; }

        [JsonProperty("StateCode")]
        public string StateCode { get; set; }

        [JsonProperty("ZipCode")]
        public string ZipCode { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("HoursOfOperation")]
        public string HoursOfOperation { get; set; }

        [JsonProperty("PharmacyChain")]
        public string PharmacyChain { get; set; }

        [JsonProperty("PharmacyStatusId")]
        public string PharmacyStatusId { get; set; }

        [JsonProperty("CompanyName")]
        public string CompanyName { get; set; }

    }

}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Portal.Common.Models
{
    public class SpecialtyCrosswalk
    {
        [HiddenInput]
        [Required(ErrorMessage = "Please select a Specialty.")]
        [JsonProperty("SpecialtyId")]
        public int SpecialtyId { get; set; }

        [Required(ErrorMessage = "The Specialty Name field is required.")]
        [JsonProperty("SpecialtyName")]
        public string SpecialtyName { get; set; }

        [Required]
        [JsonProperty("LanguageId")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a language.")]
        public int LanguageId { get; set; }

        [Required]
        [JsonProperty("DirectoryType")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a directory type.")]
        public int DirectoryType { get; set; }

        [JsonProperty("LanguageName")]
        public string LanguageName { get; set; }

        [Required]
        [JsonProperty("LineOfBusinessId")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a line of business.")]
        public int LineOfBusinessId { get; set; }

        [JsonProperty("LineOfBusinessName")]
        public string LineOfBusinessName { get; set; }

        [Required]
        [JsonProperty("CompanyId")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a company.")]
        public int CompanyId { get; set; }

        [JsonProperty("CompanyName")]
        [Display(Name = "Company::")]
        public string CompanyName { get; set; }

        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }

        [JsonProperty("EffectiveDate")]
        [Required(ErrorMessage = "Effective date is required.")]
        [DataType(DataType.Date)]
        public DateTime? EffectiveDate { get; set; }

        [JsonProperty("EndDate")]
        [Required(ErrorMessage = "End date is required.")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; } = new DateTime(2078,12,31);


        [Required(ErrorMessage = "Please select a Specialty.")]
        [JsonProperty("SpecialtyCode")]
        public string SpecialtyCode { get; set; }

        //=================================================== DDL Properties.

        [JsonProperty("SelectSpecialtyValue")]
        public string SelectSpecialtyValue { get; set; }


        [JsonProperty("CreateDate")]
        public DateTime? CreateDate { get; set; }

        [JsonProperty("CreatedBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("UpdateDate")]
        public DateTime? UpdateDate { get; set; }

        [JsonProperty("UpdatedBy")]
        public string UpdatedBy { get; set; }

        //[JsonProperty("Status")]
        //public string Status { get; set; }

        [JsonProperty("DisplayFlag")]
        public string DisplayFlag { get; set; }

    }
}
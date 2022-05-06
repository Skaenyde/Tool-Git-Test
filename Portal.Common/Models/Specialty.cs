using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Common.Models
{
    public class Specialty
    {
        [HiddenInput]
        public int SpecialtyId { get; set; }

        public string SpecialtyCode { get; set; }

        public string SpecialtyName { get; set; }
    }
}

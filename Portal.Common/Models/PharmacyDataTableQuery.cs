using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portal.Common.Models
{
    [Serializable()]
    public class PharmacyDataTableQuery{

        public string SearchString { get; set; }
        public int LineOfBusinessId { get; set; }
        public int CompanyId { get; set; }
        public string[] StateCode { get; set; }
        public string[] City { get; set; }
        public string[] County { get; set; }
    }




}

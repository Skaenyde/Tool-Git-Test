using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Common.Models
{
    public class Role : IdentityUser
    {
        public int RoleId { get; set; }

        public string RoleDesc { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string UpdatedBy { get; set; }

        public string RecordActionType { get; set; }
    }
}

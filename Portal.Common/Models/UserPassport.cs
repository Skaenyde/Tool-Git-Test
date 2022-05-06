using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Portal.Common.Enums;

namespace Portal.Common.Models
{
    public class UserPassport : IdentityUser
    {
        public int RoleId { get; set; }

        public string RoleDesc { get; set; }

        public List<RolePermission> Permissions { get; set; }

        public Common.Enums.PermissionType ModulePermission { get; set; }
    }
}

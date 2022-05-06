using System;


namespace Portal.Common.Models
{
    public class PermissionType
    {
        public int PermissionTypeId { get; set; }

        public string PermissionTypeDesc { get; set; }

        public DateTime? CreateDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string UpdatedBy { get; set; }

        public string RecordActionType { get; set; }

    }

}

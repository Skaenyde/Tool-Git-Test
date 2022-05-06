using System;
using System.Collections.Generic;
using System.Text;

//This Enum is 1:1 with the universal.Company table in EDH_DST.
namespace Portal.Common.Enums
{
    //These IDs are the relation between UserRole and the CompanyId.
    // This Company ID defines to which data he has access.

    public enum UserRoleCompanyId
    {
        //Unknown = -1, //Company Code = Unknown
        //NA = -2, //Company Code = NA
        //NO_CARRIER = 1, //Company Code = ''
        //MMM_of_Florida = 2, //Company Code = MMMFL
        //MMM_Multi_Health = 3, //Company Code = MMMMH
        //Medicare_y_Mucho_Mas = 4, //Company Code = MMM
        //Preferred_Medicare_Choice = 5, //Company Code = PMC
        Provider_Group_FL = 2,

    }
}

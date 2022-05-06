using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Common.Interfaces
{
    public interface IHomeRepository
    {
        Task<List<Language>> GetUser();

    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCaseWork.AuthServer.Data
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName;
        public string LastName;
        public int LocationId;
    }
}

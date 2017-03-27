using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCaseWork.AuthServer.Data
{
    public class UserInitializer
    {
        private UserManager<ApplicationUser> _userManager;

        public UserInitializer(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public void Initialize(ApplicationDbContext context)
        {
            context.Database.Migrate();
        }

        public async Task Seed(ApplicationDbContext context)
        {

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            ApplicationUser user = new ApplicationUser { FirstName = "Joe", LastName = "Blow", LocationId = 1, UserName = "jblow" };
            await _userManager.CreateAsync(user, "1!Password");

        }
        
    }
}

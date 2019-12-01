using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace COMP229_301044056_Assignment02.Models
{
    public class IdentitySeedData
    {
        private const string generalUser = "general1";
        private const string generalPassword = "Secret123$";
        private const string generalUser2 = "general2";
        private const string generalPassword2 = "Secret124$";

        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)
        {
            IdentityUser user = await userManager.FindByIdAsync(generalUser);
            if (user == null)
            {
                user = new IdentityUser("general1");
                await userManager.CreateAsync(user, generalPassword);
            }
            IdentityUser user2 = await userManager.FindByIdAsync(generalUser2);
            if (user2 == null)
            {
                user = new IdentityUser("general2");
                await userManager.CreateAsync(user, generalPassword2);
            }

        }
    }
}

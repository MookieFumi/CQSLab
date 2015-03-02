using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CQSLab.UI.Models
{
    public class SecurityContext : IdentityDbContext<ApplicationUser>
    {
        public SecurityContext()
            : base("Model")
        {
            Database.Initialize(false);
        }

        public static SecurityContext Create()
        {
            return new SecurityContext();
        }
    }

    public class ApplicationUser : IdentityUser
    {
        public string ProviderName { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}

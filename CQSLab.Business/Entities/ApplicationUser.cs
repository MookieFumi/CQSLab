using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CQSLab.Business.Entities
{
    public class ApplicationUser : IdentityUser, IUser<string>
    {
        public ApplicationUser()
        {
            UserLanguages = new HashSet<UserLanguage>();
            UserStudies = new HashSet<UserStudy>();
        }

        public string ProviderName { get; set; }

        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Company { get; set; }
        public virtual DateTime? BirhDate { get; set; }
        public virtual string Address { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string City { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Phone2 { get; set; }

        public byte[] Image { get; set; }

        public virtual ICollection<UserLanguage> UserLanguages { get; set; }
        public virtual ICollection<UserStudy> UserStudies { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
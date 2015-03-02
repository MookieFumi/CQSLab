using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
}

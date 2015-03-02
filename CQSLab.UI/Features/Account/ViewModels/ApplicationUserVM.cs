using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CQSLab.UI.Features.Account.ViewModels
{
    public class ApplicationUserVM
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string ProviderName { get; set; }
        [Required]
        public string UserName { get; set; }
        
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "Company")]
        public string Company { get; set; }

        [Display(Name = "Profile image")]
        public HttpPostedFileBase Image { get; set; }

        public string SavedImage { get; set; }
    }
}

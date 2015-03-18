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
        public string Id { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        public string ProviderName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        [Required]
        public string Surname { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Phone2")]
        public string Phone2 { get; set; }

        [Display(Name = "Profile image")]
        public HttpPostedFileBase Image { get; set; }

        public string SavedImage { get; set; }
    }
}

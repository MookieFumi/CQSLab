using System.ComponentModel.DataAnnotations;

namespace CQSLab.UI.Features.Customers.ViewModels
{
    public class CustomerVM
    {
        [Required]
        [Display(Name = "Name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //[Required]
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        //[Display(Name = "FechaApertura", ResourceType = typeof(CrossCutting.Resources.Common))]
        //public DateTime? FechaApertura { get; set; }
    }
}

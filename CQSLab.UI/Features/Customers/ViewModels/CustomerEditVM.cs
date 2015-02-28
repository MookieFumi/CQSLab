using System.ComponentModel.DataAnnotations;

namespace CQSLab.UI.Features.Customers.ViewModels
{
    public class CustomerEditVM : CustomerVM
    {
        [Required]
        [Display(Name = "Id")]
        public int CustomerId { get; set; }
    }
}
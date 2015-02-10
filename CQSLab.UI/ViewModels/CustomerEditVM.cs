using System.ComponentModel.DataAnnotations;

namespace CQSLab.UI.ViewModels
{
    public class CustomerEditVM : CustomerVM
    {
        [Required]
        [Display(Name = "Id")]
        public int CustomerId { get; set; }
    }
}
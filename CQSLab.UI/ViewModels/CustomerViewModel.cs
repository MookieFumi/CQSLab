using System.ComponentModel.DataAnnotations;

namespace CQSLab.UI.ViewModels
{
    public class CustomerViewModel : CustomerVM
    {
        [Required]
        [Display(Name = "Id")]
        public int CustomerId { get; set; }
    }
}
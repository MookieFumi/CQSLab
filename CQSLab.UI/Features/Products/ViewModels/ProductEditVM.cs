using System.ComponentModel.DataAnnotations;

namespace CQSLab.UI.Features.Products.ViewModels
{
    public class ProductEditVM : ProductVM
    {
        [Required]
        [Display(Name = "Id")]
        public int ProductId { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace CQSLab.UI.ViewModels
{
    public class ProductVM
    {
        [Required]
        [Display(Name = "Name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        [MaxLength(450)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Tag")]
        public string Tag { get; set; }

        [Display(Name = "ImageUrl")]
        public string ImageUrl { get; set; }
    }
}
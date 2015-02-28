using System.ComponentModel.DataAnnotations;

namespace CQSLab.UI.Features.Channels.ViewModels
{
    public class ChannelVM
    {
        [Required]
        [Display(Name = "Description")]
        [MaxLength(450)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
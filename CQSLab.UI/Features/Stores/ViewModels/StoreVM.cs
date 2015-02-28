using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CQSLab.UI.Features.Stores.ViewModels
{
    public class StoreVM
    {
        [Required]
        [Display(Name = "Description")]
        [MaxLength(450)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Channel")]
        public string ChannelId { get; set; }

        public IEnumerable<SelectListItem> Channels { get; set; }
    }
}
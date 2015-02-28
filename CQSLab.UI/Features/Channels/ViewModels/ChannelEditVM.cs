using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CQSLab.UI.Features.Channels.ViewModels
{
    public class ChannelEditVM : ChannelVM
    {
        [Required]
        [Display(Name = "Id")]
        public int ChannelId { get; set; }

        [Display(Name = "Budgets")]
        public IEnumerable<int> Budgets { get; set; }
    }
}
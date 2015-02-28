using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CQSLab.UI.ViewModels;

namespace CQSLab.UI.Features.Channels.ViewModels
{
    public class BudgetEditVM : MonthlyDataVM
    {
        [Required]
        [Display(Name = "Id")]
        public int ChannelId { get; set; }

        [Required]
        [Display(Name = "Accountant Period")]
        public int AccountantPeriod { get; set; }
    }
}
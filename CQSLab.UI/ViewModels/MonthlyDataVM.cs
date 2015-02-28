using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQSLab.UI.ViewModels
{
    public class MonthlyDataVM
    {
        [Required]
        [Display(Name = "January")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal January { get; set; }

        [Required]
        [Display(Name = "February")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal February { get; set; }

        [Required]
        [Display(Name = "March")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal March { get; set; }

        [Required]
        [Display(Name = "April")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal April { get; set; }

        [Required]
        [Display(Name = "May")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal May { get; set; }

        [Required]
        [Display(Name = "June")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal June { get; set; }

        [Required]
        [Display(Name = "July")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal July { get; set; }

        [Required]
        [Display(Name = "August")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal August { get; set; }

        [Required]
        [Display(Name = "September")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal September { get; set; }

        [Required]
        [Display(Name = "October")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal October { get; set; }

        [Required]
        [Display(Name = "November")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal November { get; set; }

        [Required]
        [Display(Name = "December")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal December { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CQSLab.UI.ViewModels
{
    public class CustomerVM
    {
        [Required]
        [Display(Name = "Name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //[Required]
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        //[Display(Name = "FechaApertura", ResourceType = typeof(CrossCutting.Resources.Common))]
        //public DateTime? FechaApertura { get; set; }
    }
}

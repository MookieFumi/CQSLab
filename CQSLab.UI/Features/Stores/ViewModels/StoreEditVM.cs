using System.ComponentModel.DataAnnotations;
using CQSLab.UI.Features.Channels.ViewModels;

namespace CQSLab.UI.Features.Stores.ViewModels
{
    public class StoreEditVM : StoreVM
    {
        [Required]
        [Display(Name = "Id")]
        public int StoreId { get; set; }
    }
}
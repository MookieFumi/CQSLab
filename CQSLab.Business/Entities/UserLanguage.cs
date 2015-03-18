using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CQSLab.Business.Entities
{
    public class UserLanguage
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Language Language { get; set; }

        public virtual int LanguageId { get; set; }

        public virtual LevelLanguage LevelLanguage { get; set; }

        public virtual int LevelLanguageId { get; set; }
    }
}
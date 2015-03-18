namespace CQSLab.Business.Entities
{
    public class UserStudy
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual AcademicLevel AcademicLevel { get; set; }

        public virtual int AcademicLevelId { get; set; }

        public virtual AcademicTraining AcademicTraining { get; set; }

        public virtual int AcademicTrainingId { get; set; }

        public virtual string Comments { get; set; }
    }
}
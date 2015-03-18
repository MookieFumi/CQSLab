namespace CQSLab.Business.Entities
{
    public class AcademicTraining
    {
        public virtual int AcademicTrainingId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public virtual AcademicLevel AcademicLevel { get; set; }
        public virtual int AcademicLevelId { get; set; }
    }
}
namespace CQSLab.Business.Entities
{
    public class MonthlyData
    {
        public virtual int AccountantPeriod { get; set; }
        public virtual decimal January { get; set; }
        public virtual decimal February { get; set; }
        public virtual decimal March { get; set; }
        public virtual decimal April { get; set; }
        public virtual decimal May { get; set; }
        public virtual decimal June { get; set; }
        public virtual decimal July { get; set; }
        public virtual decimal August { get; set; }
        public virtual decimal September { get; set; }
        public virtual decimal October { get; set; }
        public virtual decimal November { get; set; }
        public virtual decimal December { get; set; }
    }
}
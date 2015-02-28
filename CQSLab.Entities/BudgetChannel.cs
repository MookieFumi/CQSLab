namespace CQSLab.Entities
{
    public class BudgetChannel : MonthlyData
    {
        public virtual Channel Channel { get; set; }
        public virtual int ChannelId { get; set; }
    }
}
namespace CQSLab.Business.Entities
{
    public class BudgetStore : MonthlyData
    {
        public virtual Store Store { get; set; }
        public virtual int StoreId { get; set; }
    }
}
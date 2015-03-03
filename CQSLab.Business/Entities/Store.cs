using System.Collections.Generic;

namespace CQSLab.Business.Entities
{
    public class Store
    {
        public Store()
        {
            Budgets = new HashSet<BudgetStore>();
        }

        public virtual int StoreId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public virtual Channel Channel { get; set; }
        public virtual int ChannelId { get; set; }

        public ICollection<BudgetStore> Budgets { get; set; }
    }
}
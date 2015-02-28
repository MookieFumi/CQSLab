using System.Collections.Generic;

namespace CQSLab.Entities
{
    public class Channel
    {
        public Channel()
        {
            Stores = new HashSet<Store>();
            Budgets = new HashSet<BudgetChannel>();
        }

        public virtual int ChannelId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public ICollection<Store> Stores { get; set; }
        public ICollection<BudgetChannel> Budgets { get; set; }
    }
}
using System.Collections.Generic;

namespace CQSLab.Entities
{
    public class Channel
    {
        public Channel()
        {
            Budgets = new HashSet<BudgetChannel>();
        }

        public virtual int ChannelId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public ICollection<BudgetChannel> Budgets { get; set; }
    }
}
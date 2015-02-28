using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using CQSLab.Entities.DatabaseInitializers.Configurations;

namespace CQSLab.Entities
{
    public class ModelContext : DbContext
    {
        public ModelContext()
        {
            Initialization();
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 720;
        }

        public ModelContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Initialization();
        }

        #region "DBSet"

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        public DbSet<Channel> Channels { get; set; }
        public DbSet<Store> Stores { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            RemoveConventions(modelBuilder);

            AddCustomConventions(modelBuilder);

            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new OrderLineConfiguration());
            modelBuilder.Configurations.Add(new ChannelConfiguration());
            modelBuilder.Configurations.Add(new StoreConfiguration());
            modelBuilder.Configurations.Add(new BudgetChannelConfiguration());
            modelBuilder.Configurations.Add(new BudgetStoreConfiguration());
        }

        private static void RemoveConventions(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        private static void AddCustomConventions(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>().Where(x => x.Name == "Name").Configure(c => c.HasMaxLength(50).IsRequired());
            modelBuilder.Properties<string>().Where(x => x.Name == "Description").Configure(c => c.HasMaxLength(450));
        }

        private void Initialization()
        {
            Configuration.ProxyCreationEnabled = false;
        }
    }
}

using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using CQSLab.Business.DatabaseInitializers.Configurations;
using CQSLab.Business.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CQSLab.Business
{
    public class ModelContext : IdentityDbContext<ApplicationUser>
    {
        public ModelContext() : base("name=ModelContext") { }

        public ModelContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Initialization();
        }

        public static ModelContext Create()
        {
            var nameOrConnectionString = ConfigurationManager.ConnectionStrings["ModelContext"].ConnectionString;
            return new ModelContext(nameOrConnectionString);
        }

        #region "DBSet"

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        public DbSet<Channel> Channels { get; set; }
        public DbSet<BudgetChannel> BudgetsChannel { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<BudgetStore> BudgetsStore { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            RemoveConventions(modelBuilder);
            AddCustomConventions(modelBuilder);

            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());

            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new OrderLineConfiguration());
            modelBuilder.Configurations.Add(new ChannelConfiguration());
            modelBuilder.Configurations.Add(new StoreConfiguration());
            modelBuilder.Configurations.Add(new BudgetChannelConfiguration());
            modelBuilder.Configurations.Add(new BudgetStoreConfiguration());

            modelBuilder.Configurations.Add(new LanguageConfiguration());
            modelBuilder.Configurations.Add(new LevelLanguageConfiguration());
            modelBuilder.Configurations.Add(new UserLanguageConfiguration());

            modelBuilder.Configurations.Add(new AcademicLevelConfiguration());
            modelBuilder.Configurations.Add(new AcademicTrainingConfiguration());
            modelBuilder.Configurations.Add(new UserStudyConfiguration());
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

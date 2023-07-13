using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechBank.Data.Configurations;
using TechBank.DomainModel;

namespace TechBank.Data
{
    public class WebAppContext : IdentityDbContext<User, Role, int>
    {
        public WebAppContext(DbContextOptions<WebAppContext> options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<AppCustomer> Customers { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<AccountTransaction> AccountTransactions { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new AccountTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new BankAccountConfiguration());            
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ProcessSave();
            return base.SaveChanges();
        }

        public int SaveChanges(string email)
        {
            var securityDAC = new SecurityDAC();
            var user = securityDAC.GetUserByEmail(email);
            ProcessSave(user.Id);
            return base.SaveChanges();
        }

        private void ProcessSave(int? userId = null)
        {
            var currentTime = DateTime.UtcNow;

            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is IAuditEntity))
            {
                var entity = item.Entity as IAuditEntity;
                entity.Created = currentTime;
                entity.CreatedBy = userId.HasValue ? userId.Value : 1;
                entity.Modified = currentTime;
                entity.ModifiedBy = userId.HasValue ? userId.Value : 1;
            }

            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified && e.Entity is IAuditEntity))
            {
                var entity = item.Entity as IAuditEntity;
                entity.Modified = currentTime;
                entity.ModifiedBy = userId.HasValue ? userId.Value : 1;
            }
        }
    }
}
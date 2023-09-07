using Microsoft.EntityFrameworkCore;
using WBank.API.Models.Domain;

namespace WBank.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountTransaction> AccountTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between Account and Transaction
            modelBuilder.Entity<Account>()
                .HasMany(a => a.AccountTransactions) // Account has many Transactions
                .WithOne() // Transaction has one SenderAccount
                .HasForeignKey(t => t.SenderAccountId); // Foreign key in Transaction
        }
    }
}

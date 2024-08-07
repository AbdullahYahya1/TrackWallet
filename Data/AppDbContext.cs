using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrackWallet.Models;
using TrakWallet.Models;

namespace TrakWallet.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<RecurringTransaction> RecurringTransactions { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Wallet>()
                .HasOne(w => w.User)
                .WithMany(u => u.Wallets)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Restrict); 
            builder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade); 
            builder.Entity<Transaction>()
                .HasOne(t => t.Wallet)
                .WithMany(w => w.Transactions)
                .HasForeignKey(t => t.WalletId)
                .OnDelete(DeleteBehavior.Cascade); 
            builder.Entity<RecurringTransaction>()
                .HasOne(rt => rt.User)
                .WithMany(u => u.RecurringTransactions)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RecurringTransaction>()
                .HasOne(rt => rt.Wallet)
                .WithMany(w => w.RecurringTransactions)
                .HasForeignKey(rt => rt.WalletId)
                .OnDelete(DeleteBehavior.Cascade); 


            builder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18, 2)");
            builder.Entity<RecurringTransaction>()
                .Property(rt => rt.Amount)
                .HasColumnType("decimal(18, 2)");
            builder.Entity<Wallet>()
                .Property(w => w.Balance)
                .HasColumnType("decimal(18, 2)");

            // Seed initial data for Category
            builder.Entity<Category>().HasData(
                // Income Categories
                new Category { Id = 1, Name = "Salary" },
                new Category { Id = 2, Name = "Freelance" },
                new Category { Id = 3, Name = "Investments" },
                new Category { Id = 4, Name = "Gifts" },
                new Category { Id = 5, Name = "Rental Income" },
                new Category { Id = 6, Name = "Business Income" },
                new Category { Id = 7, Name = "Interest" },
                new Category { Id = 8, Name = "Dividends" },
                new Category { Id = 9, Name = "Royalties" },
                new Category { Id = 10, Name = "Grants" },
                new Category { Id = 11, Name = "Lottery Winnings" },
                new Category { Id = 12, Name = "Refunds" },
                new Category { Id = 13, Name = "Bonuses" },
                new Category { Id = 14, Name = "Other Income" },

                // Expense Categories
                new Category { Id = 15, Name = "Food" },
                new Category { Id = 16, Name = "Transport" },
                new Category { Id = 17, Name = "Utilities" },
                new Category { Id = 18, Name = "Rent" },
                new Category { Id = 19, Name = "Entertainment" },
                new Category { Id = 20, Name = "Healthcare" },
                new Category { Id = 21, Name = "Education" },
                new Category { Id = 22, Name = "Insurance" },
                new Category { Id = 23, Name = "Clothing" },
                new Category { Id = 24, Name = "Personal Care" },
                new Category { Id = 25, Name = "Travel" },
                new Category { Id = 26, Name = "Taxes" },
                new Category { Id = 27, Name = "Charity" },
                new Category { Id = 28, Name = "Savings" },
                new Category { Id = 29, Name = "Subscriptions" },
                new Category { Id = 30, Name = "Pets" },
                new Category { Id = 31, Name = "Household Supplies" },
                new Category { Id = 32, Name = "Childcare" },
                new Category { Id = 33, Name = "Debt Repayment" },
                new Category { Id = 34, Name = "Dining Out" },
                new Category { Id = 35, Name = "Gifts & Donations" },
                new Category { Id = 36, Name = "Gym & Fitness" },
                new Category { Id = 37, Name = "Home Maintenance" },
                new Category { Id = 38, Name = "Office Supplies" },
                new Category { Id = 39, Name = "Parking" },
                new Category { Id = 40, Name = "Phone & Internet" },
                new Category { Id = 41, Name = "Professional Services" },
                new Category { Id = 42, Name = "Software & Apps" },
                new Category { Id = 43, Name = "Sporting Goods" },
                new Category { Id = 44, Name = "Toys" },
                new Category { Id = 45, Name = "Vacation" },
                new Category { Id = 46, Name = "Other Expenses" }
            );

        }
    }
}

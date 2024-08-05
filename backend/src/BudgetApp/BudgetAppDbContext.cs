using BudgetApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp;

public class BudgetAppDbContext(IConfiguration configuration) : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<RecurringTransaction> RecurringTransactions { get; set; }
    public DbSet<RecurringTransactionDetails> RecurringTransactionDetails { get; set; }
    public DbSet<TransactionCategory> TransactionCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(configuration.GetConnectionString("Default"));
    }
}
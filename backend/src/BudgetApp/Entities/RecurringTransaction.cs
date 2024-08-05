namespace BudgetApp.Entities;

public class RecurringTransaction
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string Name { get; set; }

    public required TransactionCategory Category { get; set; }

    public ICollection<RecurringTransactionDetails> Details { get; set; } = new HashSet<RecurringTransactionDetails>();
}
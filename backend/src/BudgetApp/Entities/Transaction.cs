namespace BudgetApp.Entities;

public class Transaction
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string Name { get; set; }

    public required TransactionCategory Category { get; set; }

    public Account? SendingAccount { get; set; }

    public Account? ReceivingAccount { get; set; }

    public required decimal Amount { get; set; }

    public required DateOnly Date { get; set; }
}
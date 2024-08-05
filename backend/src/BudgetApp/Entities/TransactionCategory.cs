namespace BudgetApp.Entities;

public class TransactionCategory
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string Name { get; set; }
}
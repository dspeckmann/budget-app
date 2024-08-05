namespace BudgetApp.Entities;

public class Account
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string Name { get; set; }
}
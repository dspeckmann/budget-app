namespace BudgetApp.Entities;

public class RecurringTransactionDetails
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public Account? SendingAccount { get; set; }

    public Account? ReceivingAccount { get; set; }

    public required decimal Amount { get; set; }

    public required DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public required TransactionFrequency Frequency { get; set; }
}
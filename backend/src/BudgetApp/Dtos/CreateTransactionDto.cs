namespace BudgetApp.Dtos;

public record CreateTransactionDto(
    string Name,
    Guid CategoryId,
    Guid? SendingAccountId,
    Guid? ReceivingAccountId,
    decimal Amount,
    DateOnly Date);

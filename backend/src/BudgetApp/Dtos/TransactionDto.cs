namespace BudgetApp.Dtos;

public record TransactionDto(
    Guid Id,
    string Name,
    TransactionCategoryDto Category,
    AccountDto? SendingAccount,
    AccountDto? ReceivingAccount,
    decimal Amount,
    DateOnly Date);

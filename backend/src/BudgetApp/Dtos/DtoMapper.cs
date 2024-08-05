
using BudgetApp.Entities;

namespace BudgetApp.Dtos;

public static class DtoMapper
{
    public static AccountDto MapAccount(Account account)
    {
        return new AccountDto(account.Id, account.Name);
    }

    public static TransactionCategoryDto MapCategory(TransactionCategory category)
    {
        return new TransactionCategoryDto(category.Id, category.Name);
    }

    public static TransactionDto MapTransaction(Transaction transaction)
    {
        return new TransactionDto(
            transaction.Id,
            transaction.Name,
            MapCategory(transaction.Category),
            transaction.SendingAccount != null ? MapAccount(transaction.SendingAccount) : null,
            transaction.ReceivingAccount != null ? MapAccount(transaction.ReceivingAccount) : null,
            transaction.Amount,
            transaction.Date);
    }
}

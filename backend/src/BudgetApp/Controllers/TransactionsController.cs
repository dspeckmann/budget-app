using BudgetApp.Dtos;
using BudgetApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Controllers;

[ApiController]
[Route("transactions")]
public class TransactionsController(BudgetAppDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<TransactionDto>> GetTransactionsAsync(
        [FromQuery] int year,
        [FromQuery] int month,
        [FromQuery] Guid categoryId)
    {
        var transactions = await dbContext.Transactions
            .AsNoTracking()
            .Include(t => t.Category)
            .Include(t => t.SendingAccount)
            .Include(t => t.ReceivingAccount)
            .Where(t => t.Category.Id == categoryId)
            .Where(t => t.Date.Year == year && t.Date.Month == month)
            .Select(t => DtoMapper.MapTransaction(t))
            .ToListAsync();

        return Ok(transactions);
    }

    [HttpGet("{transactionId}")]
    public async Task<ActionResult<TransactionDto>> GetTransactionAsync(Guid transactionId)
    {
        var transaction = await dbContext.Transactions.FindAsync(transactionId);
        if (transaction is null)
        {
            return NotFound();
        }

        return Ok(DtoMapper.MapTransaction(transaction));
    }

    [HttpPost]
    public async Task<ActionResult<TransactionDto>> CreateTransactionAsync(CreateTransactionDto dto)
    {
        var category = await dbContext.TransactionCategories.FindAsync(dto.CategoryId);
        if (category is null)
        {
            ModelState.AddModelError(nameof(dto.CategoryId), "Category not found.");
            return BadRequest(ModelState);
        }

        var sendingAccount = dto.SendingAccountId.HasValue
            ? await dbContext.Accounts.FindAsync(dto.SendingAccountId)
            : null;

        if (dto.SendingAccountId.HasValue && sendingAccount is null)
        {
            ModelState.AddModelError(nameof(dto.SendingAccountId), "Sending account not found.");
            return BadRequest(ModelState);
        }

        var receivingAccount = dto.ReceivingAccountId.HasValue
            ? await dbContext.Accounts.FindAsync(dto.ReceivingAccountId)
            : null;

        if (dto.ReceivingAccountId.HasValue && receivingAccount is null)
        {
            ModelState.AddModelError(nameof(dto.ReceivingAccountId), "Receiving account not found.");
            return BadRequest(ModelState);
        }

        var transaction = new Transaction()
        {
            Name = dto.Name,
            Category = category,
            SendingAccount = sendingAccount,
            ReceivingAccount  = receivingAccount,
            Amount = dto.Amount,
            Date = dto.Date
        };

        dbContext.Transactions.Add(transaction);
        await dbContext.SaveChangesAsync();

        return Ok(DtoMapper.MapTransaction(transaction));
    }

    [HttpPatch("{transactionId}")]
    public async Task<ActionResult<TransactionDto>> UpdateTransactionAsync(Guid transactionId, UpdateTransactionDto dto)
    {
        var transaction = await dbContext.Transactions.FindAsync(transactionId);
        if (transaction is null)
        {
            return NotFound();
        }

        transaction.Name = dto.Name;
        transaction.Amount = dto.Amount;

        await dbContext.SaveChangesAsync();

        return Ok(DtoMapper.MapTransaction(transaction));
    }

    [HttpDelete("{transactionId}")]
    public async Task<ActionResult> DeleteTransactionAsync(Guid transactionId)
    {
        var transaction = await dbContext.Transactions.FindAsync(transactionId);
        if (transaction is null)
        {
            return NotFound();
        }

        dbContext.Transactions.Remove(transaction);
        await dbContext.SaveChangesAsync();

        return NoContent();
    }
}
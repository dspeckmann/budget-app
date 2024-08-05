using BudgetApp.Dtos;
using BudgetApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Controllers;

[ApiController]
[Route("accounts")]
public class AccountsController(BudgetAppDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountDto>>> GetAccountsAsync()
    {
        var accounts = await dbContext.Accounts
            .AsNoTracking()
            .Select(a => new AccountDto(a.Id, a.Name))
            .ToListAsync();
        return Ok(accounts);
    }

    [HttpGet("{accountId}")]
    public async Task<ActionResult<AccountDto>> GetAccountAsync(Guid accountId)
    {
        var account = await dbContext.Accounts.FindAsync(accountId);
        if (account is null)
        {
            return NotFound();
        }

        return Ok(new AccountDto(account.Id, account.Name));
    }

    [HttpPost]
    public async Task<ActionResult<AccountDto>> CreateAccountAsync(CreateOrUpdateAccountDto dto)
    {
        var account = new Account
        {
            Name = dto.Name
        };

        dbContext.Accounts.Add(account);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(
            "GetAccount",
            new { accountId = account.Id },
            new AccountDto(account.Id, account.Name));
    }

    [HttpPut("{accountId}")]
    public async Task<ActionResult<AccountDto>> UpdateAccountAsync(Guid accountId, CreateOrUpdateAccountDto dto)
    {
        var account = await dbContext.Accounts.FindAsync(accountId);
        if (account is null)
        {
            return NotFound();
        }

        account.Name = dto.Name;
        await dbContext.SaveChangesAsync();

        return Ok(new AccountDto(account.Id, account.Name));
    }

    [HttpDelete("{accountId}")]
    public async Task<ActionResult> DeleteAccountAsync(Guid accountId)
    {
        var account = await dbContext.Accounts.FindAsync(accountId);
        if (account is null)
        {
            return NotFound();
        }

        dbContext.Remove(account);
        await dbContext.SaveChangesAsync();

        return NoContent();
    }
}
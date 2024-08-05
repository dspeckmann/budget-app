using BudgetApp.Dtos;
using BudgetApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Controllers;

[ApiController]
[Route("transaction-categories")]
public class TransactionCategoriesController(BudgetAppDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionCategoryDto>>> GetTransactionCategoriesAsync()
    {
        var categories = await dbContext.TransactionCategories
            .AsNoTracking()
            .Select(c => new TransactionCategoryDto(c.Id, c.Name))
            .ToListAsync();
        
        return Ok(categories);
    }

    [HttpGet("{categoryId}")]
    public async Task<ActionResult<TransactionCategoryDto>> GetTransactionCategoryAsync(Guid categoryId)
    {
        var category = await dbContext.TransactionCategories.FindAsync(categoryId);
        if (category is null)
        {
            return NotFound();
        }

        return Ok(new TransactionCategoryDto(category.Id, category.Name));
    }

    [HttpPost]
    public async Task<ActionResult<TransactionCategoryDto>> CreateTransactionCategoryAsync(CreateOrUpdateCategoryDto categoryDto)
    {
        var category = new TransactionCategory
        {
            Id = Guid.NewGuid(),
            Name = categoryDto.Name
        };

        dbContext.TransactionCategories.Add(category);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction("GetTransactionCategory", new { categoryId = category.Id }, new TransactionCategoryDto(category.Id, category.Name));
    }

    [HttpPut("{categoryId}")]
    public async Task<IActionResult> UpdateTransactionCategoryAsync(Guid categoryId, TransactionCategoryDto categoryDto)
    {
        var category = await dbContext.TransactionCategories.FindAsync(categoryId);
        if (category is null)
        {
            return NotFound();
        }

        category.Name = categoryDto.Name;
        await dbContext.SaveChangesAsync();

        return Ok(new TransactionCategoryDto(category.Id, category.Name));
    }

    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> DeleteTransactionCategoryAsync(Guid categoryId)
    {
        var category = await dbContext.TransactionCategories.FindAsync(categoryId);
        if (category is null)
        {
            return NotFound();
        }

        dbContext.TransactionCategories.Remove(category);
        await dbContext.SaveChangesAsync();

        return NoContent();
    }
}
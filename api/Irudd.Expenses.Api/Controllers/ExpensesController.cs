using Irudd.Expenses.Api.ApiModel;
using Irudd.Expenses.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Irudd.Expenses.Api.Controllers;

public class ExpensesController(ExpensesService service) : BaseController
{
    private const string ApiPrefix = "expenses";

    /// <summary>
    /// Get a list of the {take} latest expenses skipping {skip} first. Use {categoryCode} to filter by category.
    /// </summary>
    [HttpGet]
    [Route($"{ApiPrefix}/latest")]
    public Task<ExpensesLatestResult> Latest([FromQuery] int? skip, [FromQuery] int? take, [FromQuery] string? categoryCode) => 
        service.GetLatestExpensesAsync(skip: skip, take: take, categoryCode: categoryCode);

    /// <summary>
    /// Add an expense
    /// </summary>
    [HttpPost]
    [Route($"{ApiPrefix}/add")]
    public Task<ExpenseViewModel> Add(AddExpenseRequest expense) => service.AddExpenseAsync(expense);

    /// <summary>
    /// Update an existing expense
    /// </summary>
    [HttpPost]
    [Route($"{ApiPrefix}/update")]
    public Task<ExpenseViewModel> Update(UpdateExpenseRequest expense) => service.UpdateExpenseAsync(expense);
}
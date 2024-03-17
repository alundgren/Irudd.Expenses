using Irudd.Expenses.Api.ApiModel;
using Irudd.Expenses.Api.Datamodel;
using Irudd.Expenses.Api.Support;
using Microsoft.EntityFrameworkCore;

namespace Irudd.Expenses.Api.Services;

public class ExpensesService(ExpensesContext context, ICurrentUser currentUser)
{
    public async Task<ExpensesLatestResult> GetLatestExpensesAsync(int? skip = null, int? take = null, string? categoryCode = null)
    {
        var skipCount = skip ?? 0;
        var takeCount = take ?? 50;

        var expensesQuery = CurrentUserExpenses;

        if (categoryCode != null)
            expensesQuery = expensesQuery.Where(x => x.CategoryCode == categoryCode);

        var totalCount = expensesQuery.Count();

        var expenses = await expensesQuery
            .OrderByDescending(x => x.Date)
            .Skip(skipCount)
            .Take(takeCount)
            .Select(x => new ExpenseViewModel(x.Id, x.Amount, x.Date, x.Description, x.CategoryCode))
            .ToListAsync();

        return new ExpensesLatestResult(expenses, totalCount);
    }

    private void ValidateCategoryExistsOrThrow(string? categoryCode)
    {
        if (categoryCode == null)
            return;

        if (!context.Categories.Any(x => x.Code == categoryCode))
            throw new ApiErrorException("invalidCategoryCode", "Invalid categoryCode");
    }

    public async Task<ExpenseViewModel> AddExpenseAsync(AddExpenseRequest expense)
    {
        ValidateCategoryExistsOrThrow(expense.CategoryCode);

        var dbExpense = new Expense
        {
            Id = Guid.NewGuid().ToString(),
            Amount = expense.Amount,
            CategoryCode = expense.CategoryCode,
            Description = expense.Description,
            UserId = currentUser.UserId,
            Date = expense.Date
        };

        await context.Expenses.AddAsync(dbExpense);
        await context.SaveChangesAsync();

        return new ExpenseViewModel(dbExpense.Id, dbExpense.Amount, dbExpense.Date, dbExpense.Description, dbExpense.CategoryCode);
    }

    public async Task<ExpenseViewModel> UpdateExpenseAsync(UpdateExpenseRequest expense)
    {
        ValidateCategoryExistsOrThrow(expense.CategoryCode);

        var existingExpense = await CurrentUserExpenses.Where(x => x.Id == expense.ExpenseId).FirstOrDefaultAsync();
        if(existingExpense == null)
            throw new ApiErrorException("expenseNotFound", "No such expense exists");

        existingExpense.CategoryCode = expense.CategoryCode;
        existingExpense.Amount = expense.Amount;

        await context.SaveChangesAsync();

        return new ExpenseViewModel(existingExpense.Id, existingExpense.Amount, existingExpense.Date, existingExpense.Description, existingExpense.CategoryCode);
    }

    private IQueryable<Expense> CurrentUserExpenses => context.Expenses.Where(x => x.UserId == currentUser.UserId);
}

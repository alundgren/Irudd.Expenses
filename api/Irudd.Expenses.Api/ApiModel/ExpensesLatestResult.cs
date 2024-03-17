namespace Irudd.Expenses.Api.ApiModel;

public record ExpensesLatestResult(List<ExpenseViewModel> Expenses, int TotalExpensesCount);
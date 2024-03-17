namespace Irudd.Expenses.Api.ApiModel;

public record ExpenseViewModel(string Id, decimal Amount, DateTimeOffset Date, string Description, string? CategoryCode);
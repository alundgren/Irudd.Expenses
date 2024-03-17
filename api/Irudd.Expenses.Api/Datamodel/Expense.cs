namespace Irudd.Expenses.Api.Datamodel;

public class Expense
{
    public required string Id { get; set; }
    public required string UserId { get; set; }
    public User? User { get; set; }
    public string? CategoryCode { get; set; }
    public Category? Category { get; set; }
    public required decimal Amount { get; set; }
    public required DateTimeOffset Date { get; set; }
    public required string Description { get; set; }
}
    
namespace Irudd.Expenses.Api.Datamodel;

public class Category
{
    public required string Code { get; set; }
    public required string DisplayName { get; set; }
    public virtual List<Expense>? Expenses { get; set; }
}

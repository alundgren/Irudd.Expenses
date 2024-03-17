using System.ComponentModel.DataAnnotations;

namespace Irudd.Expenses.Api.ApiModel;

public record UpdateExpenseRequest(
    [Required]
    string ExpenseId,

    [Required]
    decimal Amount,

    [StringLength(128)]
    string? CategoryCode
);

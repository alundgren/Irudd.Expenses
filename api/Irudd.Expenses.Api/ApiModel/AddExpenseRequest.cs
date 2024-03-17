using System.ComponentModel.DataAnnotations;

namespace Irudd.Expenses.Api.ApiModel;

public record AddExpenseRequest(
    [Required]
    decimal Amount,

    [Required]
    DateTimeOffset Date,

    [Required]
    [StringLength(500)]
    string Description,

    [StringLength(128)]
    string? CategoryCode
);

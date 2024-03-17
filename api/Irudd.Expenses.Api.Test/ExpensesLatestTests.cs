using Irudd.Expenses.Api.ApiModel;
using Irudd.Expenses.Api.Services;
using Irudd.Expenses.Api.Support;
using Irudd.Expenses.Api.Test.Support;

namespace Irudd.Expenses.Api.Test;

internal class ExpensesAddTests : InMemoryDatabaseTest
{
    #nullable disable
    private ExpensesService service;

    protected override void AdditionalSetup()
    {
        service = new ExpensesService(context, TestUser.TestUser1);
    }

    [Test]
    public async Task AddedExpense_IsPersisted()
    {
        await service.AddExpenseAsync(new AddExpenseRequest(500, Now(), "Test", ValidCategoryCode));

        Assert.That(context.Expenses.Count(), Is.EqualTo(1));
    }
    
    [Test]
    public void AddedExpense_WithInvalidCategoryCode_ResultsInApiError()
    {
        var exception = Assert.ThrowsAsync<ApiErrorException>(() =>
            service.AddExpenseAsync(new AddExpenseRequest(500, Now(), "Test", InvalidCategoryCode)));

        Assert.That(exception?.ErrorCode, Is.EqualTo("invalidCategoryCode"));
    }
}

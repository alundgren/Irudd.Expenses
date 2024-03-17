using Irudd.Expenses.Api.ApiModel;
using Irudd.Expenses.Api.Services;
using Irudd.Expenses.Api.Support;
using Irudd.Expenses.Api.Test.Support;

namespace Irudd.Expenses.Api.Test;

internal class ExpensesUpdateTests : InMemoryDatabaseTest
{
    #nullable disable
    private ExpensesService service;

    protected override void AdditionalSetup()
    {
        service = new ExpensesService(context, TestUser.TestUser1);
    }

    [Test]
    public async Task Update_ChangeAmountAndCategory_IsReflectedByLatest()
    {
        var newExpense = await service.AddExpenseAsync(new AddExpenseRequest(1m, Now(), "test1", ValidCategoryCode));

        await service.UpdateExpenseAsync(new UpdateExpenseRequest(newExpense.Id, 2m, OtherValidCategoryCode));

        var (expenses, _) = await service.GetLatestExpensesAsync(skip:0, take: 1);
        Assert.That(expenses?.First()?.Amount, Is.EqualTo(2m));
        Assert.That(expenses?.First()?.CategoryCode, Is.EqualTo(OtherValidCategoryCode));
    }

    [Test]
    public async Task Update_WithInvalidCategoryCode_ResultsInApiError()
    {
        var newExpense = await service.AddExpenseAsync(new AddExpenseRequest(1m, Now(), "test1", ValidCategoryCode));

        var exception = Assert.ThrowsAsync<ApiErrorException>(() =>
            service.UpdateExpenseAsync(new UpdateExpenseRequest(newExpense.Id, 2m, InvalidCategoryCode)));

        Assert.That(exception?.ErrorCode, Is.EqualTo("invalidCategoryCode"));
    }
}

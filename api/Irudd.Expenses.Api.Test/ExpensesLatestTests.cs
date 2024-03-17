using Irudd.Expenses.Api.ApiModel;
using Irudd.Expenses.Api.Services;
using Irudd.Expenses.Api.Test.Support;

namespace Irudd.Expenses.Api.Test;

internal class ExpensesLatestTests : InMemoryDatabaseTest
{
    #nullable disable
    private ExpensesService service;
    private ExpensesService otherUserService;

    protected override void AdditionalSetup()
    {
        service = new ExpensesService(context, TestUser.TestUser1);
        otherUserService = new ExpensesService(context, TestUser.TestUser2);
    }

    [Test]
    public async Task LatestExpenses_ReturnEmptyList_WhenNoExpensesExist()
    {
        var (expenses, _) = await service.GetLatestExpensesAsync(skip: 0, take: 1);

        Assert.That(expenses?.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task LatestExpenses_ReturnsLatest_WhenTakingOnlyOne()
    {
        await service.AddExpenseAsync(new AddExpenseRequest(1m, Now(), "test1", ValidCategoryCode));
        await service.AddExpenseAsync(new AddExpenseRequest(2m, Now(), "test2", ValidCategoryCode));
        await service.AddExpenseAsync(new AddExpenseRequest(3m, Now(), "test3", ValidCategoryCode));

        var (expenses, _) = await service.GetLatestExpensesAsync(skip:0, take: 1);

        Assert.That(expenses?.Count, Is.EqualTo(1));
        Assert.That(expenses?.First()?.Description, Is.EqualTo("test3"));
    }

    [Test]
    public async Task LatestExpenses_ReturnsTotalCount_WhenTakingFewer()
    {
        await service.AddExpenseAsync(new AddExpenseRequest(1m, Now(), "test1", ValidCategoryCode));
        await service.AddExpenseAsync(new AddExpenseRequest(2m, Now(), "test2", ValidCategoryCode));

        var (_, totalCount) = await service.GetLatestExpensesAsync(skip: 0, take: 1);

        Assert.That(totalCount, Is.EqualTo(2));
    }

    [TestCase(1, "test_user1")]
    [TestCase(2, "test_user2")]
    [Test]
    public async Task LatestExpenses_AreFilteredPerUser(int userNr, string expectedDescription)
    {
        await service.AddExpenseAsync(new AddExpenseRequest(1m, Now(), "test_user1", ValidCategoryCode));
        await otherUserService.AddExpenseAsync(new AddExpenseRequest(1m, Now(), "test_user2", ValidCategoryCode));

        var (expenses, _) = await (userNr == 1 ? service : otherUserService).GetLatestExpensesAsync(skip: 0, take: 2);

        Assert.That(expenses?.Count, Is.EqualTo(1));
        Assert.That(expenses?.First()?.Description, Is.EqualTo(expectedDescription));
    }
}

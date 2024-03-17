using Irudd.Expenses.Api.Services;
using Irudd.Expenses.Api.Test.Support;

namespace Irudd.Expenses.Api.Test;

internal class DatabaseInitializationTests : InMemoryDatabaseTest
{
    protected override bool SupressDatabaseInitialize => true;

    [Test]
    public void SkipInitializeDatabase_ShouldHaveNoCategories() =>
        Assert.That(context.Categories.Count(), Is.EqualTo(0));

    [Test]
    public async Task InitializeDatabase_ShouldCreateCategories()
    {
        await new DatabaseSetupService(context).InitializeDatabaseAsync();
        Assert.That(context.Categories.Count(), Is.GreaterThan(0));
    }
}
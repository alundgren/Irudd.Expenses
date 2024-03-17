using Irudd.Expenses.Api.Datamodel;
using Irudd.Expenses.Api.Services;

namespace Irudd.Expenses.Api.Test.Support;

internal abstract class InMemoryDatabaseTest
{
    protected ExpensesContext context;
    protected const string ValidCategoryCode = "food";
    protected const string OtherValidCategoryCode = "entertainment";
    protected const string InvalidCategoryCode = "d2j8rfj34ru34890";
    private DateTimeOffset baseDate = new DateTimeOffset(2024, 3, 17, 12, 0, 0, TimeSpan.Zero);
    private int dateIncrement = 0;

    protected virtual bool SupressDatabaseInitialize => false;
    protected virtual void AdditionalSetup() { }

    protected DateTimeOffset Now() => baseDate.AddSeconds(Interlocked.Increment(ref dateIncrement));

    [SetUp]
    public async Task Setup()
    {
        context = ExpensesContext.CreateInMemoryContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
        if (!SupressDatabaseInitialize)
            await new DatabaseSetupService(context).InitializeDatabaseAsync();

        AdditionalSetup();
    }
}

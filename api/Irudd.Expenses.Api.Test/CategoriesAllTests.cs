using Irudd.Expenses.Api.Services;
using Irudd.Expenses.Api.Test.Support;

namespace Irudd.Expenses.Api.Test;

internal class CategoriesAllTests : InMemoryDatabaseTest
{
    #nullable disable
    private CategoriesService service;

    protected override void AdditionalSetup()
    {
        service = new CategoriesService(context);
    }

    [Test]
    public async Task All_Returns_InitialCategories()
    {
        var allCategories = await service.GetAllAsync();

        var expectedCategoryCodes = DatabaseSetupService.GetInitialCategories().Select(x => x.Code).ToHashSet();
        var actualCategoryCodes = allCategories.Select(x => x.Code).ToHashSet();

        CollectionAssert.AreEquivalent(expectedCategoryCodes, actualCategoryCodes);
    }
}

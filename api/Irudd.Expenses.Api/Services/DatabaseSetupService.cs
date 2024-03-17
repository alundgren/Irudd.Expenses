using Irudd.Expenses.Api.Datamodel;
using Microsoft.EntityFrameworkCore;

namespace Irudd.Expenses.Api.Services;

public class DatabaseSetupService(ExpensesContext context)
{
    public async Task CreateMigrateAndInitializeDatabaseAsync()
    {
        //Create or migrate database
        await context.Database.MigrateAsync();

        await InitializeDatabaseAsync();
    }

    public async Task InitializeDatabaseAsync()
    {
        //Setup categories when database is empty
        if (!context.Categories.Any())
        {
            await context.Categories.AddRangeAsync(GetInitialCategories());

            await context.SaveChangesAsync();
        }
    }

    public static List<Category> GetInitialCategories() => new List<Category>
    {
        new Category { Code = "food", DisplayName = "Food" },
        new Category { Code = "entertainment", DisplayName = "Entertainment" },
        new Category { Code = "other", DisplayName = "Other" }
    };
}

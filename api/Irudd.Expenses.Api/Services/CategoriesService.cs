using Irudd.Expenses.Api.ApiModel;
using Irudd.Expenses.Api.Datamodel;
using Microsoft.EntityFrameworkCore;

namespace Irudd.Expenses.Api.Services;

public class CategoriesService(ExpensesContext context)
{
    public Task<List<CategoryViewModel>> GetAllAsync() =>    
        context
            .Categories
            .Select(x => new CategoryViewModel(x.Code, x.DisplayName))
            .ToListAsync();
}

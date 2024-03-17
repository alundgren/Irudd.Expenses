using Irudd.Expenses.Api.ApiModel;
using Irudd.Expenses.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Irudd.Expenses.Api.Controllers;

public class CategoriesController(CategoriesService service) : BaseController
{
    private const string ApiPrefix = "categories";

    /// <summary>
    /// Get all categories
    /// </summary>
    [HttpGet]
    [Route($"{ApiPrefix}/all")]
    public Task<List<CategoryViewModel>> All() => service.GetAllAsync();
}
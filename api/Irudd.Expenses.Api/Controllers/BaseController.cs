using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Irudd.Expenses.Api.Controllers;

[Authorize()]
[ApiController]
[Route(ApiVersion)]
public abstract class BaseController : Controller
{
    public const string ApiVersion = "v1";
}

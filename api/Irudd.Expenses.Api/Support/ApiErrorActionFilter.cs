using Irudd.Expenses.Api.ApiModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Irudd.Expenses.Api.Support;

public class ApiErrorActionFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        var apiException = context.Exception as ApiErrorException;

        if (apiException == null) 
            return;

        context.Result = new BadRequestObjectResult(new ValidationProblemDetails(new Dictionary<string, string[]> 
        { 
            [apiException.ErrorCode] = [apiException.ErrorMessage]
        }));
        context.ExceptionHandled = true;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {        
    }
}

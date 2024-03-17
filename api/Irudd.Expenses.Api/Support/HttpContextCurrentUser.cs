using Irudd.Expenses.Api.Datamodel;
using Microsoft.AspNetCore.Identity;

namespace Irudd.Expenses.Api.Support;

public class HttpContextCurrentUser(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager) : ICurrentUser
{
    public string UserId
    {
        get
        {
            var user = httpContextAccessor.HttpContext?.User;
            var userId = user == null ? null : userManager.GetUserId(user);
            if (userId == null)
                throw new Exception("Current user id missing");
            return userId;
        }
    }
}

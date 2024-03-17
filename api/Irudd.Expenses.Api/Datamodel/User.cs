using Microsoft.AspNetCore.Identity;

namespace Irudd.Expenses.Api.Datamodel
{
    public class User : IdentityUser
    {
        public virtual List<Expense>? Expenses { get; set; }
    }
}

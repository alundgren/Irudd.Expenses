using Irudd.Expenses.Api.Support;

namespace Irudd.Expenses.Api.Test.Support;

internal class TestUser(string userId) : ICurrentUser
{
    public string UserId => userId;

    public static ICurrentUser TestUser1 => new TestUser("25a57e84-2bd5-43f6-a62b-cbc70b510195");
    public static ICurrentUser TestUser2 => new TestUser("ded4d881-cd41-4396-b575-e15379c525ff");
}

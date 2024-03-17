namespace Irudd.Expenses.Api.Support;

public class ApiErrorException(string errorCode, string errorMessage) : Exception(errorMessage)
{
    public string ErrorCode { get; } = errorCode;
    public string ErrorMessage { get; } = errorMessage;
}

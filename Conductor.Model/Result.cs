namespace Conductor.Models;

public class Result
{
    public bool IsSuccess { get; set; }
    public string? Error { get; set; }
    public int StatusCode { get; set; }

    protected Result() {}

    public static Result Success(int statusCode = 200)
    {
        return new Result
        {
            IsSuccess = true,
            StatusCode = statusCode
        };
    }

    public static Result Failure(string error, int statusCode = 400)
    {
        return new Result
        {
            IsSuccess = false,
            Error = error,
            StatusCode = statusCode
        };
    }
}
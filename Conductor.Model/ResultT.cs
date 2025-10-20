namespace Conductor.Models;

public class Result<T> : Result
{
    public T? Data { get; protected set; }

    public static Result<T> Success(T data, int statusCode = 200)
    {
        return new Result<T>
        {
            IsSuccess = true,
            Data = data,
            StatusCode = statusCode
        };
    }

    public static Result<T> Failure(string error, int statusCode = 400)
    {
        return new Result<T>
        {
            IsSuccess = false,
            Error = error,
            StatusCode = statusCode
        };
    }
}
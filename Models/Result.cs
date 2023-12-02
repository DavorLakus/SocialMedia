public class Result<TSuccess, TFailure>
{
    public bool IsSuccess { get; }
    public TSuccess Value { get; }
    public TFailure Error { get; }

    private Result(bool isSuccess, TSuccess success, TFailure failure)
    {
        IsSuccess = isSuccess;
        Value = success;
        Error = failure;
    }

    public static Result<TSuccess, TFailure> Success(TSuccess success)
    {
        return new Result<TSuccess, TFailure>(true, success, default(TFailure));
    }

    public static Result<TSuccess, TFailure> Failure(TFailure failure)
    {
        return new Result<TSuccess, TFailure>(false, default(TSuccess), failure);
    }
}


namespace Notification.Infrastructure.Domain.Common.Exceptions;

public class FuncResult<T>
{
    public T Data { get; init; }

    public Exception? Exception { get; }

    public bool IsSuccess => Exception is null;

    public FuncResult(T Data) => Data = Data;
    
    public FuncResult(Exception exception) => Exception = exception;
}
using Sms.Notification.Domain.Common.Exceptions;

namespace Sms.Notification.Domain.Extensions;

public static class ExceptionExtension
{
    public static async ValueTask<FuncResult<T>> GetValueAsync<T>(this Func<Task<T>> func) where T : struct
    {
        FuncResult<T> result;

        try
        {
            result = new FuncResult<T>(await func());
        }
        catch (Exception e)
        {
            result = new FuncResult<T>(e);
        }  
        return result;
    }
}
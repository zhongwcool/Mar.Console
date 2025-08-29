using System.Text;

namespace Mar.Cheese;

public class ExceptionUtil
{
    public static string GetText(Exception? exception)
    {
        return GetExceptionText(exception);
    }

    /// <summary>
    /// 异步获取异常文本（保持向后兼容性）
    /// </summary>
    /// <param name="exception">异常对象</param>
    /// <returns>异常文本</returns>
    [Obsolete("建议使用同步方法 GetText，此方法仅用于向后兼容")]
    public static async Task<string> TaskGetText(Exception? exception)
    {
        return await Task.FromResult(GetExceptionText(exception));
    }

    private static string GetExceptionText(Exception? exception)
    {
        if (exception == null) return string.Empty;

        var builder = new StringBuilder();

        while (exception != null)
        {
            builder.AppendLine($"{exception.GetType().Name}: {exception.Message}");
            builder.AppendLine("-------------------------------------------");
            builder.AppendLine("Stack Trace:");
            builder.AppendLine(exception.StackTrace);

            exception = exception.InnerException;
        }

        return builder.ToString();
    }
}
using System.Text;

namespace Mar.Cheese;

public class ExceptionUtil
{
    public static async Task<string> TaskGetText(Exception? exception)
    {
        return await Task.FromResult(GetExceptionText(exception));
    }

    public static string GetText(Exception? exception)
    {
        return GetExceptionText(exception);
    }

    private static string GetExceptionText(Exception? exception)
    {
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
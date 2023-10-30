using System.Text;

namespace Mar.Cheese;

public class ExceptionUtil
{
    public static async Task<string> TaskGetText(Exception? exception)
    {
        var stringBuilder = new StringBuilder();

        await Task.Run(() =>
        {
            while (true)
            {
                if (exception == null) break;

                stringBuilder.Append(exception.GetType().Name + ": " + exception.Message + Environment.NewLine);
                stringBuilder.Append("-------------------------------------------" + Environment.NewLine);
                stringBuilder.Append("Stack Trace: " + Environment.NewLine);
                stringBuilder.Append(exception.StackTrace);

                exception = exception.InnerException;
            }

            return stringBuilder.ToString();
        });

        return stringBuilder.ToString();
    }

    public static string GetText(Exception? exception)
    {
        var stringBuilder = new StringBuilder();

        while (true)
        {
            if (exception == null) break;

            stringBuilder.Append(exception.GetType().Name + ": " + exception.Message + Environment.NewLine);
            stringBuilder.Append("-------------------------------------------" + Environment.NewLine);
            stringBuilder.Append("Stack Trace: " + Environment.NewLine);
            stringBuilder.Append(exception.StackTrace);

            exception = exception.InnerException;
        }

        return stringBuilder.ToString();
    }
}
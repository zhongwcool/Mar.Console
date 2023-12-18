// See https://aka.ms/new-console-template for more information

using Mar.Cheese;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] {Message:l}{NewLine}{Exception}")
    .CreateLogger();

Console.WriteLine($"Hello, {Environment.UserName}!");

var task = SystemUtil.GetSystemInfo();
task.ContinueWith(_ =>
{
    if (task.Result is { } info) info.PrintGreen();
});

SystemUtil.PrintSystemInfo(OutOptions.UseSerilog);

var atts = new[] { @"E:\link.txt", @"E:\Record_test.mp4" };

SendEmail("zhongw@uwv-tech.com", "崩溃日志-xxxx", "xxxxx", atts);

Console.Read();

void SendEmail(string to, string subject, string body, string[]? attachments = null)
{
    var task = EmailUtil.SendEmail("2872700763@qq.com", to, subject, body,
        "smtp.qq.com", 587,
        "2872700763",
        "utyydjctjirrdfgc",
        attachments
    );

    task.ContinueWith(_ =>
    {
        if (task.Result)
            "发送成功".PrintGreen();
        else
            "发送失败".PrintErr();
    });
}
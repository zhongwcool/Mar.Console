// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Reflection;
using Mar.Cheese;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] {Message:l}{NewLine}{Exception}")
    .CreateLogger();

Console.WriteLine($"Hello, {Environment.UserName}!");

var version = Assembly.GetExecutingAssembly().GetName().Version;
var info = await SystemUtil.GetSystemInfo(version);
info.PrintGreen();

var atts = new List<string>([@"E:\hex_data2.txt", @"E:\license.jpg"]);

SendEmail("zhongw@uwv-tech.com", "崩溃日志-xxxx", "xxxxx", atts);

Console.Read();

void SendEmail(string to, string subject, string body, List<string> attachments)
{
    var task = EmailUtil.SendEmailAsync("2872700763@qq.com", to, subject, body,
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
// See https://aka.ms/new-console-template for more information

using Mar.Cheese;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] {Message:l}{NewLine}{Exception}")
    .CreateLogger();

Console.WriteLine($"Hello, {Environment.UserName}!");

SystemUtil.PrintSystemInfo(true);

Console.Read();
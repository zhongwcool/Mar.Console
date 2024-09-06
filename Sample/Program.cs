// See https://aka.ms/new-console-template for more information

using System;
using System.Reflection;
using Mar.Cheese;
using Sample;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u3}] {Message:l}{NewLine}{Exception}")
    .CreateLogger();

Console.WriteLine($"Hello, {Environment.UserName}!");

var version = Assembly.GetExecutingAssembly().GetName().Version;
var info = await SystemUtil.GetSystemInfo(version);
info.PrintGreen();

var tianqis = new[]
{
    new Tianqi
    {
        Id = "2024-04-01", High = 26, Low = 16, Condition = "多云~大雨", Wind = "东南风3级", Aqi = "69良",
        Date = new DateTime(2024, 04, 01)
    },
    new Tianqi
    {
        Id = "2024-04-02", High = 21, Low = 15, Condition = "中雨", Wind = "南风3级", Aqi = "69良",
        Date = new DateTime(2024, 04, 02)
    },
    new Tianqi
    {
        Id = "2024-04-03", High = 16, Low = 10, Condition = "雾~阴", Wind = "西北风3级", Aqi = "69良",
        Date = new DateTime(2024, 04, 03)
    }
};
JsonUtil.SaveAsync("1-tianqi.json", tianqis).Wait();

Console.Read();
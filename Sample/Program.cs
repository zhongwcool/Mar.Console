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
    new Snippet
    {
        Title = "$RPT", Tips = "命令头", StartIndex = 0, Length = 4
    },
    new Snippet
    {
        Title = "0x01 心跳包", Tips = "N/A", StartIndex = 4, Length = 1
    },
    new Snippet
    {
        Title = "系统状态", Tips = "1-激活;0-未激活", StartIndex = 5, Length = 1
    },
    new Snippet
    {
        Title = "系统模式", Tips = "0-自动调节;1-手动调节;2-实验模式", StartIndex = 6, Length = 1
    },
    new Snippet
    {
        Title = "运行状态", Tips = "0-未锁定;1-锁定;2-准备中", StartIndex = 7, Length = 1
    },
    new Snippet
    {
        Title = "电流实际值(mA)", Tips = "换算关系：见附录A", StartIndex = 8, Length = 2
    },
    new Snippet
    {
        Title = "电流设定值(mA)", Tips = "换算关系：见附录A", StartIndex = 10, Length = 2
    },
    new Snippet
    {
        Title = "三角波幅度设定值(mA)", Tips = "N/A", StartIndex = 12, Length = 2
    },
    new Snippet
    {
        Title = "三角波频率设定值(Hz)", Tips = "N/A", StartIndex = 14, Length = 2
    },
    new Snippet
    {
        Title = "TEC温度实际值(℃)", Tips = "N/A", StartIndex = 16, Length = 2
    },
    new Snippet
    {
        Title = "TEC温度设定值(℃)", Tips = "N/A", StartIndex = 18, Length = 2
    },
    new Snippet
    {
        Title = "硅1电压设定值(mV)", Tips = "N/A", StartIndex = 20, Length = 2
    },
    new Snippet
    {
        Title = "硅2电压设定值(mV)", Tips = "N/A", StartIndex = 22, Length = 2
    },
    new Snippet
    {
        Title = "PZT电压(mV)", Tips = "系数：6.9 * 2500 / 65532", StartIndex = 24, Length = 2
    },
    new Snippet
    {
        Title = "PZT挡位", Tips = "N/A", StartIndex = 26, Length = 1
    },
    new Snippet
    {
        Title = "壳体温度实际值(℃)", Tips = "换算关系同TEC温度实际值", StartIndex = 27, Length = 2
    },
};
JsonUtil.SaveAsync("1-protocol.json", tianqis).Wait();

var protocol = new Protocol
{
    Snippets = [..tianqis],
    Cat = 0x01
};

JsonUtil.SaveAsync("2-protocol.json", protocol).Wait();

Console.Read();
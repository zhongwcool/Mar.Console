using System.Management;
using System.Text;
using Serilog;

namespace Mar.Cheese;

public class SystemUtil
{
    /// <summary>
    ///     打印运行环境
    ///     Print system info
    /// </summary>
    /// <param name="options">Output options: UseConsole use console, UseSerilog use Serilog</param>
    /// <param name="soft">soft version</param>
    /// <returns></returns>
    public static async Task PrintSystemInfoAsync(OutOptions options = OutOptions.UseConsole, Version? soft = null)
    {
        await Task.Run(() =>
        {
            var version = Environment.OSVersion.Version;

            LogOrPrint(options, GetWindowsVersion(version));
            LogOrPrint(options, $".NET SDK Version: {Environment.Version}");

            // 只在Windows平台上获取硬件信息
            if (IsWindows())
            {
                foreach (var info in GetHardwareInfo("Win32_Processor", "CPU"))
                    LogOrPrint(options, info);

                foreach (var info in GetHardwareInfo("Win32_VideoController", "Graphics Card"))
                    LogOrPrint(options, info);

                foreach (var info in GetMemoryInfo())
                    LogOrPrint(options, info);
            }
            else
            {
                LogOrPrint(options, "硬件信息仅在Windows平台上可用");
            }

            if (soft != null)
                LogOrPrint(options, $"Soft Version: {soft.Major}.{soft.Minor}.{soft.Build}.{soft.Revision}");
        });
    }

    private static string GetWindowsVersion(Version version)
    {
        if (IsWindows())
        {
            if (version.Major == 10)
                switch (version.Build)
                {
                    case >= 22000:
                        return $"Windows Version: Windows 11 {version.Build}";
                    case >= 19041:
                        return $"Windows Version: Windows 10 {version.Build}";
                }

            return $"Windows Version: {Environment.OSVersion}";
        }

        return $"Operating System: {Environment.OSVersion}";
    }

    private static bool IsWindows()
    {
        return Environment.OSVersion.Platform == PlatformID.Win32NT;
    }

    private static List<string> GetHardwareInfo(string query, string label)
    {
        var results = new List<string>();

        if (!IsWindows())
        {
            results.Add($"{label}: 仅在Windows平台上可用");
            return results;
        }

        try
        {
#pragma warning disable CA1416
            var searcher = new ManagementObjectSearcher($"select * from {query}");
            foreach (var o in searcher.Get())
            {
                var obj = (ManagementObject)o;
                results.Add($"{label}: {obj["Name"]}");
            }
#pragma warning restore CA1416
        }
        catch (Exception ex)
        {
            results.Add($"{label}: 获取信息失败 - {ex.Message}");
        }

        return results;
    }

    private static List<string> GetMemoryInfo()
    {
        var results = new List<string>();

        if (!IsWindows())
        {
            results.Add("Memory: 仅在Windows平台上可用");
            return results;
        }

        try
        {
#pragma warning disable CA1416
            var searcher = new ManagementObjectSearcher("select * from Win32_PhysicalMemory");
            foreach (var o in searcher.Get())
            {
                var obj = (ManagementObject)o;
                var capacityBytes = (ulong)obj["Capacity"];
                var mem = capacityBytes / 1024.0 / 1024 / 1024;
                results.Add($"Memory: {mem} GB");
            }
#pragma warning restore CA1416
        }
        catch (Exception ex)
        {
            results.Add($"Memory: 获取信息失败 - {ex.Message}");
        }

        return results;
    }

    private static void LogOrPrint(OutOptions options, string message)
    {
        if (options == OutOptions.UseSerilog)
            Log.Fatal(message);
        else
            Console.WriteLine(message);
    }

    public static async Task<string> GetSystemInfo(Version? soft = null)
    {
        var builder = new StringBuilder();

        await Task.Run(() =>
        {
            var version = Environment.OSVersion.Version;

            builder.AppendLine("-------------------------------------------");
            builder.AppendLine(GetWindowsVersion(version));
            builder.AppendLine($".NET SDK Version: {Environment.Version}");

            // 只在Windows平台上获取硬件信息
            if (IsWindows())
            {
                foreach (var info in GetHardwareInfo("Win32_Processor", "CPU"))
                    builder.AppendLine(info);

                foreach (var info in GetHardwareInfo("Win32_VideoController", "Graphics Card"))
                    builder.AppendLine(info);

                foreach (var info in GetMemoryInfo())
                    builder.AppendLine(info);
            }
            else
            {
                builder.AppendLine("硬件信息仅在Windows平台上可用");
            }

            if (soft != null)
                builder.AppendLine($"Soft Version: {soft.Major}.{soft.Minor}.{soft.Build}.{soft.Revision}");
        });

        return builder.ToString();
    }
}

public enum OutOptions
{
    UseConsole = 0,
    UseSerilog
}
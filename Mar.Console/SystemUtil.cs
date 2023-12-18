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
    /// <returns></returns>
    public static async void PrintSystemInfo(OutOptions options = OutOptions.UseConsole)
    {
        await Task.Run(() =>
        {
            var os = Environment.OSVersion;
            var version = os.Version;
            switch (version.Major)
            {
                case 10 when version.Build >= 19041:
                    if (options == OutOptions.UseSerilog)
                        Log.Fatal("Windows Version: Windows 10 {OsVersion}", version.Build);
                    else
                        Console.WriteLine($"Windows Version: Windows 10 {version.Build}");
                    break;
                case 10 when version.Build >= 22000:
                    if (options == OutOptions.UseSerilog)
                        Log.Fatal("Windows Version: Windows 11 {OsVersion}", version.Build);
                    else
                        Console.WriteLine($"Windows Version: Windows 11 {version.Build}");
                    break;
                default:
                    if (options == OutOptions.UseSerilog)
                        Log.Fatal("Windows Version: {OsVersion}", Environment.OSVersion);
                    else
                        Console.WriteLine($"Windows Version: {Environment.OSVersion}");
                    break;
            }

            if (options == OutOptions.UseSerilog)
                Log.Fatal(".NET SDK Version: {Version}", Environment.Version);
            else
                Console.WriteLine($".NET SDK Version: {Environment.Version}");

            // Query CPU
            var searcher = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (var o in searcher.Get())
            {
                var share = (ManagementObject)o;
                if (options == OutOptions.UseSerilog)
                    Log.Fatal("CPU: {Unknown}", share["Name"]);
                else
                    Console.WriteLine($"CPU: {share["Name"]}");
            }

            // Query Graphics Card
            searcher = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (var o in searcher.Get())
            {
                var share = (ManagementObject)o;
                if (options == OutOptions.UseSerilog)
                    Log.Fatal("Graphics Card: {Unknown}", share["Name"]);
                else
                    Console.WriteLine("Graphics Card: " + share["Name"]);
            }

            // Query Memory
            searcher = new ManagementObjectSearcher("select * from Win32_PhysicalMemory");
            foreach (var o in searcher.Get())
            {
                var share = (ManagementObject)o;
                var capacityBytes = (ulong)share["Capacity"];
                var mem = (double)capacityBytes / 1024 / 1024 / 1024;
                if (options == OutOptions.UseSerilog)
                    Log.Fatal("Memory: {Unknown} GB", mem);
                else
                    Console.WriteLine("Memory: " + mem + "GB");
            }
        });
    }

    public static async Task<string> GetSystemInfo()
    {
        var stringBuilder = new StringBuilder();

        await Task.Run(() =>
        {
            var os = Environment.OSVersion;
            var version = os.Version;

            stringBuilder.Append("-------------------------------------------").Append(Environment.NewLine);
            switch (version.Major)
            {
                case 10 when version.Build >= 19041:
                    stringBuilder.Append($"Windows Version: Windows 10 {version.Build}").Append(Environment.NewLine);
                    break;
                case 10 when version.Build >= 22000:
                    stringBuilder.Append($"Windows Version: Windows 11 {version.Build}").Append(Environment.NewLine);
                    break;
                default:
                    stringBuilder.Append($"Windows Version: {Environment.OSVersion}").Append(Environment.NewLine);
                    break;
            }

            stringBuilder.Append($".NET SDK Version: {Environment.Version}").Append(Environment.NewLine);

            // Query CPU
            var searcher = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (var o in searcher.Get())
            {
                var share = (ManagementObject)o;
                stringBuilder.Append($"CPU: {share["Name"]}").Append(Environment.NewLine);
            }

            // Query Graphics Card
            searcher = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (var o in searcher.Get())
            {
                var share = (ManagementObject)o;
                stringBuilder.Append("Graphics Card: " + share["Name"]).Append(Environment.NewLine);
            }

            // Query Memory
            searcher = new ManagementObjectSearcher("select * from Win32_PhysicalMemory");
            foreach (var o in searcher.Get())
            {
                var share = (ManagementObject)o;
                var capacityBytes = (ulong)share["Capacity"];
                var mem = (double)capacityBytes / 1024 / 1024 / 1024;
                stringBuilder.Append("Memory: " + mem + "GB").Append(Environment.NewLine);
            }
        });

        return stringBuilder.ToString();
    }
}

public enum OutOptions
{
    UseConsole = 0,
    UseSerilog
}
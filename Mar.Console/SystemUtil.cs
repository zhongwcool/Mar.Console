using System.Management;
using Serilog;

namespace Mar.Console;

public class SystemUtil
{
    // 打印运行环境
    public static async void PrintSystemInfo(bool useSerilog = false)
    {
        await Task.Run(() =>
        {
            var os = Environment.OSVersion;
            var version = os.Version;
            switch (version.Major)
            {
                case 10 when version.Build >= 19041:
                    if (useSerilog)
                        Log.Fatal("Windows Version: Windows 10 {OsVersion}", version.Build);
                    else
                        System.Console.WriteLine($"Windows Version: Windows 10 {version.Build}");
                    break;
                case 10 when version.Build >= 22000:
                    if (useSerilog)
                        Log.Fatal("Windows Version: Windows 11 {OsVersion}", version.Build);
                    else
                        System.Console.WriteLine($"Windows Version: Windows 11 {version.Build}");
                    break;
                default:
                    if (useSerilog)
                        Log.Fatal("Windows Version: {OsVersion}", Environment.OSVersion);
                    else
                        System.Console.WriteLine($"Windows Version: {Environment.OSVersion}");
                    break;
            }

            if (useSerilog)
                Log.Fatal(".NET SDK Version: {Version}", Environment.Version);
            else
                System.Console.WriteLine($".NET SDK Version: {Environment.Version}");

            // Query CPU
            var searcher = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (var o in searcher.Get())
            {
                var share = (ManagementObject)o;
                if (useSerilog)
                    Log.Fatal("CPU: {Unknown}", share["Name"]);
                else
                    System.Console.WriteLine($"CPU: {share["Name"]}");
            }

            // Query Graphics Card
            searcher = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (var o in searcher.Get())
            {
                var share = (ManagementObject)o;
                if (useSerilog)
                    Log.Fatal("Graphics Card: {Unknown}", share["Name"]);
                else
                    System.Console.WriteLine("Graphics Card: " + share["Name"]);
            }

            // Query Memory
            searcher = new ManagementObjectSearcher("select * from Win32_PhysicalMemory");
            foreach (var o in searcher.Get())
            {
                var share = (ManagementObject)o;
                var capacityBytes = (ulong)share["Capacity"];
                var mem = (double)capacityBytes / 1024 / 1024 / 1024;
                if (useSerilog)
                    Log.Fatal("Memory: {Unknown} GB", mem);
                else
                    System.Console.WriteLine("Memory: " + mem + "GB");
            }
        });
    }
}
using System;
using System.Diagnostics;
using System.IO;

namespace SystemMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Начинаем мониторинг...");
            while (true)
            {
                float cpuUsage = GetCpuUsage();
                float ramUsage = GetRamUsage();
                float diskFreeSpace = GetDiskFreeSpace();
                float diskTotalSpace = GetDiskTotalSpace();

                Console.Clear();
                Console.ForegroundColor = GetRandomConsoleColor();

                Console.WriteLine($"CPU: {cpuUsage}%");
                Console.WriteLine($"RAM: {ramUsage} MB");
                Console.WriteLine($"Свободное место на диске: {diskFreeSpace} GB");
                Console.WriteLine($"Всего места на диске: {diskTotalSpace} GB");
                Console.WriteLine();
                System.Threading.Thread.Sleep(250);
            }
        }
        private static Random _random = new Random();
        private static ConsoleColor GetRandomConsoleColor()
        {
            var consoleColors = Enum.GetValues(typeof(ConsoleColor));
            return (ConsoleColor)consoleColors.GetValue(_random.Next(consoleColors.Length));
        }

        static float GetCpuUsage()
        {
            var cpuUsageCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cpuUsageCounter.NextValue();
            System.Threading.Thread.Sleep(250);
            return cpuUsageCounter.NextValue();
        }

        static float GetRamUsage()
        {
            var ramUsageCounter = new PerformanceCounter("Memory", "Available MBytes");
            return ramUsageCounter.NextValue();
        }

        static float GetDiskFreeSpace()
        {
            DriveInfo drive = new DriveInfo("C");
            return (float)drive.TotalFreeSpace / (1024 * 1024 * 1024);
        }

        static float GetDiskTotalSpace()
        {
            DriveInfo drive = new DriveInfo("C");
            return (float)drive.TotalSize / (1024 * 1024 * 1024);
        }
    }
}
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Lab_31_Danylko
{
    public class ProcessInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double CpuUsage { get; set; }
        public double MemoryUsage { get; set; }
        public double NetworkUsage { get; set; }
        public ProcessPriorityClass Priority { get; set; }

        public ProcessInfo(Process process)
        {
            Id = process.Id;
            Name = process.ProcessName;
        }

        public async Task LoadDetailsAsync()
        {
            CpuUsage = await GetCpuUsageAsync();
            MemoryUsage = await GetMemoryUsageAsync();
            NetworkUsage = await GetNetworkUsageAsync();
            Priority = GetPriority();
        }

        private Task<double> GetCpuUsageAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    using (var cpuCounter = new PerformanceCounter("Process", "% Processor Time", Name, true))
                    {
                        cpuCounter.NextValue();
                        System.Threading.Thread.Sleep(1000);
                        return Math.Round(cpuCounter.NextValue() / Environment.ProcessorCount, 2);
                    }
                }
                catch
                {
                    return 0;
                }
            });
        }

        private Task<double> GetMemoryUsageAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    using (var process = Process.GetProcessById(Id))
                    {
                        return Math.Round(process.WorkingSet64 / 1024.0 / 1024.0, 2); // MB
                    }
                }
                catch
                {
                    return 0;
                }
            });
        }

        private Task<double> GetNetworkUsageAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    using (var networkCounter = new PerformanceCounter("Process", "IO Data Bytes/sec", Name, true))
                    {
                        networkCounter.NextValue();
                        System.Threading.Thread.Sleep(1000);
                        return Math.Round(networkCounter.NextValue() / 1024.0, 2); // KB/s
                    }
                }
                catch
                {
                    return 0;
                }
            });
        }

        private ProcessPriorityClass GetPriority()
        {
            try
            {
                using (var process = Process.GetProcessById(Id))
                {
                    return process.PriorityClass;
                }
            }
            catch
            {
                return ProcessPriorityClass.Normal;
            }
        }
    }
}
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Task_Manager.Interfaces;

namespace Task_Manager.Services
{
    public class CpuService : ISystemMonitorService
    {
        private readonly PerformanceCounter _cpuCounter;
        public string Name { get; set; }
        public Dictionary<string, string> Info { get; }


        public CpuService()
        {
            Name = "CPU";
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _cpuCounter.NextValue();

            Info = GetProcessorInfo();
        }

        public double GetUsage()
        {
            double usage = Math.Round(_cpuCounter.NextValue(), 2);

            return usage;
        }

        private static Dictionary<string, string> GetProcessorInfo()
        {
            ManagementObjectCollection moc;
            var Processor = new Dictionary<string, string>();

            try
            {
                moc = new ManagementObjectSearcher("select * from Win32_Processor").Get();
            }
            catch
            {
                MessageBox.Show("Error: WMI API Not loaded.");
                return Processor;
            }

            foreach (ManagementObject obj in moc)
            {
                try
                {
                    Processor.Add("L3CacheSize", obj["L3CacheSize"].ToString());
                    Processor.Add("L2CacheSize", obj["L2CacheSize"].ToString());
                    Processor.Add("Name", obj["Name"].ToString());
                    Processor["Cores"] = obj["NumberOfCores"]?.ToString() ?? "0";
                    Processor["Threads"] = obj["NumberOfLogicalProcessors"]?.ToString() ?? "0";
                    break;
                }
                catch { }
            }
            return Processor;
        }
    }
}

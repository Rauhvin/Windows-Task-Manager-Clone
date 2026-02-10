using System.Diagnostics;
using System.Management;
using Task_Manager.Interfaces;

namespace Task_Manager.Services
{
    public class RamService : ISystemMonitorService
    {
        private readonly PerformanceCounter _ramCounter;
        private readonly double _totalRam;
        public readonly double _ram;
        public string Name { get; set; }
        public readonly string _speed;
        public readonly string _manufacturer;
        public double _available;
        public Dictionary<string, string> Info { get; }


        public RamService()
        {
            Name = "RAM";
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            _totalRam = TotalRamAmount();
            _ram =Math.Round( _totalRam / (1024 * 1024 * 1024),0);
            _available = Math.Round(_ramCounter.NextValue() / 1024, 2);
            string speed = "";
            string manufacturer = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
            foreach (ManagementObject obj in searcher.Get())
            {
                 speed = obj["Speed"]?.ToString() + " MHz";
                manufacturer = obj["Manufacturer"]?.ToString();
            }

            _speed = speed;
            _manufacturer = manufacturer;
            
        }

        private double TotalRamAmount()
        {
            var searcher = new ManagementObjectSearcher("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
            double totalCapacity = 0;
            foreach (var obj in searcher.Get())
            {
                totalCapacity = Convert.ToDouble(obj["TotalPhysicalMemory"]);
            }

            return totalCapacity;
        }

        public double GetUsage()
        {
            float freeRamMb = _ramCounter.NextValue();
            double totalRamMb = _totalRam / (1024 * 1024);
            double usedRam = ((totalRamMb - freeRamMb) / totalRamMb) * 100;
            
            return Math.Round(usedRam, 2);
        }

        public double GetRam()
        {
            return Math.Round(_ramCounter.NextValue() / 1024, 2);
        }

        public double GetSpeed()
        {
            return 0;
        }
    }
}

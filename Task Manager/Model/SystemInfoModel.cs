using Microsoft.Win32;
using System.IO;
using System.Management;


namespace Task_Manager.Model
{
    public class SystemInfoModel
    {
        public string OsVersion { get; set; }
        private string OsName { get; set; }
        public string PcName { get; set; }
        public int NumbersOfThreads { get; set; }
        public double TotalRam { get; set; }
        public string CpuName { get; set; }
        public string GpuName { get; set; }
        public List<DriversModel> Drivers { get; set; }
        public string NumbersOfCores { get; set; }

        public SystemInfoModel() 
        {            
            OsName = Environment.Is64BitOperatingSystem ? " 64 bit" : " 32 bit";
            OsVersion = Environment.OSVersion.ToString() + OsName;
            PcName = Environment.MachineName;
            NumbersOfThreads = Environment.ProcessorCount;
            TotalRam = Math.Round(TotalRamAmount() / (1024 * 1024 * 1024), 0);
            CpuName = GetCpuName();
            GpuName = GetGpuName();
            Drivers = GetLogicalDrivers();
            NumbersOfCores = GetCpuCores();
        }


        private string GetCpuCores()
        {
            ManagementObjectCollection moc;
            try
            {
                moc = new ManagementObjectSearcher("select NumberOfCores from Win32_Processor").Get();
            }
            catch
            {
                return "Error";
            }

            foreach (ManagementObject m in moc)
            {
                try
                {
                    return m["NumberOfCores"].ToString();
                }
                catch { }
            }
            return "Error in GetCpuCores";
        }

        private List<DriversModel> GetLogicalDrivers()
        {
            List<DriversModel> drivers = new List<DriversModel>();
            foreach(DriveInfo drive in DriveInfo.GetDrives())
            {
                if(drive.IsReady)
                {
                    string name = drive.Name;
                    double totalSizeGb = Math.Round(drive.TotalSize / (1024.0 * 1024 * 1024), 2);
                    double freeSpaceGb = Math.Round(drive.TotalFreeSpace / (1024.0 * 1024 * 1024), 2);
                    double usedSpaceGb = totalSizeGb - freeSpaceGb;
                    drivers.Add(new DriversModel { Name = name, TotalSize = totalSizeGb, UsedSpace = usedSpaceGb });
                }
            }
            return drivers;
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

        private string GetCpuName()
        {
            string registryPath = @"HARDWARE\DESCRIPTION\System\CentralProcessor\0";

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryPath))
            {
                if (key != null)
                {
                    return key.GetValue("ProcessorNameString")?.ToString() ?? "Nieznany procesor";
                }
            }
            return "Error in GetCpuName method";
        }

        private string GetGpuName()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT Name FROM Win32_VideoController"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        return obj["Name"]?.ToString() ?? "Unknow GPU";
                    }
                }
            }
            catch { }

            return "Error reading GPU name";
        }

    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Task_Manager.Model;
using System.Xml.Linq;

namespace Task_Manager.Services
{
    public class ProcessService
    {
        private DateTime _lastTime;
        private TimeSpan _lastTotalProcessorTime;
        
        public List<ProcessModel> GetProces()
        {
            List <ProcessModel> processes = new List <ProcessModel> ();
            Process[] allProcesses = Process.GetProcesses();

            foreach (Process process in allProcesses)
            {
                processes.Add(new ProcessModel { Id = process.Id, Name = process.ProcessName, WorkingSet = process.WorkingSet64 });
            }
            return processes;
        }

    }
}

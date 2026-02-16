using System.Diagnostics;
using Task_Manager.Model;

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

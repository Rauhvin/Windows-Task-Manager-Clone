using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Xml.Linq;
using Task_Manager.Model;
using Task_Manager.Services;

namespace Task_Manager.ViewModel
{
    public partial class ProcessViewModel : ObservableObject
    {
        private ProcessService _process;
        private CpuService _cpu = new CpuService();
        private RamService _ram = new RamService();

        [ObservableProperty]
        private string? _cpuUsage;

        [ObservableProperty]
        private string? _ramUsage;

        [ObservableProperty]
        private ProcessModel? _selectedProcess;

        [ObservableProperty]
        private string? _selectedProcessCpuUsage;

        public ObservableCollection<ProcessModel> Processes {  get; set; }

        public ProcessViewModel()
        {            
           _process = new ProcessService();
            var list = _process.GetProces();
            Processes = new ObservableCollection<ProcessModel>(list);

            _ = RefreshProcessList();
            _ = MonitorSystem();
        }

        async Task MonitorSystem()
        {
            while(true)
            {
                double usageCpu = _cpu.GetUsage();
                CpuUsage = usageCpu + "%";

                double usageRam = _ram.GetUsage();
                RamUsage = usageRam + "%";
                
                if(SelectedProcess != null)
                {
                    GetProcessUsage();
                }

                await Task.Delay(1000);
            }
        }

        async Task RefreshProcessList()
        {
            while(true)
            {
                var currentProcesses = _process.GetProces();
                var currentIds = currentProcesses.Select(x => x.Id).ToHashSet();
                var toRemove = Processes.Where(p => !currentIds.Contains(p.Id)).ToList();
                foreach (var item in toRemove)
                {
                    Processes.Remove(item);
                }

                

                foreach (var p in currentProcesses)
                {
                    var existing = Processes.FirstOrDefault(x => x.Id == p.Id);
                    if (existing != null)
                    {
                        existing.WorkingSet = p.WorkingSet;
                    }
                    else
                    {
                        Processes.Add(new ProcessModel { Id = p.Id, Name = p.Name, WorkingSet = p.WorkingSet });
                    }
                }

                App.Current.Dispatcher.Invoke(() =>
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(Processes);
                    view.Refresh();
                });

                await Task.Delay(5000);
            }   
        }

        [RelayCommand(CanExecute = nameof(CanKillProcess))]
        public void KillProcess()
        {
            try
            {
                var processToKill = System.Diagnostics.Process.GetProcessById(SelectedProcess.Id);
                processToKill.Kill();

                Processes.Remove(SelectedProcess);
                RefreshProcessList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Can't kill the process: {ex.Message}");
            }
        }

        public async Task GetProcessUsage()
        {
            if (SelectedProcess == null) return;
            string processName = SelectedProcess.Name;

            try
            {
                using (var cpu = new PerformanceCounter("Process", "% Processor Time", processName, true))
                {
                    cpu.NextValue();

                    await Task.Delay(500);

                    double result = Math.Round(cpu.NextValue() / Environment.ProcessorCount, 2);

                    SelectedProcessCpuUsage =processName + ": " + result + " %";
                }
            }
            catch
            {
                SelectedProcessCpuUsage = "N/A";
            }

            
            
        }

        private bool CanKillProcess()
        {
            return SelectedProcess != null;
        }

        partial void OnSelectedProcessChanged(ProcessModel? value)
        {
            KillProcessCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand]
        private void OpenCreateProcessWindow()
        {
            var createWindow = new NewProcessView();

            createWindow.Owner = System.Windows.Application.Current.MainWindow;
            createWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            bool? result = createWindow.ShowDialog();

            if(result == true)
            {
                RefreshProcessList();
            }

        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Manager.Model;

namespace Task_Manager.ViewModel
{
    public partial class SystemInfoViewModel : ObservableObject
    {
        SystemInfoModel _model;

        [ObservableProperty]
        private string _osVersion;

        [ObservableProperty]
        private string _pcName;

        [ObservableProperty]
        private int _numberOfThreads;
        
        [ObservableProperty]
        private string _numberOfCores;
        
        [ObservableProperty]
        private double _totalRam;
        
        [ObservableProperty]
        private string _cpuName;
        
        [ObservableProperty]
        private string _gpuName;

        [ObservableProperty]
        private List<DriversModel> _drivers;

        public SystemInfoViewModel()
        {
            _model = new SystemInfoModel();

            OsVersion = _model.OsVersion;
            PcName = _model.PcName;
            NumberOfThreads = _model.NumbersOfThreads;
            NumberOfCores = _model.NumbersOfCores;
            TotalRam = _model.TotalRam;
            CpuName = _model.CpuName;
            GpuName = _model.GpuName;
            Drivers = _model.Drivers;
        }
    }
}

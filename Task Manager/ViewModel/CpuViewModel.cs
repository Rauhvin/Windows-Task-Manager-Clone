using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using Task_Manager.Interfaces;
using Task_Manager.Services;

namespace Task_Manager.ViewModel
{
    public partial class CpuViewModel : ObservableObject
    {
        private readonly ISystemMonitorService _systemMonitorService;

        [ObservableProperty]
        private double _usage;
        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private string _l2Cache;
        [ObservableProperty]
        private string _l3Cache;
        [ObservableProperty]
        private string _cores;
        [ObservableProperty]
        private string _threads;
        private readonly ObservableCollection<double> _history = new();
        public ISeries[] Series { get; set; }

        [ObservableProperty]
        private Axis[] _xAxes = new Axis[]
        {
            new Axis
            {
                IsVisible = false
            }
        };


        public CpuViewModel(CpuService monitorService) 
        {
            _systemMonitorService = monitorService;
            _name = monitorService.Info["Name"];
            _l2Cache = monitorService.Info["L2CacheSize"] + " KB";
            _l3Cache = monitorService.Info["L3CacheSize"] + " KB";
            _cores = monitorService.Info["Cores"];
            _threads = monitorService.Info["Threads"];

            Series = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = _history,
                    Name = $"{_systemMonitorService.Name} using (%)",
                    Fill = new SolidColorPaint(SKColors.CornflowerBlue.WithAlpha(50)),
                    GeometrySize = 0,
                    LineSmoothness = 1
                },

                new LineSeries<double> //set Y axes 0 - 100 %
                {
                    Values = new double[] { 0, 100 },
                    Stroke = null,
                    GeometryFill = null,
                    GeometryStroke = null,
                    Name = "SkalaFix",
                    Fill = null,
                    LineSmoothness = 0
                }
            };

            _ = StartMonitoringLoop();
        }

        private async Task StartMonitoringLoop()
        {
            while (true)
            {
                double currentUsage = _systemMonitorService.GetUsage();

                Usage = currentUsage;

                _history.Add(currentUsage);

                if (_history.Count > 50)
                {
                    _history.RemoveAt(0);
                }

                await Task.Delay(1000);
            }
        }
    }
}

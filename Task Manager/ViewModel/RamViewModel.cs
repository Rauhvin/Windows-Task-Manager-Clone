using System;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using Task_Manager.Interfaces;
using Task_Manager.Services;

namespace Task_Manager.ViewModel
{
    public partial class RamViewModel : ObservableObject
    {
        private readonly ISystemMonitorService _systemMonitorService;
        private RamService _amService = new RamService();

        [ObservableProperty]
        private double _usage;
        [ObservableProperty]
        private string _speed;
        [ObservableProperty]
        private string _manufacturer;
        [ObservableProperty]
        private string _totalRam;
        [ObservableProperty]
        private string _availableRam;
        private readonly ObservableCollection<double> _history = new();
        public ISeries[] Series { get; set; }

        public RamViewModel(RamService monitorService)
        {
            _systemMonitorService = monitorService;
            _speed = monitorService._speed;
            _manufacturer = monitorService._manufacturer;
            _totalRam = monitorService._ram + " GB";
            _availableRam = monitorService._available + " GB";

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
                AvailableRam = _amService.GetRam() + " GB";

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

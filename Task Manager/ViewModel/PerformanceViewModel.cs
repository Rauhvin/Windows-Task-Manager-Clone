using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Task_Manager.ViewModel
{
    public partial class PerformanceViewModel : ObservableObject
    {
        [ObservableProperty]
        private object? _currentPerformanceSubView;

        public PerformanceViewModel()
        {
            ShowCpu();
        }

        [RelayCommand]
        private void ShowCpu() => CurrentPerformanceSubView = new CpuViewModel(new Services.CpuService());

        [RelayCommand]
        private void ShowRam() => CurrentPerformanceSubView = new RamViewModel(new Services.RamService());
    }
}

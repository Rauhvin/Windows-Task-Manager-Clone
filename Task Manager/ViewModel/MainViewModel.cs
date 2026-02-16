using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Task_Manager.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private object? _currentView;

        public MainViewModel()
        {
            CurrentView = new PerformanceViewModel();
        }

        [RelayCommand]
        private void ShowPerformance() => CurrentView = new PerformanceViewModel();

        [RelayCommand]
        private void ShowProcesses() => CurrentView = new ProcessViewModel();

        [RelayCommand]
        private void ShowInfo() => CurrentView = new SystemInfoViewModel();

    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows;

namespace Task_Manager.ViewModel
{
    public partial class NewProcessViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _processName = string.Empty;

        [RelayCommand(CanExecute = nameof(CanRun))]
        private void StartProcess(object window)
        {
            if (!string.IsNullOrEmpty(ProcessName))
            {
                try
                {
                    Process.Start(ProcessName);
                    if (window is Window win)
                    {
                        win.DialogResult = true;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }  
        }

        private bool CanRun() => !string.IsNullOrWhiteSpace(ProcessName);

        partial void OnProcessNameChanged(string value) => StartProcessCommand.NotifyCanExecuteChanged();
    }
}

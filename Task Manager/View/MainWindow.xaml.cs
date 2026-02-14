using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task_Manager.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace Task_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PerformanceView performanceView = new PerformanceView();
            Frame.Navigate(performanceView);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PerformanceView performanceView = new PerformanceView();
            Frame.Navigate(performanceView);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ProcessesView processesView = new ProcessesView();
            Frame.Navigate(processesView);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SystemInfoView systemInfoView = new SystemInfoView();
            Frame.Navigate(systemInfoView);
        }
    }
}
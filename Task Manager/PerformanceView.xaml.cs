using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task_Manager
{
    /// <summary>
    /// Logika interakcji dla klasy PerformanceView.xaml
    /// </summary>
    public partial class PerformanceView : UserControl
    {
        public PerformanceView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CpuView cpuView = new CpuView();
            MainFrame.Navigate(cpuView);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RamView ramView = new RamView();
            MainFrame.Navigate(ramView);
        }
    }
}

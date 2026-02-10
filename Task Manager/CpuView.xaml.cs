using LiveChartsCore.SkiaSharpView.WPF;
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
using Task_Manager.Services;
using Task_Manager.ViewModel;
using LiveChartsCore;

namespace Task_Manager
{
    /// <summary>
    /// Logika interakcji dla klasy CpuView.xaml
    /// </summary>
    public partial class CpuView : UserControl
    {
        public CpuView()
        {
            InitializeComponent();
            CpuService cpu = new CpuService();
            this.DataContext = new CpuViewModel(cpu);

        }
    }
}

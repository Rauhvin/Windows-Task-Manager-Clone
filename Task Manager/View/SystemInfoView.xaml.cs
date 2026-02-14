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
using Task_Manager.ViewModel;

namespace Task_Manager
{
    /// <summary>
    /// Logika interakcji dla klasy SystemInfoView.xaml
    /// </summary>
    public partial class SystemInfoView : UserControl
    {
        public SystemInfoView()
        {
            InitializeComponent();
            SystemInfoViewModel viewModel = new SystemInfoViewModel();
            this.DataContext = viewModel;
        }
    }
}

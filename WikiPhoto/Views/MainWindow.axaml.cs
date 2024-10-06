using SukiUI.Controls;
using WikiPhoto.ViewModels;

namespace WikiPhoto.Views
{
    public partial class MainWindow : SukiWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}

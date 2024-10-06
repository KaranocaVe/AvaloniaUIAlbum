using Avalonia.Controls;
using WikiPhoto.ViewModels;

namespace WikiPhoto.Views
{
    public partial class AboutView : UserControl
    {
        public AboutView()
        {
            InitializeComponent();
            DataContext = new AboutViewModel();
        }
    }
}

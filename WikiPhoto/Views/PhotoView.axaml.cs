using Avalonia.Controls;
using WikiPhoto.ViewModels;

namespace WikiPhoto.Views
{
    public partial class PhotoView : UserControl
    {
        public PhotoView()
        {
            InitializeComponent();
            DataContext = new PhotoViewModel();
        }
    }
}

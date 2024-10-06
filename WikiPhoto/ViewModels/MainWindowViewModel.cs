using System.Collections.ObjectModel;
using ReactiveUI;
using SukiUI.Controls;
using WikiPhoto.Views;

namespace WikiPhoto.ViewModels
{

    public class MainWindowViewModel : ViewModelBase
    {


        private ObservableCollection<string> _photo = new();


        public ObservableCollection<string> Photo
        {
            get => _photo;
            set => this.RaiseAndSetIfChanged(ref _photo, value);
        }


        private ObservableCollection<SukiSideMenuItem> _menuItems;

        public ObservableCollection<SukiSideMenuItem> MenuItems
        {
            get => _menuItems;
            set => this.RaiseAndSetIfChanged(ref _menuItems, value);
        }
        
        private SukiSideMenuItem _selectedMenuItem;
        
        public SukiSideMenuItem SelectedMenuItem
        {
            get => _selectedMenuItem;
            set => this.RaiseAndSetIfChanged(ref _selectedMenuItem, value);
        }
        
        public MainWindowViewModel()
        {
            MenuItems = new ObservableCollection<SukiSideMenuItem>
            {
                new SukiSideMenuItem()
                {
                    Header = "当月照片",
                    PageContent = new PhotoView()
                },
                new SukiSideMenuItem()
                {
                    Header = "关于",
                    PageContent = new AboutView()
                }
            };
            SelectedMenuItem=MenuItems[0];
        }
    }
}

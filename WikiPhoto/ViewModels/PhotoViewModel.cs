using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace WikiPhoto.ViewModels
{
    public class PhotoViewModel : ViewModelBase
    {
        public class PhotoItem
        {
            public string Title{get;set;}
            public string Description{get;set;}
            public Bitmap Image{get;set;}
            
            public int Width => Image.PixelSize.Width/2;
            public int Height => Image.PixelSize.Height/2;
        }
        private async Task<Bitmap> DownloadPhotoAsync(string url)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            return new Bitmap(stream);
        }

        private ObservableCollection<PhotoItem> _images = new ObservableCollection<PhotoItem>();
        public ObservableCollection<PhotoItem> Images
        {
            get => _images;
            set => this.RaiseAndSetIfChanged(ref _images, value);
        }
        
        public PhotoViewModel()
        {
            LoadImages();
        }

        private async Task LoadImages()
        {
            var rssModel = new Models.RssModel();
            var infoFromRss = await rssModel.GetImageUrlFromRss("https://commons.wikimedia.org/w/api.php?action=featuredfeed&feed=potd&feedformat=atom&language=zh");
            
            foreach (var item in infoFromRss)
            {
                var image = await DownloadPhotoAsync(item.imageUrl);
                Images.Add(new PhotoItem()
                {
                    Title = $"{item.month}月{item.day}日",
                    Description = item.info,
                    Image = image
                });
            }
        }

        }
        
        
    }


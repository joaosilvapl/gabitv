using System.IO;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using GabiTV.Core;
using GabiTV.DataAccess;

namespace GabiTV.ViewModel
{
    public class MainPageViewModel : IMainPageViewModel
    {
        //private readonly IDataProvider _dataProvider = new EmbeddedDataProvider();
        private readonly IDataProvider _dataProvider = new CachedDataProvider(new HttpDataProvider());

        private int _currentPhotoIndex;

        public string PhotoLabel
        {
            get
            {
                var total = this._dataProvider.GetTotalPhotoCount();
                return $"Number of photos: {total}";
            }
        }

        public Task<BitmapImage> GetPhoto()
        {
            return this.GetPhoto(this._currentPhotoIndex);
        }

        public void MoveToNextPhoto()
        {
            if (this._currentPhotoIndex < this._dataProvider.GetTotalPhotoCount() - 1)
            {
                this._currentPhotoIndex++;
            }
        }

        public void MoveToPreviousPhoto()
        {
            if (this._currentPhotoIndex > 0)
            {
                this._currentPhotoIndex--;
            }
        }

        public int GetCurrentPhotoPosition()
        {
            return this._currentPhotoIndex + 1;
        }

        private async Task<BitmapImage> GetPhoto(int index)
        {
            //TODO: add caching, memory management, move to dedicated class

            var bytes = await this._dataProvider.GetPhotoBytesAsync(index);

            using (var s = new InMemoryRandomAccessStream())
            {
                Stream stream = s.AsStreamForWrite();
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);

                BitmapImage source = new BitmapImage();
                source.SetSource(s);

                return source;
            }
        }
    }
}
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace GabiTV.ViewModel
{
    public interface IMainPageViewModel
    {
        string PhotoLabel { get; }
        Task<BitmapImage> GetPhoto();
        void MoveToNextPhoto();
        void MoveToPreviousPhoto();
    }
}

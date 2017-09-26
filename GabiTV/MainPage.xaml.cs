using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GabiTV.ViewModel;

namespace GabiTV
{
    public sealed partial class MainPage : Page
    {
        public IMainPageViewModel MainPageViewModel;

        public MainPage()
        {
            this.InitializeComponent();

            this.MainPageViewModel = new MainPageViewModel();

            this.PhotoTextBlock.Text = this.MainPageViewModel.PhotoLabel;

            this.StartPhotoLoad();
        }

        private async void NextButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.MainPageViewModel.MoveToNextPhoto();
            await this.LoadPhoto();
        }

        private async void PreviousButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.MainPageViewModel.MoveToPreviousPhoto();
            await this.LoadPhoto();
        }
        
        private void StartPhotoLoad()
        {
            var task = this.LoadPhoto();

            //task.
            Task.WhenAll(new[] {task}).ContinueWith(t =>
            {
                //TODO: handle completion, exception
            });
        }

        private async Task LoadPhoto()
        {
            this.ImageLoadingTextBlock.Visibility = Visibility.Visible;

            this.Image.Source = null;
            
            var image = await this.MainPageViewModel.GetPhoto();

            this.Image.Source = image;

            this.ImageLoadingTextBlock.Visibility = Visibility.Collapsed;
        }
    }
}

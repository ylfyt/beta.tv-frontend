using BuletinKlp01FE.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuletinKlp01FE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoPlayer : ContentPage
    {
        private readonly Video _video;

        public VideoPlayer(Video video)
        {
            InitializeComponent();
            _video = video;
            LoadVideo();
        }

        private void LoadVideo()
        {
            if (_video.Categories.Count == 0)
            {
                categoryText.IsVisible = false;
            }
            else
            {
                var temp = "";
                _video.Categories.ForEach(cat =>
                {
                    cat = cat.Length == 1 ? cat.ToUpper() : (char.ToUpper(cat[0]) + cat[1..]);
                    temp += $"#{cat}  ";
                });
                categoryText.Text = temp;
            }

            titleText.Text = _video.Title;
            sourceText.Text = _video.ChannelName;
            authorNameText.Text = _video.AuthorName;
            descriptionText.Text = _video.Description;
            VideoWebView.Source = _video.Url;
            this.Title = "Video";
        }
    }
}
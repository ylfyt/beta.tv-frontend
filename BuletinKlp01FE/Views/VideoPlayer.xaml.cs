using BuletinKlp01FE.Models;
using BuletinKlp01FE.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

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
            categoryText.Text = "#" + _video.Category;
            titleText.Text = _video.Title;
            sourceText.Text = _video.ChannelName;
            authorNameText.Text = _video.NewsAuthorName;
            descriptionText.Text = _video.Description;
            this.Title = "Video";
        }

        public async void PlayVideo()
        {
            var youtube = new YoutubeClient();

            var streamManifest = await youtube.Videos.Streams.GetManifestAsync("_tEw-Him7ho");

            // Get highest quality muxed stream
            var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestBitrate();

           
            if (streamInfo != null)
            {
                // mediaElement.Source = "https://www.youtube.com/embed/_tEw-Him7ho";
                // DependencyService.Get<IMessage>().ShortAlert(streamInfo.Url);
                // Console.WriteLine("=====================");
                // Console.WriteLine(streamInfo.Url);
                // Console.WriteLine("=====================");
            }
        }

        // NAVIBAR
        private void HomePic_OnClicked(object sender, EventArgs e)
        {
            try
            {
                Application.Current.MainPage = new NavigationPage(new Homepage())
                {
                    BarTextColor = Color.FromHex("#3F72AF"),
                    BarBackgroundColor = Color.White,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SearchPic_OnClicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new MainPage());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SubPic_OnClicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new MainPage());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ProfilePic_OnClicked(object sender, EventArgs e)
        {
            try
            {
                //Navigation.PushAsync(new Profile());
                Application.Current.MainPage = new NavigationPage(new Profile())
                {
                    BarTextColor = Color.FromHex("#3F72AF"),
                    BarBackgroundColor = Color.White,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
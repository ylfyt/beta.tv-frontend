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
    }
}
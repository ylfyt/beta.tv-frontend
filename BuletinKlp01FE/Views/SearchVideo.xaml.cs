using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Dtos.video;
using BuletinKlp01FE.Models;
using BuletinKlp01FE.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuletinKlp01FE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchVideo : ContentPage
    {
        public SearchVideo()
        {
            InitializeComponent();

            List<Video> temp = new()
            {
                new Video()
                {
                    Title = "Coba 1",
                    ChannelName = "Nature Channel" ,
                    VideoInfo = "Nature Channel • 2.3M Views • 9 Month ago"
                },
                new Video()
                {
                    Title = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque quis pellentesque turpis. Donec justo quam, iaculis id elit sed, eg",
                    ChannelName = "Lorem",
                    VideoInfo = "Nature Channel • 2.3M Views • 9 Month ago"
                }
            };

            VideosListView.ItemsSource = temp;
        }

        public async void VideoSelected(object sender, ItemTappedEventArgs e)
        {
            var video = e.Item as Video;
            
            if (video == null)
            {
                Console.WriteLine("Something wrong!!");
                return;
            }

            await Navigation.PushAsync(new VideoPlayer(video));
        }

        public async void SearchButtonClicked(object sender, EventArgs e)
        {
            try
            {
                SearchButton.Source = "search_icon_primary.png";
                MessageText.Text = "Loading...";

                if (QueryTextInput.Text == null || QueryTextInput.Text == "")
                {
                    MessageText.Text = "Input not valid";
                    SearchButton.Source = "search_icon.png";
                    return;
                }

                VideosListView.ItemsSource = null;

                var token = Preferences.Get("token", "");
                if (token == "")
                {
                    MessageText.Text = "Please, Login first!!";
                    SearchButton.Source = "search_icon.png";
                    return;
                }

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", token);
                var content = new StringContent(JsonConvert.SerializeObject(new { query = QueryTextInput.Text }), Encoding.UTF8, "application/json");
                string weburl = Constants.SEARCH_VIDEO_ENDPOINT;
                var httpResponseMessage = await client.PostAsync(weburl, content);

                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                var responseVideo = JsonConvert.DeserializeObject<ResponseDto<DataVideos>>(responseBody);

                if (responseVideo == null)
                {
                    MessageText.Text = "Something wrong";
                    SearchButton.Source = "search_icon.png";
                    return;
                }

                if (!responseVideo.Success)
                {
                    MessageText.Text = "Gagal mendapatkan video!!";
                    SearchButton.Source = "search_icon.png";
                    return;
                }

                responseVideo.Data?.Videos.ForEach(video =>
                {
                    video.ChannelName = video.Id;
                    video.VideoInfo = video.ChannelName + " • " + 1231230 + " • " + DateTime.Today;
                });

                VideosListView.ItemsSource = responseVideo.Data?.Videos;

                MessageText.Text = "Hasil Pencarian";
                SearchButton.Source = "search_icon.png";
            }
            catch (Exception ex)
            {
                MessageText.Text = "Something wrong";
                SearchButton.Source = "search_icon.png";
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
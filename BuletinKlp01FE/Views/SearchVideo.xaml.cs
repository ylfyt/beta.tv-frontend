using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Dtos.video;
using BuletinKlp01FE.Models;
using BuletinKlp01FE.Services;
using BuletinKlp01FE.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            List<Video> temp = new();
            VideosListView.ItemsSource = temp;
            QueryTextInput.Focus();
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
                SetUI(true, "Loading...", false);

                if (QueryTextInput.Text == null || QueryTextInput.Text == "")
                { 
                    SetUI(false, "Tolong masukan kata pencarian!");
                    return;
                }

                VideosListView.ItemsSource = null;


                var client = HttpClientGetter.GetHttpClientWithTokenHeader();
                if (client == null)
                {
                    SetUI(false, "Silahkan login terlebih dahulu");
                    return;
                }

                var content = new StringContent(JsonConvert.SerializeObject(new { query = QueryTextInput.Text }), Encoding.UTF8, "application/json");
                string weburl = Constants.SEARCH_VIDEO_ENDPOINT;
                var httpResponseMessage = await client.PostAsync(weburl, content);

                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                var responseVideo = JsonConvert.DeserializeObject<ResponseDto<DataVideos>>(responseBody);

                if (responseVideo == null)
                {
                    SetUI(false, "Gagal mendapatkan video!");
                    return;
                }

                if (!responseVideo.Success)
                {
                    SetUI(false, "Gagal mendapatkan video!");
                    Console.WriteLine(responseBody);
                    return;
                }

                if (responseVideo.Data?.Videos.Count == 0)
                {
                    SetUI(false, "Tidak ada video yang ditemukan!");
                    return;
                }

                VideosListView.ItemsSource = responseVideo.Data?.Videos;

                SetUI(false, "Hasil pencarian: ", false);
            }
            catch (Exception ex)
            {
                SetUI(false, "Gagal mendapatkan video!");
                Console.WriteLine(ex.Message);
            }
            
        }

        public void SetUI(bool fetching, string message, bool error = true)
        {
            MessageText.Text = message;
            if (error)
            {
                MessageText.TextColor = Color.Orange;
            }
            else
            {
                MessageText.TextColor = Color.FromHex("#3F72AF");
            }
            if (fetching)
            {
                SearchButton.Source = "search_icon_primary.png";
            }
            else
            {
                SearchButton.Source = "search_icon.png";
            }
        }
    }
}
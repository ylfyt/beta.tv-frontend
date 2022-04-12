using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Dtos.video;
using BuletinKlp01FE.Models;
using BuletinKlp01FE.Services;
using BuletinKlp01FE.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuletinKlp01FE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Homepage : ContentPage
    {
        readonly string color0 = "#3F72AF";
        readonly string color1 = "#112D4E";
        readonly string color5 = "#DBE2EF";

        private readonly List<Button> catButtons;

        public Homepage()
        {
            InitializeComponent();

            catButtons = new List<Button>();
            catButtons.Add(ButtonAll);
            catButtons.Add(ButtonTech);
            catButtons.Add(ButtonDesain);
            catButtons.Add(ButtonBisnis);
            _ = GetVideos();
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

        async Task GetVideos(string? category = null)
        {
            try
            {
                var client = HttpClientGetter.GetHttpClientWithTokenHeader();
                if (client == null)
                {
                    Console.WriteLine("client null");
                    DependencyService.Get<IMessage>().ShortAlert("Client is null!");
                    return;
                }
                
                string weburl = Constants.VIDEO_ENDPOINT;

                if (category != null)
                {
                    weburl += "/category/" + category;
                }

                VideosListView.ItemsSource = null;
                SetMessage("Loading...");
                var httpResponseMessage = await client.GetAsync(weburl);


                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    SetMessage();
                    DependencyService.Get<IMessage>().ShortAlert("Failed to get video!");
                    return;
                }

                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                var responseVideo = JsonConvert.DeserializeObject<ResponseDto<DataVideos>>(responseBody);
                
                if (responseVideo == null)
                {
                    SetMessage();
                    DependencyService.Get<IMessage>().ShortAlert("Failed to get video!");
                    return;
                }

                if (!responseVideo.Success)
                {
                    SetMessage();
                    DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                    return;
                }

                if (responseVideo.Data?.Videos.Count == 0)
                {
                    SetMessage("No Videos Found!");
                    DependencyService.Get<IMessage>().ShortAlert("No Videos Found!");
                    return;
                }

                VideosListView.ItemsSource = responseVideo.Data?.Videos;
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                Console.WriteLine(ex.Message);
            }

            SetMessage();
        }

        void SetMessage(string? message=null)
        {
            if (message == null || message == "")
            {
                ErrorMessage.IsVisible = false;
                return;
            }

            ErrorMessage.IsVisible = true;
            ErrorMessage.Text = message;
        }

        async void AllVideosClicked(object sender, EventArgs args)
        {
            ChangeActiveCatButton("all");
            await GetVideos();
        }

        async void TechVideosClicked(object sender, EventArgs args)
        {
            ChangeActiveCatButton("tech");
            await GetVideos("tech");
        }

        async void DesainVideosClicked(object sender, EventArgs args)
        {
            ChangeActiveCatButton("desain");
            await GetVideos("desain");
        }

        async void BisnisVideosClicked(object sender, EventArgs args)
        {
            ChangeActiveCatButton("bisnis");
            await GetVideos("bisnis");
        }


        void ChangeActiveCatButton(string cat)
        {
            catButtons.ForEach(but => {
                but.BackgroundColor = Color.FromHex(color5);
                but.TextColor = Color.FromHex(color1);
            });

            if (cat == "tech")
            {
                ButtonTech.BackgroundColor = Color.FromHex(color0);
                ButtonTech.TextColor = Color.FromHex("#ffffff");
            }
            else if (cat == "bisnis")
            {
                ButtonBisnis.BackgroundColor = Color.FromHex(color0);
                ButtonBisnis.TextColor = Color.FromHex("#ffffff");
            }
            else if (cat == "desain")
            {
                ButtonDesain.BackgroundColor = Color.FromHex(color0);
                ButtonDesain.TextColor = Color.FromHex("#ffffff");
            }
            else
            {
                ButtonAll.BackgroundColor = Color.FromHex(color0);
                ButtonAll.TextColor = Color.FromHex("#ffffff");
            }
        }
        
    }
}
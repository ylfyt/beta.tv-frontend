using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Dtos.video;
using BuletinKlp01FE.Models;
using BuletinKlp01FE.Services;
using BuletinKlp01FE.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuletinKlp01FE.ViewModels
{
    class HomepageViewModel : BindableObject
    {
        public ObservableRangeCollection<Video>? Beritasaya { get; set; }
        public string errmsg { get; set; } = String.Empty;

        public HomepageViewModel()
        {
            /*
            Beritasaya = new ObservableRangeCollection<Video>();
            IShowAllVideos = new Command(ShowAllVideos);

            ShowAllVideos();
            */
        }

        public void getBeritaSaya()
        {
            //Beritasaya.Add(new Video { VideoInfo = "tutorial xamarin nomor 1 cepat", AuthorTitle = "ChannelAuthorTitle contoh", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs", ThumbnailSource = "sampleVideo" });
        }

        public ICommand IShowAllVideos { get; } = null!;
        public ICommand IShowCategoryVideos
        {
            get
            {
                return new Command<string>((x) => ShowCategoryVideos(x));
            }
        }

        async void ShowAllVideos()
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

                Beritasaya.Clear();
                
                string weburl = Constants.HOMEPAGE_ALL_VIDEO_ENDPOINT;
                var httpResponseMessage = await client.GetAsync(weburl);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Failed to get video!");
                    return;
                }

                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                var responseVideo = JsonConvert.DeserializeObject<ResponseDto<DataVideos>>(responseBody);

                if (responseVideo == null)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Failed to get video!");
                    return;
                }

                if (!responseVideo.Success)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Failed to get video!");
                    return;
                }

                if (responseVideo.Data?.Videos.Count == 0)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Videos is empty");
                    return;
                }

                responseVideo.Data?.Videos.ForEach(video =>
                {
                    Beritasaya.Add(video);
                });
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                Console.WriteLine(ex.Message);
            }

        }

        async void ShowCategoryVideos(String x)
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

                Beritasaya.Clear();

                string weburl = Constants.VIDEO_ENDPOINT + "/" + x;
                var httpResponseMessage = await client.GetAsync(weburl);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Failed to get video!");
                    return;
                }

                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                var responseVideo = JsonConvert.DeserializeObject<ResponseDto<DataVideos>>(responseBody);

                if (responseVideo == null)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Failed to get video!");
                    return;
                }

                if (!responseVideo.Success)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Failed to get video!");
                    return;
                }

                if (responseVideo.Data?.Videos.Count == 0)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Videos is empty");
                    return;
                }

                responseVideo.Data?.Videos.ForEach(video =>
                {
                    Beritasaya.Add(video);
                });
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                Console.WriteLine(ex.Message);
            }
        }
    }
}

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
        public ObservableRangeCollection<Video> Beritasaya { get; set; }
        public string errmsg { get; set; }

        public HomepageViewModel()
        {
            Beritasaya = new ObservableRangeCollection<Video>();
            IShowAllVideos = new Command(ShowAllVideos);

            ShowAllVideos();

        }

        public void getBeritaSaya()
        {
            //Beritasaya.Add(new Video { VideoInfo = "tutorial xamarin nomor 1 cepat", AuthorTitle = "ChannelAuthorTitle contoh", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs", ThumbnailSource = "sampleVideo" });
        }

        public ICommand IShowAllVideos { get; }
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
                var client = HttpClientGetter.GetHttpClient();

                string weburl = Constants.HOMEPAGE_ALL_VIDEO_ENDPOINT;
                client.BaseAddress = new Uri(weburl);
                //Beritasaya.Add(new Video { VideoInfo = "sini1", AuthorTitle = "sini1", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs", ThumbnailSource = "sampleVideo" });
                var httpResponseMessage = await client.GetAsync("");
                //Beritasaya.Add(new Video { VideoInfo = "sini", AuthorTitle = "sini", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs", ThumbnailSource = "sampleVideo" });

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    //Beritasaya.Add(new Video { VideoInfo = "semua", AuthorTitle = "semua", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs", ThumbnailSource = "sampleVideo" });
                    string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                    //Beritasaya.Add(new Video { VideoInfo = "semua1", AuthorTitle = "semua1", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs", ThumbnailSource = "sampleVideo" });
                    var responseVideo = JsonConvert.DeserializeObject<ResponseDto<DataVideos>>(responseBody);
                    errmsg = "menampilkan hasil";

                    foreach (Video v in responseVideo.Data.Videos){
                        Beritasaya.Add(new Video { VideoInfo = "nih", AuthorTitle = "semuaasrtdggkj", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs", ThumbnailSource = "sampleVideo" });
                    }

                    responseVideo.Data.Videos.ForEach(video =>
                    {
                        var dt = DateTimeOffset.FromUnixTimeSeconds(int.Parse(video.CreateAt)).LocalDateTime;

                        video.ChannelName = "hasil";//video.ChannelName;
                        video.VideoInfo = video.ChannelName + " • " + dt.ToString("MMMM dd, yyyy");
                        video.ThumbnailSource = "sampleVideo";//ImageSource.FromUri(new Uri(video.ThumbnailUrl));
                        Beritasaya.Add(video);
                        Beritasaya.Add(new Video { VideoInfo = video.ChannelName, AuthorTitle = "semuaasrtdggkj", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs", ThumbnailSource = "sampleVideo" });
                    });

                    Beritasaya.Add(new Video { VideoInfo = responseVideo.Message, AuthorTitle = responseVideo.Data.ToString(), Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs", ThumbnailSource = "sampleVideo" });


                }
                else
                {
                    errmsg = "gagal";
                    Beritasaya.Add(new Video { VideoInfo = "ggal", AuthorTitle = "gagal", Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs", ThumbnailSource = "sampleVideo" });
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                errmsg = ex.Message;
                Beritasaya.Add(new Video { VideoInfo = "exception", AuthorTitle = ex.Message, Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs", ThumbnailSource = "sampleVideo" });
            }

        }

        async void ShowCategoryVideos(String x)
        {
            //Beritasaya.Add(new Video { VideoInfo = x, AuthorTitle = x, Url = "https://www.youtube.com/watch?v=JH8ekYJrFHs", ThumbnailSource = "sampleVideo" });

            try
            {
                var client = HttpClientGetter.GetHttpClient();

                string weburl = Constants.HOMEPAGE_ALL_VIDEO_ENDPOINT + "/"+x;
                client.BaseAddress = new Uri(weburl);
                var httpResponseMessage = await client.GetAsync("");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    
                    string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                    var responseVideo = JsonConvert.DeserializeObject<ResponseDto<DataVideos>>(responseBody);

                    responseVideo.Data?.Videos.ForEach(video =>
                    {
                        var dt = DateTimeOffset.FromUnixTimeSeconds(int.Parse(video.CreateAt)).LocalDateTime;

                        video.ChannelName = video.ChannelName;
                        video.VideoInfo = video.ChannelName + " • " + dt.ToString("MMMM dd, yyyy");
                        video.ThumbnailSource = ImageSource.FromUri(new Uri(video.ThumbnailUrl));
                    });

                    foreach (Video video in responseVideo.Data?.Videos)
                    {
                        Beritasaya.Add(video);
                    }

                    errmsg = "menampilkan hasil";
                }
                else
                {
                    errmsg = "Gagal memperbarui daftar video. Coba lagi nanti";
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                errmsg = ex.Message;
            }
        }
    }
}

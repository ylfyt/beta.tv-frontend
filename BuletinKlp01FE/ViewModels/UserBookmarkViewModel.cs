using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Models;
using BuletinKlp01FE.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuletinKlp01FE.ViewModels
{
    class UserBookmarkViewModel
    {
        public ObservableRangeCollection<Video> Beritasaya { get; set; }
        public UserBookmarkViewModel()
        {
            Beritasaya = new ObservableRangeCollection<Video>();
            getBookmarks();

        }

        async void getBookmarks()
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

                string weburl = Constants.BOOKMARK_END_POINT;
                var httpResponseMessage = await client.GetAsync(weburl);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Failed to get video!");
                    return;
                }
                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                var responseVideo = JsonConvert.DeserializeObject<ResponseDto<Dtos.video.DataVideos>>(responseBody);

                if (responseVideo == null)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Failed to get video!");
                    return;
                }

                if (!responseVideo.Success)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                    return;
                }

                if (responseVideo.Data?.Videos.Count == 0)
                {
                    DependencyService.Get<IMessage>().ShortAlert("No Videos Found!");
                    return;
                }

                foreach (Video video in responseVideo.Data?.Videos)
                {
                    Beritasaya.Add(video);
                }
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                Console.WriteLine(ex.Message);
            }


        }
    }
}

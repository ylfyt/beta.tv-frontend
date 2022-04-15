using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Dtos.video;
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
            GetBookmarks();

        }

        public async void GetBookmarks()
        {
            try
            {
                var response = await APIRequest.Send<DataVideos>(Constants.ENDPOINT_BOOKMARK);
                if (!response.Success)
                {
                    Beritasaya.Clear();
                    return;
                }

                Beritasaya.AddRange(response.Data!.Videos);
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                Console.WriteLine(ex.Message);
            }
        }
    }
}

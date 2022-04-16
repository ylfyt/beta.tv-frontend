using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Dtos.video;
using BuletinKlp01FE.Models;
using BuletinKlp01FE.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BuletinKlp01FE.ViewModels
{
    class UserBookmarkViewModel : BindableObject
    {
        public ObservableCollection<Video> Beritasaya { get; set; }
        bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { 
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        public UserBookmarkViewModel()
        {
            Beritasaya = new ObservableCollection<Video>();
            _ = GetBookmarks();

        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    Beritasaya.Clear();
                    RefreshData();
                });
            }
        }

        async void RefreshData()
        {
            IsRefreshing = true;
            await GetBookmarks();
            IsRefreshing = false;
        }

        public async Task GetBookmarks()
        {
            try
            {
                var response = await APIRequest.Send<DataVideos>(Constants.ENDPOINT_BOOKMARK);
                if (!response.Success)
                {
                    DependencyService.Get<IMessage>().ShortAlert("Gagal mendapatkan bookmark");
                    return;
                }
                response.Data!.Videos.ForEach(v => Beritasaya.Add(v));
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                Console.WriteLine(ex.Message);
            }
        }
    }
}

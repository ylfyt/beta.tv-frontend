using BuletinKlp01FE.Dtos.video;
using BuletinKlp01FE.Models;
using BuletinKlp01FE.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BuletinKlp01FE.ViewModels
{
    class HomepageViewModel : BindableObject
    {
        private ObservableCollection<Video> _videos { get; set; }
        public ObservableCollection<Video> Videos
        {
            get => _videos;
            set { _videos = value; }
        }
        private string? slug = null;

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public HomepageViewModel()
        {
            _videos = new ObservableCollection<Video>();
            _ = FetchVideo();
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    _videos.Clear();
                    RefreshData();
                });
            }
        }

        public async void RefreshData()
        {
            IsRefreshing = true;
            await FetchVideo();
            IsRefreshing = false;
        }

        public void GetVideo(string x = "")
        {
            slug = x == string.Empty ? null : x;
            _videos.Clear();
            RefreshData();
        }

        async Task FetchVideo()
        {
            string endpoint = slug == null ? "/video" : "/video/category/" + slug;

            var response = await APIRequest.Send<DataVideos>(endpoint);

            if (!response.Success)
            {
                DependencyService.Get<IMessage>().ShortAlert("Gagal mendapatkan video!");
                return;
            }

            if (response.Data!.Videos.Count == 0)
            {
                DependencyService.Get<IMessage>().ShortAlert("Tidak ada video yang ditemukan!");
                return;
            }

            response.Data.Videos.ForEach(v => _videos.Add(v));
        }
    }
}

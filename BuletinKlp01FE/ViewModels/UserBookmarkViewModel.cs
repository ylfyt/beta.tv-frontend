using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Dtos.user;
using BuletinKlp01FE.Dtos.video;
using BuletinKlp01FE.Models;
using BuletinKlp01FE.Services;
using FFImageLoading;
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

        ImageSource userprofile;
        public ImageSource Userprofile
        {
            get => userprofile;
            set
            {
                if (value == userprofile)
                {
                    return;
                }
                userprofile = value;
                OnPropertyChanged(nameof(Userprofile));
            }
        }

        string usernameDisplay = "username";
        public string UsernameDisplay
        {
            get => usernameDisplay;
            set
            {
                if (value == usernameDisplay)
                {
                    return;
                }
                usernameDisplay = value;
                OnPropertyChanged(nameof(UsernameDisplay));
            }
        }
        int id;
        public int Id
        {
            get => id;
            set
            {
                if (value == id)
                {
                    return;
                }
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }


        string nameEmailDisplay;
        public string NameEmailDisplay
        {
            get => nameEmailDisplay;
            set
            {
                if (value == nameEmailDisplay)
                {
                    return;
                }
                nameEmailDisplay = value;
                OnPropertyChanged(nameof(NameEmailDisplay));
            }
        }

        public UserBookmarkViewModel()
        {
            Beritasaya = new ObservableCollection<Video>();
            _ = GetBookmarks();
            GetPhoto();

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

        public void VModelActivate(Page sender, EventArgs eventArgs)
        {
            _ = GetBookmarks();
            GetPhoto();
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
                Beritasaya.Clear();
                response.Data!.Videos.ForEach(v => Beritasaya.Add(v));
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessage>().ShortAlert("Something wrong!");
                Console.WriteLine(ex.Message);
            }
        }

        async void GetPhoto()
        {
            try
            {
                var client = HttpClientGetter.GetHttpClientWithTokenHeader();
                if (client == null)
                {
                    return;
                }

                string weburl = Constants.ME_END_POINT;

                var httpResponseMessage = await client.GetAsync(weburl);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<ResponseDto<DataUser>>(responseBody);

                    Id = response.Data.user.Id;
                    UsernameDisplay = response.Data.user.Username;
                    NameEmailDisplay = response.Data.user.Name + " - " + response.Data.user.Email;

                    try {
                        var url = response.Data.user.PhotoURL;
                        if (url != String.Empty)
                        {
                            Userprofile = ImageSource.FromUri(new Uri(url));
                        }
                        else
                        {
                            Userprofile = "userprofile";
                            DependencyService.Get<IMessage>().ShortAlert("Foto profil belum diset");
                        }
                        
                    }
                    catch (Exception e)
                    {
                        await Application.Current.MainPage.DisplayAlert("Gagal memuat foto profil", e.Message, "OK");
                        Userprofile = "userprofile";
                    }
                }
                else
                {
                    DependencyService.Get<IMessage>().ShortAlert("Something's wrong");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("exception", ex.Message, "Ok");
            }
        }
    }
}

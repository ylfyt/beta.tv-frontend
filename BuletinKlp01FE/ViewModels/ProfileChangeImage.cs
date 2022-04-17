using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Services;
using BuletinKlp01FE.Utils;
using Newtonsoft.Json;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Firebase.Storage;
using BuletinKlp01FE.Dtos.user;

namespace BuletinKlp01FE.ViewModels
{
    class ProfileChangeImage : BindableObject
    {
        ImageSource userProfileImg;
        string Username;
        string downloadLink;

        public ImageSource UserProfileImg
        {
            get => userProfileImg;
            set
            {
                if (value == userProfileImg)
                {
                    return;
                }
                userProfileImg = value;
                OnPropertyChanged(nameof(UserProfileImg));
            }
        }
        

        private MediaFile? _mediaFile;

        public ICommand UploadImage { get; } 

        public ProfileChangeImage()
        {
            userProfileImg = "userprofile";
            UploadImage = new Command(UploadFromGalery);
            GetMe();

        }

        async void GetMe()
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

                    Username = response.Data.user.Username;
                    downloadLink = response.Data.user.PhotoURL;
                    if (downloadLink != String.Empty)
                    {
                        UserProfileImg = ImageSource.FromUri(new Uri(downloadLink));
                    }
                    else
                    {
                        UserProfileImg = "userprofile";
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

        async void UploadFromGalery()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Info", "Tidak dapat mengambil foto dari galeri", "OK");
                return;
            }

            _mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { });

            if (_mediaFile == null)
            {
                return;
            }

            UserProfileImg = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });

            // send image to server via API
            sendImage();
        }

        async void sendImage()
        {
            var task = new FirebaseStorage("betatv-7ad1d.appspot.com", new FirebaseStorageOptions { ThrowOnCancel = true })
                .Child("UserProfilePicture").Child(Username).PutAsync(_mediaFile?.GetStream());

            downloadLink = await task;

            // send image URL to backend
            try
            {
                var client = HttpClientGetter.GetHttpClientWithTokenHeader();
                if (client == null)
                {
                    return;
                }

                var postData = new List<KeyValuePair<string, string>>();
                var content1 = new StringContent(JsonConvert.SerializeObject(new { Username = Username, PhotoURL = downloadLink }), Encoding.UTF8, "application/json");

                var content = new FormUrlEncodedContent(postData);
                string weburl = Constants.CHANGE_PROFPIC_END_POINT;

                var response = await client.PostAsync(weburl, content1);
                string responseBody = await response.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<ResponseDto<DataUser>>(responseBody);
                if (responseDto != null && responseDto.Success)
                {
                    //await Application.Current.MainPage.DisplayAlert("Ganti data berhasil", "Selamat, data Anda berhasil diupdate", "Ok");
                    DependencyService.Get<IMessage>().ShortAlert("Ganti data berhasil");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Ganti data gagal", responseDto.Message, "Ok");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Ups", "Data Anda gagal diupdate. Pastikan Anda terhubung ke internet", "Ok");
            }
        }
    }
}

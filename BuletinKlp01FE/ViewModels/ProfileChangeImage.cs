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

namespace BuletinKlp01FE.ViewModels
{
    class ProfileChangeImage : BindableObject
    {
        ImageSource userProfileImg;

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
            try
            {
                var client = HttpClientGetter.GetHttpClientWithTokenHeader();
                if (client == null)
                {
                    return;
                }

                var content = new MultipartFormDataContent();

                content.Add(new StreamContent(_mediaFile.GetStream()),
                    "\"file\"",
                    $"\"{_mediaFile.Path}\"");

                string weburl = Constants.CHANGE_PROFILE_IMAGE_END_POINT;

                var response = await client.PostAsync(weburl, content);
                string responseBody = await response.Content.ReadAsStringAsync();

                //var responseDto = JsonConvert.DeserializeObject<ResponseDto<String>>(responseBody);
                //await Application.Current.MainPage.DisplayAlert("Upl;kbjkfyus", responseBody, "Ok");
                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("yes", responseBody, "Ok");
                    //await Application.Current.MainPage.DisplayAlert("Ganti data berhasil", "Selamat, data Anda berhasil diupdate", "Ok");
                    DependencyService.Get<IMessage>().ShortAlert("Ganti foto profil berhasil");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Ganti data gagal", "argh", "Ok");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Ups", ex.Message, "Ok");
            }
        }

    }
}

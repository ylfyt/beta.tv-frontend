using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Dtos.user;
using BuletinKlp01FE.Services;
using BuletinKlp01FE.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BuletinKlp01FE.ViewModels
{
    class ProfileChangePassViewModel : BindableObject
    {
        string oldUsername = "";

        string oldPass = "";
        public string OldPass
        {
            get => oldPass;
            set
            {
                if (value == oldPass)
                {
                    return;
                }
                oldPass = value;
                OnPropertyChanged(nameof(OldPass));
            }
        }

        string newPass = "";
        public string NewPass
        {
            get => newPass;
            set
            {
                if (value == newPass)
                {
                    return;
                }
                newPass = value;
                OnPropertyChanged(nameof(NewPass));
            }
        }

        string confirmPass = "";
        public string ConfirmPass
        {
            get => confirmPass;
            set
            {
                if (value == confirmPass)
                {
                    return;
                }
                confirmPass = value;
                OnPropertyChanged(nameof(ConfirmPass));
            }
        }

        string buttonTxt = "Simpan Perubahan";
        public string ButtonTxt
        {
            get => buttonTxt;
            set
            {
                if (value == buttonTxt)
                {
                    return;
                }
                buttonTxt = value;
                OnPropertyChanged(nameof(ButtonTxt));
            }
        }

        public ProfileChangePassViewModel()
        {
            getMEdata();
            SaveChanges = new Command(Save);
        }

        async void getMEdata()
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

                    oldUsername = response.Data.user.Username;
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

        public ICommand SaveChanges { get; }

        async void Save()
        {
            ButtonTxt = "Harap tunggu";

            // check if NewPass != ConfirmPass
            if (NewPass != ConfirmPass)
            {
                await Application.Current.MainPage.DisplayAlert("Input salah", "Password baru dan konfirmasinya belum sama", "Ok");
                return;
            }

            try
            {
                var client = HttpClientGetter.GetHttpClientWithTokenHeader();
                if (client == null)
                {
                    return;
                }

                var postData = new List<KeyValuePair<string, string>>();
                var content1 = new StringContent(JsonConvert.SerializeObject(new { Username = oldUsername, OldPassword = OldPass, NewPassword = NewPass }), Encoding.UTF8, "application/json");

                var content = new FormUrlEncodedContent(postData);
                string weburl = Constants.CHANGE_PASS_END_POINT;

                var response = await client.PostAsync(weburl, content1);
                string responseBody = await response.Content.ReadAsStringAsync();
                var responseDto = JsonConvert.DeserializeObject<ResponseDto<DataUser>>(responseBody);
                if (responseDto != null && responseDto.Success)
                {
                    //await Application.Current.MainPage.DisplayAlert("Ganti data berhasil", "Selamat, data Anda berhasil diupdate", "Ok");
                    DependencyService.Get<IMessage>().ShortAlert("Ganti password berhasil");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Ganti data gagal", responseDto.Message, "Ok");
                }
                buttonTxt = "Simpan Perubahan";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Ups", "Data Anda gagal diupdate. Pastikan Anda terhubung ke internet", "Ok");
            }
            buttonTxt = "Simpan Perubahan";

        }
    }
}

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
    class ProfileChangePasswordViewModel : BindableObject
    {
        string oldPassDisplay;
        public string OldPassDisplay
        {
            get => OldPassDisplay;
            set
            {
                if (value == oldPassDisplay)
                {
                    return;
                }
                oldPassDisplay = value;
                OnPropertyChanged(nameof(OldPassDisplay));
            }
        }

        string newPassDisplay;
        public string NewPassDisplay
        {
            get => newPassDisplay;
            set
            {
                if (value == newPassDisplay)
                {
                    return;
                }
                newPassDisplay = value;
                OnPropertyChanged(nameof(NewPassDisplay));
            }
        }

        string newPassConfirm;
        public string NewPassConfirm
        {
            get => newPassConfirm;
            set
            {
                if (value == newPassConfirm)
                {
                    return;
                }
                newPassConfirm = value;
                OnPropertyChanged(nameof(NewPassConfirm));
            }
        }

        public ProfileChangePasswordViewModel()
        {
            SaveEditPassword = new Command(Save);
        }

        public ICommand SaveEditPassword { get; }

        async void Save()
        {
            // check if NewPassConfirm = NewPassDisplay
            if (NewPassConfirm != NewPassDisplay)
            {
                await Application.Current.MainPage.DisplayAlert("Input salah", "Masukan password yang baru berbeda dengan masukan kedua", "Ok");
                return;
            }

            // post changes
            try
            {
                var client = new System.Net.Http.HttpClient();
                var postData = new List<KeyValuePair<string, string>>();
                var content1 = new StringContent(JsonConvert.SerializeObject(new { Oldpass = OldPassDisplay, Newpass = NewPassDisplay}), Encoding.UTF8, "application/json");

                var content = new FormUrlEncodedContent(postData);
                string weburl = Constants.CHANGE_PROFILE_DATA_END_POINT;
                client.BaseAddress = new Uri(weburl);

                var response = await client.PostAsync("", content1);
                string responseBody = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Ganti data berhasil", "Selamat, data Anda berhasil diupdate", "Ok");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Ganti data gagal", "Data Anda gagal diupdate. Tunggu beberapa saat dan coba lagi", "Ok");
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

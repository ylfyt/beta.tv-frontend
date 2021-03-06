using BuletinKlp01FE.Dtos;
using BuletinKlp01FE.Models;
using BuletinKlp01FE.Services;
using BuletinKlp01FE.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using BuletinKlp01FE.Dtos.user;

namespace BuletinKlp01FE.ViewModels
{
    class ProfileChangeDataViewModel : BindableObject
    {
        string oldUsername;
        public string OldUsername
        {
            get => oldUsername;
            set
            {
                if (value == oldUsername)
                {
                    return;
                }
                oldUsername = value;
                OnPropertyChanged(nameof(OldUsername));
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
        string nameDisplay = "name";
        public string NameDisplay
        {
            get => nameDisplay;
            set
            {
                if (value == nameDisplay)
                {
                    return;
                }
                nameDisplay = value;
                OnPropertyChanged(nameof(NameDisplay));
            }
        }
        string emailDisplay = "email";
        public string EmailDisplay
        {
            get => emailDisplay;
            set
            {
                if (value == emailDisplay)
                {
                    return;
                }
                emailDisplay = value;
                OnPropertyChanged(nameof(EmailDisplay));
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

        int inputIsValid = 0;
        public int InputIsValid
        {
            get => inputIsValid;
            set
            {
                if (value == inputIsValid)
                {
                    return;
                }
                inputIsValid = value;
                OnPropertyChanged(nameof(InputIsValid));
            }
        }

        public ProfileChangeDataViewModel()
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

                    NameDisplay = response.Data.user.Name;
                    UsernameDisplay = response.Data.user.Username;
                    EmailDisplay = response.Data.user.Email;
                    OldUsername = response.Data.user.Username;
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

            // check whether the input is true/not
            if (string.IsNullOrEmpty(NameDisplay) || NameDisplay.Any(char.IsDigit))
            {
                await Application.Current.MainPage.DisplayAlert("Input salah", "Masukan nama yang baru belum benar", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(UsernameDisplay))
            {
                await Application.Current.MainPage.DisplayAlert("Input salah", "Masukan username yang baru belum benar", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(EmailDisplay) || !IsValidEmail(EmailDisplay))
            {
                await Application.Current.MainPage.DisplayAlert("Input salah", "Masukan email yang baru belum benar", "Ok");
                return;
            }

            // confirm with password
            string password = await Application.Current.MainPage.DisplayPromptAsync("Konfirmasi", "Masukkan password Anda");

            try
            {
                var client = HttpClientGetter.GetHttpClientWithTokenHeader();
                if (client == null)
                {
                    return;
                }

                var postData = new List<KeyValuePair<string, string>>();
                var content1 = new StringContent(JsonConvert.SerializeObject(new { OldUsername = OldUsername, Name = NameDisplay, Username = UsernameDisplay, Email = EmailDisplay, Password = password }), Encoding.UTF8, "application/json");

                var content = new FormUrlEncodedContent(postData);
                string weburl = Constants.CHANGE_PROFILE_DATA_END_POINT;

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
                ButtonTxt = "Simpan Perubahan";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Application.Current.MainPage.DisplayAlert("Ups", "Data Anda gagal diupdate. Pastikan Anda terhubung ke internet", "Ok");
                ButtonTxt = "Simpan Perubahan";
            }

        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}

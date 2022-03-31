using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuletinKlp01FE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Homepage : ContentPage
    {
        String color0 = "#3F72AF";
        String color1 = "#112D4E";
        String color5 = "#DBE2EF";

        public Homepage()
        {
            InitializeComponent();
        }

        public void AllVideosClicked(object sender, EventArgs args)
        {
            var btnall = this.FindByName<Button>("all");
            btnall.BackgroundColor = Color.FromHex(color0);
            btnall.TextColor = Color.FromHex("#ffffff");

            var btntech = this.FindByName<Button>("tech");
            btntech.BackgroundColor = Color.FromHex(color5);
            btntech.TextColor = Color.FromHex(color1);

            var btndesain = this.FindByName<Button>("desain");
            btndesain.BackgroundColor = Color.FromHex(color5);
            btndesain.TextColor = Color.FromHex(color1);

            var btnbisnis = this.FindByName<Button>("bisnis");
            btnbisnis.BackgroundColor = Color.FromHex(color5);
            btnbisnis.TextColor = Color.FromHex(color1);
        }

        public void TechVideosClicked(object sender, EventArgs args)
        {
            var btnall = this.FindByName<Button>("all");
            btnall.BackgroundColor = Color.FromHex(color5);
            btnall.TextColor = Color.FromHex(color1);

            var btntech = this.FindByName<Button>("tech");
            btntech.BackgroundColor = Color.FromHex(color0);
            btntech.TextColor = Color.FromHex("#ffffff");

            var btndesain = this.FindByName<Button>("desain");
            btndesain.BackgroundColor = Color.FromHex(color5);
            btndesain.TextColor = Color.FromHex(color1);

            var btnbisnis = this.FindByName<Button>("bisnis");
            btnbisnis.BackgroundColor = Color.FromHex(color5);
            btnbisnis.TextColor = Color.FromHex(color1);
        }

        public void DesainVideosClicked(object sender, EventArgs args)
        {
            var btnall = this.FindByName<Button>("all");
            btnall.BackgroundColor = Color.FromHex(color5);
            btnall.TextColor = Color.FromHex(color1);

            var btntech = this.FindByName<Button>("tech");
            btntech.BackgroundColor = Color.FromHex(color5);
            btntech.TextColor = Color.FromHex(color1);

            var btndesain = this.FindByName<Button>("desain");
            btndesain.BackgroundColor = Color.FromHex(color0);
            btndesain.TextColor = Color.FromHex("#ffffff");

            var btnbisnis = this.FindByName<Button>("bisnis");
            btnbisnis.BackgroundColor = Color.FromHex(color5);
            btnbisnis.TextColor = Color.FromHex(color1);
        }

        public void BisnisVideosClicked(object sender, EventArgs args)
        {
            var btnall = this.FindByName<Button>("all");
            btnall.BackgroundColor = Color.FromHex(color5);
            btnall.TextColor = Color.FromHex(color1);

            var btntech = this.FindByName<Button>("tech");
            btntech.BackgroundColor = Color.FromHex(color5);
            btntech.TextColor = Color.FromHex(color1);

            var btndesain = this.FindByName<Button>("desain");
            btndesain.BackgroundColor = Color.FromHex(color5);
            btndesain.TextColor = Color.FromHex(color1);

            var btnbisnis = this.FindByName<Button>("bisnis");
            btnbisnis.BackgroundColor = Color.FromHex(color0);
            btnbisnis.TextColor = Color.FromHex("#ffffff");
        }

        
    }
}
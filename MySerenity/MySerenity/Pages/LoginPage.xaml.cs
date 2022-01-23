using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySerenity.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using NavigationPage = Xamarin.Forms.NavigationPage;
using MySerenity.Helpers;

namespace MySerenity.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            this.SetIPhoneSafeArea();
            this.On<iOS>().SetUseSafeArea(true);
        }

        private async void Login_clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EmailEntry.Text) && string.IsNullOrEmpty(PasswordEntry.Text))
            {
                // do not navigate
            }
            else
            {
                bool result = await Auth.LoginUser(EmailEntry.Text, PasswordEntry.Text);
                // navigate
                if (result) await Navigation.PushAsync(new HomePage());
            }
        }

        private void Create_account(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AccountCreation());
        }

        private void Forgotten_Password(object sender, EventArgs e)
        {

        }
    }
}
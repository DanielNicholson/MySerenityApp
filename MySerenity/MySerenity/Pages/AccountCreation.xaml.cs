using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySerenity.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using NavigationPage = Xamarin.Forms.NavigationPage;

namespace MySerenity.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountCreation : ContentPage
    {
        private bool isUserClient = true;
        public AccountCreation()
        {
            NavigationPage.SetHasNavigationBar(this, false);



            InitializeComponent();
            this.On<iOS>().SetUseSafeArea(true);
        }

        private async void Create_Account(object sender, EventArgs e)
        {
            bool result = await Auth.RegisterUser(EmailInput.Text, PasswordInput.Text);
            if (result)
            {
                await Navigation.PushAsync(new HomePage());
            }
        }

        public static bool IsStrongPassword(string password)
        {
            return false;
        }

        private void return_to_login(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        private void RoleSelectionSwitchToggled(object sender, ToggledEventArgs e)
        {
            if (RoleSelectionSwitch.IsToggled == true)
            {
                TherapistRoleLabel.FontAttributes = FontAttributes.Bold;
                ClientRoleLabel.FontAttributes = FontAttributes.None;
                isUserClient = false;
            }
            else
            {
                TherapistRoleLabel.FontAttributes = FontAttributes.None;
                ClientRoleLabel.FontAttributes = FontAttributes.Bold;
                isUserClient = true;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            this.On<iOS>().SetUseSafeArea(true); // uses full screen on iOS
        }

        // when the user tries to login, get the email and password from the text boxes and make request to firebase to authenticate.
        private async void Login_clicked(object sender, EventArgs e)
        {
            // check to see if email or password is empty 
            if (string.IsNullOrEmpty(EmailEntry.Text) && string.IsNullOrEmpty(PasswordEntry.Text))
            {
                // do not navigate
            }
            else // if both have been filled out, try to authenticate and login.
            {
                bool result = await Auth.LoginUser(EmailEntry.Text, PasswordEntry.Text);
                
                // if user has been authenticated, allow user to login. Firebase stores userID if they are authenticated to use against all actions done in app.
                if (result && Auth.IsUserAuthenticated())
                {
                    // get user role
                    string userRole = await Firestore.GetUserRole();

                    if (userRole == "Client")
                    {
                        await Navigation.PushAsync(new HomePage());
                    }
                    else if (userRole == "Therapist")
                    {
                        await Navigation.PushAsync(new TherapistDashboard());
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Error occurred - please contact support.", "Ok");
                    }
                }
            }
        }

        // navigate to create account screen.
        private void Create_account(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AccountCreation());
        }

        // navigate to forgotten password screen.
        private void Forgotten_Password(object sender, EventArgs e)
        {

        }
    }
}
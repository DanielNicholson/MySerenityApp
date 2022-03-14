using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySerenity.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySerenity.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgottenPasswordPage : ContentPage
    {
        public ForgottenPasswordPage()
        {
            InitializeComponent();
        }

        private void ReturnToHome_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        private void ResetPasswordButton_OnClicked(object sender, EventArgs e)
        {
            var result = Auth.ResetPassword(EmailEntry.Text);

            DisplayAlert("Password Reset", result ? "Password Reset Link Successfully sent, please check your email." : "Password Reset Failed - please contact support", "Ok");
        }
    }
}
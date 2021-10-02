using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



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
        public AccountCreation()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            this.On<iOS>().SetUseSafeArea(true);
        }

        private void Create_Account(object sender, EventArgs e)
        {
           
        }

        public static bool IsStrongPassword(string password)
        {
            return false;
        }

        private void return_to_login(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}
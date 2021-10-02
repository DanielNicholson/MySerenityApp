using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySerenity
{
    public partial class App : Application
    {
        public static string DatabasePath;
        public App()
        {
            var colorTypeConverter = new ColorTypeConverter();
            

            InitializeComponent();

            MainPage = new NavigationPage(new Pages.LoginPage())
            {
                BarBackgroundColor = (Xamarin.Forms.Color) colorTypeConverter.ConvertFromInvariantString("#85aed0"),
                BarTextColor = (Xamarin.Forms.Color)colorTypeConverter.ConvertFromInvariantString("#c4dbe0"),
            };
        }

        public App(string databasePath)
        {
            var colorTypeConverter = new ColorTypeConverter();

            InitializeComponent();

            DatabasePath = databasePath;

            MainPage = new NavigationPage(new Pages.LoginPage())
            {
                BarBackgroundColor = (Xamarin.Forms.Color)colorTypeConverter.ConvertFromInvariantString("#85aed0"),
                BarTextColor = (Xamarin.Forms.Color)colorTypeConverter.ConvertFromInvariantString("#c4dbe0"),
            };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

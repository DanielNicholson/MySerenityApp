using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Firebase.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Lemonada-Regular.ttf", Alias = "MyCustomFont")]

namespace MySerenity
{
    public partial class App : Application
    {
        public static string DatabasePath;
        public static FirebaseClient realTimeClient;
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
            string firebaseClient = "https://myserenity-982fc-default-rtdb.europe-west1.firebasedatabase.app/";
            string firebaseSecret = "VP4wqxqBfiaUWAtzAWdrqlYw0tAQ7kuciOJ40jvT";

            realTimeClient = new FirebaseClient(firebaseClient, 
                                                new FirebaseOptions {AuthTokenAsyncFactory = () => Task.FromResult(firebaseSecret)});

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

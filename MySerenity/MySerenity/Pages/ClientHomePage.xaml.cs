using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySerenity.Helpers;
using MySerenity.Model;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace MySerenity.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage
    {
        public static TherapistInfo Info;
        public HomePage(TherapistInfo info)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var colorTypeConverter = new ColorTypeConverter();
            BarBackgroundColor = (Xamarin.Forms.Color)colorTypeConverter.ConvertFromInvariantString("#85aed0");
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            InitializeComponent();

        }

        public HomePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var colorTypeConverter = new ColorTypeConverter();
            BarBackgroundColor = (Xamarin.Forms.Color)colorTypeConverter.ConvertFromInvariantString("#85aed0");
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            InitializeComponent();

            var pages = Children.GetEnumerator();
            pages.MoveNext();
            pages.MoveNext();
            pages.MoveNext();
            CurrentPage = pages.Current;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
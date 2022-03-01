using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using MySerenity.Extensions;
using MySerenity.Helpers;
using MySerenity.Model;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MySerenity.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        private List<ChartEntry> entries;

        public DashboardPage()
        {
            InitializeComponent();
            this.On<iOS>().SetUseSafeArea(true);
            entries = new List<ChartEntry>();

            BindingContext = this;
            WelcomeStringProp = "Welcome to MySerenity";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            entries = await GetMoodChart();

            var chart = new LineChart()
            {
                Entries = entries,
                LineMode = LineMode.Straight,
                LineSize = 8,
                PointMode = PointMode.Square,
                PointSize = 18,
            };

            chartView.Chart = chart;
        }

        private string welcomeStringProp;
        public string WelcomeStringProp
        {
            get { return welcomeStringProp; }
            set
            {
                welcomeStringProp = value;
                OnPropertyChanged(nameof(WelcomeStringProp));
            }
        }

        public async Task<List<ChartEntry>> GetMoodChart()
        {
            return await Firestore.RetrieveMoodData();
        }
    }
}
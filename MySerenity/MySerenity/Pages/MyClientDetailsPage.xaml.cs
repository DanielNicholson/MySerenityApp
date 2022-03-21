using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using MySerenity.Helpers;
using MySerenity.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySerenity.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyClientDetailsPage : ContentPage
    {
        private Clientquestionnaire _clientDetails;

        private List<ChartEntry> entries;

        public MyClientDetailsPage()
        {
            InitializeComponent();
            _clientDetails = new Clientquestionnaire();
        }

        public MyClientDetailsPage(Clientquestionnaire info)
        {
            InitializeComponent();
            ClientNameEntry.Text = info.ClientName;
            ClientGenderEntry.Text = info.Gender;
            ClientAgeEntry.Text = info.Age.ToString();
            ClientTherapyReasonsEntry.Text = info.ReasonsForTherapy;

            _clientDetails = info;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            entries = await GetMoodChartDataPoints();

            var chart = new LineChart()
            {
                Entries = entries,
                LineMode = LineMode.Straight,
                LineSize = 8,
                PointMode = PointMode.Square,
                PointSize = 18,
                LabelTextSize = 30f,
                LabelOrientation = Orientation.Horizontal
            };

            chartView.Chart = chart;
        }

        public async Task<List<ChartEntry>> GetMoodChartDataPoints()
        {
            return await Firestore.RetrieveMoodData(_clientDetails.UserId);
        }


        private async void PrivateMessage_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PrivateMessagePage(_clientDetails));
        }

        
        public async Task OpenBrowser()
        {
            try
            {
                await Browser.OpenAsync("https://myserenityvideocalling.z33.web.core.windows.net", BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                // An unexpected error occured. No browser may be installed on the device.
            }
        }

        private async void Call_OnClicked(object sender, EventArgs e)
        {
            await OpenBrowser();
        }

        private async void UnmatchClient_OnClicked(object sender, EventArgs e)
        {
            bool ans = await DisplayAlert("Are you sure you want to unmatch from:", _clientDetails.ClientName, "Yes", "No");

            if (ans)
            {
                var result = await Firestore.UnmatchTherapistFromClient(_clientDetails);

                if (result)
                {
                    await DisplayAlert("Unmatched successfully", $"You have successfully unmatched from {_clientDetails.ClientName}", "Ok");
                    await Navigation.PopAsync();
                }
            }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySerenity.Helpers;
using MySerenity.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySerenity.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyTherapistInfopage : ContentPage
    {
        public static TherapistInfo info;
        public MyTherapistInfopage()
        {
            InitializeComponent();
            info = null;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            info = await Firestore.GetTherapistForClient();

            if (info == null)
            {
                TherapistInfoFrame.IsVisible = false;
                TherapistInfoScrollView.IsVisible = false;

                NoTherapistFrame.IsVisible = true;
            }
            else
            {
                TherapistInfoFrame.IsVisible = true;
                TherapistInfoScrollView.IsVisible = true;

                NoTherapistFrame.IsVisible = false;

                TherapistNameEntry.Text = info.Name;
                MembershipEntry.Text = info.Membership;
            }
        }

        private async void PrivateMessage_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PrivateMessagePage(info));
        }

        private async void CallButton_OnClicked(object sender, EventArgs e)
        {
            await OpenBrowser();
            //await Navigation.PushAsync(new VideoCallingPage());
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySerenity.Helpers;
using MySerenity.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySerenity.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TherapistProfilePage : ContentPage
    {
        private TherapistInfo info;
        private TherapistWorkingDays schedule;
        public TherapistProfilePage()
        {
            InitializeComponent();

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            info = await Firestore.GetTherapistInfo();
            schedule = await Firestore.GetTherapistSchedule(info.UserId);


            // displays the therapist info on the UI
            TherapistNameEntry.Text = info.Name;
            TherapistMembershipEntry.Text = info.Membership;
            TherapistDescriptionEntry.Text = info.Description;

            if (schedule != null)
            {
                // displays the working days on the UI
                MondayBox.IsChecked = schedule.Monday;
                TuesdayBox.IsChecked = schedule.Tuesday;
                WednesdayBox.IsChecked = schedule.Wednesday;
                ThursdayBox.IsChecked = schedule.Thursday;
                FridayBox.IsChecked = schedule.Friday;
                SaturdayBox.IsChecked = schedule.Saturday;
                SundayBox.IsChecked = schedule.Sunday;
            }
        }

        private void EditProfile_OnCLicked(object sender, EventArgs e)
        {
            // change which button is displayed to the user (save or edit)
            EditProfileFrame.IsVisible = !EditProfileFrame.IsVisible;
            SaveProfileFrame.IsVisible = !SaveProfileFrame.IsVisible;
            
            // changes whether the input fields are editable or not 
            TherapistNameEntry.IsReadOnly        = !TherapistNameEntry.IsReadOnly;
            TherapistDescriptionEntry.IsReadOnly = !TherapistDescriptionEntry.IsReadOnly;

            // changes the background colour of the input boxes to show the user can edit them or not.
            TherapistNameEntry.BackgroundColor = Color.White;
            TherapistDescriptionEntry.BackgroundColor = Color.White;

            MondayBox.IsEnabled = !MondayBox.IsEnabled;
            TuesdayBox.IsEnabled = !TuesdayBox.IsEnabled;
            WednesdayBox.IsEnabled = !WednesdayBox.IsEnabled;
            ThursdayBox.IsEnabled = !ThursdayBox.IsEnabled;
            FridayBox.IsEnabled = !FridayBox.IsEnabled;
            SaturdayBox.IsEnabled = !SaturdayBox.IsEnabled;
            SundayBox.IsEnabled = !SundayBox.IsEnabled;

            OnAppearing();
        }

        private async void SaveProfile_OnCLicked(object sender, EventArgs e)
        {
            // change which button is displayed to the user (save or edit)
            EditProfileFrame.IsVisible = !EditProfileFrame.IsVisible;
            SaveProfileFrame.IsVisible = !SaveProfileFrame.IsVisible;

            // changes whether the input fields are editable or not 
            TherapistNameEntry.IsReadOnly = !TherapistNameEntry.IsReadOnly;
            ///TherapistWorkingDaysEntry.IsReadOnly = !TherapistWorkingDaysEntry.IsReadOnly;
            TherapistDescriptionEntry.IsReadOnly = !TherapistDescriptionEntry.IsReadOnly;

            // changes the background colour of the input boxes to show the user can edit them or not.
            TherapistNameEntry.BackgroundColor = Color.LightSkyBlue;
            //TherapistWorkingDaysEntry.BackgroundColor = Color.LightSkyBlue;
            TherapistDescriptionEntry.BackgroundColor = Color.LightSkyBlue;

            info.Description = TherapistDescriptionEntry.Text;
            info.Name = TherapistNameEntry.Text;

            TherapistWorkingDays schedule = new TherapistWorkingDays()
            {
                UserId = info.UserId,
                Monday = MondayBox.IsChecked,
                Tuesday = TuesdayBox.IsChecked,
                Wednesday = WednesdayBox.IsChecked,
                Thursday = ThursdayBox.IsChecked,
                Friday = FridayBox.IsChecked,
                Saturday = SaturdayBox.IsChecked,
                Sunday = SundayBox.IsChecked,
            };

            var result = await Firestore.UpdateTherapistInfo(info);
            var sceduleResult = await Firestore.SaveTherapistSchedule(schedule);

            await DisplayAlert("Profile Saved", result ? "Saved Successfully" : "Saved Failed", "Ok");

            OnAppearing();
        }
    }
}
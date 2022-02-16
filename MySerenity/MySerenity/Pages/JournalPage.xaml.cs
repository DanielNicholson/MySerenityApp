using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySerenity.Extensions;
using MySerenity.Helpers;
using MySerenity.Model;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MySerenity.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JournalPage : ContentPage
    {
        public JournalPage()
        {
            InitializeComponent();
            this.On<iOS>().SetUseSafeArea(true); // uses whole screen on iOS
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            // When the journal entry view appears, refresh the list from firestore
            var journalEntries = await Firestore.ReadAllJournalEntriesForUser();
            journalEntries.Sort((y, x) => DateTime.Compare(DateTime.Parse(x.JournalEntryEntryTime), DateTime.Parse(y.JournalEntryEntryTime)));
            JournalListView.ItemsSource = null;                 // clear the list to ensure latest version is shown.
            JournalListView.ItemsSource = journalEntries; // update list.
        }

        // Brings up new journal page/
        private void AddJournalEntry_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddJournalPage() );
        }

        // when the user selects an old journal entry, loads up the journal so they can view the details.
        private void Handle_Item_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            // takes the journal entry the user has selected from the list view.
            var journalEntry = JournalListView.SelectedItem as JournalEntry;

            // checks to see if they journal is empty and loads the page if not.
            if (journalEntry != null)
            {
                Navigation.PushAsync(new AddJournalPage(journalEntry));
            }
        }

        // when the user holds a journal entry in the list it lets them delete the entry.
        private async void JournalItemDelete_OnClicked(object sender, EventArgs e)
        {
            // gets the journal entry the user has selected.
            var journal = ((MenuItem)sender).BindingContext as JournalEntry;

            // asks the user to confirm if they want to delete the entry
            bool ans = await DisplayAlert("Are you sure you want this Journal deleted?", journal.JournalEntryTitle, "Yes", "No");


            if (ans)
            {
                // sends the delete request to firestore
                await Firestore.Delete(journal);
                JournalListView.ItemsSource = null;   // resets the list of entries
                JournalListView.ItemsSource = await Firestore.ReadAllJournalEntriesForUser(); // updates the list of entries with the latest from firestore
            }
        }
    }
}
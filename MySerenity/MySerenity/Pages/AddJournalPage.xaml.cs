using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySerenity.Extensions;
using MySerenity.Model;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MySerenity.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddJournalPage : ContentPage
    {
        public AddJournalPage()
        {
            InitializeComponent();
            this.On<iOS>().SetUseSafeArea(true);
        }

        // used to load the journal entry page when selecting a journal entry from the list to view details.
        public AddJournalPage(JournalEntry entry)
        {
            InitializeComponent();
            journalTitle.Text = entry.JournalEntryTitle;
            journalText.Text = entry.JournalEntryText;
            picker.SelectedIndex = entry.JournalEntryMoodData;
            journalTitle.IsReadOnly = true;
            journalText.IsReadOnly = true;
            picker.IsEnabled = false;
            Submit.IsVisible = false;

            ToolBarTitle.Text = entry.JournalEntryTitle;
        }

        private void SubmitJournal_clicked(object sender, EventArgs e)
        {
            var newEntry = new JournalEntry()
            {
                JournalEntryTitle = journalTitle.Text,
                JournalEntryText = journalText.Text,
                JournalEntryEntryTime = System.DateTime.Now,
                JournalEntryMoodData = picker.SelectedIndex
            };

            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<JournalEntry>();

                int row = connection.Insert(newEntry);

                DisplayAlert("Entry Saved", "Saved Successfully", "Ok");
                Navigation.PopAsync();
            }
        }
    }
}
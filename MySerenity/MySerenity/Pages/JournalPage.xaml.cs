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
    public partial class JournalPage : ContentPage
    {
        public JournalPage()
        {
            InitializeComponent();
            this.On<iOS>().SetUseSafeArea(true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<JournalEntry>();

                var experiencesList = connection.Table<JournalEntry>().ToList();

                experiencesList.Reverse();

                JournalListView.ItemsSource = experiencesList;
            }
        }

        private void AddJournalEntry_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddJournalPage() );
        }

        private void Handle_Item_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedExperience = JournalListView.SelectedItem as JournalEntry;

            if (selectedExperience != null)
            {
                Navigation.PushAsync(new AddJournalPage(selectedExperience));
            }
        }

        private async void JournalItemDelete_OnClicked(object sender, EventArgs e)
        {
            using (var connection = new SQLiteConnection(App.DatabasePath))
            {
                connection.CreateTable<JournalEntry>();

                var journal = ((MenuItem)sender).BindingContext as JournalEntry;
                bool ans = await DisplayAlert("Are you sure you want this Journal deleted?", journal.JournalEntryTitle, "Yes", "No");


                if (ans)
                {
                    int row = connection.Delete(journal);


                    if (row > 0)
                    {
                        var experiencesList = connection.Table<JournalEntry>().ToList();
                        JournalListView.ItemsSource = experiencesList;
                    }
                }
                
            }
        }
    }
}
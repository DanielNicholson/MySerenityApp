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
    public partial class AvailableClientsPage : ContentPage
    {
        public AvailableClientsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var availableClients = await Firestore.ReadAllAvailableClients();

            AvailableClientListView.ItemsSource = null;
            AvailableClientListView.ItemsSource = availableClients;

        }

        private void Handle_Item_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            if (AvailableClientListView.SelectedItem is Clientquestionnaire newClient)
            {
                Navigation.PushAsync(new NewClientInfoPage(newClient));
            }
        }
    }
}
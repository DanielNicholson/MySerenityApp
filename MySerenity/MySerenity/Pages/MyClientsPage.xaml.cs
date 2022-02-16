using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySerenity.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firestore = MySerenity.Helpers.Firestore;

namespace MySerenity.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyClientsPage : ContentPage
    {
        public MyClientsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var availableClients = await Firestore.ReadAllClientsForTherapist();
            MyClientListView.ItemsSource = null;

            if (availableClients.Count != 0)
            {
                MyClientListView.ItemsSource = availableClients;
            }
        }

        private void Handle_Item_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
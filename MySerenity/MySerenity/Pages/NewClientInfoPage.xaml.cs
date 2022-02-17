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
    public partial class NewClientInfoPage : ContentPage
    {
        private Clientquestionnaire clientDetails;

        public NewClientInfoPage()
        {
            InitializeComponent();
        }

        public NewClientInfoPage(Clientquestionnaire info)
        {
            InitializeComponent();
            ClientNameEntry.Text = info.ClientName;
            ClientGenderEntry.Text = info.Gender;
            ClientAgeEntry.Text = info.Age.ToString();
            ClientTherapyEntry.Text = info.PreviousTherapyExperience;
            ClientReasonsEntry.Text = info.ReasonsForTherapy;
            ClientInterestEntry.Text = info.LowInterestLevels;
            ClientEnergyEntry.Text = info.LowEnergyLevels;
            ClientDepressionEntry.Text = info.LowMoodLevels;
            ClientSuicideEntry.Text = info.SuicidalThoughts;
            ClientMedicationEntry.Text = info.CurrentMedication;
            ClientTherapistEntry.Text = info.TherapistPreferences;
            clientDetails = info;
        }

        private async void Pair_with_client(object sender, EventArgs e)
        {
            await Firestore.MatchTherapistWithClient(clientDetails);
            await Navigation.PopAsync();
        }
    }
}
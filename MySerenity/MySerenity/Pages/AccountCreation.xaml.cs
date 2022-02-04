using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySerenity.Helpers;
using MySerenity.Model;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using NavigationPage = Xamarin.Forms.NavigationPage;

namespace MySerenity.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountCreation : ContentPage
    {
        private List<int> ageList = new List<int>();

        private bool _isUserClient = true;
        public AccountCreation()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            for (int i = 16; i < 100; i++)
            {
                ageList.Add(i);
            }

            InitializeComponent();
            this.On<iOS>().SetUseSafeArea(true);

            AgePicker.ItemsSource = null;
            AgePicker.ItemsSource = ageList;
        }

        private async void Create_Account(object sender, EventArgs e)
        {
            bool result = await Auth.RegisterUser(EmailInput.Text, PasswordInput.Text);
            if (result)
            {
                Firestore.SaveUserRole(_isUserClient);

                // sign up logic if user selects to sign up as a client.
                if (_isUserClient)
                {
                    // build reasons for therapy string
                    string reasonsForTherapy = "";
                    if (AnxietyBox.IsChecked)
                    {
                        reasonsForTherapy += "Anxiety, ";
                    }
                    if (DepressionBox.IsChecked)
                    {
                        reasonsForTherapy += "Depression, ";
                    }
                    if (MoodBox.IsChecked)
                    {
                        reasonsForTherapy += "Mood swings, ";
                    }
                    if (RelationshipBox.IsChecked)
                    {
                        reasonsForTherapy += "Relationship issues, ";
                    }
                    if (GriefBox.IsChecked)
                    {
                        reasonsForTherapy += "Grief, ";
                    }
                    if (TraumaBox.IsChecked)
                    {
                        reasonsForTherapy += "Trauma, ";
                    }
                    if (OtherBox.IsChecked)
                    {
                        reasonsForTherapy += "Other, ";
                    }

                    Clientquestionnaire signup = new Clientquestionnaire()
                    {
                        Gender = GenderPicker.SelectedItem.ToString(),
                        Age = Int32.Parse(AgePicker.SelectedItem.ToString()),
                        PreviousTherapyExperience = TherapyExperiencePicker.SelectedItem.ToString(),
                        ReasonsForTherapy = reasonsForTherapy,
                        LowInterestLevels = InterestLevelPicker.SelectedItem.ToString(),
                        LowEnergyLevels = EnergyLevelPicker.SelectedItem.ToString(),
                        LowMoodLevels = DepressedLevelPicker.SelectedItem.ToString(),
                        SuicidalThoughts = SuicideLevelPicker.SelectedItem.ToString(),
                        CurrentMedication = MedicationPicker.SelectedItem.ToString(),
                        EmergencyContactName = NameEntry.Text,
                        EmergencyContactNumber = NumberEntry.Text,
                        IsApproved = false
                    };

                    Firestore.SaveSignUpQuestions(signup);
                }
                else
                {
                    
                }

                
                await Navigation.PushAsync(new HomePage());
            }
        }

        private void return_to_login(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        private void ClientRoleSwitchSwitched(object sender, ToggledEventArgs e)
        {
            if (ClientRoleSwitch.IsToggled == true)
            {
                TherapistRoleLabel.FontAttributes = FontAttributes.None;
                ClientRoleLabel.FontAttributes = FontAttributes.Bold;
                _isUserClient = true;
                TherapistRoleSwitch.IsToggled = false;


                ClientSignUpQuestions.IsVisible = true;
                TherapistSignUpQuestions.IsVisible = false;
            }
            else
            {
                TherapistRoleLabel.FontAttributes = FontAttributes.Bold;
                ClientRoleLabel.FontAttributes = FontAttributes.None;
                _isUserClient = false;
                TherapistRoleSwitch.IsToggled = true;


                ClientSignUpQuestions.IsVisible = false;
                TherapistSignUpQuestions.IsVisible = true;
            }
        }

        private void TherapistRoleSwitchSwitched(object sender, ToggledEventArgs e)
        {
            if (TherapistRoleSwitch.IsToggled == true)
            {
                TherapistRoleLabel.FontAttributes = FontAttributes.Bold;
                ClientRoleLabel.FontAttributes = FontAttributes.None;
                _isUserClient = false;
                ClientRoleSwitch.IsToggled = false;

                ClientSignUpQuestions.IsVisible = false;
                TherapistSignUpQuestions.IsVisible = true;
            }
            else
            {
                TherapistRoleLabel.FontAttributes = FontAttributes.None;
                ClientRoleLabel.FontAttributes = FontAttributes.Bold;
                _isUserClient = true;
                ClientRoleSwitch.IsToggled = true;

                ClientSignUpQuestions.IsVisible = true;
                TherapistSignUpQuestions.IsVisible = false;
            }
        }
    }
}
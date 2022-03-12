using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySerenity.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace MySerenity.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExercisePage : ContentPage
    {

        static int currentPrompt = 4;
        static int answersRemaining = currentPrompt + 1;

        public ExercisePage()
        {
            InitializeComponent();
            this.On<iOS>().SetUseSafeArea(true);

            currentPrompt = 4;
            answersRemaining = currentPrompt + 1;
            
            BindingContext = this;
            PromptLabelProp = prompts[currentPrompt];
            r = rnd.Next(8);
            ButtonOneProp = answers[r];
            r = rnd.Next(8);
            ButtonTwoProp = answers[r];
            r = rnd.Next(8);
            ButtonThreeProp = answers[r];
            r = rnd.Next(8);
            ButtonFourProp = answers[r];
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            currentPrompt = 4;
            answersRemaining = currentPrompt + 1;

            PromptLabelProp = prompts[currentPrompt];
            r = rnd.Next(8);
            ButtonOneProp = answers[r];
            r = rnd.Next(8);
            ButtonTwoProp = answers[r];
            r = rnd.Next(8);
            ButtonThreeProp = answers[r];
            r = rnd.Next(8);
            ButtonFourProp = answers[r];
        }

        List<string> prompts = new List<string>
        {
            $"What are 1 things you can taste?",
            $"What are 2 things you can smell?",
            $"What are 3 things you can hear?",
            $"What are 4 things you can feel?",
            $"What are 5 things you can see?",
        };

        List<string> answers = new List<string>
        {
            "Dog",
            "Cat",
            "Tree",
            "Bacon",
            "Burger",
            "House",
            "Car",
            "People",
            "Football",
            "Bike"
        };

        static Random rnd = new Random();
        int r = rnd.Next(8);
        // used to display name on welcome message
        private string promptLabelProp;
        public string PromptLabelProp
        {
            get { return promptLabelProp; }
            set
            {
                promptLabelProp = value;
                OnPropertyChanged(nameof(PromptLabelProp));
            }
        }

        private string buttonOneProp;
        public string ButtonOneProp
        {
            get { return buttonOneProp; }
            set
            {
                buttonOneProp = value;
                OnPropertyChanged(nameof(ButtonOneProp));
            }
        }

        private async void Handle_ClickedOne(object sender, EventArgs e)
        {
            answersRemaining--;
            if (answersRemaining == 0)
            {
                currentPrompt--;
                answersRemaining = currentPrompt + 1;
                if (currentPrompt >= 0)
                {
                    PromptLabelProp = prompts[currentPrompt];
                }
                else
                {
                    await DisplayAlert("54321 finished", "Remember to take some deep breaths.", "OK");
                }

            }
            r = rnd.Next(8);
            ButtonOneProp = answers[r];
        }

        private string buttonTwoProp;
        public string ButtonTwoProp
        {
            get { return buttonTwoProp; }
            set
            {
                buttonTwoProp = value;
                OnPropertyChanged(nameof(ButtonTwoProp));
            }
        }

        private async void Handle_ClickedTwo(object sender, EventArgs e)
        {
            answersRemaining--;
            if (answersRemaining == 0)
            {
                currentPrompt--;
                answersRemaining = currentPrompt + 1;
                if (currentPrompt >= 0)
                {
                    PromptLabelProp = prompts[currentPrompt];
                }
                else
                {
                    await DisplayAlert("54321 finished", "Remember to take some deep breaths.", "OK");
                }

            }
            r = rnd.Next(8);
            ButtonTwoProp = answers[r];
        }

        private string buttonThreeProp;
        public string ButtonThreeProp
        {
            get { return buttonThreeProp; }
            set
            {
                buttonThreeProp = value;
                OnPropertyChanged(nameof(ButtonThreeProp));
            }
        }

        private async void Handle_ClickedThree(object sender, EventArgs e)
        {
            answersRemaining--;
            if (answersRemaining == 0)
            {
                currentPrompt--;
                answersRemaining = currentPrompt + 1;
                if (currentPrompt >= 0)
                {
                    PromptLabelProp = prompts[currentPrompt];
                }
                else
                {
                    await DisplayAlert("54321 finished", "Remember to take some deep breaths.", "OK");
                }
            }

            r = rnd.Next(8);
            ButtonThreeProp = answers[r];
        }

        private string buttonFourProp;
        public string ButtonFourProp
        {
            get { return buttonFourProp; }
            set
            {
                buttonFourProp = value;
                OnPropertyChanged(nameof(ButtonFourProp));
            }
        }

        private async void Handle_ClickedFour(object sender, EventArgs e)
        {
            answersRemaining--;
            if (answersRemaining == 0)
            {
                currentPrompt--;
                answersRemaining = currentPrompt + 1;
                if (currentPrompt >= 0)
                {
                    PromptLabelProp = prompts[currentPrompt];
                }
                else
                {
                    await DisplayAlert("54321 finished", "Remember to take some deep breaths.", "OK");
                }
            }

            r = rnd.Next(8);
            ButtonFourProp = answers[r];
        }
    }
}
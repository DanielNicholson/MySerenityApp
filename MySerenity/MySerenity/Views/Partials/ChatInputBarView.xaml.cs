using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;
using MySerenity.Helpers;
using MySerenity.Model;
using MySerenity.Pages;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySerenity.Views.Partials
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatInputBarView : ContentView
    {
        public ChatInputBarView()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
            {
                this.SetBinding(HeightRequestProperty, new Binding("Height", BindingMode.OneWay, null, null, null, chatTextInput));
            }
        }

        public async void Handle_Completed(object sender, EventArgs e)
        {
            string userRole = await Firestore.GetUserRole();
            string therapistID = userRole == "Therapist" ? Auth.GetCurrentUserId() : PrivateMessagePage.recieverID;
            string clientID = userRole == "Client" ? Auth.GetCurrentUserId() : PrivateMessagePage.recieverID;
            string chatID = therapistID + clientID;

            var message = new Message()
            {
                SenderId = Auth.GetCurrentUserId(),
                ReceiverId = PrivateMessagePage.recieverID,
                MessageText = chatTextInput.Text,
                MessageSentTime = System.DateTime.Now.ToString(new CultureInfo("en-GB")),
            };

            await App.realTimeClient.Child("Message").Child(chatID).PostAsync(message);

            chatTextInput.Text = "";
        }

        public void UnFocusEntry()
        {
            chatTextInput?.Unfocus();
        }
    }
}
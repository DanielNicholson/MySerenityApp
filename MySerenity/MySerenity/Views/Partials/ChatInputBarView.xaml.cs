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

        }

        public async void Handle_Completed(object sender, EventArgs e)
        {
            // send a message to realtime database.

            // get client and therapist ID based on current authenticated role and 2 people in the chat.
            string userRole = await Firestore.GetUserRole();
            string therapistID = userRole == "Therapist" ? Auth.GetCurrentUserId() : PrivateMessagePage.recieverID;
            string clientID = userRole == "Client" ? Auth.GetCurrentUserId() : PrivateMessagePage.recieverID;
            string chatID = therapistID + clientID;

            // create a message
            var message = new Message()
            {
                SenderId = Auth.GetCurrentUserId(),
                ReceiverId = PrivateMessagePage.recieverID,
                MessageText = chatTextInput.Text,
                MessageSentTime = System.DateTime.Now.ToString(new CultureInfo("en-GB")),
            };

            // put message into real time DB
            await App.realTimeClient.Child("Message").Child(chatID).PostAsync(message);

            // reset Editor to empty message.
            chatTextInput.Text = "";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database.Query;
using MySerenity.Helpers;
using MySerenity.Model;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MySerenity.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrivateMessagePage : ContentPage
    {
        public string senderID;

        public static string recieverID;

        public string chatID;

        public ObservableCollection<Message> chatLog { get; set; } = new ObservableCollection<Message>();

        // constructor that is called when navigating from therapist account 
        public PrivateMessagePage(Clientquestionnaire clientDetails)
        {
            InitializeComponent();
            BindingContext = this;
            senderID = Auth.GetCurrentUserId(); // therapist opened the page - will always be sender.
            recieverID = clientDetails.UserId; // client is the other person in chat
            chatID = senderID + recieverID;

            CreateConversation();

        }

        public PrivateMessagePage(TherapistInfo info)
        {
            InitializeComponent();
            BindingContext = this;
            senderID = Auth.GetCurrentUserId(); // client opened the page - will always be sender.
            recieverID = info.UserId; // therapist is the other person in chat
            chatID = recieverID + senderID;

            CreateConversation();

        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
        //    var conversation = await GetConversation();

        //    MessageListView.ItemsSource = null; // clear the list to ensure latest version is shown.
        //    MessageListView.ItemsSource = conversation;
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            GetConversation();
        }

        protected async void CreateConversation()
        {
            string userRole = await Firestore.GetUserRole();
            string therapistID = userRole == "Therapist" ? Auth.GetCurrentUserId() : recieverID;
            string clientID = userRole == "Client" ? Auth.GetCurrentUserId() : recieverID;
            string chatID = therapistID + clientID;

            Conversation convo = new Conversation() {therapistID = therapistID, ClientID = clientID};

            await App.realTimeClient.Child("Conversation").Child(chatID).PutAsync(convo);
        }


        // !!!!!!! DO NOT DELETE - WORKING METHOD !!!!!!!!!
        //protected async Task<ObservableCollection<Message>> GetConversation()
        //{
        //    var getItems = (await App.realTimeClient
        //        .Child("Message")
        //        .Child(chatID)
        //        .OnceSingleAsync<Message[]>());

        //    var convo = new ObservableCollection<Message>(getItems);
        //    return convo;
        //}

        //protected async Task<ObservableCollection<Message>> GetConversation()
        //{
        //    var getItems = (await App.realTimeClient
        //        .Child("Message")
        //        .Child(chatID)
        //        .OnceAsync<Message>()).Select(m => new Message()
        //    {
        //        SenderId = m.Object.SenderId,
        //        ReceiverId = m.Object.ReceiverId,
        //        MessageText = m.Object.MessageText,
        //        MessageSentTime = m.Object.MessageSentTime,
        //    });

        //    var convo = new ObservableCollection<Message>(getItems);
        //    return convo;
        //}

        protected void GetConversation()
        {
            var getItems = App.realTimeClient
                .Child("Message")
                .Child(chatID)
                .AsObservable<Message>()
                .Subscribe((dbevent) =>
                {
                    if (dbevent.Object != null)
                    {
                        chatLog.Add(dbevent.Object);
                        MessageListView.ScrollTo(chatLog[chatLog.Count -1], ScrollToPosition.End, true);
                    }
                });
        }

    }
}

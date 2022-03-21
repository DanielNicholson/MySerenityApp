using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.Gms.Extensions;
using Android.Gms.Tasks;
using Android.Util;
using Firebase.Auth;
using Firebase.Firestore;
using Google.Apis.Util;
using Google.Cloud.Firestore;
using Java.Interop;
using Java.Lang;
using Java.Util;
using Microcharts;
using MySerenity.Helpers;
using MySerenity.Model;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Io.OpenCensus.Metrics.Export;
using CollectionReference = Firebase.Firestore.CollectionReference;
using DocumentSnapshot = Firebase.Firestore.DocumentSnapshot;
using Exception = System.Exception;
using FieldPath = Firebase.Firestore.FieldPath;
using Message = MySerenity.Model.Message;
using Object = Java.Lang.Object;
using Query = Firebase.Firestore.Query;
using QuerySnapshot = Firebase.Firestore.QuerySnapshot;
using Task = Android.Gms.Tasks.Task;

[assembly: Xamarin.Forms.Dependency(typeof(MySerenity.Droid.Dependencies.FirestoreAndriod))]
namespace MySerenity.Droid.Dependencies
{
    public class FirestoreAndriod : IFirestore
    {


        public FirestoreAndriod()
        {

        }

        // saves a journal entry to firestore
        public bool SaveJournalEntry(JournalEntry entry)
        {
            // firestore is organised as a dictionary of keys and values - to save an object, we need to split the journal entry in to a dictionary that matches the columns in firestore and the values to store.
            try
            {
                // create the dictionary to store in firestore
                var journalEntry = new Dictionary<string, Object>
                {
                    {"userID", FirebaseAuth.Instance.CurrentUser.Uid},
                    {"journalEntryTitle", entry.JournalEntryTitle},
                    {"journalEntryText", entry.JournalEntryText},
                    {"journalEntryMoodData", entry.JournalEntryMoodData},
                    {"journalEntryEntryTime", entry.JournalEntryEntryTime.ToString()}
                };

                // make a reference to the firestore collection
                var collection = FirebaseFirestore.Instance.Collection("JournalEntries");

                // add the new journal
                collection.Add(new HashMap(journalEntry));

                // no errors - return true
                return true;
            }
            catch (Exception e)
            {
                // errors - return false
                return false;
            }
        }

        // deletes a journal by ID
        public async Task<bool> DeleteJournalEntry(JournalEntry entry)
        {
            try
            {
                // gets a reference to current journal entries in firestore
                var collection = FirebaseFirestore.Instance.Collection("JournalEntries");

                // delete entry by ID
                await collection.Document(entry.Id).Delete();

                // no errors - return true
                return true;
            }
            catch (Exception e)
            {
                // error - return false
                return false;
            }
        }

        // updates a journal entry in firebase
        public async Task<bool> UpdateJournalEntry(JournalEntry entry)
        {
            // firestore is organised as a dictionary of keys and values - to update an object, we need to split the journal entry in to a dictionary that matches the columns in firestore and the values to store.
            try
            {
                // create dictionary of keys and values that match 'JournalEntries' in firestore
                var journalEntry = new Dictionary<string, Object>
                {
                    {"userID", FirebaseAuth.Instance.CurrentUser.Uid},
                    {"journalEntryTitle", entry.JournalEntryTitle},
                    {"journalEntryText", entry.JournalEntryText},
                    {"journalEntryMoodData", entry.JournalEntryMoodData},
                    {"journalEntryEntryTime", entry.JournalEntryEntryTime.ToString()}

                };

                // get the collection of JournalEntries
                var collection = FirebaseFirestore.Instance.Collection("JournalEntries");

                // update the document by ID with the dictionary of values
                await collection.Document(entry.Id).Update(journalEntry);

                // return true if no errors
                return true;
            }
            catch (Exception e)
            {
                // error has occured - return false
                return false;
            }
        }

        // Saves the user role to firestore on account creation
        public bool SaveUserRole(bool isClient)
        {
            string role = isClient ? "Client" : "Therapist";

            // firestore is organised as a dictionary of keys and values - to save an object, we need to split the journal entry in to a dictionary that matches the columns in firestore and the values to store.
            try
            {
                // create the dictionary to store in firestore
                var userRole = new Dictionary<string, Object>
                {
                    {"userID", FirebaseAuth.Instance.CurrentUser.Uid},
                    {"role", role}
                };

                // make a reference to the firestore collection
                var collection = FirebaseFirestore.Instance.Collection("UserRole");

                // add the new journal
                collection.Add(new HashMap(userRole));

                // no errors - return true
                return true;
            }
            catch (Exception e)
            {
                // errors - return false
                return false;
            }
        }

        // saves the clients sign up questionaire to firestore on account creation.
        public bool SaveSignUpQuestions(Clientquestionnaire questions)
        {
            // firestore is organised as a dictionary of keys and values - to save an object, we need to split the questions in to a dictionary that matches the columns in firestore and the values to store.
            try
            {
                // create the dictionary to store in firestore
                var questionsDictionary = new Dictionary<string, Object>
                {
                    {"ClientName", questions.ClientName},
                    {"userID", FirebaseAuth.Instance.CurrentUser.Uid},
                    {"Gender", questions.Gender},
                    {"Age", questions.Age},
                    {"PreviousTherapyExperience", questions.PreviousTherapyExperience},
                    {"ReasonsForTherapy", questions.ReasonsForTherapy},
                    {"LowInterestLevels", questions.LowInterestLevels},
                    {"LowEnergyLevels", questions.LowEnergyLevels},
                    {"LowMoodLevels", questions.LowMoodLevels},
                    {"SuicidalThoughts", questions.SuicidalThoughts},
                    {"TherapistPreferences", questions.TherapistPreferences},
                    {"CurrentMedication", questions.CurrentMedication},
                    {"EmergencyContactName", questions.EmergencyContactName},
                    {"EmergencyContactNumber", questions.EmergencyContactNumber},
                };

                // make a reference to the firestore collection
                var collection = FirebaseFirestore.Instance.Collection("Clientquestionnaire");

                // add the new journal
                collection.Add(new HashMap(questionsDictionary));

                // no errors - return true
                return true;
            }
            catch (Exception e)
            {
                // errors - return false
                return false;
            }
        }

        // sets the client to be available for a therapist in firestore
        public bool ClientLookingForTherapist(ClientTherapistRelationship relation)
        {
            // firestore is organised as a dictionary of keys and values - to save an object, we need to split the questions in to a dictionary that matches the columns in firestore and the values to store.
            try
            {
                // create the dictionary to store in firestore
                var relationDictionary = new Dictionary<string, Object>
                {
                    {"userID", FirebaseAuth.Instance.CurrentUser.Uid},
                    {"TherapistID", "NULL"},
                    {"IsApproved", false},
                };

                // make a reference to the firestore collection
                var collection = FirebaseFirestore.Instance.Collection("ClientTherapistRelationship");

                // add the new entry
                collection.Add(new HashMap(relationDictionary));

                // no errors - return true
                return true;
            }
            catch (Exception e)
            {
                // errors - return false
                return false;
            }
        }

        // saves the therapist info to firestore on account creation
        public bool SaveTherapistInfo(TherapistInfo info)
        {
            // firestore is organised as a dictionary of keys and values - to save an object, we need to split the questions in to a dictionary that matches the columns in firestore and the values to store.
            try
            {
                // create the dictionary to store in firestore
                var relationDictionary = new Dictionary<string, Object>
                {
                    {"userID", FirebaseAuth.Instance.CurrentUser.Uid},
                    {"Name",  info.Name},
                    {"Membership", info.Membership},
                    {"MySerenityInterest", info.MySerenityInterest},
                    {"MySerenityTime", info.MySerenityTime},
                    {"MySerenityAwareness", info.MySerenityAwareness},
                    {"Description", info.Description},
                };

                // make a reference to the firestore collection
                var collection = FirebaseFirestore.Instance.Collection("TherapistInfo");

                // add the new therapist info
                collection.Add(new HashMap(relationDictionary));

                // no errors - return true
                return true;
            }
            catch (Exception e)
            {
                // errors - return false
                return false;
            }
        }

        // gets the user role from firestore to log them into to the correct functionality
        public async Task<string> GetUserRole()
        {
            string role = "";

            // get collection of all user roles where the current authenticated userID matches the userID of the document.
            Query collectionQuery = FirebaseFirestore.Instance.Collection("UserRole").WhereEqualTo("userID", FirebaseAuth.Instance.CurrentUser.Uid);
            QuerySnapshot collectionSnapshot = (QuerySnapshot)await collectionQuery.Get();

            // should only ever be one item returned.
            if (collectionSnapshot.Size() != 1)
            {
                throw new Exception("Multiple User Role's found, please contact support");
            }
            else
            {
                // access the documents and get the role from the doc - return role.
                foreach (DocumentSnapshot doc in collectionSnapshot.Documents)
                {
                    role = doc.Get("role").ToString();
                }

                return role;
            }

            throw new Exception("User Role Not Found");
        }

        // gets a list of all available clients from firestore
        public async Task<List<Clientquestionnaire>> ReadAllAvailableClients()
        {
            // get collection of all clients who haven't been matches with a therapist.
            Query unapprovedClients = FirebaseFirestore.Instance.Collection("ClientTherapistRelationship").WhereEqualTo("IsApproved", false);
            QuerySnapshot unapprovedClientsSnapshot = (QuerySnapshot)await unapprovedClients.Get();

            // loop through the list and add all userIDs to a list.
            IList<Object> list = new List<Object>();
            foreach (var entry in unapprovedClientsSnapshot.Documents)
            {
                list.Add(entry.Get("userID").ToString());
            }

            List<Clientquestionnaire> clientDetails = new List<Clientquestionnaire>();

            if (list.Count != 0) 
            {
                // get all client sign up questionnaires of clients who haven't been matched with a therapist.
                FieldPath path = FieldPath.Of("userID");
                Query unapprovedClientDetails = FirebaseFirestore.Instance.Collection("Clientquestionnaire").WhereIn(path, list);
                QuerySnapshot unapprovedClientDetailsSnapshot = (QuerySnapshot)await unapprovedClientDetails.Get();

            

                foreach (var entry in unapprovedClientDetailsSnapshot.Documents)
                {
                    // create a newClientquestionnaire and fill in all values in document
                    var newQuestions = new Clientquestionnaire()
                    {
                        UserId = entry.Get("userID").ToString(),
                        ClientName = entry.Get("ClientName").ToString(),
                        Gender = entry.Get("Gender").ToString(),
                        Age = (int)entry.Get("Age"),
                        PreviousTherapyExperience = entry.Get("PreviousTherapyExperience").ToString(),
                        ReasonsForTherapy = entry.Get("ReasonsForTherapy").ToString(),
                        LowInterestLevels = entry.Get("LowInterestLevels").ToString(),
                        LowEnergyLevels = entry.Get("LowEnergyLevels").ToString(),
                        LowMoodLevels = entry.Get("LowMoodLevels").ToString(),
                        SuicidalThoughts = entry.Get("SuicidalThoughts").ToString(),
                        CurrentMedication = entry.Get("CurrentMedication").ToString(),
                        TherapistPreferences = entry.Get("TherapistPreferences").ToString(),
                        EmergencyContactName = entry.Get("EmergencyContactName").ToString(),
                        EmergencyContactNumber = entry.Get("EmergencyContactNumber").ToString(),
                        Id = entry.Id
                    };

                    // add the question to the return list.
                    clientDetails.Add(newQuestions);

                }
            }

            return clientDetails;
        }

        // returns all clients that are matched with the current authenticated therapist.
        public async Task<List<Clientquestionnaire>> ReadAllClientsForTherapist()
        {
            // get collection of all clients who haven't been matches with a therapist.
            Query unapprovedClients = FirebaseFirestore.Instance.Collection("ClientTherapistRelationship").WhereEqualTo("TherapistID", FirebaseAuth.Instance.CurrentUser.Uid);
            QuerySnapshot unapprovedClientsSnapshot = (QuerySnapshot)await unapprovedClients.Get();

            // loop through the list and add all userIDs to a list.
            IList<Object> list = new List<Object>();
            foreach (var entry in unapprovedClientsSnapshot.Documents)
            {
                list.Add(entry.Get("userID").ToString());
            }

            List<Clientquestionnaire> clientDetails = new List<Clientquestionnaire>();
            if (list.Count > 0)
            {
                // get all client sign up questionnaires of clients.
                FieldPath path = FieldPath.Of("userID");
                Query unapprovedClientDetails = FirebaseFirestore.Instance.Collection("Clientquestionnaire").WhereIn(path, list);
                QuerySnapshot unapprovedClientDetailsSnapshot = (QuerySnapshot)await unapprovedClientDetails.Get();

                foreach (var entry in unapprovedClientDetailsSnapshot.Documents)
                {
                    // create a new Clientquestionnaire and fill in all values in document
                    var newQuestions = new Clientquestionnaire()
                    {
                        UserId = entry.Get("userID").ToString(),
                        ClientName = entry.Get("ClientName").ToString(),
                        Gender = entry.Get("Gender").ToString(),
                        Age = (int)entry.Get("Age"),
                        PreviousTherapyExperience = entry.Get("PreviousTherapyExperience").ToString(),
                        ReasonsForTherapy = entry.Get("ReasonsForTherapy").ToString(),
                        LowInterestLevels = entry.Get("LowInterestLevels").ToString(),
                        LowEnergyLevels = entry.Get("LowEnergyLevels").ToString(),
                        LowMoodLevels = entry.Get("LowMoodLevels").ToString(),
                        SuicidalThoughts = entry.Get("SuicidalThoughts").ToString(),
                        CurrentMedication = entry.Get("CurrentMedication").ToString(),
                        TherapistPreferences = entry.Get("TherapistPreferences").ToString(),
                        EmergencyContactName = entry.Get("EmergencyContactName").ToString(),
                        EmergencyContactNumber = entry.Get("EmergencyContactNumber").ToString(),
                        Id = entry.Id
                    };

                    // add the question to the return list.
                    clientDetails.Add(newQuestions);

                }
            }
            return clientDetails;
        }

        // updates a client therapist relationship to match with the therapist
        public async Task<bool> MatchTherapistWithClient(Clientquestionnaire entry) 
        {
            // firestore is organised as a dictionary of keys and values - to update an object, we need to split the journal entry in to a dictionary that matches the columns in firestore and the values to store.
            try
            {
                // create dictionary of keys and values that match 'Clientquestionnaire' in firestore
                var clientTherapistRelationship = new Dictionary<string, Object>
                {
                    {"userID", entry.UserId},
                    {"IsApproved", true},
                    {"TherapistID", FirebaseAuth.Instance.CurrentUser.Uid}
                };

                // get the collection of Clientquestionnaires
                var collection = FirebaseFirestore.Instance.Collection("ClientTherapistRelationship");

                // get record of client who matches the userID
                Query unapprovedClients = FirebaseFirestore.Instance.Collection("ClientTherapistRelationship").WhereEqualTo("userID", entry.UserId);
                QuerySnapshot unapprovedClientsSnapshot = (QuerySnapshot) await unapprovedClients.Get();

                if (unapprovedClientsSnapshot.Size() == 1)
                {
                    // update the document by ID with the dictionary of values
                    await collection.Document(unapprovedClientsSnapshot.Documents.First().Id).Update(clientTherapistRelationship);

                    // return true if no errors
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                // error has occured - return false
                return false;
            }
        }

        // saves a sent message to firestore
        public bool SendMessage(Message message)
        {
            // firestore is organised as a dictionary of keys and values - to save an object, we need to split the journal entry in to a dictionary that matches the columns in firestore and the values to store.
            try
            {
                // create the dictionary to store in firestore
                var journalEntry = new Dictionary<string, Object>
                {
                    {"SenderId", FirebaseAuth.Instance.CurrentUser.Uid},
                    {"ReceiverId", message.ReceiverId},
                    {"MessageText", message.MessageText},
                    {"MessageSentTime", message.MessageSentTime.ToString()}
                };

                // make a reference to the firestore collection
                var collection = FirebaseFirestore.Instance.Collection("Message");

                // add the new journal
                collection.Add(new HashMap(journalEntry));

                // no errors - return true
                return true;
            }
            catch (Exception e)
            {
                // errors - return false
                return false;
            }
        }

        // retrieves a conversation from firestore
        public async Task<List<Message>> RetrieveConversation(string recieverID)
        {
            var entries = new List<Message>();

            // get collection of all message entries where the current authenticated userID matches senderID.
            Query collectionQuery = FirebaseFirestore.Instance.Collection("Message").WhereEqualTo("SenderId", FirebaseAuth.Instance.CurrentUser.Uid);
            QuerySnapshot collectionSnapshot = (QuerySnapshot) await collectionQuery.Get();


            // loop through all documents in query
            foreach (var entry in collectionSnapshot.Documents)
            {
                // skip document if receiver isn't in conversation
                if (entry.Get("ReceiverId").ToString() != recieverID) continue;

                // create a new message and fill in all values in document
                var newMessage = new Message()
                {
                    SenderId = entry.Get("SenderId").ToString(),
                    ReceiverId = entry.Get("ReceiverId").ToString(),
                    MessageText = entry.Get("MessageText").ToString(),
                    MessageSentTime = entry.Get("MessageSentTime").ToString(),
                };

                // add the entry to list 
                entries.Add(newMessage);

            }
            

            // get collection of all message entries where the current authenticated userID matches senderID.
            Query collectionQueryTwo = FirebaseFirestore.Instance.Collection("Message").WhereEqualTo("ReceiverId", FirebaseAuth.Instance.CurrentUser.Uid);
            QuerySnapshot collectionSnapshotTwo = (QuerySnapshot)await collectionQueryTwo.Get();

            // loop through all documents in query
            foreach (var entry in collectionSnapshotTwo.Documents)
            {
                // skip document if receiver isn't in conversation
                if (entry.Get("SenderId").ToString() != recieverID) continue;

                // create a new message and fill in all values in document
                var newMessage = new Message()
                {
                    SenderId = entry.Get("SenderId").ToString(),
                    ReceiverId = entry.Get("ReceiverId").ToString(),
                    MessageText = entry.Get("MessageText").ToString(),
                    MessageSentTime = entry.Get("MessageSentTime").ToString(),
                };

                // add the message to list and 
                entries.Add(newMessage);
            }

            return entries;
        }

        // retried mood data to display on the mood on the client dashboard
        public async Task<List<ChartEntry>> RetrieveMoodData()
        {
            List<ChartEntry> moodEntries = new List<ChartEntry>();

            // get a list of all saved journal entries for the authenticated user
            var journalEntries = await Helpers.Firestore.ReadAllJournalEntriesForUser();

            foreach (var journalEntry in journalEntries)
            {
                var chartEntry = new ChartEntry(journalEntry.JournalEntryMoodData)
                {
                    Label = journalEntry.JournalEntryEntryTime,
                    Color = SKColor.Parse("#3498db"),
                    
                };

                moodEntries.Insert(moodEntries.Count, chartEntry);
            }

            moodEntries.Reverse();
            return moodEntries;
        }

        // retried mood data to display on the mood on the MyClientDetails page
        public async Task<List<ChartEntry>> RetrieveMoodData(string userID)
        {
            List<ChartEntry> moodEntries = new List<ChartEntry>();

            // get a list of all saved journal entries for the authenticated user
            var journalEntries = await Helpers.Firestore.ReadAllJournalEntriesForUser(userID);

            foreach (var journalEntry in journalEntries)
            {
                var chartEntry = new ChartEntry(journalEntry.JournalEntryMoodData)
                {
                    Label = journalEntry.JournalEntryEntryTime,
                    Color = SKColor.Parse("#3498db"),

                };

                moodEntries.Insert(moodEntries.Count, chartEntry);
            }

            moodEntries.Reverse();
            return moodEntries;
        }

        // returns therapist information from firestore for the client.
        public async Task<TherapistInfo> GetTherapistForClient()
        {
            TherapistInfo info = null;
            string therapistID = "";

            // get collection of all user roles where the current authenticated userID matches the userID of the document.
            Query collectionQuery = FirebaseFirestore.Instance.Collection("ClientTherapistRelationship").WhereEqualTo("userID", FirebaseAuth.Instance.CurrentUser.Uid);
            QuerySnapshot collectionSnapshot = (QuerySnapshot)await collectionQuery.Get();

            // should only ever be one item returned.
            if (collectionSnapshot.Size() != 1)
            {
                throw new Exception($"Error, Too many client records in ClientTherapistRelationship for {FirebaseAuth.Instance.CurrentUser.Uid}.");
            }
            else
            {
                // only 1 doc stored in collectionSnapshot - get therapist ID to search for therapist details in therapistInfo table.
                foreach (DocumentSnapshot doc in collectionSnapshot.Documents)
                {
                    therapistID = doc.Get("TherapistID").ToString();
                }

                // get a collection snapshot of all therapistInfo where userID == therapistID from previous query
                Query collectionQueryTwo = FirebaseFirestore.Instance.Collection("TherapistInfo").WhereEqualTo("userID", therapistID);
                QuerySnapshot collectionSnapshotTwo = (QuerySnapshot)await collectionQueryTwo.Get();

                // should only ever be one therapist info for each user ID
                if (collectionSnapshotTwo.Size() != 1)
                {
                    return info;
                    throw new Exception($"Error, error receiving therapist info documents in TherapistInfo table for {therapistID}");
                }

                // get therapist info out of document and store in info (to be returned)
                foreach (DocumentSnapshot doc in collectionSnapshotTwo.Documents)
                {
                    info = new TherapistInfo()
                    {
                        Name = doc.Get("Name").ToString(),
                        Membership = doc.Get("Membership").ToString(),
                        MySerenityInterest = doc.Get("MySerenityInterest").ToString(),
                        MySerenityTime = doc.Get("MySerenityTime").ToString(),
                        MySerenityAwareness = doc.Get("MySerenityAwareness").ToString(),
                        UserId = doc.Get("userID").ToString(),
                        Description = doc.Get("Description").ToString()

                    };
                }
            }

            return info;
        }

        // unmatch a client from a therapist - client side.
        public async Task<bool> UnmatchClientFromTherapist(TherapistInfo info)
        {
            // firestore is organised as a dictionary of keys and values - to update an object
            try
            {
                // create dictionary of keys and values that match 'Clientquestionnaire' in firestore
                var clientTherapistRelationship = new Dictionary<string, Object>
                {
                    {"userID", FirebaseAuth.Instance.CurrentUser.Uid},
                    {"IsApproved", false},
                    {"TherapistID", ""}
                };

                // get the collection of ClientTherapistRelationship
                var collection = FirebaseFirestore.Instance.Collection("ClientTherapistRelationship");

                // get record of client who matches the userID
                Query clientRecords = FirebaseFirestore.Instance.Collection("ClientTherapistRelationship").WhereEqualTo("userID", FirebaseAuth.Instance.CurrentUser.Uid);
                QuerySnapshot unapprovedClientsSnapshot = (QuerySnapshot)await clientRecords.Get();

                if (unapprovedClientsSnapshot.Size() == 1)
                {
                    // update the document by ID with the dictionary of values
                    await collection.Document(unapprovedClientsSnapshot.Documents.First().Id).Update(clientTherapistRelationship);

                    // return true if no errors
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                // error has occured - return false
                return false;
            }
        }

        // unmatch a therapist from a client - therapist side.
        public async Task<bool> UnmatchTherapistFromClient(Clientquestionnaire client)
        {
            // firestore is organised as a dictionary of keys and values - to update an object
            try
            {
                // create dictionary of keys and values that match 'Clientquestionnaire' in firestore
                var clientTherapistRelationship = new Dictionary<string, Object>
                {
                    {"userID", client.UserId},
                    {"IsApproved", false},
                    {"TherapistID", ""}
                };

                // get the collection of all ClientTherapistRelationship documents from firestore
                var collection = FirebaseFirestore.Instance.Collection("ClientTherapistRelationship");

                // get record of client who matches the client userID
                Query clientRecords = FirebaseFirestore.Instance.Collection("ClientTherapistRelationship").WhereEqualTo("userID", client.UserId);
                QuerySnapshot unapprovedClientsSnapshot = (QuerySnapshot)await clientRecords.Get();

                // should only ever return one record
                if (unapprovedClientsSnapshot.Size() == 1)
                {
                    // update the document by ID with the dictionary of values
                    await collection.Document(unapprovedClientsSnapshot.Documents.First().Id).Update(clientTherapistRelationship);

                    // return true if no errors
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // return the therapist info for current authenticated user
        public async Task<TherapistInfo> GetTherapistInfo()
        {
            TherapistInfo info = null;
            string therapistID = "";

            // get a collection snapshot of all therapistInfo where userID == therapistID
            Query collectionQueryTwo = FirebaseFirestore.Instance.Collection("TherapistInfo").WhereEqualTo("userID", FirebaseAuth.Instance.CurrentUser.Uid);
            QuerySnapshot collectionSnapshotTwo = (QuerySnapshot)await collectionQueryTwo.Get();

            // should only ever be a max of one therapist info for each user ID - return null record if record does not exist
            if (collectionSnapshotTwo.Size() != 1)
            {
                return info;
                throw new Exception($"Error, error receiving therapist info documents in TherapistInfo table for {therapistID}");
            }

            // get therapist info out of document and store in info (to be returned)
            foreach (DocumentSnapshot doc in collectionSnapshotTwo.Documents)
            {
                info = new TherapistInfo()
                {
                    Name = doc.Get("Name").ToString(),
                    Membership = doc.Get("Membership").ToString(),
                    MySerenityInterest = doc.Get("MySerenityInterest").ToString(),
                    MySerenityTime = doc.Get("MySerenityTime").ToString(),
                    MySerenityAwareness = doc.Get("MySerenityAwareness").ToString(),
                    UserId = doc.Get("userID").ToString(),
                    Description = doc.Get("Description").ToString(),
                };
            }

            return info;
        }

        // update therapist info from therapist profile page
        public async Task<bool> UpdateTherapistInfo(TherapistInfo info)
        {
            // firestore is organised as a dictionary of keys and values - to update an object, we need to split the journal entry in to a dictionary that matches the columns in firestore and the values to store.
            try
            {
                // create dictionary of keys and values that match 'TherapistInfo' in firestore
                var therapistInfo = new Dictionary<string, Object>
                {
                    {"userID", info.UserId},
                    {"Name", info.Name},
                    {"Membership", info.Membership},
                    {"MySerenityInterest", info.MySerenityInterest},
                    {"MySerenityTime", info.MySerenityTime},
                    {"MySerenityAwareness", info.MySerenityAwareness},
                    {"Description", info.Description},
                };

                // get the collection of TherapistInfo
                var collection = FirebaseFirestore.Instance.Collection("TherapistInfo");

                // get record of client who matches the userID
                Query TherapistInfo = FirebaseFirestore.Instance.Collection("TherapistInfo").WhereEqualTo("userID", info.UserId);
                QuerySnapshot TherapistInfoSnapshot = (QuerySnapshot)await TherapistInfo.Get();

                if (TherapistInfoSnapshot.Size() == 1)
                {
                    // update the document by ID with the dictionary of values
                    await collection.Document(TherapistInfoSnapshot.Documents.First().Id).Update(therapistInfo);

                    // return true if no errors
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                // error has occured - return false
                return false;
            }
        }

        // saves the theapist working days in firestore
        public async Task<bool> SaveTherapistSchedule(TherapistWorkingDays schedule)
        {
            // firestore is organised as a dictionary of keys and values - to update an object, we need to split the journal entry in to a dictionary that matches the columns in firestore and the values to store.
            try
            {
                // create dictionary of keys and values that match 'TherapistWorkingDays' in firestore
                var therapistSchedule = new Dictionary<string, Object>
                {
                    {"UserId", schedule.UserId},
                    {"Monday", schedule.Monday},
                    {"Tuesday", schedule.Tuesday},
                    {"Wednesday",schedule.Wednesday},
                    {"Thursday", schedule.Thursday},
                    {"Friday", schedule.Friday},
                    {"Saturday", schedule.Saturday},
                    {"Sunday", schedule.Sunday},
                };

                // get the collection of TherapistWorkingDays
                var collection = FirebaseFirestore.Instance.Collection("TherapistWorkingDays");

                // get record of therapist who matches the userID
                Query TherapistInfo = FirebaseFirestore.Instance.Collection("TherapistWorkingDays").WhereEqualTo("UserId", FirebaseAuth.Instance.CurrentUser.Uid);
                QuerySnapshot TherapistInfoSnapshot = (QuerySnapshot) await TherapistInfo.Get();

                if (TherapistInfoSnapshot.Size() == 1)
                {
                    // update the document by ID with the dictionary of values
                    await collection.Document(TherapistInfoSnapshot.Documents.First().Id).Update(therapistSchedule);

                    // return true if no errors
                    return true;
                }
                else
                {
                    // no record previously found, save new record.
                    collection.Add(new HashMap(therapistSchedule));
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                // error has occured - return false
                return false;
            }
        }

        // retrieve the therapist's working schedule - used to display on TherapistProfilePage and MyTherapistProfilePage
        public async Task<TherapistWorkingDays> GetTherapistSchedule(string userID)
        {
            TherapistWorkingDays schedule = null;

            // get a collection snapshot of all therapistInfo where userID == therapistID from previous query
            Query collectionQuery = FirebaseFirestore.Instance.Collection("TherapistWorkingDays").WhereEqualTo("UserId", userID);
            QuerySnapshot collectionSnapshot = (QuerySnapshot)await collectionQuery.Get();

            // should only ever be one therapist info for each user ID
            if (collectionSnapshot.Size() != 1)
            {
                return schedule;
            }

            // get therapist info out of document and store in info (to be returned)
            foreach (DocumentSnapshot doc in collectionSnapshot.Documents)
            {
                schedule = new TherapistWorkingDays()
                {
                    UserId = doc.Get("UserId").ToString(),
                    Monday = (bool)doc.Get("Monday"),
                    Tuesday = (bool)doc.Get("Tuesday"),
                    Wednesday = (bool)doc.Get("Wednesday"),
                    Thursday = (bool)doc.Get("Thursday"),
                    Friday = (bool)doc.Get("Friday"),
                    Saturday = (bool)doc.Get("Saturday"),
                    Sunday = (bool)doc.Get("Sunday"),
                };
            }

            return schedule;
        }

        // return all journal entries for the current authenticated user
        public async Task<List<JournalEntry>> ReadAllJournalEntriesForUser()
        {
            // get collection of all journal entries where the current authenticated userID matches the userID of the document.
            Query collectionQuery = FirebaseFirestore.Instance.Collection("JournalEntries").WhereEqualTo("userID", FirebaseAuth.Instance.CurrentUser.Uid);
            QuerySnapshot collectionSnapshot = (QuerySnapshot)await collectionQuery.Get();
            
            var entries = new List<JournalEntry>();

            // loop through all documents in query
            foreach (var entry in collectionSnapshot.Documents)
            {
                // create a new Journal Entry and fill in all values in document
                var newJournal = new JournalEntry()
                {
                    UserId = entry.Get("userID").ToString(),
                    JournalEntryTitle = entry.Get("journalEntryTitle").ToString(),
                    JournalEntryText = entry.Get("journalEntryText").ToString(),
                    JournalEntryMoodData = Int32.Parse(entry.Get("journalEntryMoodData").ToString()),
                    JournalEntryEntryTime = entry.Get("journalEntryEntryTime").ToString(),
                    Id = entry.Id
                };

                // add the entry to list and reverse to show in correct order
                entries.Add(newJournal);

            }

            // order by date
            entries.OrderBy(p => DateTime.Parse(p.JournalEntryEntryTime));
            return entries;
        }

        // return all journal entries for the given user to get mood data for the mood chart on MyClientDetailsPage
        public async Task<List<JournalEntry>> ReadAllJournalEntriesForUser(string userID)
        {
            // get collection of all journal entries where the userID matches the userID of the document.
            Query collectionQuery = FirebaseFirestore.Instance.Collection("JournalEntries").WhereEqualTo("userID", userID);
            QuerySnapshot collectionSnapshot = (QuerySnapshot)await collectionQuery.Get();

            var entries = new List<JournalEntry>();

            // loop through all documents in query
            foreach (var entry in collectionSnapshot.Documents)
            {
                // create a new Journal Entry and fill in all values in document
                var newJournal = new JournalEntry()
                {
                    UserId = entry.Get("userID").ToString(),
                    JournalEntryTitle = entry.Get("journalEntryTitle").ToString(),
                    JournalEntryText = entry.Get("journalEntryText").ToString(),
                    JournalEntryMoodData = Int32.Parse(entry.Get("journalEntryMoodData").ToString()),
                    JournalEntryEntryTime = entry.Get("journalEntryEntryTime").ToString(),
                    Id = entry.Id
                };

                // add the entry to list and reverse to show in correct order
                entries.Add(newJournal);
            }

            entries.OrderBy(p => DateTime.Parse(p.JournalEntryEntryTime));
            return entries;
        }
    }
}
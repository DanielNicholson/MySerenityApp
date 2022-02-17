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
using Firebase.Auth;
using Firebase.Firestore;
using Java.Interop;
using Java.Util;
using MySerenity.Helpers;
using MySerenity.Model;
using Object = Java.Lang.Object;
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
                    {"MySerenityAwareness", info.MySerenityAwareness}
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
                foreach (DocumentSnapshot doc in collectionSnapshot.Documents)
                {
                    role = doc.Get("role").ToString();
                }

                return role;
            }

            throw new Exception("User Role Not Found");
        }

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

            entries.OrderBy(p => DateTime.Parse(p.JournalEntryEntryTime));
            return entries;
        }


    }
}
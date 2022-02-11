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
        // used to return when retrieving all entries from the firestore
        private List<JournalEntry> _entries;

        public FirestoreAndriod()
        {
            _entries = new List<JournalEntry>();
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
                collection.Document(entry.Id).Delete();

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
                collection.Document(entry.Id).Update(journalEntry);

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

            if (collectionSnapshot.Size() == 1)
            {
                foreach (DocumentSnapshot doc in collectionSnapshot.Documents)
                {
                    role = doc.Get("role").ToString();
                }

                return role;
            }
            throw new Exception("User Role Not Found");
        }


        public async Task<List<JournalEntry>> ReadAllJournalEntriesForUser()
        {
            // get collection of all journal entries where the current authenticated userID matches the userID of the document.
            Query collectionQuery = FirebaseFirestore.Instance.Collection("JournalEntries").WhereEqualTo("userID", FirebaseAuth.Instance.CurrentUser.Uid);
            QuerySnapshot collectionSnapshot = (QuerySnapshot)await collectionQuery.Get();
            
            // clear list of current journal entries to avoid duplication
            _entries.Clear();

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
                _entries.Add(newJournal);

            }

            _entries.OrderBy(p => DateTime.Parse(p.JournalEntryEntryTime));
            return _entries;
        }
    }
}
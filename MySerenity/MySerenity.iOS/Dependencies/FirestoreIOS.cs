using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using MySerenity.Helpers;
using MySerenity.Model;
using UIKit;
using WebKit;
using Auth = Firebase.Auth.Auth;
using Firestore = Firebase.CloudFirestore.Firestore;

[assembly: Xamarin.Forms.Dependency(typeof(MySerenity.iOS.Dependencies.FirestoreIos))]
namespace MySerenity.iOS.Dependencies
{
    class FirestoreIos : IFirestore
    {

        // saves journal entry to firestore
        public bool SaveJournalEntry(JournalEntry entry)
        {
            // firestore is organised as a dictionary of keys and values - to store an object, we need to split the journal entry in to a NSdictionary that matches the columns in firestore and the values to store.
            try
            {
                // create the keys that correspond to the firestore collection columns for Journal Entries
                var keys = new[]
                {
                    new NSString("userID"),
                    new NSString("journalEntryTitle"),
                    new NSString("journalEntryText"),
                    new NSString("journalEntryMoodData"),
                    new NSString("journalEntryEntryTime")
                };

                // create the values that correspond to the keys Journal Entries
                var values = new NSObject[]
                {
                    new NSString(Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid),
                    new NSString(entry.JournalEntryTitle),
                    new NSString(entry.JournalEntryText),
                    new NSNumber(entry.JournalEntryMoodData),
                    new NSString(entry.JournalEntryEntryTime.ToString())
                };

                // put the keys and pairs together and submit to firestore
                var doc = new NSDictionary<NSString, NSObject>(keys, values);
                var collection = Firestore.SharedInstance.GetCollection("JournalEntries");
                collection.AddDocument(doc);

                // if we get this far, the insert has been successful.
                return true;
            }
            catch (Exception e)
            {
                // insert has failed
                return false;
            }
        }


        // delete entry from firestore
        public async Task<bool> DeleteJournalEntry(JournalEntry entry)
        {
            try
            {
                // get the collection of journal entries

                var collection = Firestore.SharedInstance.GetCollection("JournalEntries");
                // delete the given entry by ID
                await collection.GetDocument(entry.Id).DeleteDocumentAsync();

                // return true if no errors happen
                return true;
            }
            catch (Exception e)
            {
                // Error occured - return false;
                return false;
            }
        }

        public async Task<bool> UpdateJournalEntry(JournalEntry entry)
        {
            // firestore is organised as a dictionary of keys and values - to update an object, we need to split the journal entry in to a NSdictionary that matches the columns in firestore and the values to store.
            try
            {
                // create the keys that correspond to the firestore collection columns for Journal Entries
                var keys = new[]
                {
                    new NSString("userID"),
                    new NSString("journalEntryTitle"),
                    new NSString("journalEntryText"),
                    new NSString("journalEntryMoodData"),
                    new NSString("journalEntryEntryTime")
                };

                // create the values that correspond to the keys Journal Entries
                var values = new NSObject[]
                {
                    new NSString(Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid),
                    new NSString(entry.JournalEntryTitle),
                    new NSString(entry.JournalEntryText),
                    new NSNumber(entry.JournalEntryMoodData),
                    new NSString(entry.JournalEntryEntryTime.ToString())
                };

                // NSString inherits from NSObject - Update document requires 2 Object arrays.
                var doc = new NSDictionary<NSObject, NSObject>(keys, values);

                // get the collection of journal entries
                var collection = Firestore.SharedInstance.GetCollection("JournalEntries");

                // update the entry in firestore by ID - stores the Dictionary against the entry ID in firestore.
                await collection.GetDocument(entry.Id).UpdateDataAsync(doc);

                // return true if no errors occur and update is successful
                return true;
            }
            catch (Exception e)
            {
                // return false if errors occur and update is unsuccessful
                return false;
            }
        }

        // get all journal entries for the current authenticated user
        public async Task<List<JournalEntry>> ReadAllJournalEntriesForUser()
        {
            // firestore is organised as a dictionary of keys and values - to get all journal entries, we need to read the dictionary and 'build' the journal entry object from the values.
            try
            {
                // get collection reference where 'userID' matches the authenticated user
                var collection = Firestore.SharedInstance.GetCollection("JournalEntries").WhereEqualsTo("userID", Auth.DefaultInstance.CurrentUser.Uid);

                // get all journal entries from the collection
                var documents = await collection.GetDocumentsAsync();

                // create a list of journal entries to add each entry to as we build it
                List<JournalEntry> toReturn = new List<JournalEntry>();

                // loop through all documents in the collection
                foreach (var document in documents.Documents)
                {

                    // access the data of the document
                    var dictionary = document.Data;

                    // build up the journal entry from the information.
                    var newJournal = new JournalEntry()
                    {
                        UserId = dictionary.ValueForKey(new NSString("userID")) as NSString,
                        JournalEntryTitle = dictionary.ValueForKey(new NSString("journalEntryTitle")) as NSString,
                        JournalEntryText = dictionary.ValueForKey(new NSString("journalEntryText")) as NSString,
                        JournalEntryMoodData = Int32.Parse(dictionary.ValueForKey(new NSString("journalEntryMoodData")) as NSString),
                        JournalEntryEntryTime = dictionary.ValueForKey(new NSString("journalEntryEntryTime")) as NSString,
                        Id = document.Id
                    };

                    // add to the list 
                    toReturn.Add(newJournal);
                }

                // reverse list to show in correct order and return
                toReturn.Reverse();
                return toReturn;
            }
            catch (Exception e)
            {
                // if there is any error, return empty list to not crash app.
                Console.WriteLine(e);
                return new List<JournalEntry>();
            }
        }

        public bool SaveUserRole(bool userRole)
        {
            throw new NotImplementedException();
        }

        public bool SaveSignUpQuestions(Clientquestionnaire questions)
        {
            throw new NotImplementedException();
        }

        public bool ClientLookingForTherapist(ClientTherapistRelationship relation)
        {
            throw new NotImplementedException();
        }

        public bool SaveTherapistInfo(TherapistInfo info)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserRole()
        {
            throw new NotImplementedException();
        }

        public Task<List<Clientquestionnaire>> ReadAllAvailableClients()
        {
            throw new NotImplementedException();
        }

        public Task<List<Clientquestionnaire>> ReadAllClientsForTherapist()
        {
            throw new NotImplementedException();
        }

        public Task<bool> MatchTherapistWithClient(Clientquestionnaire entry)
        {
            throw new NotImplementedException();
        }

        public bool SendMessage(Message message)
        {
            throw new NotImplementedException();
        }

        Task<List<Message>> IFirestore.RetrieveConversation(string recieverID)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChartEntry>> RetrieveMoodData()
        {
            throw new NotImplementedException();
        }

        public Task<TherapistInfo> GetTherapistForClient()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnmatchClientFromTherapist(TherapistInfo info)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RetrieveConversation(string recieverID)
        {
            throw new NotImplementedException();
        }
    }
}
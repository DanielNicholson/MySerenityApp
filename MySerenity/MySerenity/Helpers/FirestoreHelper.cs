using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using MySerenity.Model;
using Xamarin.Forms;

namespace MySerenity.Helpers
{

    // interface to be implemented on both iOS and Android - used as each platform has specific code for firestore. 
    public interface IFirestore
    {
        bool SaveJournalEntry(JournalEntry entry);                             // saves journal entry to firestore
        Task<bool> DeleteJournalEntry(JournalEntry entry);                     // delete selected entry from firestore
        Task<bool> UpdateJournalEntry(JournalEntry entry);                     // Update selected entry in firestore
        Task<List<JournalEntry>> ReadAllJournalEntriesForUser();               // retrieve list of all entries for current authenticated user from firestore
        bool SaveUserRole(bool isClient);                                      // saves the user role to firestore upon account creation
        bool SaveSignUpQuestions(Clientquestionnaire questions);               // saves journal entry to firestore
        bool ClientLookingForTherapist(ClientTherapistRelationship relation);  // saves journal entry to firestore
        bool SaveTherapistInfo(TherapistInfo info);                            // Save the therapist signup info to firestor
        Task<string> GetUserRole();                                            // retrieves the user role from firestore for current authenticated user
        Task<List<Clientquestionnaire>> ReadAllAvailableClients();             // gets a list of all unpaired clients to populate available client list
        Task<List<Clientquestionnaire>> ReadAllClientsForTherapist();          // gets a list of all clients paired with current therapist
        Task<bool> MatchTherapistWithClient(Clientquestionnaire entry);        // Update selected entry in firestore
        bool SendMessage(Message message);                                     // Saves user's message to firestore
        Task<List<Message>> RetrieveConversation(string recieverID);           // Retrieves conversation from firestore
        Task<List<ChartEntry>> RetrieveMoodData();                             // Retrieves mood entry data from firestore to display on homepage graph

    }

    public class Firestore
    {
        // checks to see which operating system the app is being run on and runs the platform specific firestore code.
        private static IFirestore _firestore = DependencyService.Get<IFirestore>();

        // save journal entry to firestore.
        public static bool SaveJournalEntry(JournalEntry entry)
        {
            return _firestore.SaveJournalEntry(entry);
        }

        // deletes journal entry to firestore.
        public static Task<bool> Delete(JournalEntry entry)
        {
            return _firestore.DeleteJournalEntry(entry);
        }

        // update journal entry in firestore.
        public static Task<bool> Update(JournalEntry entry)
        {
            return _firestore.UpdateJournalEntry(entry);
        }

        // read all journal entries from firestore for current authenticated user.
        public static Task<List<JournalEntry>> ReadAllJournalEntriesForUser()
        {
            return _firestore.ReadAllJournalEntriesForUser();
        }

        public static bool SaveUserRole(bool isClient)
        {
            return _firestore.SaveUserRole(isClient);
        }

        public static bool SaveSignUpQuestions(Clientquestionnaire questions)
        {
            return _firestore.SaveSignUpQuestions(questions);
        }

        public static bool ClientLookingForTherapist(ClientTherapistRelationship relation)
        {
            return _firestore.ClientLookingForTherapist(relation);
        }

        public static bool SaveTherapistInfo(TherapistInfo info)
        {
            return _firestore.SaveTherapistInfo(info);
        }

        public static Task<string> GetUserRole()
        {
            return _firestore.GetUserRole();
        }

        public static Task<List<Clientquestionnaire>> ReadAllAvailableClients()
        {
            return _firestore.ReadAllAvailableClients();
        }

        public static Task<List<Clientquestionnaire>> ReadAllClientsForTherapist()
        {
            return _firestore.ReadAllClientsForTherapist();
        }

        public static Task<bool> MatchTherapistWithClient(Clientquestionnaire entry)
        {
            return _firestore.MatchTherapistWithClient(entry);
        }

        public static bool SendMessage(Message message)
        {
           return _firestore.SendMessage(message);
        }

        public static Task<List<Message>> RetrieveConversation(string recieverID)
        {
            return _firestore.RetrieveConversation(recieverID);
        }

        public static Task<List<ChartEntry>> RetrieveMoodData()
        {
            return _firestore.RetrieveMoodData();
        }
    }

}
    


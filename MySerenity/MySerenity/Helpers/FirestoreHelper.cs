using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MySerenity.Model;
using Xamarin.Forms;

namespace MySerenity.Helpers
{

    // interface to be implemented on both iOS and Android - used as each platform has specific code for firestore. 
    public interface IFirestore
    {
        bool SaveJournalEntry(JournalEntry entry);                      // saves journal entry to firestore
        Task<bool> DeleteJournalEntry(JournalEntry entry);              // delete selected entry from firestore
        Task<bool> UpdateJournalEntry(JournalEntry entry);              // Update selected entry in firestore
        Task<List<JournalEntry>> ReadAllJournalEntriesForUser();        // retrieve list of all entries for current authenticated user from firestore
        bool SaveUserRole(bool isClient);                               // saves the user role to firestore upon account creation
        bool SaveSignUpQuestions(Clientquestionnaire questions);        // saves journal entry to firestore
        bool ClientLookingForTherapist(ClientTherapistRelationship relation);  // saves journal entry to firestore
        bool SaveTherapistInfo(TherapistInfo info);
        Task<string> GetUserRole();

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
    }

}
    


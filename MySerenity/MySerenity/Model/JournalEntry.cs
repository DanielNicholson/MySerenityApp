using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MySerenity.Model
{
    public class JournalEntry
    {
        public string Id { get; set; }                         // ID of the record in firestore - given when saved to firestore
        public string UserId { get; set; }                     // userID of who created the entry - Authenticated user is stored against Auth instance to grab
        public string JournalEntryTitle { get; set; }          // Filled in by user on New journal entry page
        public string JournalEntryText { get; set; }           // Filled in by user on New journal entry page
        public int JournalEntryMoodData { get; set; }          // Filled in by user on New journal entry page
        public string JournalEntryEntryTime { get; set; }      // Filled in when user submits entry

    }
}

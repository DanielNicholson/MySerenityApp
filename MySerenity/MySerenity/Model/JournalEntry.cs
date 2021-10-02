using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MySerenity.Model
{
    public class JournalEntry
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string JournalEntryTitle { get; set; }
        public string JournalEntryText { get; set; }
        public int JournalEntryMoodData { get; set; }
        public DateTime JournalEntryEntryTime { get; set; }

    }
}

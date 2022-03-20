using System;
using System.Collections.Generic;
using System.Text;

namespace MySerenity.Model
{
    public class TherapistWorkingDays
    {
        public string Id { get; set; } // ID of the record in firestore - given when saved to firestore

        public string UserId { get; set; } // userID of who filled out the questionnaire

        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MySerenity.Model
{
    public class TherapistInfo
    {
        public string Id { get; set; } // ID of the record in firestore - given when saved to firestore

        public string UserId { get; set; } // userID of who filled out the questionaire

        public string Name { get; set; }

        public string Membership { get; set; }

        public string MySerenityInterest { get; set; }

        public string MySerenityTime { get; set; }

        public string MySerenityAwareness { get; set; }

        public string Description { get; set; }


    }
}

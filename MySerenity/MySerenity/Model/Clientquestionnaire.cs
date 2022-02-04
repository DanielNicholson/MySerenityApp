using System;
using System.Collections.Generic;
using System.Text;

namespace MySerenity.Model
{
    public class Clientquestionnaire
    {
        public string ID { get; set; } // ID of the record in firestore - given when saved to firestore

        public string UserID { get; set; } // userID of who filled out the questionaire

        public string Gender { get; set; }

        public int Age { get; set; }

        public string PreviousTherapyExperience { get; set; }           
               
        public string ReasonsForTherapy { get; set; }

        public string LowInterestLevels { get; set; }

        public string LowEnergyLevels { get; set; }

        public string LowMoodLevels { get; set; }

        public string SuicidalThoughts { get; set; }

        public string CurrentMedication { get; set; }

        public string EmergencyContactName { get; set; }

        public string EmergencyContactNumber { get; set; }

        public bool IsApproved { get; set; }
    }
}

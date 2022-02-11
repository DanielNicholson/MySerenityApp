using System;
using System.Collections.Generic;
using System.Text;

namespace MySerenity.Model
{
    public class ClientTherapistRelationship
    {
        public string Id { get; set; }           // ID of the record in firestore - given when saved to firestore
        public string UserId { get; set; }       // userID of client
        public string TherapistId { get; set; }  // userID of therapist - NULL to start, updated when therapist approves client
        public bool IsApproved { get; set; }     // NULL by default - set to true when therapist approves client.
    }
}

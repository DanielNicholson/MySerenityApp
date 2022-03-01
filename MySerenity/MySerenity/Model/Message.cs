using System;
using System.Collections.Generic;
using System.Text;

namespace MySerenity.Model
{
    public class Message
    {

        public string SenderId { get; set; }           // userID of sent the message

        public string ReceiverId { get; set; }         // userID of who receives the message

        public string MessageText { get; set; }        // text contents of the message.

        public string MessageSentTime { get; set; }    // Filled in when user sends message

        public Message()
        {
            
        }
    }
}

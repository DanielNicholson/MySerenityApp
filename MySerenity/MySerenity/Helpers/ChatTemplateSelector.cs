using System;
using System.Collections.Generic;
using System.Text;
using MySerenity.Model;
using MySerenity.Views;
using Xamarin.Forms;

namespace MySerenity.Helpers
{
    class ChatTemplateSelector : DataTemplateSelector
    {
        DataTemplate incomingDataTemplate;
        DataTemplate outgoingDataTemplate;

        public ChatTemplateSelector()
        { 
            incomingDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            outgoingDataTemplate = new DataTemplate(typeof(OutgoingViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            // takes a given message - passed into to method when populating chat screen.
            Message currentMessage = item as Message;

            // catch all, if there is no message - return null
            if (currentMessage == null)
            {
                return null;
            }

            // if the current authenticated user is the sender the the message is a sent message
            return (currentMessage.SenderId == Auth.GetCurrentUserId()) ? outgoingDataTemplate : incomingDataTemplate;
        }

    }
}

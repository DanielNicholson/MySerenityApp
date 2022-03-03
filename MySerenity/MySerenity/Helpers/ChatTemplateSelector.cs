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
            var messageVm = item as Message;
            if (messageVm == null)
                return null;


            return (messageVm.SenderId == Auth.GetCurrentUserId()) ? outgoingDataTemplate : incomingDataTemplate;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using WordTran.Models;
using WordTran.Views;
using Xamarin.Forms;

namespace WordTran.Helpers
{
    public class ChatTemplateSelector: DataTemplateSelector
    {
        DataTemplate incomingDataTemplate;
        DataTemplate outgoingDataTemplate;

        public ChatTemplateSelector()
        {
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingMessageViewCell));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingMessageViewCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as Message;
            if (messageVm == null)
                return null;

            return (messageVm.Sender == MessageSender.User) ? outgoingDataTemplate : incomingDataTemplate;
        }
    }
}

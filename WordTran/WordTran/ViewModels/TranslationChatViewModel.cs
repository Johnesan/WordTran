using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using WordTran.Models;
using Xamarin.Forms;

namespace WordTran.ViewModels
{
    public class TranslationChatViewModel : BaseViewModel
    {
        public bool ShowScrollTap { get; set; } = false;
        public bool LastMessageVisible { get; set; } = true;
        public int PendingMessageCount { get; set; } = 0;
        public bool PendingMessageCountVisible { get { return PendingMessageCount > 0; } }

        public Queue<Message> DelayedMessages { get; set; } = new Queue<Message>();
        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
        public string TextToSend { get; set; }
        public ICommand OnSendCommand { get; set; }
        public ICommand MessageAppearingCommand { get; set; }
        public ICommand MessageDisappearingCommand { get; set; }

        public TranslationChatViewModel()
        {
            Messages.Insert(0, new Message() { Text = "Hi there", Sender = MessageSender.System, DateCreated = DateTime.Now, Language = "English", Status = MessageStatus.Delivered });
            Messages.Insert(0, new Message() { Text = "I de o", Sender = MessageSender.User, DateCreated = DateTime.Now, Language = "English", Status = MessageStatus.Sent });
            Messages.Insert(0, new Message() { Text = "Soft Soft", Sender = MessageSender.System, DateCreated = DateTime.Now, Language = "Spain", Status = MessageStatus.Read });
            Messages.Insert(0, new Message() { Text = "Translate this guy for me shap shap. E get why I talk am.", Sender = MessageSender.User, DateCreated = DateTime.Now, Language = "English", Status = MessageStatus.Pending });
            MessageAppearingCommand = new Command<Message>(OnMessageAppearing);
            MessageDisappearingCommand = new Command<Message>(OnMessageDisappearing);

            OnSendCommand = new Command(() =>
            {
                if (!string.IsNullOrEmpty(TextToSend))
                {
                    Messages.Add(new Message() { Text = TextToSend, DateCreated = DateTime.Now, Language = "English", Status = MessageStatus.Delivered });
                    TextToSend = string.Empty;
                }

            });

            //Code to simulate reveing a new message procces
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                if (LastMessageVisible)
                {
                    Messages.Insert(0, new Message() { Text = "New message test", Sender = MessageSender.System, DateCreated = DateTime.Now, Status = MessageStatus.Delivered });
                }
                else
                {
                    DelayedMessages.Enqueue(new Message() { Text = "New message test", Sender = MessageSender.User, DateCreated = DateTime.Now, Status = MessageStatus.Read });
                    PendingMessageCount++;
                }
                return true;
            });
        }

        void OnMessageAppearing(Message message)
        {
            var idx = Messages.IndexOf(message);
            if (idx <= 6)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    while (DelayedMessages.Count > 0)
                    {
                        Messages.Insert(0, DelayedMessages.Dequeue());
                    }
                    ShowScrollTap = false;
                    LastMessageVisible = true;
                    PendingMessageCount = 0;
                });
            }
        }

        void OnMessageDisappearing(Message message)
        {
            var idx = Messages.IndexOf(message);
            if (idx >= 6)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ShowScrollTap = true;
                    LastMessageVisible = false;
                });

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace WordTran.Models
{
    public class Message
    {
        public string Text { get; set; }
        public string Language { get; set; }
        public DateTime DateCreated { get; set; }
        public MessageSender Sender { get; set; }
        public MessageStatus Status { get; set; }
    }
    public enum MessageSender
    {
        User,
        System
    }
    public enum MessageStatus
    {
        Pending,
        Sent,
        Delivered,
        Read
    }
}

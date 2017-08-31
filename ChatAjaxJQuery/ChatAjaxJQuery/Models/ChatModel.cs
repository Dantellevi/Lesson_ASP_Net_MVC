using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatAjaxJQuery.Models
{
    public class ChatModel
    {
        public List<ChatUser> Users;    //// Все пользователи чата
        public List<ChatMessage> Messages;  //все сообщения

        public ChatModel()
        {
            Users = new List<ChatUser>();
            Messages = new List<ChatMessage>();
            Messages.Add(new ChatMessage() { Text = "Чат запущен " + DateTime.Now });
        }
    }
}
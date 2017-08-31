using System;

namespace ChatAjaxJQuery.Models
{
    public class ChatMessage
    {
        public ChatUser User;//автор сообщения , если null-автор сервер
        // текст сообщения
        public string Text = "";
        //время сообщения
        public DateTime Date = DateTime.Now;


    }
}
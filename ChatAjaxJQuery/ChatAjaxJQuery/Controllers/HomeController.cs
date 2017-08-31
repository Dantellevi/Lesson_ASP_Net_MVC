﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChatAjaxJQuery.Models;


namespace ChatAjaxJQuery.Controllers
{
    public class HomeController : Controller
    {
        static ChatModel chatModel;

        public ActionResult Index(string user, bool? logOn, bool? logOff, string chatMessage)
        {
            try
            {
                if(chatModel==null)
                {
                    chatModel = new ChatModel();
                }

                //оставняелм только последние 10 сообщений
                if(chatModel.Messages.Count>100)
                {
                    chatModel.Messages.RemoveRange(0, 10);
                }
                //если обычный запрос ,просто возвращаем представление
                if (!Request.IsAjaxRequest())
                {
                    return View(chatModel);
                }
                // если передан параметр logOn
                else if(logOn!=null && (bool)logOn)
                {
                    //проверяем , существует ли уже такой пользователь
                    if(chatModel.Users.FirstOrDefault(u=>u.Name==user)!=null)
                    {
                        throw new Exception("Пользователь с таким ником уже существует!!");
                    }
                    else if(chatModel.Users.Count>10)
                    {
                        throw new Exception("Чат заполнен");
                    }
                    else
                    {
                        //добавляем в список нового пользователя
                        chatModel.Users.Add(new ChatUser()
                        {
                            Name = user,
                            LoginTime = DateTime.Now,
                            LastPing = DateTime.Now
                        });
                        // добавляем в список сообщений сообщение о новом пользователе
                        chatModel.Messages.Add(new ChatMessage()
                        {
                            Text = user + " вошел в чат",
                            Date = DateTime.Now
                        });
                    }
                    return PartialView("ChatRoom", chatModel);
                }

                // если передан параметр logOff
                else if (logOff != null && (bool)logOff)
                {
                    LogOff(chatModel.Users.FirstOrDefault(u => u.Name == user));
                    return PartialView("ChatRoom", chatModel);
                }
                else
                {
                    ChatUser currentUser = chatModel.Users.FirstOrDefault(u => u.Name == user);

                    //для каждлого пользователя запоминаем время последнего обновления
                    currentUser.LastPing = DateTime.Now;

                    // удаляем неаквтивных пользователей, если время простоя больше 30 сек
                    List<ChatUser> toRemove = new List<ChatUser>();
                    foreach (ChatUser usr in chatModel.Users)
                    {
                        TimeSpan span = DateTime.Now - usr.LastPing;
                        if (span.TotalSeconds > 30)
                            toRemove.Add(usr);
                    }
                    foreach (ChatUser u in toRemove)
                    {
                        LogOff(u);
                    }

                    // добавляем в список сообщений новое сообщение
                    if (!string.IsNullOrEmpty(chatMessage))
                    {
                        chatModel.Messages.Add(new ChatMessage()
                        {
                            User = currentUser,
                            Text = chatMessage,
                            Date = DateTime.Now
                        });
                    }

                    return PartialView("History", chatModel);
                }
            }
            catch(Exception ex)
            {
                //в случае ошибки посылаем статусный код 500
                Response.StatusCode = 500;
                return Content(ex.Message);
            }
            
        }

        private void LogOff(ChatUser user)
        {
            chatModel.Users.Remove(user);
            chatModel.Messages.Add(new ChatMessage()
            {
                Text = user.Name + " покинул чат.",
                Date = DateTime.Now
            });
        }
    }
}
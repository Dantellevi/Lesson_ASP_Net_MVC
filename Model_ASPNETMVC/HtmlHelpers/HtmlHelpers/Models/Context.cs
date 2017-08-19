using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HtmlHelpers.Models
{
    public class Context
    {
        public List<Book> books { get; set; }

        public Context()
        {
            books = new List<Book>()
            {
                new Book {Id=1,Author="Пушкин",NameBook="Сказка про золотую рыбку",Year=1810, Price=128 },
                new Book {Id=2,Author="Лермантов",NameBook="Герой нашего времени",Year=1820, Price=128 },
                new Book {Id=3,Author="Конан Дойль",NameBook="Приключения Шерлока Холмса и доктора Ватсона",Year=1910, Price=128 },
                new Book {Id=4,Author="Достоевский",NameBook="Братья Карамазовы",Year=1840, Price=128 },
                new Book {Id=5,Author="Достоевский",NameBook="Отцы и дети",Year=1810, Price=128 },
                new Book {Id=6,Author="Гоголь",NameBook="Мертвые души",Year=1810, Price=128 }
            };
        }
    }
}
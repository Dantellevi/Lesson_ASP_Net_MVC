using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HtmlHelpers.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Display(Name ="Автор книги")]
        public string Author { get; set; }
        [Display(Name ="Название книги")]
        public string NameBook { get; set; }
        [Display(Name ="Год издания")]
        public int Year { get; set; }
        [Display(Name ="Стоимость")]
        public decimal Price { get; set; }

    }
}
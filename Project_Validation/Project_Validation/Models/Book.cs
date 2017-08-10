using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Validation.Models
{
    public class Book
    {
        [HiddenInput(DisplayValue = false)]         //не выводить данные столбца Id
        public int Id { get; set; }

        [Required(ErrorMessage ="Введите название книги")]      //проверка , на ввод в поле при добавлении
        [Display(Name="Название книги")]                        //в столбце header отображается данная надпись
        public string Name { get; set; }

        [Required]
        [Display(Name = "Автор")]               //название заголовка в таблице
        public string Author { get; set; }


        [Required]
        [Display(Name ="Стоимость книги")]
        [Range(typeof(decimal),"10","100000000,6",ErrorMessage = "Наименьшая цена -10руб,в качестве разделителя дробной и целой части используется запятая")]   //выбор диапазона значений
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]  //форматный вывод данных даты
        [Display(Name = "Год")]
       
        public DateTime bookDate { get; set; }

        [Required(ErrorMessage = "Введите описание книги")]
        public string description { get; set; }

    }
}
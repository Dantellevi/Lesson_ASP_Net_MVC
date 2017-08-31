using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Binding_model.Models
{

    /*
     * В данном случае выборочная привязки применена к методу Create.
     *  Что если нам надо осуществить выборочную привязку глобально во всем приложении?
     *   Тогда мы можем применить атрибут непосредственно к модели, и в этом случае атрибут
     *    будет применен по умолчанию ко всем методам действий контроллеров проекта
     *    
     *    [Bind (Exclude="Year")]
            public class Book
     * */

    public class Book
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Display(Name = "Год")]
        public int Year { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Threading.Tasks;


namespace ASP_Identity_Auth.Infrastructure
{
    public class CustomPasswordValidator: PasswordValidator
    {

        public override async Task<IdentityResult> ValidateAsync(string pass)
        {
            //----------------------------------------------------------------------------
            /*
             * В этом примере мы переопределили метод ValidateAsync()
             *  и вызвали сначала базовую реализацию этого метода для
             *   запуска встроенной проверки. Далее мы добавили пользовательскую проверку
             *    паролей на предмет того, не содержит ли он последовательности чисел «12345».
             *     Свойство Errors объекта IdentityResult доступно только для чтения,
             *      поэтому чтобы вернуть список ошибок мы создали новый объект IdentityResult
             *       и объединили ошибки из базовой проверки достоверности паролей с ошибками
             *        из пользовательской проверки. Я использовал LINQ для соединения этих ошибок.
             * 
             * */


            //----------------------------------------------------------------------------


            IdentityResult result = await base.ValidateAsync(pass);
           
            if(pass.Contains("12345"))
            {
                var errors = result.Errors.ToList();
                errors.Add("Пароль не должен содержать последовательности чисел.");
                result = new IdentityResult(errors);
            }
            return result;
        }

    }
}
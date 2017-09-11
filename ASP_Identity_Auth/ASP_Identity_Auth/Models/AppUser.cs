using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ASP_Identity_Auth.Models
{
    public enum Cities
    {
        [Display(Name ="Лондон")]
        LONDON,

        [Display(Name ="Париж")]
        PARIS,

        [Display(Name ="Москва")]
        MOSCOW

    }


    public enum Countries
    {
        [Display(Name ="Не указана!")]
        NONE,

        [Display(Name ="Англия")]
        ENG,

        [Display(Name ="Россия")]
        RUS,

        [Display(Name ="Франция")]
        FRU

    }

    public class AppUser: IdentityUser
    {
        public Cities City { get; set; }

        public Countries Country { get; set; }

        public void SetCountryFromCity(Cities city)
        {
            switch(city)
            {
                case Cities.LONDON:
                    Country = Countries.ENG;
                    break;

                case Cities.MOSCOW:
                    Country = Countries.RUS;
                    break;

                case Cities.PARIS:
                    Country = Countries.FRU;
                    break;

                default:
                    Country = Countries.NONE;
                    break;
            }


        }



    }
}
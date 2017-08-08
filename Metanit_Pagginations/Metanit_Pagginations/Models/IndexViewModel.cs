using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metanit_Pagginations.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Phone> Phones { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
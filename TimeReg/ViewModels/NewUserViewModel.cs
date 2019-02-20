using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeReg.ViewModels
{
    public class NewUserViewModel
    {

        [Required]
        public string First_Name { get; set; }
        [Required]
        public string Last_Name { get; set; }


        public NewUserViewModel() { }
   
    }
}
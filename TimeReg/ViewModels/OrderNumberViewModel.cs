using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeReg.ViewModels
{
    public class OrderNumberViewModel
    {
        public int PK_Id { get; set; }
        [Required]
        public string Number { get; set; }
    }
}
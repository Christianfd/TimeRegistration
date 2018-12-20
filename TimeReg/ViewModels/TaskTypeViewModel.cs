using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeReg.ViewModels
{
    public class TaskTypeViewModel
    {
        
        public int PK_Id { get; set; }
        [Required]
        public string Name { get; set; }

        public TaskTypeViewModel()
        {

        }

        public TaskTypeViewModel(int id, string name)
        {
            PK_Id = id;
            Name = name;
        }
    }
}
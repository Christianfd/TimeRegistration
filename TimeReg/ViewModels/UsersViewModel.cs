using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeReg.ViewModels
{
    public class UsersViewModel
    {

        public int PK_Id { get; set; }
        [Required]
        public string NK_Name { get; set; }
        [Required]
        public string NK_ZId { get; set; }

        public UsersViewModel() { }
        public UsersViewModel(VI_Users viUsers)
        {
            PK_Id = viUsers.PK_Id;
            NK_Name = viUsers.NK_Name;
            NK_ZId = viUsers.NK_ZId;
        }
    }
}
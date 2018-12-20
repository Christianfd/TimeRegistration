using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeReg.ViewModels
{
    public class CommentsViewModel
    {
        public int PK_Id { get; set; }
        [Required]
        public int WeekNr { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public int FK_ProjectId { get; set; }

        public int ProjectName { get; set; }
        [Required]
        public int FK_User { get; set; }
   
        public int UserName { get; set; }

        //Currently not in use, was temporarily used to create a drop down list.
        //public IEnumerable<SelectListItem> Projects { get; set; }
        //public int SelectedProject { get; set; }

        //public IEnumerable<SelectListItem> Users { get; set; }
        //public int SelectedUser { get; set; }
        

        public CommentsViewModel()
        {
      
        }



    }
}

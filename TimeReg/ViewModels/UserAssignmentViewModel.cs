using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeReg.ViewModels
{
    public class UserAssignmentViewModel
    {
    
        public int PK_Id { get; set; }
        [Required]
        public int FK_UserId { get; set; }
        [Required]
        public int FK_ProjectId { get; set; }
   
        public string UserName { get; set; }
  
        public string NK_ZId { get; set; }
     
        public string ProjectName { get; set; }

        public UserAssignmentViewModel() { }

        public UserAssignmentViewModel(VI_UserAssignment VIUserAssignment)
        {
            PK_Id = VIUserAssignment.PK_Id;
            FK_UserId = VIUserAssignment.FK_UserId;
            FK_ProjectId = VIUserAssignment.FK_ProjectId;
            UserName = VIUserAssignment.UserName;
            NK_ZId = VIUserAssignment.NK_ZId;
            ProjectName = VIUserAssignment.ProjectName;
        }
    }

    


}
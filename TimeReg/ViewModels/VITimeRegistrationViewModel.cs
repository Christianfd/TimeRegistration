using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeReg.ViewModels
{
    public class VITimeRegistrationViewModel
    {
        public int PK_Id { get; set; }
        [Required]
        public int FK_UserId { get; set; }
        [Required]
        public int FK_ProjectId { get; set; }
        [Required]
        public int FK_TaskId { get; set; }
        [Required]
        public int Time { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public System.DateTime DateEntry { get; set; }
        public string TaskTypeName { get; set; }
        public string ProjectName { get; set; }
        public string UserName { get; set; }
        public List<TimeReg.VI_TimeRegistration> VI_TimeRegistration_List { get; set; }
        public VITimeRegistrationViewModel() { }
        //Overload created for shorter controller code when assigned the values of View to a ViewModel
        public VITimeRegistrationViewModel(VI_TimeRegistration viTimeRegistration)
        {
            PK_Id = viTimeRegistration.PK_Id;

            FK_UserId = viTimeRegistration.FK_UserId;
            FK_ProjectId = viTimeRegistration.FK_ProjectId;
            FK_TaskId = viTimeRegistration.FK_TaskId;
            Time = viTimeRegistration.Time;
            Date = viTimeRegistration.Date;
            DateEntry = viTimeRegistration.DateEntry;
            TaskTypeName = viTimeRegistration.TaskTypeName;
            ProjectName = viTimeRegistration.ProjectName;
            UserName = viTimeRegistration.UserName;
        }
     
    }
}
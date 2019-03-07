using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeReg.ViewModels
{
    public class TimeRegistrationViewModel
    {
        public int PK_Id { get; set; }
        [Required]
        public int FK_UserId { get; set; }
        [Required]
        public int FK_ProjectId { get; set; }
        [Required]
        public int FK_OrderId { get; set; }
        [Required]
        public int FK_TaskId { get; set; }
        [Required]
        public int Time { get; set; }
        [Required]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime DateEntry { get; set; }
        public string Comment { get; set; }
        public string TaskTypeName { get; set; }
        public string ProjectName { get; set; }
        public string OrderName { get; set; }
        public string UserName { get; set; }
        public List<TimeReg.VI_TimeRegistration> VI_TimeRegistration_List { get; set; }



        public TimeRegistrationViewModel() { }

        //Overload created for shorter controller code when assigned the values of View to a ViewModel
        public TimeRegistrationViewModel(VI_TimeRegistration viTimeRegistration)
        {
            PK_Id = viTimeRegistration.PK_Id;

            FK_UserId = viTimeRegistration.FK_UserId;
            FK_ProjectId = viTimeRegistration.FK_ProjectId;
            FK_TaskId = viTimeRegistration.FK_TaskId;
            FK_OrderId = viTimeRegistration.FK_OrderId;
            Time = viTimeRegistration.Time;
            Date = viTimeRegistration.Date;
            DateEntry = viTimeRegistration.DateEntry;
            TaskTypeName = viTimeRegistration.TaskTypeName;
            ProjectName = viTimeRegistration.ProjectName;
            UserName = viTimeRegistration.UserName;
            Comment = viTimeRegistration.Comment;
            OrderName = viTimeRegistration.OrderName;
        }

        public TimeRegistrationViewModel(List<TimeReg.VI_TimeRegistration> list)
        {
            VI_TimeRegistration_List = list;
        }


    }
}
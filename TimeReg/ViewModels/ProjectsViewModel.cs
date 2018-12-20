using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeReg.ViewModels
{
    public class ProjectsViewModel
    {
        public int PK_Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int FK_OrderNumber { get; set; }
        [Required]
        public int TimeEstimation { get; set; }
        [Required]
        public Nullable<int> FK_ProjectLeader { get; set; }
        public string UserName { get; set; }
      
        public string Scope { get; set; }
        public Nullable<int> timeSum { get; set; }
        [Required]
        public int FK_TimeType { get; set; }
        public string TimeTypeName { get; set; }

     

    }
}
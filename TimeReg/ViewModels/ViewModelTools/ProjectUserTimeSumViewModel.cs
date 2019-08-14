using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TimeReg.ViewModels.ViewModelTools;

namespace TimeReg.ViewModels
{
    public class ProjectUserTimeSumViewModel
    {
        public int PK_Id { get; set; }
        public string UserName { get; set; }
        public Nullable<double> timeSum { get; set; }
        public int FK_ProjectId { get; set; }
       }

}
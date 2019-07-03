using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeReg.ViewModels.ViewModelTools;

namespace TimeReg.ViewModels
{
    public class UserTimePerProjectViewModel
    {
        public int PK_Id { get; set; }
        public string UserName { get; set; }
        public string NK_ZId { get; set; }
        public string Name { get; set; }
        public Nullable<double> timeSum { get; set; }
        public int FK_ProjectId { get; set; }
        public List<WeeklyTimeViewModel> WeeklyTimePerProject { get; set; }
    }

}
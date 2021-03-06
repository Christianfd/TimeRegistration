using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeReg.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public VI_Projects VIProjects { get; set; }
        public IQueryable<VI_Comments> VIComments { get; set; }
        public List<ProjectUserTimeSumViewModel> VITimeSpentPerUser { get; set; }
        public List<ProjectTaskTypeTimeSumViewModel> VITimeSpentPerTaskType { get; set; }
    }
}
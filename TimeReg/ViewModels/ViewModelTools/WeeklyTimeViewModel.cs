using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeReg.ViewModels.ViewModelTools
{
    public class WeeklyTimeViewModel
    {
        public int Week { get; set; }
        public double TotalTime { get; set; }
        public string UserName { get; set; }


        public WeeklyTimeViewModel()
        {
        }

    }
}
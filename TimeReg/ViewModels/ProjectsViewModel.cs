﻿using System;
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
        [Required]
        public string Scope { get; set; }
        public Nullable<int> timeSum { get; set; }
        [Required]
        public int FK_TimeType { get; set; }
        public string TimeTypeName { get; set; }
        public string SiteOrVersion { get; set; }
        public int FK_Country { get; set; }
        public string CountryName { get; set; }
        public int FK_PlatformOrProduct { get; set; }
        public string PlatformOrProductName { get; set; }
        public int FK_Turbine { get; set; }
        public string TurbineName { get; set; }
        public string ProjectComment { get; set; }


     

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeReg.ViewModels
{
    //Currently not in use***
    public class ProjectOrderUnionViewModel
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int PK_Id { get; set; }

        public ProjectOrderUnionViewModel() { }
        public ProjectOrderUnionViewModel(String UnionType, String UnionName, int UnionPK_Id)
        {
            Type = UnionType;
            Name = UnionName;
            PK_Id = UnionPK_Id;
        }
         
    }


}
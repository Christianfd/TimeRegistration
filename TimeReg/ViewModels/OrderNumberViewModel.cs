using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimeReg.ViewModels
{
    public class OrderNumberViewModel
    {
        public int PK_Id { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public int FK_RequestOrg { get; set; }
        public string Organization { get; set; }
        [Required]
        public int FK_Requester { get; set; }
        public string RequesterName { get; set; }
        [Required]
        public int FK_CustomerRef { get; set; }
        public string CustomerRefName { get; set; }



        public OrderNumberViewModel() { }
        //Overload for View
        public OrderNumberViewModel(VI_OrderNumber vI_OrderNumber)
        {
            PK_Id = vI_OrderNumber.PK_Id;
            Number = vI_OrderNumber.Number;
            FK_RequestOrg = vI_OrderNumber.FK_RequestOrg;
            Organization = vI_OrderNumber.Organization;
            FK_Requester = vI_OrderNumber.FK_Requester;
            RequesterName = vI_OrderNumber.RequesterName;
            FK_CustomerRef = vI_OrderNumber.FK_CustomerRef;
            CustomerRefName = vI_OrderNumber.CustomerRefName;
        }


    }
}
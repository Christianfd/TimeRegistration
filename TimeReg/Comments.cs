//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TimeReg
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comments
    {
        public int PK_Id { get; set; }
        public int WeekNr { get; set; }
        public int Year { get; set; }
        public string Text { get; set; }
        public int FK_ProjectId { get; set; }
        public int FK_User { get; set; }
    
        public virtual Projects Projects { get; set; }
        public virtual Users Users { get; set; }
    }
}

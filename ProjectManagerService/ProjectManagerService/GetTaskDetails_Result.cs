//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectManagerService
{
    using System;
    
    public partial class GetTaskDetails_Result
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string ParentTaskName { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
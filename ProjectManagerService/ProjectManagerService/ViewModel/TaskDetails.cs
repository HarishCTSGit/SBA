using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProjectManagerService.ViewModel
{
    [DataContract]
    public class TaskDetails
    {
        [DataMember]
        public int? TaskId { get; set; }
        [DataMember]
        public string TaskName { get; set; }
        [DataMember]
        public int ParentTaskId { get; set; }
        [DataMember]
        public string ParentTaskName { get; set; }
        [DataMember]
        public int? Priority { get; set; }
        [DataMember]
        public DateTime? StartDate { get; set; }
        [DataMember]
        public DateTime? EndDate { get; set; }
        [DataMember]
        public bool? IsEnded { get; set; }

        [DataMember]
        public int? ProjectID { get; set; }

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public int? UserID { get; set; }
    }
}
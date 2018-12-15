using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProjectManagerService.ViewModel
{
    [DataContract]
    public class ProjectDetails
    {
        [DataMember]
        public int ProjectId { get; set; }

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }

        [DataMember]
        public int? Priority { get; set; }

        [DataMember]
        public int? UserID { get; set; }

        [DataMember]
        public int? NumberofTasks { get; set; }

        [DataMember]
        public bool? Completed { get; set; }

        [DataMember]
        public string Manager { get; set; }
    }
}
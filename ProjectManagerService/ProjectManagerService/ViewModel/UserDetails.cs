using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProjectManagerService.ViewModel
{
    [DataContract]
    public class UserDetails
    {
        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public int? EmployeeID { get; set; }

        [DataMember]
        public int ProjectID { get; set; }

        [DataMember]
        public int TaskID { get; set; }
    }
}
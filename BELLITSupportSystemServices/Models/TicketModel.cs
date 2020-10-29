using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BELLITSupportSystemServices
{
    [DataContract]
    public class TicketModel
    {
        [DataMember]
        public int TicketID { get; set; }
        [DataMember]
        public string ProjectName { get; set; }
        [DataMember]
        public int EmployeeID { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public DateTime RequestedON { get; set; }
        [DataMember]
        public virtual DepartmentModel modelDepartment { get; set; }
        [DataMember]
        public virtual EmployeeModel modelEmployee { get; set; }
    }
}
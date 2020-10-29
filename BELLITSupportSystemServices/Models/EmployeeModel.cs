using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BELLITSupportSystemServices
{
    [DataContract]
    public class EmployeeModel
    {
        [DataMember]
        public int EmployeeID { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public int DepartmentID { get; set; }
    }
}
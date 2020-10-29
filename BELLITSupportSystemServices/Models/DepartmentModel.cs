using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BELLITSupportSystemServices
{
    [DataContract]
    public class DepartmentModel
    {
        [DataMember]
        public int DepartmentID { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
    }
}
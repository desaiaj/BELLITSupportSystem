using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BELLITSupportSystem.Models
{
    public class DepartmentModel
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public List<DepartmentModel> lstDepartment = null;
    }
}
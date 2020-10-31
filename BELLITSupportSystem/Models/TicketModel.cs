using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BELLITSupportSystem.Models
{
    public class TicketModel
    {
        public int TicketID { get; set; }
        public string ProjectName { get; set; }
        public int EmployeeID { get; set; }
        public String Description { get; set; }
        public DateTime RequestedON { get; set; }
        public string SubmittedON { get; set; }
        public virtual DepartmentModel modelDepartment { get; set; }
        public virtual EmployeeModel modelEmployee { get; set; }

        public List<TicketModel> lstTickets = null;
    }
}
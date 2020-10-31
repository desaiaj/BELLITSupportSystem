using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BELLITSupportSystemServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class TicketService : ITicketService
    {
        private static BellTicketTrackingDBEntities _context;
        public TicketService()
        {
            _context = new BellTicketTrackingDBEntities();
        }

        #region Methods to Retrieve data from Database

        public List<DepartmentModel> GetAllDepartments()
        {
            try
            {
                List<Department> objDepartments = _context.Departments.ToList();
                List<DepartmentModel> lstDepartment = new List<DepartmentModel>();
                lstDepartment.AddRange(objDepartments.Select(x => new DepartmentModel
                {
                    DepartmentID = x.DepartmentID,
                    DepartmentName = x.DepartmentName
                }));
                return lstDepartment;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Custom Error: #" + e.Message);
            }
            return null;
        }

        public List<EmployeeModel> GetAllEmployees()
        {
            try
            {
                List<Employee> objEmployees = _context.Employees.ToList();
                List<EmployeeModel> lstEmployee = new List<EmployeeModel>();
                lstEmployee.AddRange(objEmployees.Select(x => new EmployeeModel
                {
                    EmployeeID = x.EmployeeID,
                    DepartmentID = x.DepartmentID,
                    EmployeeName = x.EmployeeName
                }));
                return lstEmployee;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Custom Error: #" + e.Message);
            }
            return null;
        }

        public List<TicketModel> GetAllTickets()
        {
            try
            {
                List<Ticket> objTickets = _context.Tickets.ToList();
                List<TicketModel> lstTickets = new List<TicketModel>();
                lstTickets.AddRange(objTickets.Select(x => new TicketModel
                {
                    EmployeeID = x.EmployeeID,
                    Description = x.Description,
                    ProjectName = x.ProjectName,
                    RequestedON = x.RequestedON,
                    TicketID = x.TicketID,
                    modelEmployee = new EmployeeModel()
                    {
                        EmployeeID = x.Employee.EmployeeID,
                        EmployeeName = x.Employee.EmployeeName,
                        DepartmentID = x.Employee.DepartmentID
                    },
                    modelDepartment = new DepartmentModel()
                    {
                        DepartmentID = x.Employee.Department.DepartmentID,
                        DepartmentName = x.Employee.Department.DepartmentName
                    }
                }));
                return lstTickets;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Custom Error: #" + e.Message);
            }
            return null;
        }

        public List<TicketModel> GetTicketBySearch(string ProjectName, string EmployeeName, string DepartmentName, string Description, string RequestedOn)
        {
            try
            {
                if ((ProjectName == null || ProjectName.Trim() == "") && (EmployeeName == null || EmployeeName.Trim() == "") && (DepartmentName == null || DepartmentName.Trim() == "") && (Description == null || Description == "") && (RequestedOn == null || RequestedOn.Trim() == ""))
                {
                    return GetAllTickets();
                }
                List<Ticket> objTickets = _context.Tickets.ToList();
                List<TicketModel> lstTickets = new List<TicketModel>();

                lstTickets.AddRange(objTickets.FindAll(
                    x => (Description != null && x.Description.ToUpper().Contains(Description.ToUpper()))
                    || (ProjectName != null && x.ProjectName.ToUpper().Contains(ProjectName.ToUpper()))
                    || (RequestedOn != null && x.RequestedON.ToShortDateString().Equals(DateTime.Parse(RequestedOn).ToShortDateString()) && x.RequestedON.ToString("hh:mm").Equals(DateTime.Parse(RequestedOn).ToString("hh:mm")))
                    || (EmployeeName != null && x.Employee.EmployeeName.ToUpper().Contains(EmployeeName.ToUpper()))
                    || (DepartmentName != null && x.Employee.Department.DepartmentName.ToUpper().Contains(DepartmentName.ToUpper()))
                ).Select(x => new TicketModel
                {
                    EmployeeID = x.EmployeeID,
                    Description = x.Description,
                    ProjectName = x.ProjectName,
                    RequestedON = x.RequestedON,
                    TicketID = x.TicketID,
                    modelEmployee = new EmployeeModel()
                    {
                        EmployeeID = x.Employee.EmployeeID,
                        EmployeeName = x.Employee.EmployeeName,
                        DepartmentID = x.Employee.DepartmentID
                    },
                    modelDepartment = new DepartmentModel()
                    {
                        DepartmentID = x.Employee.Department.DepartmentID,
                        DepartmentName = x.Employee.Department.DepartmentName
                    }
                }));

                return lstTickets;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Custom Error: #" + e.Message);
            }
            return null;
        }

        //GetData() - For Testing purpose, STRONG CANDIDATE FOR CODE CLEANING
        public string GetData(int value)
        {
            return _context.Employees.ToList().Select(x => x.EmployeeName).First();
        }

        #endregion

        #region Insert/Save Ticket into database
        public bool InsertTicket(string ProjectName, int EmployeeID, string Description)
        {
            try
            {
                Ticket newTicket = new Ticket
                {
                    ProjectName = ProjectName,
                    EmployeeID = EmployeeID,
                    RequestedON = DateTime.Now,
                    Description = Description
                };

                _context.Tickets.Add(newTicket);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Custom Insert Error #" + e.Message);
                return false;
            }

            return true;
        }
        #endregion
    }
}

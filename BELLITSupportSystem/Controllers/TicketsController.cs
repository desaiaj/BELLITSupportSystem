using BELLITSupportSystem.ITSupportServices;
using BELLITSupportSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DepartmentModel = BELLITSupportSystem.Models.DepartmentModel;
using EmployeeModel = BELLITSupportSystem.Models.EmployeeModel;
using TicketModel = BELLITSupportSystem.Models.TicketModel;

namespace BELLITSupportSystem.Controllers
{
    public class TicketsController : Controller
    {
        TicketServiceClient serviceClient;
        public TicketsController()
        {
            serviceClient = new TicketServiceClient();
        }
        public ActionResult Index()
        {
            //var d = serviceClient.GetAllDepartments();
            //var d1 = serviceClient.GetAllEmployees();
            // var d2 = serviceClient.GetAllTickets();
            TempData["IndexPage"] = "active";
            return View();
        }

        public ActionResult TicketReport()
        {
            ViewBag.Message = "Your application description page.";
            TempData["TicketReport"] = "active";

            return View();
        }

        public ActionResult TicketsView()
        {
            ViewBag.Message = "All Requested Tickets";
            TicketModel objTicket = new TicketModel();
            objTicket.lstTickets = new List<TicketModel>();
            objTicket.lstTickets.AddRange(serviceClient.GetAllTickets().Select(x => new TicketModel
            {
                EmployeeID = x.EmployeeID,
                Description = x.Description,
                ProjectName = x.ProjectName,
                RequestedON = x.RequestedON,
                TicketID = x.TicketID,
                modelDepartment = new DepartmentModel
                {
                    DepartmentID = x.modelDepartment.DepartmentID,
                    DepartmentName = x.modelDepartment.DepartmentName
                },
                modelEmployee = new EmployeeModel()
                {
                    EmployeeID = x.modelEmployee.EmployeeID,
                    EmployeeName = x.modelEmployee.EmployeeName,
                    DepartmentID = x.modelEmployee.DepartmentID
                }
            }));
            TempData["TicketsView"] = "active";

            return View(objTicket);
        }

        public PartialViewResult DepartmentList()
        {
            DepartmentModel objDepartment = new DepartmentModel();
            objDepartment.lstDepartment = new List<DepartmentModel>();
            objDepartment.lstDepartment.AddRange(serviceClient.GetAllDepartments().Select(x => new DepartmentModel
            {
                DepartmentID = x.DepartmentID,
                DepartmentName = x.DepartmentName
            }));
            return PartialView(objDepartment);
        }
        public PartialViewResult getEmployeesByDepartmentID(string DepartmentID = "0")
        {
            EmployeeModel objEmployee = new EmployeeModel();
            objEmployee.lstEmployees = new List<EmployeeModel>();
            objEmployee.lstEmployees.AddRange(serviceClient.GetAllEmployees().Where(m => m.DepartmentID == int.Parse(DepartmentID)).Select(x => new EmployeeModel
            {
                EmployeeID = x.EmployeeID,
                EmployeeName = x.EmployeeName,
                DepartmentID = x.DepartmentID
            }));
            return PartialView(objEmployee);
        }
    }
}
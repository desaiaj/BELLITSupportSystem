using BELLITSupportSystem.ITSupportServices;
using BELLITSupportSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
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
            TempData["IndexPage"] = "active";
            return View();
        }

        public ActionResult TicketReport()
        {
            TempData["TicketReport"] = "active";

            var TicketCounter = serviceClient.GetAllTickets().GroupBy(x => x.ProjectName).Select(x => new { ProjectName = x.Key, Count = x.Count() });

            ViewBag.ChartData = TicketCounter;
            return View();
        }

        public ActionResult TicketsView()
        {
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
            return PartialView("~/Views/Shared/_DepartmentList.cshtml", objDepartment);
        }

        public PartialViewResult getEmployeesByDepartmentID(int DepartmentID = 0)
        {
            EmployeeModel objEmployee = new EmployeeModel();
            objEmployee.lstEmployees = new List<EmployeeModel>();
            objEmployee.lstEmployees.AddRange(serviceClient.GetAllEmployees().Where(m => m.DepartmentID == DepartmentID).Select(x => new EmployeeModel
            {
                EmployeeID = x.EmployeeID,
                EmployeeName = x.EmployeeName,
                DepartmentID = x.DepartmentID
            }));

            if (objEmployee.lstEmployees.Count != 0)
                return PartialView("~/Views/Shared/_EmployeeList.cshtml", objEmployee);
            else
                return null;
        }

        public ActionResult IssueTicket(TicketModel model)
        {
            bool isInserted = false;
            if (model.EmployeeID != 0 && !model.ProjectName.IsEmpty() && !model.Description.IsEmpty())
                isInserted = serviceClient.InsertTicket(model.ProjectName, model.EmployeeID, model.Description);
            TempData["ActionStatus"] = isInserted;
            return RedirectToAction("Index");
        }
    }
}
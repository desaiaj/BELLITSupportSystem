using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BELLITSupportSystemServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ITicketService
    {

        [OperationContract]
        string GetData(int value);

        // TODO: Add your service operations here

        #region Methods to Retrieve Data from database

        [OperationContract]
        List<DepartmentModel> GetAllDepartments();

        [OperationContract]
        List<EmployeeModel> GetAllEmployees();

        [OperationContract]
        List<TicketModel> GetAllTickets();

        [OperationContract]
        List<TicketModel> GetTicketBySearch(string ProjectName, string EmployeeName, string DepartmentName, string Description, string RequestedOn);

        #endregion

        #region Methods to Insert Data Into database

        [OperationContract]
        bool InsertTicket(string ProjectName, int EmployeeID, String Description);

        #endregion
    }
}

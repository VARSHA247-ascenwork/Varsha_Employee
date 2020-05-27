using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarshaWeb.BAL.EmployeeManagement;
using VarshaWeb.Models.EmployeeManagement;


namespace VarshaWeb.Controllers.EmployeeManagement
{
    public class EmployeeDashboardController : Controller
    {
        // GET: EmployeeDashboard
        public ActionResult Index()
        {
            List<Emp_BasicInfoModels> empBasicinfo = new List<Emp_BasicInfoModels>();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                EmployeeDashboard emp = new EmployeeDashboard();
                empBasicinfo = emp.GetAllEmployee(clientContext);

            }
            ViewBag.EmpList = empBasicinfo;
            return View();
        }
        public ActionResult EmployeeDashboard()
        {
            
            return View();
        }
    }
}





















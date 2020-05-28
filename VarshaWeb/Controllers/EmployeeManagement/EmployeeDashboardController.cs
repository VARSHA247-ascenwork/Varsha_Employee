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
        List<Emp_BasicInfoModels> empBasicinfo = new List<Emp_BasicInfoModels>();
        EmployeeDashboardBal emp = new EmployeeDashboardBal();
        //get company
        List<Emp_CompanyMasterModels> emp_Company = new List<Emp_CompanyMasterModels>();
        CompanyMasterBal company = new CompanyMasterBal();
        //get designation
        List<Emp_DesignationModels> emp_designation = new List<Emp_DesignationModels>();
        DesignationMasterBal designation = new DesignationMasterBal();
        //get Department
        List<Emp_DepartmentModels> emp_department = new List<Emp_DepartmentModels>();
        DepartmentMasterBal department = new DepartmentMasterBal();
        //get Division
        List<Emp_DivisionModels> emp_division = new List<Emp_DivisionModels>();
        DivisionMasterBal division = new DivisionMasterBal();
        //get Branch
        List<Emp_BranchModels> emp_Branch = new List<Emp_BranchModels>();
        BranchMasterBal Branch = new BranchMasterBal();
        //get Region
        List<Emp_RegionModels> emp_Region = new List<Emp_RegionModels>();
        RegionMasterBal Region = new RegionMasterBal();

        // GET: EmployeeDashboard
        public ActionResult Index()
        {



            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {

                empBasicinfo = emp.GetAllEmployee(clientContext);

            }
            ViewBag.EmpList = empBasicinfo;
            return View();
        }
        [SharePointContextFilter]
        public ActionResult EmployeeDetails(int? id)
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                empBasicinfo = emp.GeEmployeeById(clientContext, id.ToString());
            }
            return View(empBasicinfo);
        }
      
        
        [HttpGet]
        public ActionResult AddEmployeeBasicInfo()
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_Company = company.GetAllCompany(clientContext);
                emp_designation = designation.GetDesignation(clientContext);
                emp_department = department.GetAllDepartment(clientContext);
                emp_division = division.GetDivision(clientContext);
                emp_Branch = Branch.GetBranch(clientContext);
                emp_Region = Region.GetRegion(clientContext);
            }
            ViewBag.company = emp_Company;
            ViewBag.designation = emp_designation;
            ViewBag.department = emp_department;
            ViewBag.division = emp_division;
            ViewBag.Branch = emp_Branch;
            ViewBag.Region = emp_Region;
            return View();
        }
        [HttpPost]
        [ActionName("AddEmployeeBasicInfo")]
        public ActionResult AddEmployeeBasicInfo(Emp_BasicInfoModels _BasicInfoModels)
        {
            if (ModelState.IsValid)
            {
                var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
                using (var clientContext = spContext.CreateUserClientContextForSPHost())

                {

                    string itemdata = "'FirstName' : '" + _BasicInfoModels.FirstName + "'";
                    itemdata = "'MiddleName' : '" + _BasicInfoModels.MiddleName + "'";
                    itemdata = "'LastName' : '" + _BasicInfoModels.LastName + "'";

                    emp.saveEmp(clientContext, itemdata);
                }
                return View();

            }
            return View();
        } 
    }

}





























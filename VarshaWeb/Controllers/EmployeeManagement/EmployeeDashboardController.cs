using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarshaWeb.BAL.EmployeeManagement;
using VarshaWeb.Models.EmployeeManagement;
using VarshaWeb.DAL;


namespace VarshaWeb.Controllers.EmployeeManagement
{
    public class EmployeeDashboardController : Controller
    {
        List<Emp_BasicInfoModels> empBasicinfo = new List<Emp_BasicInfoModels>();
        Emp_BasicInfoBal emp = new Emp_BasicInfoBal();
        //get company
        List<Emp_CompanyMasterModels> emp_Company = new List<Emp_CompanyMasterModels>();
        Emp_CompanyMasterBal company = new Emp_CompanyMasterBal();
        //get designation
        List<Emp_DesignationModels> emp_designation = new List<Emp_DesignationModels>();
        Emp_DesignationBal designation = new Emp_DesignationBal();
        //get Department
        List<Emp_DepartmentModels> emp_department = new List<Emp_DepartmentModels>();
        Emp_DepartmentBal department = new Emp_DepartmentBal();
        //get Division
        List<Emp_DivisionModels> emp_division = new List<Emp_DivisionModels>();
        Emp_DivisionBal division = new Emp_DivisionBal();
        //get Branch
        List<Emp_BranchModels> emp_Branch = new List<Emp_BranchModels>();
        Emp_BranchBal Branch = new Emp_BranchBal();
        //get Region
        List<Emp_RegionModels> emp_Region = new List<Emp_RegionModels>();
        Emp_RegionBal Region = new Emp_RegionBal();

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
        [HttpGet]
      
        [ActionName("EmployeeDetails")]
        public ActionResult EmployeeDetails()
        {
            string ViewEmployeeID = Request.Cookies["ViewEmployeeID"].Value;
            Emp_BasicInfoBal EmpDashBal = new Emp_BasicInfoBal();
          
            List<Emp_BasicInfoModels> empview = new List<Emp_BasicInfoModels>();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                empview = EmpDashBal.GeEmployeeById(clientContext, ViewEmployeeID);
                
            }
            ViewBag.name = empview;
          
            return View();


        }
      
        
        [HttpGet]
        public ActionResult AddEmployeeBasicInfo()
        {
            List<Emp_BasicInfoModels> empmanager = new List<Emp_BasicInfoModels>();
            Emp_BasicInfoBal manager = new Emp_BasicInfoBal();

            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {

                emp_Company = company.GetAllCompany(clientContext);
                emp_designation = designation.GetDesignation(clientContext);
                emp_department = department.GetAllDepartment(clientContext);
                emp_division = division.GetDivision(clientContext);
                emp_Branch = Branch.GetBranch(clientContext);
                emp_Region = Region.GetRegion(clientContext);
                empmanager = manager.GetManager(clientContext);
            }
            ViewBag.company = emp_Company;
            ViewBag.designation = emp_designation;
            ViewBag.department = emp_department;
            ViewBag.division = emp_division;
            ViewBag.Branch = emp_Branch;
            ViewBag.Region = emp_Region;
            ViewBag.manager = empmanager;
            return View();
        }
        [HttpPost]
        
        public ActionResult SaveInfo(Emp_BasicInfoModels EmpInfo)
        {
                var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
                using (var clientContext = spContext.CreateUserClientContextForSPHost())

                {
                        string itemdata = " 'FirstName' : '" + EmpInfo.FirstName + "',";
                        itemdata += "'MiddleName': '" + EmpInfo.MiddleName + "',";
                        itemdata += "'LastName': '" + EmpInfo.LastName + "',";
                        itemdata += "'EmpCode': '" + EmpInfo.EmpCode + "',";
                        itemdata += "'JoiningDate': '" + EmpInfo.JoiningDate + "',";
                        itemdata += "'DOB': '" + EmpInfo.DOB + "',";
                        itemdata += "'Gender': '" + EmpInfo.MiddleName + "',";
                        itemdata += "'MaritalStatus': '" + EmpInfo.MaritalStatus + "',";
                        itemdata += "'OnProbationTill': '" + EmpInfo.OnProbationTill + "',";
                        itemdata += "'ProbationStatus': '" + EmpInfo.ProbationStatus + "',";
                       // itemdata += "'ManagerId': '" + EmpInfo.Manager + "',";
                        itemdata += "'OfficeEmail': '" + EmpInfo.OfficeEmail + "',";
                        itemdata += "'ContactNumber': '" + EmpInfo.ContactNumber + "',";
                        itemdata += "'EmpStatus': '" + EmpInfo.EmpStatus + "',";
                        itemdata += "'CompanyId': '" + EmpInfo.Company + "',";
                        itemdata += "'DesignationId': '" + EmpInfo.Designation + "',";
                        itemdata += "'DepartmentId': '" + EmpInfo.Department + "',";
                        itemdata += "'DivisionId': '" + EmpInfo.Division + "',";
                        itemdata += "'RegionId': '" + EmpInfo.Region + "',";
                        itemdata += "'BranchId': '" + EmpInfo.Branch + "'";
                      //  itemdata += "'User_Name': '" + EmpInfo.User_Name + "'";
                       
                emp.saveEmp(clientContext, itemdata);
                }
                return View();

          
        } 
    }

}





























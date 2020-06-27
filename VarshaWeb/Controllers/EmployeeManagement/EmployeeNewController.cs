using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarshaWeb.BAL.EmployeeManagement;
using VarshaWeb.Models;
using VarshaWeb.Models.EmployeeManagement;
using VarshaWeb.BAL;

namespace VarshaWeb.Controllers.EmployeeManagement
{
    public class EmployeeNewController : Controller
    {
        List<Emp_BasicInfoModel> empBasicinfo = new List<Emp_BasicInfoModel>();
        Emp_BasicInfoBal emp = new Emp_BasicInfoBal();

        List<UserGroupModel> user = new List<UserGroupModel>();
        UsersGroup username = new UsersGroup();

        //get company
        List<Emp_CompanyMasterModel> emp_Company = new List<Emp_CompanyMasterModel>();
        Emp_CompanyMasterBal company = new Emp_CompanyMasterBal();

        //get designation
        List<Emp_DesignationModel> emp_designation = new List<Emp_DesignationModel>();
        Emp_DesignationBal designation = new Emp_DesignationBal();
        //get Department
        List<Emp_DepartmentModel> emp_department = new List<Emp_DepartmentModel>();
        Emp_DepartmentBal department = new Emp_DepartmentBal();
        //get Division
        List<Emp_DivisionModel> emp_division = new List<Emp_DivisionModel>();
        Emp_DivisionBal division = new Emp_DivisionBal();
        //get Branch
        List<Emp_BranchModel> emp_Branch = new List<Emp_BranchModel>();
        Emp_BranchBal Branch = new Emp_BranchBal();
        //get Region
        List<Emp_RegionModel> emp_Region = new List<Emp_RegionModel>();
        Emp_RegionBal Region = new Emp_RegionBal();
        // GET: EmployeeNew
        public ActionResult Index()
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                ViewBag.company = company.GetAllCompany(clientContext);
                ViewBag.designation = designation.GetDesignation(clientContext);
                ViewBag.department = department.GetAllDepartment(clientContext);
                ViewBag.division = division.GetDivision(clientContext);
                ViewBag.Branch = Branch.GetBranch(clientContext);
                ViewBag.Region = Region.GetRegion(clientContext);
                ViewBag.username = username.Getusergroup(clientContext);
                ViewBag.emp = emp.GetAllEmployee(clientContext);
            }
            return View();
        }
        public ActionResult getEmpdata(int id)
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                empBasicinfo = emp.GetAllEmployee(clientContext);
               
            }
            Session["managercode"] = empBasicinfo;
            return Json(empBasicinfo, JsonRequestBehavior.AllowGet);

        }

        public ActionResult getEmpdataById(string EId)
        {

            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                empBasicinfo = emp.GetEmployeeById(clientContext, EId);

            }
            return Json(empBasicinfo, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SaveInfo(Emp_BasicInfoModel EmpInfo)
        {
            int returnID = 0;
            List<Emp_BasicInfoModel> empBasicinfo = (List<Emp_BasicInfoModel>)Session["managercode"];
            
            int a = '0';
            if (empBasicinfo != null)
            {
                a = empBasicinfo.Count();
                EmpInfo.EmpCode = "AW00" + a;

                var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

                using (var clientContext = spContext.CreateUserClientContextForSPHost())
                {
                    foreach (Emp_BasicInfoModel emparr in empBasicinfo)
                    {
                        if (emparr.UserNameId == EmpInfo.Manager)
                        {
                            EmpInfo.Manager = (emparr.ID).ToString();

                            EmpInfo.ManagerCode = emparr.EmpCode;
                        }
                    }

                    {
                        string itemdata = "'FirstName' : '" + EmpInfo.FirstName + "',";
                        itemdata += "'MiddleName': '" + EmpInfo.MiddleName + "',";
                        itemdata += "'LastName': '" + EmpInfo.LastName + "',";
                        itemdata += "'EmpCode': '" + EmpInfo.EmpCode + "',";
                        itemdata += "'JoiningDate': '" + EmpInfo.JoiningDate + "',";
                        itemdata += "'DOB': '" + EmpInfo.DOB + "',";
                        itemdata += "'Gender': '" + EmpInfo.Gender + "',";
                        itemdata += "'MaritalStatus': '" + EmpInfo.MaritalStatus + "',";
                        itemdata += "'OnProbationTill': '" + EmpInfo.OnProbationTill + "',";
                        itemdata += "'ProbationStatus': '" + EmpInfo.ProbationStatus + "',";
                        itemdata += "'ManagerId': " + EmpInfo.Manager + ",";
                        itemdata += "'ManagerCode':  '" + EmpInfo.ManagerCode + "',";
                        itemdata += "'OfficeEmail': '" + EmpInfo.OfficeEmail + "',";
                        itemdata += "'ContactNumber': '" + EmpInfo.ContactNumber + "',";
                        itemdata += "'MobileNo': '" + EmpInfo.MobileNo + "',";
                        itemdata += "'CompanyId':" + EmpInfo.Company + ",";
                        itemdata += "'DesignationId':" + EmpInfo.Designation + ",";
                        itemdata += "'DepartmentId':" + EmpInfo.Department + ",";
                        itemdata += "'DivisionId':" + EmpInfo.Division + ",";
                        itemdata += "'RegionId': " + EmpInfo.Region + ",";
                        itemdata += "'BranchId': " + EmpInfo.Branch + ",";
                        itemdata += "'DOB_Months': '" + EmpInfo.DOB_Months + "',";
                        itemdata += "'JoiningDate_Month': '" + EmpInfo.JoiningDate_Month + "',";
                        itemdata += "'User_NameId': " + EmpInfo.User_Name + "";

                        emp.saveEmp(clientContext, itemdata);
                    }
                }
            }
            return Json(returnID, JsonRequestBehavior.AllowGet);
        }

        //save update data
        [SharePointContextFilter]
        public ActionResult UpdateInfo(Emp_BasicInfoModel EditEmpInfo)
        {
            string returnID = "0";
            string ID = EditEmpInfo.ID.ToString();
            List<Emp_BasicInfoModel> empBasicinfo = (List<Emp_BasicInfoModel>)Session["managercode"];

            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                 foreach (Emp_BasicInfoModel emparr in empBasicinfo)
                  {
                      if (emparr.ID .ToString()== EditEmpInfo.Manager)
                      {
                          EditEmpInfo.Manager = (emparr.ID).ToString();

                          EditEmpInfo.ManagerCode = emparr.EmpCode;
                      }
                  } 

                   string itemdata = "'JoiningDate': '" + EditEmpInfo.JoiningDate + "',";
                   itemdata += "'DOB': '" + EditEmpInfo.DOB + "',";
                   itemdata += "'MaritalStatus': '" + EditEmpInfo.MaritalStatus + "',";
                   itemdata += "'Gender': '" + EditEmpInfo.Gender + "',";
                   //itemdata += "'MiddleName': '" + EditEmpInfo.MiddleName + "',";
                   //itemdata += "'LastName': '" + EditEmpInfo.LastName + "'";
                   itemdata += "'OnProbationTill': '" + EditEmpInfo.OnProbationTill + "',";
                   itemdata += "'ProbationStatus': '" + EditEmpInfo.ProbationStatus + "',";
                   itemdata += "'ManagerId': " + EditEmpInfo.Manager + ",";
                   itemdata += "'ManagerCode':  '" + EditEmpInfo.ManagerCode + "',";
                   itemdata += "'OfficeEmail': '" + EditEmpInfo.OfficeEmail + "',";
                   itemdata += "'ContactNumber': '" + EditEmpInfo.ContactNumber + "',";
                   itemdata += "'MobileNo': '" + EditEmpInfo.MobileNo + "',";
                   itemdata += "'DOB_Months': '" + EditEmpInfo.DOB_Months + "',";
                   itemdata += "'JoiningDate_Month': '" + EditEmpInfo.JoiningDate_Month + "',";
                   itemdata += "'CompanyId': " + EditEmpInfo.Company + ",";
                   itemdata += "'DesignationId': " + EditEmpInfo.Designation + ",";
                   itemdata += "'DepartmentId': " + EditEmpInfo.Department + ",";
                   itemdata += "'DivisionId': " + EditEmpInfo.Division + ",";
                   itemdata += "'RegionId': " + EditEmpInfo.Region + ",";
                   itemdata += "'BranchId':" + EditEmpInfo.Branch + ""; 

                emp.UpdateEmp(clientContext, itemdata, ID);

            }
            return Json(returnID, JsonRequestBehavior.AllowGet);
        }

    }
}
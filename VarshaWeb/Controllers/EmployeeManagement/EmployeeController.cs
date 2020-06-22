
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarshaWeb.BAL.EmployeeManagement;
using VarshaWeb.Models.EmployeeManagement.ViewModels;
using VarshaWeb.Models.EmployeeManagement;
using VarshaWeb.DAL;
using VarshaWeb.Models;
using VarshaWeb.BAL;
using Newtonsoft.Json;
using System.Web.Helpers;

namespace VarshaWeb.Controllers.EmployeeManagement
{
    public class EmployeeController : Controller
    {

        VM_Emp_BasicInfoModel VM_Emp_Detail = new VM_Emp_BasicInfoModel();

        List<Emp_BasicInfoModel> emp_manager = new List<Emp_BasicInfoModel>();
        Emp_BasicInfoBal empmanager = new Emp_BasicInfoBal();

        List<Emp_BasicInfoModel> empBasicinfo = new List<Emp_BasicInfoModel>();
        Emp_BasicInfoBal emp = new Emp_BasicInfoBal();

        //get user
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
       
        [SharePointContextFilter]
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

            Session["managercode"] = empBasicinfo;
            
            return View();
        }
        public ActionResult getEmpdata(int id)
        {

          
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                empBasicinfo = emp.GetAllEmployee(clientContext);

            }
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


        public ActionResult EditBasicInfo(Emp_BasicInfoModel EmpInfo)
        {
            Session["EmpID"] = EmpInfo.ID.ToString();

            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
               
                empBasicinfo = emp.GetEmployeeById(clientContext, EmpInfo.ID.ToString());
                VM_Emp_Detail.VMEmpdata = empBasicinfo.Where(s => s.ID == Convert.ToInt32(EmpInfo.ID.ToString())).FirstOrDefault();
                IEnumerable<Emp_CompanyMasterModel> CompanyModels = company.GetAllCompany(clientContext);
                VM_Emp_Detail.SelectedCompanyID = VM_Emp_Detail.VMEmpdata.CompanyId;
                VM_Emp_Detail.company = CompanyModels;

               VM_Emp_Detail.SelectetManagerID = VM_Emp_Detail.VMEmpdata.ManagerId;
               VM_Emp_Detail.manager = VM_Emp_Detail.VMEmpdata.Manager;

                IEnumerable<Emp_DesignationModel> DesignationModel = designation.GetDesignation(clientContext);
                VM_Emp_Detail.SelectedDesignationID = VM_Emp_Detail.VMEmpdata.DesignationId;
                VM_Emp_Detail.Designation = DesignationModel;

                IEnumerable<Emp_DepartmentModel> DepartmentModel = department.GetAllDepartment(clientContext);
                VM_Emp_Detail.SelectedDepartmentID = VM_Emp_Detail.VMEmpdata.DepartmentId;
                VM_Emp_Detail.DepartmentName = DepartmentModel;   

                IEnumerable<Emp_DivisionModel> DivisionModel = division.GetDivision(clientContext);
                VM_Emp_Detail.SelectedDivisionID = VM_Emp_Detail.VMEmpdata.DivisionId;
                VM_Emp_Detail.Division = DivisionModel;

                IEnumerable<Emp_RegionModel> RegionModel = Region.GetRegion(clientContext);
                VM_Emp_Detail.SelectedRegionID = VM_Emp_Detail.VMEmpdata.RegionId;
                VM_Emp_Detail.Region = RegionModel;

                IEnumerable<Emp_BranchModel> BranchModel = Branch.GetBranch(clientContext);
                VM_Emp_Detail.SelectedBranchID = VM_Emp_Detail.VMEmpdata.BranchId;
                VM_Emp_Detail.Branch = BranchModel;

               empBasicinfo = emp.GetAllEmployee(clientContext);
              //  VM_Emp_Detail.SelectetManagerID = VM_Emp_Detail.VMEmpdata.ManagerId;
                VM_Emp_Detail.SelectedEmpID = VM_Emp_Detail.VMEmpdata.ID;
                VM_Emp_Detail.Empdata = empBasicinfo;
                //  VM_Emp_Detail.FirstName = EmployeeModel;


            }
            return PartialView("_PV_Emp_Basic_Info", VM_Emp_Detail);
        }

        [SharePointContextFilter]
       
        public ActionResult SaveInfoEmp(Emp_BasicInfoModel EmpInfo)
        {
            string returnID = "0";
            //   string ID = Request.Cookies["ID"].Value.ToString();
            List<Emp_BasicInfoModel> empBasicinfo = (List<Emp_BasicInfoModel>)Session["managercode"];
            //   Emp_BasicInfoBal emp = new Emp_BasicInfoBal();
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
                        itemdata += "'CompanyId': " + EmpInfo.Company + ",";
                        itemdata += "'DesignationId': " + EmpInfo.Designation + ",";
                        itemdata += "'DepartmentId': " + EmpInfo.Department + ",";
                        itemdata += "'DivisionId': " + EmpInfo.Division + ",";
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


        //update Employee data
        /*  public ActionResult UpdateInfo(Emp_BasicInfoModel EmpInfo)
          {
          }  */

        /*     public ActionResult EmployeeEdit(Emp_BasicInfoModel empinfoedit)
         {
             int ID = Convert.ToInt32(TempData["mytempdata"]);
             return View();

         } */

     


        //save update data
      [SharePointContextFilter]
        public ActionResult UpdateInfo(Emp_BasicInfoModel EditEmpInfo)
        {
            string returnID = "0";
            string id = Session["EmpID"].ToString();
            //  string EmpId = Session["EMPID"].ToString();
            //   List<Emp_BasicInfoModel> empBasicinfo = (List<Emp_BasicInfoModel>)Session["manager"];

            //  string EditEmployeeID = Request.Cookies["EditEmployeeID"].Value;
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                /*  foreach (Emp_BasicInfoModel emparr in empBasicinfo)
                  {
                      if (emparr.UserNameId == EditEmpInfo.Manager)
                      {
                          EditEmpInfo.Manager = (emparr.ID).ToString();

                          EditEmpInfo.ManagerCode = emparr.EmpCode;
                      }
                  } */


              string itemdata = EditEmpInfo.FirstName == null ? "'FirstName' : null" : "'FirstName' : '" + EditEmpInfo.FirstName + "'";
               itemdata += EditEmpInfo.LastName == null ? ",'LastName' : null" : ",'LastName' : '" + EditEmpInfo.LastName + "'";

             // string itemdata = "'FirstName': '" + EditEmpInfo.FirstName + "'";


                //items += emp_Publication.Publication == null ? ",'Publication':null" : ",'Publication':'" + emp_Publication.Publication + "'";
                //   itemdata += "'MiddleName': '" + EditEmpInfo.MiddleName + "',";
             ///   itemdata += "'LastName': '" + EditEmpInfo.LastName + "'";
                /*   itemdata += "'JoiningDate': '" + EditEmpInfo.JoiningDate + "',";
                   // itemdata += "'JoiningDate': '" + dt.ToString("MM/dd/yyyy") + "',";
                   itemdata += "'DOB': '" + EditEmpInfo.DOB + "',";
                   itemdata += "'Gender': '" + EditEmpInfo.Gender + "',";
                   itemdata += "'MaritalStatus': '" + EditEmpInfo.MaritalStatus + "',";
                   itemdata += "'OnProbationTill': '" + EditEmpInfo.OnProbationTill + "',";
                   itemdata += "'ProbationStatus': '" + EditEmpInfo.ProbationStatus + "',";
                   itemdata += "'ManagerId': " + EditEmpInfo.Manager + ",";
                   itemdata += "'ManagerCode':  '" + EditEmpInfo.ManagerCode + "',";
                   itemdata += "'OfficeEmail': '" + EditEmpInfo.OfficeEmail + "',";
                   itemdata += "'ContactNumber': '" + EditEmpInfo.ContactNumber + "',";
                   itemdata += "'MobileNo': '" + EditEmpInfo.MobileNo + "',";
                   itemdata += "'CompanyId': " + EditEmpInfo.Company + ",";
                   itemdata += "'DesignationId': " + EditEmpInfo.Designation + ",";
                   itemdata += "'DepartmentId': " + EditEmpInfo.Department + ",";
                   itemdata += "'DivisionId': " + EditEmpInfo.Division + ",";
                   itemdata += "'RegionId': " + EditEmpInfo.Region + ",";
                   itemdata += "'BranchId':" + EditEmpInfo.Branch + ""; */

                       emp.UpdateEmp(clientContext, itemdata,id);
                //    VM_Emp_Detail.Empdata = emp.GetEmployeeById(clientContext, id);
                
            }
            return Json(returnID, JsonRequestBehavior.AllowGet);
         }





    }
}

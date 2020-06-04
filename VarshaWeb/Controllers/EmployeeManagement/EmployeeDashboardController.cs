﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VarshaWeb.BAL.EmployeeManagement;
using VarshaWeb.Models.EmployeeManagement;
using VarshaWeb.DAL;
using VarshaWeb.Models;
using VarshaWeb.BAL;

namespace VarshaWeb.Controllers.EmployeeManagement
{
    public class EmployeeDashboardController : Controller
    {
      

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
          
            List<Emp_BasicInfoModel> empview = new List<Emp_BasicInfoModel>();
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
           
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
               

                emp_Company = company.GetAllCompany(clientContext);
                emp_designation = designation.GetDesignation(clientContext);
                emp_department = department.GetAllDepartment(clientContext);
                emp_division = division.GetDivision(clientContext);
                emp_Branch = Branch.GetBranch(clientContext);
                emp_Region = Region.GetRegion(clientContext);

                empBasicinfo = emp.GetAllEmployee(clientContext);
                user = username.Getusergroup(clientContext);
                
                //emp_manager = empmanager.GetManager(clientContext);
            }
            ViewBag.company = emp_Company;
            ViewBag.designation = emp_designation;
            ViewBag.department = emp_department;
            ViewBag.division = emp_division;
            ViewBag.Branch = emp_Branch;
            ViewBag.Region = emp_Region;
            ViewBag.emp = empBasicinfo;
            ViewBag.username = user;
             Session["managercode"] = empBasicinfo;
            // ViewBag.empmanager = emp_manager;
            return View();
        }
        [HttpPost]
      

        public ActionResult SaveInfo(Emp_BasicInfoModel EmpInfo)
        {
           string returnID = "0";
            //   string ID = Request.Cookies["ID"].Value.ToString();
            List<Emp_BasicInfoModel> empBasicinfo = (List<Emp_BasicInfoModel>)Session["managercode"];

         //   Emp_BasicInfoBal emp = new Emp_BasicInfoBal();

            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                foreach(Emp_BasicInfoModel emparr in empBasicinfo)
                {
                    if (emparr.UserNameId == EmpInfo.Manager)
                    {
                        EmpInfo.Manager = (emparr.ID).ToString();
                      
                        EmpInfo.ManagerCode = emparr.EmpCode;
                    }
                    }

                    
                    

                    {

                        string itemdata = " 'FirstName' : '" + EmpInfo.FirstName + "',";
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
                    //itemdata += "'ManagerId': '" + EmpInfo.Manager + "',";
                        itemdata += "'ManagerCode':  '" + EmpInfo.ManagerCode + "',";

                        itemdata += "'OfficeEmail': '" + EmpInfo.OfficeEmail + "',";
                        itemdata += "'ContactNumber': '" + EmpInfo.ContactNumber + "',";
                       // itemdata += "'EmpStatus': '" + EmpInfo.EmpStatus + "',";
                        itemdata += "'CompanyId': '" + EmpInfo.Company + "',";
                        itemdata += "'DesignationId': '" + EmpInfo.Designation + "',";
                        itemdata += "'DepartmentId': '" + EmpInfo.Department + "',";
                        itemdata += "'DivisionId': '" + EmpInfo.Division + "',";
                        itemdata += "'RegionId': '" + EmpInfo.Region + "',";
                        itemdata += "'BranchId': '" + EmpInfo.Branch + "',";
                    itemdata += "'User_NameId': " + EmpInfo.User_Name + "";
                   
                        emp.saveEmp(clientContext, itemdata);
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
       [HttpGet]
        public ActionResult EmployeeEdit()
        {


            //   TempData.Add("mytempdata", ID);
            //  return Json(ID, JsonRequestBehavior.AllowGet);

             string EditEmployeeID = Request.Cookies["EditEmployeeID"].Value;
              Emp_BasicInfoBal EmpDashBal = new Emp_BasicInfoBal();

              List<Emp_BasicInfoModel> empEdit = new List<Emp_BasicInfoModel>();
              var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
              using (var clientContext = spContext.CreateUserClientContextForSPHost())
              {
                  empEdit = EmpDashBal.GeEmployeeById(clientContext, EditEmployeeID);

              }
              ViewBag.Empdata = empEdit;

              return View(); 

           /* string empid = Request.Cookies["ID"].Value.ToString();
            
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                empBasicinfo = emp.GeEmployeeById(clientContext, empid.ToString());
            }
            var empdata = empBasicinfo.Where(s => s.ID == Convert.ToInt32(empid)).FirstOrDefault();
            return View(empdata); */

        }



    }

}





























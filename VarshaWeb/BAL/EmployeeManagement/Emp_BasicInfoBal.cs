using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VarshaWeb.Models.EmployeeManagement;
using VarshaWeb.DAL;
using VarshaWeb.Models;

namespace VarshaWeb.BAL.EmployeeManagement
{
    public class Emp_BasicInfoBal
    {
            public string saveEmp(ClientContext clientContext, string ItemData)
            {

                string response = RESTSave(clientContext, ItemData);

                return response;
            }

            public string UpdateEmp(ClientContext clientContext, string ItemData, string ID)
            {
            string response = RESTUpdate(clientContext, ItemData, ID);
            return response;
            }

        public List<Emp_BasicInfoModel> GetManager(ClientContext clientContext)
        {
            List<Emp_BasicInfoModel> emp_Manager = new List<Emp_BasicInfoModel>();

            JArray jArray = RESTGet(clientContext, null);

            foreach (JObject j in jArray)
            {
                emp_Manager.Add(new Emp_BasicInfoModel
                {
                    ManagerId = Convert.ToInt32(j["Manager"]["Id"]),
                    Manager = j["Manager"]["FirstName"] == null ? "" : j["Manager"]["FirstName"].ToString(),
                });
            }


            return emp_Manager;

        }
        public List<Emp_BasicInfoModel> GetAllEmployee(ClientContext clientContext)
        {
            List<Emp_BasicInfoModel> EmpBasicinfo = new List<Emp_BasicInfoModel>();



            JArray jArray = RESTGet(clientContext, null);



            foreach (JObject j in jArray)
            {
                EmpBasicinfo.Add(new Emp_BasicInfoModel
                {
                     ID= Convert.ToInt32(j["ID"]),
                  
                    FirstName = j["FirstName"] == null ? "" : j["FirstName"].ToString(),
                    Company = j["Company"]["CompanyName"] == null ? "" : j["Company"]["CompanyName"].ToString(),
                    EmpCode = j["EmpCode"] == null ? "" : j["EmpCode"].ToString(),
                    Designation = j["Designation"]["Designation"] == null ? "" : j["Designation"]["Designation"].ToString(),
                    Department =j["Department"]["DepartmentName"] == null ? "" : j["Department"]["DepartmentName"].ToString(),
                  
                    ManagerId = Convert.ToInt32(j["Manager"]["Id"]),
                    Manager = j["Manager"]["FirstName"] == null ? "" : j["Manager"]["FirstName"].ToString(),
                    ManagerCode = j["Manager"]["EmpCode"] == null ? "" : j["Manager"]["EmpCode"].ToString(),
                    UserNameId = j["User_Name"]["Id"] == null ? "" : Convert.ToString(j["User_Name"]["Id"]),
                    User_Name = j["User_Name"]["Title"] == null ? "" : Convert.ToString(j["User_Name"]["Title"]).Trim(),
                   
                });
            }
            return EmpBasicinfo;

        }


       

        public List<Emp_BasicInfoModel> GetEmployeeById(ClientContext clientContext, string empid)
        {
            List<Emp_BasicInfoModel> empdetails = new List<Emp_BasicInfoModel>();
            string filter = "ID eq '" + empid + "'";
          //  string filter = "ID eq " + empid;
            JArray jArray = RESTGet(clientContext, filter);
            foreach (JObject j in jArray)
            {
               DateTime dt = Convert.ToDateTime(j["JoiningDate"]);
               DateTime dt1 = Convert.ToDateTime(j["DOB"]);
                DateTime dt2 = Convert.ToDateTime(j["OnProbationTill"]);
                empdetails.Add(new Emp_BasicInfoModel
                {
                 
                    ID = Convert.ToInt32(j["ID"]),
                    FirstName = j["FirstName"] == null ? "" : j["FirstName"].ToString(),
                    MiddleName = j["MiddleName"] == null ? "" : j["MiddleName"].ToString(),
                    LastName = j["LastName"] == null ? "" : j["LastName"].ToString(),
                    EmpCode = j["EmpCode"] == null ? "" : j["EmpCode"].ToString(),
                    JoiningDate = dt.ToString("dd/MM/yyyy"),
                    DOB= dt1.ToString("dd/MM/yyyy"),
                    Gender = j["Gender"] == null ? "" : j["Gender"].ToString(),
                    MaritalStatus = j["MaritalStatus"] == null ? "" : j["MaritalStatus"].ToString(),
                    OnProbationTill= dt2.ToString("dd/MM/yyyy"),
                    ProbationStatus = j["ProbationStatus"] == null ? "" : j["ProbationStatus"].ToString(),
                    ManagerId = Convert.ToInt32(j["Manager"]["Id"]),
                    Manager = j["Manager"]["FirstName"] == null ? "" : j["Manager"]["FirstName"].ToString(),
                  //  Manager = j["Manager"]["FirstName"] == null ? "" : j["Manager"]["FirstName"].ToString(),
                    ManagerCode = j["Manager"]["EmpCode"] == null ? "" : j["Manager"]["EmpCode"].ToString(),
                    OfficeEmail = j["OfficeEmail"] == null ? "" : j["OfficeEmail"].ToString(),
                    ContactNumber = j["ContactNumber"] == null ? "" : j["ContactNumber"].ToString(),
                    EmpStatus = j["EmpStatus"] == null ? "" : j["EmpStatus"].ToString(),
                    CompanyId = Convert.ToInt32(j["Company"]["Id"]),
                    Company = j["Company"]["CompanyName"] == null ? "" : j["Company"]["CompanyName"].ToString(),
                    DesignationId = Convert.ToInt32(j["Designation"]["Id"]),
                    Designation = j["Designation"]["Designation"] == null ? "" : j["Designation"]["Designation"].ToString(),
                    DepartmentId = Convert.ToInt32(j["Department"]["Id"]),
                    Department = j["Department"]["DepartmentName"] == null ? "" : j["Department"]["DepartmentName"].ToString(),
                    DivisionId = Convert.ToInt32(j["Division"]["Id"]),
                    Division = j["Division"]["Division"] == null ? "" : j["Division"]["Division"].ToString(),
                    RegionId = Convert.ToInt32(j["Region"]["Id"]),
                    Region = j["Region"]["Region"] == null ? "" : j["Region"]["Region"].ToString(),
                    BranchId = Convert.ToInt32(j["Branch"]["Id"]),
                    Branch = j["Branch"]["Branch"] == null ? "" : j["Branch"]["Branch"].ToString(),
                    MobileNo = j["MobileNo"] == null ? "" : j["MobileNo"].ToString(),
                    UserNameId = j["User_Name"]["Id"] == null ? "" : Convert.ToString(j["User_Name"]["Id"]),
                    User_Name = j["User_Name"] ["Title"] == null ? "" : j["User_Name"]["Title"].ToString(),
                });
            }
            return empdetails;

        }

        private JArray RESTGet(ClientContext clientContext, string filter)
            {
                RestService restService = new RestService();
                JArray jArray = new JArray();
                RESTOption rESTOption = new RESTOption();
                rESTOption.filter = filter;
                rESTOption.select = "ID,FirstName,MiddleName,LastName,EmpCode,Gender,MaritalStatus,DOB,JoiningDate,OnProbationTill,ProbationStatus,OfficeEmail,ContactNumber,EmpStatus,Designation/Id,Designation/Designation,Department/Id,Department/DepartmentName,Division/Id,Division/Division,Region/Id,Region/Region,Branch/Id,Branch/Branch,Company/Id,Company/CompanyName,Manager/Id,Manager/FirstName,Manager/EmpCode,User_Name/Id,User_Name/Title,Profile_Pic_Url,Facebook,Google,LinkedIn,Twitter,Instagram,Tiktok,DOB_Months,ManagerCode,JoiningDate_Month,MobileNo";
                rESTOption.expand = "Company,Designation,Department,Division,Region,Branch,User_Name,Manager";
                rESTOption.top = "5000";
                jArray = restService.GetAllItemFromList(clientContext, "Emp_BasicInfo", rESTOption);
                return jArray;
            }

        

            private string RESTSave(ClientContext clientContext, string ItemData)
            {
                RestService restService = new RestService();

                return restService.SaveItem(clientContext, "Emp_BasicInfo", ItemData);
            }

            private string RESTUpdate(ClientContext clientContext, string ItemData, string ID)
            {
                 RestService restService = new RestService();

                 return restService.UpdateItem(clientContext, "Emp_BasicInfo", ItemData, ID);
            }
    }
    }

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
    public class EmployeeDashboard
    {
            public string saveEmp(ClientContext clientContext, string ItemData)
            {

                string response = RESTSave(clientContext, ItemData);

                return response;
            }

        public List<Emp_BasicInfoModels> GetAllEmployee(ClientContext clientContext)
        {
            List<Emp_BasicInfoModels> EmpBasicinfo = new List<Emp_BasicInfoModels>();



            JArray jArray = RESTGet(clientContext, null);



            foreach (JObject j in jArray)
            {
                EmpBasicinfo.Add(new Emp_BasicInfoModels
                {
                         //  ID= Convert.ToInt32(j["ID"]),
                  
                    FirstName = j["FirstName"].ToString(),
                    MiddleName = j["MiddleName"].ToString(),
                    LastName = j["LastName"].ToString(),
                    Company = j["Company"]["CompanyName"].ToString(),
                    // EmpCode = j["EmpCode"].ToString(),
                    EmpCode = j["EmpCode"].ToString(),
                    Designation = j["Designation"]["Designation"].ToString(),
                    Department = j["Department"].ToString(),
                    //  Department = j["Department"].ToString(),




                });
            }




            return EmpBasicinfo;



        }


        private JArray RESTGet(ClientContext clientContext, string filter)
            {
                RestService restService = new RestService();
                JArray jArray = new JArray();
                RESTOption rESTOption = new RESTOption();
                rESTOption.filter = filter;
            rESTOption.select = "ID,FirstName,MiddleName,LastName,EmpCode,Gender,MaritalStatus,DOB,JoiningDate,OnProbationTill,ProbationStatus,OfficeEmail,ContactNumber,EmpStatus,Designation/Id,Designation/Designation,Department/Id,Department/DepartmentName,Division/Id,Division/Division,Region/Id,Region/Region,Branch/Id,Branch/Branch,Company/Id,Company/CompanyName,Manager/Id,User_Name/Id,User_Name/Title";
            rESTOption.expand = "Company,Designation,Department,Division,Region,Branch,User_Name,Manager";
            rESTOption.top = "5000";


                jArray = restService.GetAllItemFromList(clientContext, "Emp_BasicInfo", rESTOption);

                return jArray;
            }

         /*   private JArray RESTGetByID(ClientContext clientContext, string ID)
            {
                RestService restService = new RestService();
                JArray jArray = new JArray();
                RESTOption rESTOption = new RESTOption();

            rESTOption.select = "ID,FirstName,MiddleName,LastName,EmpCode,Gender,MaritalStatus,DOB,JoiningDate,OnProbationTill,ProbationStatus,OfficeEmail,ContactNumber,EmpStatus,Designation/Id,Designation/Designation,Department/Id,Department/DepartmentName,Division/Id,Division/Division,Region/Id,Region/Region,Branch/Id,Branch/Branch,Manager/Id,Manager/Title,Company/Id,Company/CompanyName";
            rESTOption.expand = "Company,Designation,Manager,Department,Division,Region,Branch";
            rESTOption.top = "5000";


                jArray = restService.GetItemByID(clientContext, "Emp_BasicInfo", rESTOption, ID);

                return jArray;
            } */




            private string RESTSave(ClientContext clientContext, string ItemData)
            {
                RestService restService = new RestService();

                return restService.SaveItem(clientContext, "Emp_BasicInfo", ItemData);
            }



        }
    }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Web.Mvc;
using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VarshaWeb.Models.EmployeeManagement;
using VarshaWeb.DAL;
using VarshaWeb.Models;
using System.Collections;

namespace VarshaWeb.BAL.EmployeeManagement
{
    public class Emp_DepartmentBal
    {
        public List<Emp_DepartmentModel> GetAllDepartment(ClientContext clientContext)
        {
            List<Emp_DepartmentModel> emp_department = new List<Emp_DepartmentModel>();

            JArray jArray = RESTGet(clientContext, null);

            foreach (JObject j in jArray)
            {
                emp_department.Add(new Emp_DepartmentModel
                {
                    ID = Convert.ToInt32(j["ID"]),
                    DepartmentName = j["DepartmentName"].ToString(),
                });
            }


            return emp_department;

        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,DepartmentName,HeadOfDepartment/Id,Description";
            rESTOption.expand = "HeadOfDepartment";
             rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_Department", rESTOption);

            return jArray;
        }

        
    }
}
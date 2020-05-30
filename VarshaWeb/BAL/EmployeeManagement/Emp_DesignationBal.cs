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

namespace VarshaWeb.BAL.EmployeeManagement
{
    public class Emp_DesignationBal
    {
        public List<Emp_DesignationModels> GetDesignation(ClientContext clientContext)
        {
            List<Emp_DesignationModels> emp_designation = new List<Emp_DesignationModels>();

            JArray jArray = RESTGet(clientContext, null);

            foreach (JObject j in jArray)
            {
                emp_designation.Add(new Emp_DesignationModels
                {
                    ID = Convert.ToInt32(j["ID"]),
                    Designation = j["Designation"].ToString(),
                });
            }


            return emp_designation;

        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,Designation,RoleDescription";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_Designation", rESTOption);

            return jArray;
        }
    }
}
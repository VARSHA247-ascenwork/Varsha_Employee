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
    public class Emp_GradeBal
    {
        public List<Emp_GradeModel> GetGrade(ClientContext clientContext)
        {
            List<Emp_GradeModel> GradeModel = new List<Emp_GradeModel>();
            JArray jArray = RESTGet(clientContext, null);
            foreach (JObject j in jArray)
            {
                GradeModel.Add(new Emp_GradeModel
                {
                    ID = Convert.ToInt32(j["ID"]),
                    Grade= j["Grade"].ToString(),
                });
            }
            return GradeModel;
        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();
            rESTOption.filter = filter;
            rESTOption.select = "ID,Grade,Description";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_Grade", rESTOption);
            return jArray;
        }
    }
}
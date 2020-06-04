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
    public class Emp_DivisionBal
    {
        public List<Emp_DivisionModel> GetDivision(ClientContext clientContext)
        {
            List<Emp_DivisionModel> emp_division = new List<Emp_DivisionModel>();

            JArray jArray = RESTGet(clientContext, null);

            foreach (JObject j in jArray)
            {
                emp_division.Add(new Emp_DivisionModel
                {
                    ID = Convert.ToInt32(j["ID"]),
                    Division = j["Division"].ToString(),
                });
            }


            return emp_division;

        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,Division,HeadOfDivision,Description";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_Division", rESTOption);

            return jArray;
        }
    }
}
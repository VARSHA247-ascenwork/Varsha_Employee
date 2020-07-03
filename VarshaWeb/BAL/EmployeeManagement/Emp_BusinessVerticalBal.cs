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
    public class Emp_BusinessVerticalBal
    {
        public List<Emp_BusinessVerticalModel> GetBusinessVertical(ClientContext clientContext)
        {
            List<Emp_BusinessVerticalModel> Vertical_model = new List<Emp_BusinessVerticalModel>();

            JArray jArray = RESTGet(clientContext, null);

            foreach (JObject j in jArray)
            {
                Vertical_model.Add(new Emp_BusinessVerticalModel
                {
                    ID = Convert.ToInt32(j["ID"]),
                    BusinessVertical = j["BusinessVertical"].ToString(),
                });
            }
            return Vertical_model;
        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();
            rESTOption.filter = filter;
            rESTOption.select = "ID,BusinessVertical,BusinessVerticalHead,Description";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_BusinessVertical", rESTOption);
            return jArray;
        }
    }
}
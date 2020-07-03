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
    public class Emp_CostCentreBal
    {
        public List<Emp_CostCentreModel> GetCostCenter(ClientContext clientContext)
        {
            List<Emp_CostCentreModel> Cost_center = new List<Emp_CostCentreModel>();
            JArray jArray = RESTGet(clientContext, null);
            foreach (JObject j in jArray)
            {
                Cost_center.Add(new Emp_CostCentreModel
                {
                    ID = Convert.ToInt32(j["ID"]),
                    CostCenter= j["CostCentre"].ToString(),
                });
            }
            return Cost_center;
        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();
            rESTOption.filter = filter;
            rESTOption.select = "ID,CostCentre,Description";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_CostCentre", rESTOption);
            return jArray;
        }
    }
}
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
    public class Emp_RegionBal
    {
        public List<Emp_RegionModel> GetRegion(ClientContext clientContext)
        {
            List<Emp_RegionModel> emp_Region = new List<Emp_RegionModel>();

            JArray jArray = RESTGet(clientContext, null);

            foreach (JObject j in jArray)
            {
                emp_Region.Add(new Emp_RegionModel
                {
                    ID = Convert.ToInt32(j["ID"]),
                    Region = j["Region"].ToString(),
                });
            }


            return emp_Region;

        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,Region,HeadOfRegion,Description";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_Region", rESTOption);

            return jArray;
        }
    }
}
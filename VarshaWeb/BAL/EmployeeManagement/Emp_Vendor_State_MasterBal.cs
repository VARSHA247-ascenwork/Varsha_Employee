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
    public class Emp_Vendor_State_MasterBal
    {
        public List<Emp_Vendor_State_MasterModel> GetState(ClientContext clientContext)
        {
            List<Emp_Vendor_State_MasterModel> state = new List<Emp_Vendor_State_MasterModel>();

            JArray jArray = RESTGet(clientContext, null);

            foreach (JObject j in jArray)
            {
                state.Add(new Emp_Vendor_State_MasterModel
                {
                    ID = Convert.ToInt32(j["ID"]),
                    StateName= j["StateName"].ToString(),
                });
            }
            return state;
        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,StateName";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_Vendor_State_Master", rESTOption);

            return jArray;
        }
    }
}
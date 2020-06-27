using Microsoft.SharePoint.Client;
using Newtonsoft.Json.Linq;
using VarshaWeb.DAL;
using VarshaWeb.Models;
using VarshaWeb.Models.EmployeeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarshaWeb.BAL.EmployeeManagement
{
    public class Emp_StateMasterBal
    {
        public List<Emp_StateMasterModel> GetStateById(ClientContext clientContext,string countryid)
        {
            List<Emp_StateMasterModel> emp_StateMasters = new List<Emp_StateMasterModel>();
            string filter = "CountryName/Id eq " + countryid;
            JArray jArray = RESTGet(clientContext, filter);
            if (jArray.Count > 0)
            {
                foreach (JObject j in jArray)
                {
                    emp_StateMasters.Add(new Emp_StateMasterModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        StateName = j["StateName"].ToString(),
                        CountryName = j["CountryName"]["CountryName"].ToString(),
                        CountryNameID = j["CountryName"].ToString(),
                    });
                }

            }
            return emp_StateMasters;

        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,StateName,CountryName/CountryName,CountryName/Id";
            rESTOption.expand = "CountryName";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_StateMaster", rESTOption);

            return jArray;
        }
    }
}

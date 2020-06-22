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
    public class Emp_CountryBal
    {
        public List<Emp_CountryModel> GetAllCountries(ClientContext clientContext)
        {
            List<Emp_CountryModel> emp_Countries = new List<Emp_CountryModel>();

            JArray jArray = RESTGet(clientContext, null);

            foreach (JObject j in jArray)
            {
                emp_Countries.Add(new Emp_CountryModel
                {
                    ID = Convert.ToInt32(j["ID"]),
                    CountryName = j["CountryName"].ToString(),
                });
            }


            return emp_Countries;

        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,CountryName";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_Country", rESTOption);

            return jArray;
        }
    }
}
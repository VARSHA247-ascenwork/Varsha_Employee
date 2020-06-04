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
    public class Emp_CompanyMasterBal
    {
        public List<Emp_CompanyMasterModel> GetAllCompany(ClientContext clientContext)
        {
            List<Emp_CompanyMasterModel> emp_Company = new List<Emp_CompanyMasterModel>();

            JArray jArray = RESTGet(clientContext, null);

            foreach (JObject j in jArray)
            {
                emp_Company.Add(new Emp_CompanyMasterModel
                {
                    ID = Convert.ToInt32(j["ID"]),
                    CompanyName = j["CompanyName"].ToString(),
                });
            }


            return emp_Company;

        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,CompanyName,CompanyOwner,CompanyDescription,ContactPerson,ContactNumber,Website,EmailID,GSTNumber,PANNumber,City,State,Country/Id,Country/CountryName";
            rESTOption.expand = "Country";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_CompanyMaster", rESTOption);

            return jArray;
        }
    }
}
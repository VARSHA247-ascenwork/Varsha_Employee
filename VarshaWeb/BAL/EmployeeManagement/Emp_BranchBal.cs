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
    public class Emp_BranchBal
    {
        public List<Emp_BranchModel> GetBranch(ClientContext clientContext)
        {
            List<Emp_BranchModel> emp_branch = new List<Emp_BranchModel>();

            JArray jArray = RESTGet(clientContext, null);

            foreach (JObject j in jArray)
            {
                emp_branch.Add(new Emp_BranchModel
                {
                    ID = Convert.ToInt32(j["ID"]),
                    Branch= j["Branch"].ToString(),
                });
            }


            return emp_branch;

        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,Branch,HeadOfBranch,Address1,Address2,PostalCode,Description,State,City,Country/Id,Country/CountryName";
            rESTOption.expand = "Country";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_Branch", rESTOption);

            return jArray;
        }
    }
}
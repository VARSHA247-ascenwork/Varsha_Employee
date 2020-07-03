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
    public class Emp_RoleBal
    {
        public List<Emp_RoleModel> GetRole(ClientContext clientContext)
        {
            List<Emp_RoleModel> RoleModel = new List<Emp_RoleModel>();
            JArray jArray = RESTGet(clientContext, null);
            foreach (JObject j in jArray)
            {
                RoleModel.Add(new Emp_RoleModel
                {
                    ID = Convert.ToInt32(j["ID"]),
                    Role = j["Role"].ToString(),
                });
            }
            return RoleModel;

        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();
            rESTOption.filter = filter;
            rESTOption.select = "ID,Role,Description";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_Role", rESTOption);
            return jArray;
        }
    }
}
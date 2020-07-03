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
    public class Emp_ProductBal
    {
        public List<Emp_ProductModel> GetProduct(ClientContext clientContext)
        {
            List<Emp_ProductModel> ProductModel = new List<Emp_ProductModel>();
            JArray jArray = RESTGet(clientContext, null);
            foreach (JObject j in jArray)
            {
                ProductModel.Add(new Emp_ProductModel
                {
                    ID = Convert.ToInt32(j["ID"]),
                    Product= j["Product"].ToString(),
                });
            }
            return ProductModel;
        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();
            rESTOption.filter = filter;
            rESTOption.select = "ID,Product,Description";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_Product", rESTOption);
            return jArray;
        }
    }
}
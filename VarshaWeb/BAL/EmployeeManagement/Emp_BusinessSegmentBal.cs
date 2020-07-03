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
    public class Emp_BusinessSegmentBal
    {
        public List<Emp_BusinessSegmentModel> GetBusinessSegment(ClientContext clientContext)
        {
            List<Emp_BusinessSegmentModel> Segment_model = new List<Emp_BusinessSegmentModel>();

            JArray jArray = RESTGet(clientContext, null);

            foreach (JObject j in jArray)
            {
                Segment_model.Add(new Emp_BusinessSegmentModel
                {
                    ID = Convert.ToInt32(j["ID"]),
                    BusinessSegment = j["BusinessSegment"].ToString(),
                });
            }
            return Segment_model;
        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();
            rESTOption.filter = filter;
            rESTOption.select = "ID,BusinessSegment,Description";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_BusinessSegment", rESTOption);
            return jArray;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VarshaWeb.Models.EmployeeManagement;
using VarshaWeb.DAL;
using VarshaWeb.Models;

namespace VarshaWeb.BAL.EmployeeManagement
{
    public class Emp_Vendor_DocumentBal
    {
        public List<Emp_Vendor_DocumentModel> GetVendorDocumentById(ClientContext clientContext, string id, string path)
        {

            List<Emp_Vendor_DocumentModel> VendorDocument = new List<Emp_Vendor_DocumentModel>();
            var filter = "libraryIdId eq " + id;
            JArray jArray = RESTGet(clientContext, filter);
            foreach (JObject j in jArray)
            {
                VendorDocument.Add(new Emp_Vendor_DocumentModel
                {
                    ID = Convert.ToInt32(j["ID"]),
                    Name = path + j["File"]["ServerRelativeUrl"].ToString(),
                    libraryId = j["libraryId"]["VendorName"].ToString(),
                    DocumentPath = j["DocumentPath"].ToString(),
                });
            }
            return VendorDocument;
        }
         private JArray RESTGet(ClientContext clientContext, string filter)
         {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "*,libraryId/ID,libraryId/VendorName,File";
            rESTOption.expand = "File,libraryId";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_Vendor_Document", rESTOption);

            return jArray;
         }
    }
 }

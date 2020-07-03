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
    public class Emp_Client_DocumentBal
    {
        public List<Emp_Client_DocumentModel> GetClientDocumentById(ClientContext clientContext, string id,string path)
        {
            List<Emp_Client_DocumentModel> emp_Client_Documents = new List<Emp_Client_DocumentModel>();
            var filter = "libraryIdId eq " + id;
            JArray jArray = RESTGet(clientContext, filter);
            if (jArray.Count() > 0)
            {
                foreach (JObject j in jArray)
                {
                    emp_Client_Documents.Add(new Emp_Client_DocumentModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        Name = path + j["File"]["ServerRelativeUrl"].ToString(),
                        libraryId = j["libraryId"]["ClientName"].ToString(),
                        DocumentPath = j["DocumentPath"].ToString(),
                    });
                }
            }
            return emp_Client_Documents;
        }
        public string DeleteClientDocument(ClientContext client, string id)
        {
            RestService restService = new RestService();
            string delete = restService.DeleteItem(client, "Emp_Client_Document", id);
            return delete;
        }
        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();
            rESTOption.filter = filter;
            rESTOption.select = "*,libraryId/ID,libraryId/ClientName,File";
            rESTOption.expand = "File,libraryId";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_Client_Document", rESTOption);
            return jArray;
        }
    }
}
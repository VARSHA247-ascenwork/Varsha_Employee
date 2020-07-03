using Microsoft.SharePoint.Client;
using Newtonsoft.Json.Linq;
using WizrrWeb.DAL;
using WizrrWeb.Models;
using WizrrWeb.Models.EmployeeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WizrrWeb.BAL.EmployeeManagement
{
    public class EmpProfilePicBal
    {
        public List<EmpProfilePicModel> GetEmpProfileById(ClientContext clientContext, string id, string path)
        {

            List<EmpProfilePicModel> emp_Client_Documents = new List<EmpProfilePicModel>();
            var filter = "EmpCodeIdId eq " + id;
            JArray jArray = RESTGet(clientContext, filter);
            if (jArray.Count > 0)
            {
                foreach (JObject j in jArray)
                {
                    emp_Client_Documents.Add(new EmpProfilePicModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        Name = path + j["File"]["ServerRelativeUrl"].ToString(),
                        EmpCodeId = j["EmpCodeId"]["ID"].ToString(),
                        DocumentPath = j["DocumentPath"].ToString(),
                    });
                }
            }
            return emp_Client_Documents;
        }
        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "*,EmpCodeId/ID,EmpCodeId/FirstName,File";
            rESTOption.expand = "File,EmpCodeId";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "EmpProfilePic", rESTOption);

            return jArray;
        }
    }
}
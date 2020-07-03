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
    public class Emp_DisciplinaryDetailsBal
    {
        #region Get Employee Disciplinary Details By Employee ID Method
        public List<Emp_DisciplinaryDetailsModel> GetDisciplinaryDetailsByEmpID(ClientContext clientContext, string EmpId)
        {
            List<Emp_DisciplinaryDetailsModel> emp_DisciplinaryDetails = new List<Emp_DisciplinaryDetailsModel>();
            string filter = "EmpCodeId eq " + EmpId;
            JArray jArray = RESTGet(clientContext, filter);
            if (jArray.Count > 0)
            {
                foreach (JObject j in jArray)
                {
                    emp_DisciplinaryDetails.Add(new Emp_DisciplinaryDetailsModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        Description = j["Description"] == null ? "" : j["Description"].ToString(),
                        ActionOn = j["ActionOn"] == null ? "" : j["ActionOn"].ToString(),
                        EmpCode = j["EmpCode"] == null ? "" : j["EmpCode"].ToString()
                    });
                }
            }
            return emp_DisciplinaryDetails;
        }
        #endregion

        #region Get Employee Disciplinary Details By ID Method
        public List<Emp_DisciplinaryDetailsModel> GetDisciplinaryDetailsByID(ClientContext clientContext, string Id)
        {
            List<Emp_DisciplinaryDetailsModel> emp_DisciplinaryDetails = new List<Emp_DisciplinaryDetailsModel>();
            string filter = "ID eq " + Id;
            JArray jArray = RESTGet(clientContext, filter);
            if (jArray.Count > 0)
            {
                foreach (JObject j in jArray)
                {
                    emp_DisciplinaryDetails.Add(new Emp_DisciplinaryDetailsModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        Description = j["Description"] == null ? "" : j["Description"].ToString(),
                        ActionOn = j["ActionOn"] == null ? "" : j["ActionOn"].ToString(),
                        EmpCode = j["EmpCode"] == null ? "" : j["EmpCode"].ToString()
                    });
                }
            }
            return emp_DisciplinaryDetails;
        }
        #endregion

        public string SaveDisciplinaryDetails(ClientContext clientContext, string ItemData)
        {

            string response = RESTSave(clientContext, ItemData);

            return response;
        }

        public string UpdateDisciplinaryDetails(ClientContext clientContext, string ItemData)
        {

            string response = RESTSave(clientContext, ItemData);

            return response;
        }

        public string DeleteDisciplinaryDetails(ClientContext client, string id)
        {
            RestService restService = new RestService();
            string delete = restService.DeleteItem(client, "Emp_DisciplinaryDetails", id);
            return delete;
        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,Description,ActionOn,EmpCode/Id,EmpCode/EmpCode";
            rESTOption.expand = "EmpCode";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_DisciplinaryDetails", rESTOption);
            return jArray;
        }

        private string RESTSave(ClientContext clientContext, string ItemData)
        {
            RestService restService = new RestService();

            return restService.SaveItem(clientContext, "Emp_DisciplinaryDetails", ItemData);
        }

        private string RESTUpdate(ClientContext clientContext, string ItemData, string ID)
        {
            RestService restService = new RestService();

            return restService.UpdateItem(clientContext, "Emp_DisciplinaryDetails", ItemData, ID);
        }

    }
}
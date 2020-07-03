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
    public class Emp_EmergencyContactBal
    {
        #region Get Employee Emergency Contact Details By Employee ID Method
        public List<Emp_EmergencyContactModel> GetEmergencyContactByEmpID(ClientContext clientContext, string EmpId)
        {
            List<Emp_EmergencyContactModel> emp_EmergencyContacts = new List<Emp_EmergencyContactModel>();
            string filter = "EmpCodeId eq " + EmpId;
            JArray jArray = RESTGet(clientContext, filter);

            if (jArray.Count > 0)
            {
                foreach (JObject j in jArray)
                {
                    emp_EmergencyContacts.Add(new Emp_EmergencyContactModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        Address = j["Address"] == null ? "" : j["Address"].ToString(),
                        AlternatePhoneNumber = j["AlternatePhoneNumber"] == null ? "" : j["AlternatePhoneNumber"].ToString(),
                        PersonName = j["PersonName"] == null ? "" : j["PersonName"].ToString(),
                        PrimaryPhoneNumber = j["PrimaryPhoneNumber"] == null ? "" : j["PrimaryPhoneNumber"].ToString(),
                        Relationship = j["Relationship"] == null ? "" : j["Relationship"].ToString()
                    });
                }
            }
            return emp_EmergencyContacts;
        }
        #endregion

        #region Get Employee Emergency Contact Details By ID Method
        public List<Emp_EmergencyContactModel> GetEmergencyContactByID(ClientContext clientContext, string Id)
        {
            List<Emp_EmergencyContactModel> emp_EmergencyContacts = new List<Emp_EmergencyContactModel>();
            string filter = "ID eq " + Id;
            JArray jArray = RESTGet(clientContext, filter);

            if (jArray.Count > 0)
            {
                foreach (JObject j in jArray)
                {
                    emp_EmergencyContacts.Add(new Emp_EmergencyContactModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        Address = j["Address"] == null ? "" : j["Address"].ToString(),
                        AlternatePhoneNumber = j["AlternatePhoneNumber"] == null ? "" : j["AlternatePhoneNumber"].ToString(),
                        PersonName = j["PersonName"] == null ? "" : j["PersonName"].ToString(),
                        PrimaryPhoneNumber = j["PrimaryPhoneNumber"] == null ? "" : j["PrimaryPhoneNumber"].ToString(),
                        Relationship = j["Relationship"] == null ? "" : j["Relationship"].ToString()
                    });
                }
            }
            return emp_EmergencyContacts;
        }
        #endregion

        public string SaveEmergencyContact(ClientContext clientContext, string ItemData)
        {

            string response = RESTSave(clientContext, ItemData);

            return response;
        }

        public string UpdateEmergencyContact(ClientContext clientContext, string ItemData, string ID)
        {

            string response = RESTUpdate(clientContext, ItemData, ID);

            return response;
        }

        public string DeleteEmergencyContact(ClientContext client, string id)
        {
            RestService restService = new RestService();
            string delete = restService.DeleteItem(client, "Emp_EmergencyContact", id);
            return delete;
        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,PersonName,Relationship,PrimaryPhoneNumber,AlternatePhoneNumber,Address,EmpCode/Id,EmpCode/EmpCode";
            rESTOption.expand = "EmpCode";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_EmergencyContact", rESTOption);
            return jArray;
        }

        private string RESTSave(ClientContext clientContext, string ItemData)
        {
            RestService restService = new RestService();

            return restService.SaveItem(clientContext, "Emp_EmergencyContact", ItemData);
        }

        private string RESTUpdate(ClientContext clientContext, string ItemData, string ID)
        {
            RestService restService = new RestService();

            return restService.UpdateItem(clientContext, "Emp_EmergencyContact", ItemData, ID);
        }

    }
}
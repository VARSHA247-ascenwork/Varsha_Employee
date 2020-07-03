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
    public class Emp_ReferenceDetailsBal
    {
        #region Get Employee Reference Details By Employee ID Method
        public List<Emp_ReferenceDetailsModel> GetReferenceDetailsByEmpID(ClientContext clientContext, string EmpId)
        {
            List<Emp_ReferenceDetailsModel> emp_ReferenceDetails = new List<Emp_ReferenceDetailsModel>();
            string filter = "EmpCodeId eq " + EmpId;
            JArray jArray = RESTGet(clientContext, filter);

            if (jArray.Count > 0)
            {
                foreach (JObject j in jArray)
                {
                    emp_ReferenceDetails.Add(new Emp_ReferenceDetailsModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        Person = j["Person"] == null ? "" : j["Person"].ToString(),
                        Company = j["Company"] == null ? "" : j["Company"].ToString(),
                        Email = j["Email"] == null ? "" : j["Email"].ToString(),
                        Phone = j["Phone"] == null ? "" : j["Phone"].ToString(),
                        Designation = j["Designation"] == null ? "" : j["Designation"].ToString(),
                        Notes = j["Notes"] == null ? "" : j["Notes"].ToString(),
                        Address = j["Address"] == null ? "" : j["Address"].ToString(),
                        HowDoYouKnowPerson = j["HowDoYouKnowPerson"] == null ? "" : j["HowDoYouKnowPerson"].ToString(),
                        EmpCode = j["EmpCode"] == null ? "" : j["EmpCode"].ToString(),
                    });
                }
            }
            return emp_ReferenceDetails;
        }
        #endregion

        #region Get Employee Reference Details By ID Method
        public List<Emp_ReferenceDetailsModel> GetReferenceDetailsByID(ClientContext clientContext, string Id)
        {
            List<Emp_ReferenceDetailsModel> emp_ReferenceDetails = new List<Emp_ReferenceDetailsModel>();
            string filter = "ID eq " + Id;
            JArray jArray = RESTGet(clientContext, filter);

            if (jArray.Count > 0)
            {
                foreach (JObject j in jArray)
                {
                    emp_ReferenceDetails.Add(new Emp_ReferenceDetailsModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        Person = j["Person"] == null ? "" : j["Person"].ToString(),
                        Company = j["Company"] == null ? "" : j["Company"].ToString(),
                        Email = j["Email"] == null ? "" : j["Email"].ToString(),
                        Phone = j["Phone"] == null ? "" : j["Phone"].ToString(),
                        Designation = j["Designation"] == null ? "" : j["Designation"].ToString(),
                        Notes = j["Notes"] == null ? "" : j["Notes"].ToString(),
                        Address = j["Address"] == null ? "" : j["Address"].ToString(),
                        HowDoYouKnowPerson = j["HowDoYouKnowPerson"] == null ? "" : j["HowDoYouKnowPerson"].ToString(),
                        EmpCode = j["EmpCode"] == null ? "" : j["EmpCode"].ToString(),
                    });
                }
            }
            return emp_ReferenceDetails;
        }
        #endregion
        public string SaveReferenceDetails(ClientContext clientContext, string ItemData)
        {

            string response = RESTSave(clientContext, ItemData);

            return response;
        }

        public string UpdateReferenceDetails(ClientContext clientContext, string ItemData, string id)
        {

            string response = RESTUpdate(clientContext, ItemData, id);

            return response;
        }

        public string DeleteReferenceDetails(ClientContext client, string id)
        {
            RestService restService = new RestService();
            string delete = restService.DeleteItem(client, "Emp_ReferenceDetails", id);
            return delete;
        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,Person,Company,Email,Phone,Designation,Notes,Address,HowDoYouKnowPerson,EmpCode/Id,EmpCode/EmpCode";
            rESTOption.expand = "EmpCode";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_ReferenceDetails", rESTOption);
            return jArray;
        }

        private string RESTSave(ClientContext clientContext, string ItemData)
        {
            RestService restService = new RestService();

            return restService.SaveItem(clientContext, "Emp_ReferenceDetails", ItemData);
        }

        private string RESTUpdate(ClientContext clientContext, string ItemData, string ID)
        {
            RestService restService = new RestService();

            return restService.UpdateItem(clientContext, "Emp_ReferenceDetails", ItemData, ID);
        }

    }
}
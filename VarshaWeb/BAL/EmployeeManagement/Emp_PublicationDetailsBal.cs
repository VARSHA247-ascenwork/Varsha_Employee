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
    public class Emp_PublicationDetailsBal
    {
        #region Get Employee Publication Details By Employee ID Method
        public List<Emp_PublicationDetailsModel> GetPublicationDetailsByEmpID(ClientContext clientContext, string EmpId)
        {
            List<Emp_PublicationDetailsModel> emp_PublicationDetails = new List<Emp_PublicationDetailsModel>();
            string filter = "EmpCodeId eq " + EmpId;
            JArray jArray = RESTGet(clientContext, filter);

            if (jArray.Count > 0)
            {
                foreach (JObject j in jArray)
                {
                    emp_PublicationDetails.Add(new Emp_PublicationDetailsModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        Publication = j["Publication"] == null ? "" : j["Publication"].ToString(),
                        PublishedOn = j["PublishedOn"] == null ? "" : j["PublishedOn"].ToString(),
                        Description = j["Description"] == null ? "" : j["Description"].ToString(),
                        Url = j["Url"] == null ? "" : j["Url"].ToString(),
                        EmpCode = j["EmpCode"] == null ? "" : j["EmpCode"].ToString(),

                    }); ;
                }
            }
            return emp_PublicationDetails;
        }
        #endregion


        #region Get Employee Publication Details By ID Method
        public List<Emp_PublicationDetailsModel> GetPublicationDetailsByID(ClientContext clientContext, string Id)
        {
            List<Emp_PublicationDetailsModel> emp_PublicationDetails = new List<Emp_PublicationDetailsModel>();
            string filter = "ID eq " + Id;
            JArray jArray = RESTGet(clientContext, filter);

            if (jArray.Count > 0)
            {
                foreach (JObject j in jArray)
                {
                    emp_PublicationDetails.Add(new Emp_PublicationDetailsModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        Publication = j["Publication"] == null ? "" : j["Publication"].ToString(),
                        PublishedOn = j["PublishedOn"] == null ? "" : j["PublishedOn"].ToString(),
                        Description = j["Description"] == null ? "" : j["Description"].ToString(),
                        Url = j["Url"] == null ? "" : j["Url"].ToString(),
                        EmpCode = j["EmpCode"] == null ? "" : j["EmpCode"].ToString(),

                    }); ;
                }
            }
            return emp_PublicationDetails;
        }
        #endregion



        public string SavePublicationDetails(ClientContext clientContext, string ItemData)
        {

            string response = RESTSave(clientContext, ItemData);

            return response;
        }

        public string UpdatePublicationDetails(ClientContext clientContext, string ItemData, string id)
        {

            string response = RESTUpdate(clientContext, ItemData, id);

            return response;
        }
        public string DeletePublicationDetails(ClientContext client, string id)
        {
            RestService restService = new RestService();
            string delete = restService.DeleteItem(client, "Emp_PublicationDetails", id);
            return delete;
        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,PublishedOn,Publication,Description,Url,EmpCode/Id,EmpCode/EmpCode";
            rESTOption.expand = "EmpCode";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_PublicationDetails", rESTOption);
            return jArray;
        }

        private string RESTSave(ClientContext clientContext, string ItemData)
        {
            RestService restService = new RestService();

            return restService.SaveItem(clientContext, "Emp_PublicationDetails", ItemData);
        }

        private string RESTUpdate(ClientContext clientContext, string ItemData, string ID)
        {
            RestService restService = new RestService();

            return restService.UpdateItem(clientContext, "Emp_PublicationDetails", ItemData, ID);
        }

    }
}
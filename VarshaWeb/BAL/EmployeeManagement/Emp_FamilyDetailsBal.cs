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
    public class Emp_FamilyDetailsBal
    {
        #region Get Employee Family Details By Employee ID Method
        public List<Emp_FamilyDetailsModel> GetFamilyDetailsByEmpID(ClientContext clientContext, string EmpId)
        {
            List<Emp_FamilyDetailsModel> emp_FamilyDetails = new List<Emp_FamilyDetailsModel>();
            string filter = "EmpCodeId eq " + EmpId;
            JArray jArray = RESTGet(clientContext, filter);

            if (jArray.Count > 0)
            {
                DateTime BirthDate = DateTime.Today;
                foreach (JObject j in jArray)
                {
                    if ( j["BirthDate"].ToString() != "" && j["BirthDate"] != null)
                    {
                        BirthDate = Convert.ToDateTime(j["BirthDate"]);
                      //  string asdf = j["BirthDate"].ToString();

                    }
                    emp_FamilyDetails.Add(new Emp_FamilyDetailsModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        Age = j["Age"] == null ? "" : j["Age"].ToString(),
                        BirthDate = j["BirthDate"] == null ? "" : BirthDate.ToString("dd/MM/yyyy"),
                        BirthPlace = j["BirthPlace"] == null ? "" : j["BirthPlace"].ToString(),
                        ContactNumber = j["ContactNumber"] == null ? "" : j["ContactNumber"].ToString(),
                        Gender = j["Gender"] == null ? "" : j["Gender"].ToString(),
                        MediclaimCovered = j["MediclaimCovered"] == null ? "" : j["MediclaimCovered"].ToString(),
                        PersonName = j["PersonName"] == null ? "" : j["PersonName"].ToString(),
                        Relationship = j["Relationship"] == null ? "" : j["Relationship"].ToString(),
                    });
                }
            }
            return emp_FamilyDetails;
        }
        #endregion

        #region Get Employee Family Details By ID Method
        public List<Emp_FamilyDetailsModel> GetFamilyDetailsByID(ClientContext clientContext, string Id)
        {
            List<Emp_FamilyDetailsModel> emp_FamilyDetails = new List<Emp_FamilyDetailsModel>();
            string filter = "ID eq " + Id;
            JArray jArray = RESTGet(clientContext, filter);

            if (jArray.Count > 0)
            {
                DateTime BirthDate = DateTime.Today;
                foreach (JObject j in jArray)
                {
                    if (j["BirthDate"] != null)
                    {
                        BirthDate = Convert.ToDateTime(j["BirthDate"]);
                    }
                    emp_FamilyDetails.Add(new Emp_FamilyDetailsModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        Age = j["Age"] == null ? "" : j["Age"].ToString(),
                        BirthDate = j["BirthDate"] == null ? "" : BirthDate.ToString("dd/MM/yyyy"),
                        BirthPlace = j["BirthPlace"] == null ? "" : j["BirthPlace"].ToString(),
                        ContactNumber = j["ContactNumber"] == null ? "" : j["ContactNumber"].ToString(),
                        Gender = j["Gender"] == null ? "" : j["Gender"].ToString(),
                        MediclaimCovered = j["MediclaimCovered"] == null ? "" : j["MediclaimCovered"].ToString(),
                        PersonName = j["PersonName"] == null ? "" : j["PersonName"].ToString(),
                        Relationship = j["Relationship"] == null ? "" : j["Relationship"].ToString(),
                    });
                }
            }
            return emp_FamilyDetails;
        }
        #endregion

        public string SaveFamilyDetails(ClientContext clientContext, string ItemData)
        {

            string response = RESTSave(clientContext, ItemData);

            return response;
        }

        public string UpdateFamilyDetails(ClientContext clientContext, string ItemData, string Id)
        {

            string response = RESTUpdate(clientContext, ItemData, Id);

            return response;
        }

        public string DeleteFamilyDetails(ClientContext client, string id)
        {
            RestService restService = new RestService();
            string delete = restService.DeleteItem(client, "Emp_FamilyDetails", id);
            return delete;
        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,PersonName,Relationship,BirthDate,Age,Gender,MediclaimCovered,BirthPlace,ContactNumber,EmpCode/Id,EmpCode/EmpCode";
            rESTOption.expand = "EmpCode";
            rESTOption.top = "5000";
            rESTOption.orderby = "ID desc";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_FamilyDetails", rESTOption);
            return jArray;
        }

        private string RESTSave(ClientContext clientContext, string ItemData)
        {
            RestService restService = new RestService();

            return restService.SaveItem(clientContext, "Emp_FamilyDetails", ItemData);
        }

        private string RESTUpdate(ClientContext clientContext, string ItemData, string ID)
        {
            RestService restService = new RestService();

            return restService.UpdateItem(clientContext, "Emp_FamilyDetails", ItemData, ID);
        }

    }
}
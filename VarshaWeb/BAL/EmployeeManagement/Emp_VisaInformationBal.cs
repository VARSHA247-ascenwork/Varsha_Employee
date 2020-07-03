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
    public class Emp_VisaInformationBal
    {
        #region Get Employee VisaInformation Details By Employee ID Method
        public List<Emp_VisaInformationModel> GetVisaInformationByEmpID(ClientContext clientContext, string EmpId)
        {
            List<Emp_VisaInformationModel> emp_VisaInformation = new List<Emp_VisaInformationModel>();
            string filter = "EmpCodeId eq " + EmpId;
            JArray jArray = RESTGet(clientContext, filter);

            if (jArray.Count > 0)
            {
                DateTime ValidUntil=DateTime.Today;
                foreach (JObject j in jArray)
                {
                    if (j["ValidUntil"]!=null)
                    {
                         ValidUntil = Convert.ToDateTime(j["ValidUntil"]);
                    }
                    emp_VisaInformation.Add(new Emp_VisaInformationModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        VisaType = j["VisaType"] == null ? "" : j["VisaType"].ToString(),
                        ValidUntil = j["ValidUntil"] == null ? "" : ValidUntil.ToString("dd/MM/yyyy"),
                        Country = j["Country"] == null ? "" : j["Country"]["CountryName"].ToString(),
                    });
                }
            }
            return emp_VisaInformation;
        }
        #endregion

        #region Get Employee VisaInformation Details By ID Method
        public List<Emp_VisaInformationModel> GetVisaInformationByID(ClientContext clientContext, string Id)
        {
            List<Emp_VisaInformationModel> emp_VisaInformation = new List<Emp_VisaInformationModel>();
            string filter = "ID eq " + Id;
            JArray jArray = RESTGet(clientContext, filter);

            if (jArray.Count > 0)
            {
                DateTime ValidUntil = DateTime.Today;
                foreach (JObject j in jArray)
                {
                    if (j["ValidUntil"] != null)
                    {
                        ValidUntil = Convert.ToDateTime(j["ValidUntil"]);
                    }
                    emp_VisaInformation.Add(new Emp_VisaInformationModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        VisaType = j["VisaType"] == null ? "" : j["VisaType"].ToString(),
                        ValidUntil = j["ValidUntil"] == null ? "" : ValidUntil.ToString("dd/MM/yyyy"),
                        Country = j["Country"] == null ? "" : j["Country"]["CountryName"].ToString(),
                        CountryID=j["Country"]==null?"": j["Country"]["Id"].ToString(),
                    });
                }
            }
            return emp_VisaInformation;
        }

        #endregion

        public string SaveVisaInformationsDetails(ClientContext clientContext, string ItemData)
        {

            string response = RESTSave(clientContext, ItemData);

            return response;
        }

        public string UpdateVisaInformationsDetails(ClientContext clientContext, string ItemData, string id)
        {

            string response = RESTUpdate(clientContext, ItemData,id);

            return response;
        }

        public string DeleteVisaInformationsDetails(ClientContext client, string id)
        {
            RestService restService = new RestService();
            string delete = restService.DeleteItem(client, "Emp_VisaInformation", id);
            return delete;
        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,VisaType,ValidUntil,Country/Id,Country/CountryName,EmpCode/Id,EmpCode/EmpCode";
            rESTOption.expand = "EmpCode,Country";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_VisaInformation", rESTOption);
            return jArray;
        }

        private string RESTSave(ClientContext clientContext, string ItemData)
        {
            RestService restService = new RestService();

            return restService.SaveItem(clientContext, "Emp_VisaInformation", ItemData);
        }

        private string RESTUpdate(ClientContext clientContext, string ItemData, string ID)
        {
            RestService restService = new RestService();

            return restService.UpdateItem(clientContext, "Emp_VisaInformation", ItemData, ID);
        }

    }
}
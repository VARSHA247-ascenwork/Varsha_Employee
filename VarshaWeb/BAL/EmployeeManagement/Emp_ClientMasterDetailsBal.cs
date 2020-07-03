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
    public class Emp_ClientMasterDetailsBal
    {
        #region Get all client
        public List<Emp_ClientMasterDetailsModel> GetAllClient(ClientContext clientContext)
        {
            List<Emp_ClientMasterDetailsModel> emp_ClientMasterDetailsModels = new List<Emp_ClientMasterDetailsModel>();

            JArray jArray = RESTGet(clientContext, null);

            foreach (JObject j in jArray)
            {
                emp_ClientMasterDetailsModels.Add(new Emp_ClientMasterDetailsModel
                {
                    ID = Convert.ToInt32(j["ID"]),
                    ClientName = j["ClientName"] == null ? "" : j["ClientName"].ToString(),
                    ClientContact = j["ClientContact"] == null ? "" : j["ClientContact"].ToString(),
                    ClientMailID = j["ClientMailID"] == null ? "" : j["ClientMailID"].ToString(),
                    ClientDesignation = j["ClientDesignation"]["Designation"] == null ? "" : j["ClientDesignation"]["Designation"].ToString(),
                });
            }


            return emp_ClientMasterDetailsModels;

        }
        #endregion

        public List<Emp_ClientMasterDetailsModel> GetClient(ClientContext clientContext)
        {
            List<Emp_ClientMasterDetailsModel> lstClient = new List<Emp_ClientMasterDetailsModel>();
            JArray jArray = RESTGet(clientContext, null);
            foreach (JObject j in jArray)
            {
                lstClient.Add(new Emp_ClientMasterDetailsModel
                {
                    ID = Convert.ToInt32(j["Id"]),
                    ClientName = j["ClientName"].ToString(),
                }); ;
            }
            return lstClient;
        }
        #region Get client by ID
        public List<Emp_ClientMasterDetailsModel> GetClientById(ClientContext clientContext, string id)
        {
            List<Emp_ClientMasterDetailsModel> emp_ClientMasterDetailsModels = new List<Emp_ClientMasterDetailsModel>();
            var filter = "ID eq " + id;
            JArray jArray = RESTGet(clientContext, filter);

            foreach (JObject j in jArray)
            {
                emp_ClientMasterDetailsModels.Add(new Emp_ClientMasterDetailsModel
                {
                    ID = Convert.ToInt32(j["ID"]),
                    ClientName = j["ClientName"] == null ? "" : j["ClientName"].ToString(),
                    ClientContact = j["ClientContact"] == null ? "" : j["ClientContact"].ToString(),
                    ClientMailID = j["ClientMailID"] == null ? "" : j["ClientMailID"].ToString(),
                    ClientDesignationId = Convert.ToInt32(j["ClientDesignation"]["ID"]),
                    ClientDesignation = j["ClientDesignation"]["Designation"] == null ? "" : j["ClientDesignation"]["Designation"].ToString(),
                    ClientAddress = j["ClientAddress"] == null ? "" : j["ClientAddress"].ToString(),
                    ClientGSTNO = j["ClientGSTNO"] == null ? "" : j["ClientGSTNO"].ToString(),
                    ClientPanCardNo = j["ClientPanCardNo"] == null ? "" : j["ClientPanCardNo"].ToString(),
                    MobileNo = j["MobileNo"] == null ? "" : j["MobileNo"].ToString(),
                    ClientCity = j["ClientCity"] == null ? "" : j["ClientCity"].ToString(),
                    ClientCountryId = Convert.ToInt32(j["ClientCountry"]["ID"]),
                    ClientCountry = j["ClientCountry"]["CountryName"] == null ? "" : j["ClientCountry"]["CountryName"].ToString(),
                    ClientStateId = Convert.ToInt32(j["ClientState"]["ID"]),
                    ClientState = j["ClientState"]["StateName"] == null ? "" : j["ClientState"]["StateName"].ToString(),
                    ClientRemark = j["ClientRemark"] == null ? "" : j["ClientRemark"].ToString(),
                });
            }


            return emp_ClientMasterDetailsModels;

        }
        #endregion

   

        public string SaveClient(ClientContext clientContext, string ItemData)
        {

            string response = RESTSave(clientContext, ItemData);

            return response;
        }

        public string UpdateClient(ClientContext clientContext, string ItemData, string ID)
        {
            string response = RESTUpdate(clientContext, ItemData, ID);
            return response;
        }

        public int UploadDocument(ClientContext clientContext, HttpPostedFileBase files, string ItemData)
        {

            return RESTUploadFile(clientContext, files, ItemData);
        }

        #region Common method of Get Client from list
        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,ClientName,ClientAddress,ClientContact,ClientMailID,ClientDesignation/ID,ClientDesignation/Designation,ClientGSTNO,ClientPanCardNo,ClientState/ID,ClientState/StateName,ClientCity,ClientRemark,MobileNo,ClientCountry/ID,ClientCountry/CountryName";
            rESTOption.expand = "ClientCountry,ClientState,ClientDesignation";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_ClientMasterDetails", rESTOption);

            return jArray;
        } 
        #endregion

        private string RESTSave(ClientContext clientContext, string ItemData)
        {
            RestService restService = new RestService();

            return restService.SaveItem(clientContext, "Emp_ClientMasterDetails", ItemData);
        }

        private string RESTUpdate(ClientContext clientContext, string ItemData, string ID)
        {
            RestService restService = new RestService();

            return restService.UpdateItem(clientContext, "Emp_ClientMasterDetails", ItemData, ID);
        }

        private int RESTUploadFile(ClientContext clientContext, HttpPostedFileBase files, string ItemData)
        {

            RestService restService = new RestService();

            return restService.UploadDocument(clientContext, "Emp_Client_Document", files, ItemData);
        }
    }
}
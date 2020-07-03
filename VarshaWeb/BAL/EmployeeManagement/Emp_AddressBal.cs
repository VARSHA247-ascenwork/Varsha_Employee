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
    public class Emp_AddressBal
    {
        #region Get Employee Address Details By Employee ID Method
        public List<Emp_AddressModel> GetEmpAddressByEmpID(ClientContext clientContext, string EmpId)
        {
            List<Emp_AddressModel> emp_Addresses = new List<Emp_AddressModel>();
            string filter = "EmpCodeId eq " + EmpId;
            JArray jArray = RESTGet(clientContext, filter);

            if (jArray.Count > 0)
            {
                foreach (JObject j in jArray)
                {
                    emp_Addresses.Add(new Emp_AddressModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        AddressType = j["AddressType"] == null ? "" : j["AddressType"].ToString(),
                        Address = j["Address"] == null ? "" : j["Address"].ToString(),
                        Country = j["Country"]["CountryName"] == null ? "" : j["Country"]["CountryName"].ToString(),
                        City = j["City"] == null ? "" : j["City"].ToString(),
                        CountryId = j["Country"]["Id"] == null ? "" : j["Country"]["Id"].ToString(),
                        Landmark = j["Landmark"] == null ? "" : j["Landmark"].ToString(),
                        PostalCode = j["PostalCode"] == null ? "" : j["PostalCode"].ToString(),
                        ResidingSince = j["ResidingSince"] == null ? "" : j["ResidingSince"].ToString(),
                        State = j["State"]["StateName"] == null ? "" : j["State"]["StateName"].ToString(),
                        StateId = j["State"]["Id"] == null ? "" : j["State"]["Id"].ToString(),

                    });
                }
            }
            return emp_Addresses;
        }
        #endregion

        #region Get Employee Address Details By ID Method
        public List<Emp_AddressModel> GetEmpAddressByID(ClientContext clientContext, string Id)
        {
            List<Emp_AddressModel> emp_Addresses = new List<Emp_AddressModel>();
            string filter = "ID eq " + Id;
            JArray jArray = RESTGet(clientContext, filter);

            if (jArray.Count > 0)
            {
                foreach (JObject j in jArray)
                {
                    emp_Addresses.Add(new Emp_AddressModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        AddressType = j["AddressType"] == null ? "" : j["AddressType"].ToString(),
                        Address = j["Address"] == null ? "" : j["Address"].ToString(),
                        Country = j["Country"]["CountryName"] == null ? "" : j["Country"]["CountryName"].ToString(),
                        City = j["City"] == null ? "" : j["City"].ToString(),
                        CountryId = j["Country"]["Id"] == null ? "" : j["Country"]["Id"].ToString(),
                        Landmark = j["Landmark"] == null ? "" : j["Landmark"].ToString(),
                        PostalCode = j["PostalCode"] == null ? "" : j["PostalCode"].ToString(),
                        ResidingSince = j["ResidingSince"] == null ? "" : j["ResidingSince"].ToString(),
                        State = j["State"]["StateName"] == null ? "" : j["State"]["StateName"].ToString(),
                        StateId = j["State"]["Id"] == null ? "" : j["State"]["Id"].ToString(),

                    });
                }
            }
            return emp_Addresses;
        }

        #endregion

        public string SaveEmp_Address(ClientContext clientContext, string ItemData)
        {

            string response = RESTSave(clientContext, ItemData);

            return response;
        }

        public string UpdateEmp_Address(ClientContext clientContext, string ItemData, string id)
        {

            string response = RESTUpdate(clientContext, ItemData, id);

            return response;
        }

        public string DeleteEmpAddress(ClientContext client, string id)
        {
            RestService restService = new RestService();
            string delete = restService.DeleteItem(client, "Emp_Address", id);
            return delete;
        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,AddressType,Address,Country/Id,Country/CountryName,EmpCode/Id,EmpCode/EmpCode,State/Id,State/StateName,City,Landmark,PostalCode,ResidingSince";
            rESTOption.expand = "EmpCode,Country,State";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_Address", rESTOption);
            return jArray;
        }

        private string RESTSave(ClientContext clientContext, string ItemData)
        {
            RestService restService = new RestService();

            return restService.SaveItem(clientContext, "Emp_Address", ItemData);
        }

        private string RESTUpdate(ClientContext clientContext, string ItemData, string ID)
        {
            RestService restService = new RestService();

            return restService.UpdateItem(clientContext, "Emp_Address", ItemData, ID);
        }

    }
}
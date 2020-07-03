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
    public class Emp_BankingDetailsBal
    {
        #region Get Employee Banking Details By Employee ID Method
        public List<Emp_BankingDetailsModel> GetBankingDetailsByEmpID(ClientContext clientContext, string EmpId)
        {
            List<Emp_BankingDetailsModel> emp_VisaInformation = new List<Emp_BankingDetailsModel>();
            string filter = "EmpCodeId eq " + EmpId;
            JArray jArray = RESTGet(clientContext, filter);
            if (jArray.Count() > 0)
            {
                foreach (JObject j in jArray)
                {
                    emp_VisaInformation.Add(new Emp_BankingDetailsModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        AccountNumber = j["AccountNumber"] == null ? "" : j["AccountNumber"].ToString(),
                        AccountType = j["AccountType"] == null ? "" : j["AccountType"].ToString(),
                        BankName = j["BankName"] == null ? "" : j["BankName"].ToString(),
                        Branch = j["Branch"] == null ? "" : j["Branch"].ToString(),
                        City = j["City"] == null ? "" : j["City"].ToString(),
                        Country = j["Country"]["CountryName"] == null ? "" : j["Country"]["CountryName"].ToString(),
                        RoutingNumber = j["RoutingNumber"] == null ? "" : j["RoutingNumber"].ToString(),
                        State = j["State"]["StateName"] == null ? "" : j["State"]["StateName"].ToString()

                    });
                }
            }
            return emp_VisaInformation;
        }
        #endregion

        #region Get Employee Banking Details By ID Method
        public List<Emp_BankingDetailsModel> GetBankingDetailsByID(ClientContext clientContext, string Id)
        {
            List<Emp_BankingDetailsModel> emp_VisaInformation = new List<Emp_BankingDetailsModel>();
            string filter = "ID eq " + Id;
            JArray jArray = RESTGet(clientContext, filter);
            if (jArray.Count() > 0)
            {
                foreach (JObject j in jArray)
                {
                    emp_VisaInformation.Add(new Emp_BankingDetailsModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        AccountNumber = j["AccountNumber"] == null ? "" : j["AccountNumber"].ToString(),
                        AccountType = j["AccountType"] == null ? "" : j["AccountType"].ToString(),
                        BankName = j["BankName"] == null ? "" : j["BankName"].ToString(),
                        Branch = j["Branch"] == null ? "" : j["Branch"].ToString(),
                        City = j["City"] == null ? "" : j["City"].ToString(),
                        Country = j["Country"]["CountryName"] == null ? "" : j["Country"]["CountryName"].ToString(),
                        RoutingNumber = j["RoutingNumber"] == null ? "" : j["RoutingNumber"].ToString(),
                        State = j["State"]["StateName"] == null ? "" : j["State"]["StateName"].ToString(),
                        CountryID= j["Country"]["Id"] == null ? "" : j["Country"]["Id"].ToString(),
                        StateID= j["State"]["Id"] == null ? "" : j["State"]["Id"].ToString()
                    });
                }
            }
            return emp_VisaInformation;
        }
        #endregion


        public string SaveBankingDetails(ClientContext clientContext, string ItemData)
        {

            string response = RESTSave(clientContext, ItemData);

            return response;
        }

        public string UpdateBankingDetails(ClientContext clientContext, string ItemData,string ID)
        {

            string response = RESTUpdate(clientContext, ItemData,ID);

            return response;
        }
        public string DeleteBankingDetails(ClientContext client, string id)
        {
            RestService restService = new RestService();
            string delete = restService.DeleteItem(client, "Emp_BankingDetails", id);
            return delete;
        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,BankName,Branch,City,State/Id,State/StateName,AccountNumber,RoutingNumber,AccountType,Country/Id,Country/CountryName,EmpCode/Id,EmpCode/EmpCode";
            rESTOption.expand = "EmpCode,Country,State";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_BankingDetails", rESTOption);
            return jArray;
        }

        private string RESTSave(ClientContext clientContext, string ItemData)
        {
            RestService restService = new RestService();

            return restService.SaveItem(clientContext, "Emp_BankingDetails", ItemData);
        }

        private string RESTUpdate(ClientContext clientContext, string ItemData, string ID)
        {
            RestService restService = new RestService();

            return restService.UpdateItem(clientContext, "Emp_BankingDetails", ItemData, ID);
        }

    }
}
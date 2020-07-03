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
    public class Emp_MedicalInformationBal
    {
        #region Get Employee Medical Information By Employee ID Method
        public List<Emp_MedicalInformationModel> GetMedicalInformationByEmpID(ClientContext clientContext, string EmpId)
        {
            List<Emp_MedicalInformationModel> emp_MedicalInformation = new List<Emp_MedicalInformationModel>();
            string filter = "EmpCodeId eq " + EmpId;
            JArray jArray = RESTGet(clientContext, filter);

            if (jArray.Count > 0)
            {
                //DateTime MedicalConditionFrom = DateTime.Today;
                foreach (JObject j in jArray)
                {
                    //if (j["MedicalConditionFrom"] == null)
                    //{
                    //    MedicalConditionFrom = Convert.ToDateTime(j["MedicalConditionFrom"]);
                    //}
                    emp_MedicalInformation.Add(new Emp_MedicalInformationModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        MedicalConditionName = j["MedicalConditionName"] == null ? "" : j["MedicalConditionName"].ToString(),
                        MedicalConditionFrom = j["MedicalConditionFrom"] == null ? "" : j["MedicalConditionFrom"].ToString(),
                        CurrentlyActive = j["CurrentlyActive"] == null ? "" : j["CurrentlyActive"].ToString(),
                        NeedSpecialAttention = j["NeedSpecialAttention"] == null ? "" : j["NeedSpecialAttention"].ToString(),
                        Notes = j["Notes"] == null ? "" : j["Notes"].ToString()
                    });
                }
            }
            return emp_MedicalInformation;
        }
        #endregion

        #region Get Employee Medical Information By ID Method
        public List<Emp_MedicalInformationModel> GetMedicalInformationByID(ClientContext clientContext, string Id)
        {
            List<Emp_MedicalInformationModel> emp_MedicalInformation = new List<Emp_MedicalInformationModel>();
            string filter = "ID eq " + Id;
            JArray jArray = RESTGet(clientContext, filter);

            if (jArray.Count > 0)
            {
                //DateTime MedicalConditionFrom = DateTime.Today;
                foreach (JObject j in jArray)
                {
                    //if (j["MedicalConditionFrom"] == null)
                    //{
                    //    MedicalConditionFrom = Convert.ToDateTime(j["MedicalConditionFrom"]);
                    //}
                    emp_MedicalInformation.Add(new Emp_MedicalInformationModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        MedicalConditionName = j["MedicalConditionName"] == null ? "" : j["MedicalConditionName"].ToString(),
                        MedicalConditionFrom = j["MedicalConditionFrom"] == null ? "" : j["MedicalConditionFrom"].ToString(),
                        CurrentlyActive = j["CurrentlyActive"] == null ? "" : j["CurrentlyActive"].ToString(),
                        NeedSpecialAttention = j["NeedSpecialAttention"] == null ? "" : j["NeedSpecialAttention"].ToString(),
                        Notes = j["Notes"] == null ? "" : j["Notes"].ToString()
                    });
                }
            }
            return emp_MedicalInformation;
        }
        #endregion


        public string SaveMedicalInformation(ClientContext clientContext, string ItemData)
        {

            string response = RESTSave(clientContext, ItemData);

            return response;
        }

        public string UpdateMedicalInformation(ClientContext clientContext, string ItemData, string ID)
        {

            string response = RESTUpdate(clientContext, ItemData, ID);

            return response;
        }

        public string DeleteMedicalInformation(ClientContext client, string id)
        {
            RestService restService = new RestService();
            string delete = restService.DeleteItem(client, "Emp_MedicalInformation", id);
            return delete;
        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,MedicalConditionName,MedicalConditionFrom,CurrentlyActive,NeedSpecialAttention,Notes,EmpCode/Id,EmpCode/EmpCode";
            rESTOption.expand = "EmpCode";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_MedicalInformation", rESTOption);
            return jArray;
        }

        private string RESTSave(ClientContext clientContext, string ItemData)
        {
            RestService restService = new RestService();

            return restService.SaveItem(clientContext, "Emp_MedicalInformation", ItemData);
        }

        private string RESTUpdate(ClientContext clientContext, string ItemData, string ID)
        {
            RestService restService = new RestService();

            return restService.UpdateItem(clientContext, "Emp_MedicalInformation", ItemData, ID);
        }

    }
}
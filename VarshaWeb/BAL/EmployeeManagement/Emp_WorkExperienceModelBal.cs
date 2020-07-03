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
    public class Emp_WorkExperienceModelBal
    {

        #region Get WorkExperience By EmpID Method
        public List<Emp_WorkExperienceModel> GetWorkExperienceByEmpID(ClientContext clientContext, string EmpId)
        {
            List<Emp_WorkExperienceModel> emp_Works = new List<Emp_WorkExperienceModel>();
            string filter = "EmpCodeId eq " + EmpId;
            JArray jArray = RESTGet(clientContext, filter);

            if (jArray.Count > 0)
            {
                foreach (JObject j in jArray)
                {
                    emp_Works.Add(new Emp_WorkExperienceModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        OrganizationName = j["OrganizationName"] == null ? "" : j["OrganizationName"].ToString(),
                        Designation = j["Designation"] == null ? "" : j["Designation"].ToString(),
                        FromDate = j["FromDate"] == null ? "" : j["FromDate"].ToString(),
                        ToDate = j["ToDate"] == null ? "" : j["ToDate"].ToString(),
                        Notes = j["Notes"] == null ? "" : j["Notes"].ToString(),
                        ContactNumber = j["ContactNumber"] == null ? "" : j["ContactNumber"].ToString(),
                        AnnualSalary = j["AnnualSalary"] == null ? "" : j["AnnualSalary"].ToString(),
                        EmpCode = j["EmpCode"] == null ? "" : j["EmpCode"].ToString(),
                    });
                }
            }
            return emp_Works;
        }
        #endregion

        #region Get WorkExperience By ID Method
        public List<Emp_WorkExperienceModel> GetWorkExperienceByID(ClientContext clientContext, string Id)
        {
            List<Emp_WorkExperienceModel> emp_Works = new List<Emp_WorkExperienceModel>();
            string filter = "ID eq " + Id;
            JArray jArray = RESTGet(clientContext, filter);

            if (jArray.Count > 0)
            {
                foreach (JObject j in jArray)
                {
                    DateTime FromDate = Convert.ToDateTime(j["FromDate"]);
                    emp_Works.Add(new Emp_WorkExperienceModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        OrganizationName = j["OrganizationName"] == null ? "" : j["OrganizationName"].ToString(),
                        Designation = j["Designation"] == null ? "" : j["Designation"].ToString(),
                        FromDate = j["FromDate"] == null ? "" : FromDate.ToString("dd/MM/yyyy"),//j["FromDate"].ToString(),
                        ToDate = j["ToDate"] == null ? "" : j["ToDate"].ToString(),
                        Notes = j["Notes"] == null ? "" : j["Notes"].ToString(),
                        ContactNumber = j["ContactNumber"] == null ? "" : j["ContactNumber"].ToString(),
                        AnnualSalary = j["AnnualSalary"] == null ? "" : j["AnnualSalary"].ToString(),
                        EmpCode = j["EmpCode"] == null ? "" : j["EmpCode"].ToString(),
                    }) ;
                }
            }
            return emp_Works;
        }

        #endregion


        public string SaveEmp_WorkExperience(ClientContext clientContext, string ItemData)
        {

            string response = RESTSave(clientContext, ItemData);

            return response;
        }
        public string UpdateEmp_WorkExperience(ClientContext clientContext, string ItemData,string Id)
        {

            string response = RESTUpdate(clientContext, ItemData,Id);

            return response;
        }
        public string DeleteEmp_WorkExperience(ClientContext client, string id)
        {
            RestService restService = new RestService();
            string delete = restService.DeleteItem(client, "Emp_WorkExperience", id);
            return delete;
        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,OrganizationName,Designation,FromDate,ToDate,Notes,ContactNumber,AnnualSalary,EmpCode/Id,EmpCode/EmpCode";
            rESTOption.expand = "EmpCode";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_WorkExperience", rESTOption);
            return jArray;
        }

        private string RESTSave(ClientContext clientContext, string ItemData)
        {
            RestService restService = new RestService();

            return restService.SaveItem(clientContext, "Emp_WorkExperience", ItemData);
        }

        private string RESTUpdate(ClientContext clientContext, string ItemData, string ID)
        {
            RestService restService = new RestService();

            return restService.UpdateItem(clientContext, "Emp_WorkExperience", ItemData, ID);
        }

    }
}
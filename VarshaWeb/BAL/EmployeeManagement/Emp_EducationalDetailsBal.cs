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
    public class Emp_EducationalDetailsBal
    {

        #region Get Education Details By ID Method
        public List<Emp_EducationalDetailsModel> GetEducationDetailsByEmpID(ClientContext clientContext, string EmpId)
        {
            List<Emp_EducationalDetailsModel> emp_Educationals = new List<Emp_EducationalDetailsModel>();
            string filter = "EmpCodeId eq " + EmpId;
            JArray jArray = RESTGet(clientContext, filter);
            if (jArray.Count > 0)
            {
                foreach (JObject j in jArray)
                {
                    //DateTime From = Convert.ToDateTime(j["From"]);
                    //DateTime To = Convert.ToDateTime((j["To"]));
                    emp_Educationals.Add(new Emp_EducationalDetailsModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        UniversityInstitute = j["UniversityInstitute"] == null ? "" : j["UniversityInstitute"].ToString(),
                        Degree = j["Degree"] == null ? "" : j["Degree"].ToString(),
                        MarksInPercentage = j["MarksInPercentage"] == null ? "" : j["MarksInPercentage"].ToString(),
                        From = j["From"] == null ? "" : j["From"].ToString(),//From.ToString("dd/M/yyyy"),
                        To = j["To"] == null ? "" : j["To"].ToString(), //To.ToString("dd/M/yyyy"),
                        Subject = j["Subject"] == null ? "" : j["Subject"].ToString(),
                    });
                }
            }
            return emp_Educationals;
        }

        public List<Emp_EducationalDetailsModel> GetEducationDetailsByID(ClientContext clientContext, string Id)
        {
            List<Emp_EducationalDetailsModel> emp_Educationals = new List<Emp_EducationalDetailsModel>();
            string filter = "ID eq " + Id;
            JArray jArray = RESTGet(clientContext, filter);
            if (jArray.Count > 0)
            {
                foreach (JObject j in jArray)
                {
                    //DateTime From = Convert.ToDateTime(j["From"]);
                    //DateTime To = Convert.ToDateTime((j["To"]));
                    emp_Educationals.Add(new Emp_EducationalDetailsModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        UniversityInstitute = j["UniversityInstitute"] == null ? "" : j["UniversityInstitute"].ToString(),
                        Degree = j["Degree"] == null ? "" : j["Degree"].ToString(),
                        MarksInPercentage = j["MarksInPercentage"] == null ? "" : j["MarksInPercentage"].ToString(),
                        From = j["From"] == null ? "" : j["From"].ToString(),//From.ToString("dd/M/yyyy"),
                        To = j["To"] == null ? "" : j["To"].ToString(), //To.ToString("dd/M/yyyy"),
                        Subject = j["Subject"] == null ? "" : j["Subject"].ToString(),
                    });
                }
            }
            return emp_Educationals;
        }
        #endregion


        public string SaveEducationDetails(ClientContext clientContext, string ItemData)
        {

            string response = RESTSave(clientContext, ItemData);

            return response;
        }
        public string UpdateEducationDetails(ClientContext clientContext, string ItemData, string ID)
        {

            string response = RESTUpdate(clientContext, ItemData,ID);

            return response;
        }

        public  string DeleteEducationDetails(ClientContext client,string id)
        {
            RestService restService = new RestService();
            string delete = restService.DeleteItem(client, "Emp_EducationalDetails", id);
            return delete;
        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,UniversityInstitute,Degree,MarksInPercentage,From,To,Subject";
            rESTOption.expand = "";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_EducationalDetails", rESTOption);

            return jArray;
        }

        private string RESTSave(ClientContext clientContext, string ItemData)
        {
            RestService restService = new RestService();

            return restService.SaveItem(clientContext, "Emp_EducationalDetails", ItemData);
        }

        private string RESTUpdate(ClientContext clientContext, string ItemData, string ID)
        {
            RestService restService = new RestService();

            return restService.UpdateItem(clientContext, "Emp_EducationalDetails", ItemData, ID);
        }

    }
}
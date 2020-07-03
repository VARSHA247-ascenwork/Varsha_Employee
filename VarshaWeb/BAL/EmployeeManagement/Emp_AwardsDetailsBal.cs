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
    public class Emp_AwardsDetailsBal
    {
        #region Get Employee Awards Details By Employee ID Method
        public List<Emp_AwardsDetailsModel> GetAwardsDetailsByEmpID(ClientContext clientContext, string EmpId)
        {
            List<Emp_AwardsDetailsModel> emp_Works = new List<Emp_AwardsDetailsModel>();
            string filter = "EmpCodeId eq " + EmpId;
            JArray jArray = RESTGet(clientContext, filter);

            if (jArray.Count > 0)
            {
                foreach (JObject j in jArray)
                {
                    emp_Works.Add(new Emp_AwardsDetailsModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        EmpCode = j["EmpCode"] == null ? "" : j["EmpCode"].ToString(),
                        Award = j["Award"] == null ? "" : j["Award"].ToString(),
                        AwardedBy = j["AwardedBy"] == null ? "" : j["AwardedBy"].ToString(),
                        AwardedOn = j["AwardedOn"] == null ? "" : j["AwardedOn"].ToString(),
                        Description = j["Description"] == null ? "" : j["Description"].ToString(),
                    });
                }
            }
            return emp_Works;
        }
        #endregion

        #region Get Employee Awards Details By ID Method
        public List<Emp_AwardsDetailsModel> GetAwardsDetailsByID(ClientContext clientContext, string Id)
        {
            List<Emp_AwardsDetailsModel> emp_Works = new List<Emp_AwardsDetailsModel>();
            string filter = "ID eq " + Id;
            JArray jArray = RESTGet(clientContext, filter);

            if (jArray.Count > 0)
            {
                foreach (JObject j in jArray)
                {
                    emp_Works.Add(new Emp_AwardsDetailsModel
                    {
                        ID = Convert.ToInt32(j["ID"]),
                        EmpCode = j["EmpCode"] == null ? "" : j["EmpCode"].ToString(),
                        Award = j["Award"] == null ? "" : j["Award"].ToString(),
                        AwardedBy = j["AwardedBy"] == null ? "" : j["AwardedBy"].ToString(),
                        AwardedOn = j["AwardedOn"] == null ? "" : j["AwardedOn"].ToString(),
                        Description = j["Description"] == null ? "" : j["Description"].ToString(),
                    });
                }
            }
            return emp_Works;
        }
        #endregion


        public string SaveAwardsDetails(ClientContext clientContext, string ItemData)
        {

            string response = RESTSave(clientContext, ItemData);

            return response;
        }

        public string UpdateAwardsDetails(ClientContext clientContext, string ItemData, string id)
        {

            string response = RESTUpdate(clientContext, ItemData,id);

            return response;
        }

        public string DeleteAwardsDetails(ClientContext client, string id)
        {
            RestService restService = new RestService();
            string delete = restService.DeleteItem(client, "Emp_AwardsDetails", id);
            return delete;
        }

        private JArray RESTGet(ClientContext clientContext, string filter)
        {
            RestService restService = new RestService();
            JArray jArray = new JArray();
            RESTOption rESTOption = new RESTOption();

            rESTOption.filter = filter;
            rESTOption.select = "ID,Award,AwardedBy,AwardedOn,Description,EmpCode/Id,EmpCode/EmpCode";
            rESTOption.expand = "EmpCode";
            rESTOption.top = "5000";
            jArray = restService.GetAllItemFromList(clientContext, "Emp_AwardsDetails", rESTOption);
            return jArray;
        }

        private string RESTSave(ClientContext clientContext, string ItemData)
        {
            RestService restService = new RestService();

            return restService.SaveItem(clientContext, "Emp_AwardsDetails", ItemData);
        }

        private string RESTUpdate(ClientContext clientContext, string ItemData, string ID)
        {
            RestService restService = new RestService();

            return restService.UpdateItem(clientContext, "Emp_AwardsDetails", ItemData, ID);
        }

    }
}
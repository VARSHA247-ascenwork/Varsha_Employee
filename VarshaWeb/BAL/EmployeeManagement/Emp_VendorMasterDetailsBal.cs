using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VarshaWeb.Models.EmployeeManagement;
using VarshaWeb.DAL;
using VarshaWeb.Models;

namespace VarshaWeb.BAL.EmployeeManagement
{
    public class Emp_VendorMasterDetailsBal
    {
            public string saveVendor(ClientContext clientContext, string ItemData)
            {

                string response = RESTSave(clientContext, ItemData);

                return response;
            }

            public string UpdateVendor(ClientContext clientContext, string ItemData, string ID)
            {
            string response = RESTUpdate(clientContext, ItemData, ID);
            return response;
            }

             public int UploadDocument(ClientContext clientContext, HttpPostedFileBase files, string ItemData)
            {

                return RESTUploadFile(clientContext, files, ItemData);
            }
             public List<Emp_VendorMasterDetailsModel> GetAllVendor(ClientContext clientContext)
            {
            List<Emp_VendorMasterDetailsModel> VendorInfo = new List<Emp_VendorMasterDetailsModel>();

            JArray jArray = RESTGet(clientContext, null);

            foreach (JObject j in jArray)
            {
                VendorInfo.Add(new Emp_VendorMasterDetailsModel
                {
                    ID = Convert.ToInt32(j["ID"]),
                    VendorName = j["VendorName"] == null ? "" : j["VendorName"].ToString(),
                    VendorAddress = j["VendorAddress"] == null ? "" : j["VendorAddress"].ToString(),
                    VendorContact = j["VendorContact"] == null ? "" : j["VendorContact"].ToString(),
                      MobileNo = j["MobileNo"] == null ? "" : j["MobileNo"].ToString(),
                    VendormailID = j["VendormailID"] == null ? "" : j["VendormailID"].ToString(),
                    Designation = j["Designation"]["Designation"] == null ? "" : j["Designation"]["Designation"].ToString(),
                    VendorCompany = j["VendorCompany"] == null ? "" : j["VendorCompany"].ToString(),
                    City = j["City"] == null ? "" : j["City"].ToString(),
                    GstNo = j["GstNo"] == null ? "" : j["GstNo"].ToString(),
                    PanCardNo = j["PanCardNo"] == null ? "" : j["PanCardNo"].ToString(),
                    Remark = j["Remark"] == null ? "" : j["Remark"].ToString(),
                  
                    VendorState = j["VendorState"]["StateName"] == null ? "" : j["VendorState"]["StateName"].ToString(),
                    Country = j["Country"]["CountryName"] == null ? "" : j["Country"]["CountryName"].ToString(),

                });
            }
            return VendorInfo;

        }


        public List<Emp_VendorMasterDetailsModel> GetVendorById(ClientContext clientContext, string vendorid)
        {
            List<Emp_VendorMasterDetailsModel> Vendordetails = new List<Emp_VendorMasterDetailsModel>();
          //  string filter = "ID eq " + vendorid;
            string filter = "ID eq '" + vendorid + "'";
            JArray jArray = RESTGet(clientContext, filter);

            foreach (JObject j in jArray)
            {
                Vendordetails.Add(new Emp_VendorMasterDetailsModel
                {
                    ID = Convert.ToInt32(j["ID"]),
                    VendorName = j["VendorName"] == null ? "" : j["VendorName"].ToString(),
                    VendorAddress = j["VendorAddress"] == null ? "" : j["VendorAddress"].ToString(),
                    VendorContact = j["VendorContact"] == null ? "" : j["VendorContact"].ToString(),
                    VendormailID = j["VendormailID"] == null ? "" : j["VendormailID"].ToString(),
                    DesignationId = Convert.ToInt32(j["Designation"]["Id"]),
                    Designation = j["Designation"]["Designation"] == null ? "" : j["Designation"]["Designation"].ToString(),
                    VendorCompany = j["VendorCompany"] == null ? "" : j["VendorCompany"].ToString(),
                    City = j["City"] == null ? "" : j["City"].ToString(),
                    GstNo = j["GstNo"] == null ? "" : j["GstNo"].ToString(),
                    PanCardNo = j["PanCardNo"] == null ? "" : j["PanCardNo"].ToString(),
                    Remark = j["Remark"] == null ? "" : j["Remark"].ToString(),
                    VendorStateId = Convert.ToInt32(j["VendorState"]["Id"]),
                    VendorState = j["VendorState"]["StateName"] == null ? "" : j["VendorState"]["StateName"].ToString(),
                    CountryId = Convert.ToInt32(j["Country"]["Id"]),
                    Country = j["Country"]["CountryName"] == null ? "" : j["Country"]["CountryName"].ToString(),
                    MobileNo = j["MobileNo"] == null ? "" : j["MobileNo"].ToString(),
                    });
                }
            return Vendordetails;

        }

        private JArray RESTGet(ClientContext clientContext, string filter)
            {
                RestService restService = new RestService();
                JArray jArray = new JArray();
                RESTOption rESTOption = new RESTOption();
                rESTOption.filter = filter;
                rESTOption.select = "ID,VendorName,VendorAddress,VendorContact,VendormailID,VendorCompany,City,GstNo,PanCardNo,Designation/Id,Designation/Designation,Country/Id,Country/CountryName,Remark,VendorState/Id,VendorState/StateName,MobileNo";
                rESTOption.expand = "Country,VendorState,Designation";
                rESTOption.top = "5000";
                jArray = restService.GetAllItemFromList(clientContext, "Emp_VendorMasterDetails", rESTOption);
                return jArray;
            }

       
            private string RESTSave(ClientContext clientContext, string ItemData)
            {
                RestService restService = new RestService();

                return restService.SaveItem(clientContext, "Emp_VendorMasterDetails", ItemData);
            }

            private string RESTUpdate(ClientContext clientContext, string ItemData, string ID)
            {
                 RestService restService = new RestService();

                 return restService.UpdateItem(clientContext, "Emp_VendorMasterDetails", ItemData, ID);
            }
            private int RESTUploadFile(ClientContext clientContext, HttpPostedFileBase files, string ItemData)
            {

            RestService restService = new RestService();

            return restService.UploadDocument(clientContext, "Emp_Vendor_Document", files, ItemData);
            }


    }
 }

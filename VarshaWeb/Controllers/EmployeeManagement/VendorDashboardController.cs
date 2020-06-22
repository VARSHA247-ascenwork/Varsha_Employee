using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using VarshaWeb.BAL.EmployeeManagement;
using VarshaWeb.Models.EmployeeManagement;
using VarshaWeb.DAL;
using VarshaWeb.Models;
using VarshaWeb.BAL;
using Newtonsoft.Json;

namespace VarshaWeb.Controllers.EmployeeManagement
{
    public class VendorDashboardController : Controller
    {
        List<Emp_VendorMasterDetailsModel> vendor_datail = new List<Emp_VendorMasterDetailsModel>();
        Emp_VendorMasterDetailsBal vendor = new Emp_VendorMasterDetailsBal();

       List<Emp_Vendor_DocumentModel> vendor_Doc = new List<Emp_Vendor_DocumentModel>();
       Emp_Vendor_DocumentBal vendorDocument = new Emp_Vendor_DocumentBal();

        //Get state
        List<Emp_Vendor_State_MasterModel> vendor_state = new List<Emp_Vendor_State_MasterModel>();
        Emp_Vendor_State_MasterBal state = new Emp_Vendor_State_MasterBal();

        //Get Country
        List<Emp_CountryModel> vendor_country = new List<Emp_CountryModel>();
        Emp_CountryBal country = new Emp_CountryBal();


        //get designation
        List<Emp_DesignationModel>  vendor_designation = new List<Emp_DesignationModel>();
        Emp_DesignationBal designation = new Emp_DesignationBal();

        // GET: VendorDashboard
      
        public ActionResult Index()
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {

                vendor_datail = vendor.GetAllVendor(clientContext);

            }
            ViewBag.vendorInfo = vendor_datail;
            return View();
        }
        [SharePointContextFilter]
        public ActionResult AddVendor()
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
               vendor_state = state.GetState(clientContext);
               vendor_country = country.GetAllCountries(clientContext);
                vendor_designation = designation.GetDesignation(clientContext);
            }
            ViewBag.state = vendor_state;
            ViewBag.country = vendor_country;
            ViewBag.designation = vendor_designation;
            return View();
        }
        [SharePointContextFilter]
        public ActionResult VendorDetails()
        {
            string ViewVendorID = Request.Cookies["ViewVendorID"].Value;
            Emp_VendorMasterDetailsBal VendorBal = new Emp_VendorMasterDetailsBal();
            List<Emp_VendorMasterDetailsModel> VendorView = new List<Emp_VendorMasterDetailsModel>();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                VendorView = VendorBal.GetVendorById(clientContext, ViewVendorID);
            }
            ViewBag.VendorInfo = VendorView;
            return View();
        }
        
     /*   public ActionResult VendorEdit()
        {
            // string items = "":
            string EditVendorID = Request.Cookies["EditVendorID"].Value;
            Emp_VendorMasterDetailsBal VendorBal = new Emp_VendorMasterDetailsBal();

            List<Emp_VendorMasterDetailsModel> VendorEdit = new List<Emp_VendorMasterDetailsModel>();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
             //   var path = spContext.SPHostUrl.Scheme + "://" + spContext.SPHostUrl.Host.ToString();
                VendorEdit = VendorBal.GetVendorById(clientContext, EditVendorID);
                vendor_designation = designation.GetDesignation(clientContext);
                vendor_state = state.GetState(clientContext);
                vendor_country = country.GetAllCountries(clientContext);

              //  vendor_Doc = vendorDocument.GetVendorDocumentById(clientContext, EditVendorID,path);
            }
            ViewBag.Vendordata = VendorEdit;
            ViewBag.designation = vendor_designation;
            ViewBag.state = vendor_state;
            ViewBag.country = vendor_country;
          //  ViewBag.document = vendor_Doc;
            return View();
        } */
        [SharePointContextFilter]
        public ActionResult EditVendorDetail()
        {
            // string items = "":
            string EditVendorID = Request.Cookies["EditVendorID"].Value;
            Emp_VendorMasterDetailsBal VendorBal = new Emp_VendorMasterDetailsBal();

            List<Emp_VendorMasterDetailsModel> VendorEdit = new List<Emp_VendorMasterDetailsModel>();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                   var path = spContext.SPHostUrl.Scheme + "://" + spContext.SPHostUrl.Host.ToString();
                VendorEdit = VendorBal.GetVendorById(clientContext, EditVendorID);
                vendor_designation = designation.GetDesignation(clientContext);
                vendor_state = state.GetState(clientContext);
                vendor_country = country.GetAllCountries(clientContext);

               vendor_Doc = vendorDocument.GetVendorDocumentById(clientContext, EditVendorID,path);
            }
            ViewBag.Vendordata = VendorEdit;
            ViewBag.designation = vendor_designation;
            ViewBag.state = vendor_state;
            ViewBag.country = vendor_country;
            ViewBag.document = vendor_Doc;
            return View();
        }


        [SharePointContextFilter]
        public ActionResult SaveInfo(FormCollection formCollection)
        {
            string returnID = "0";
            
            var name = formCollection["VendorDetails"];

            vendor_datail = JsonConvert.DeserializeObject<List<Emp_VendorMasterDetailsModel>>(name);

            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string itemdata = "'VendorCompany' : '" + vendor_datail[0].VendorCompany + "',";
                itemdata += "'VendorName': '" + vendor_datail[0].VendorName + "',";
                itemdata += "'VendorContact': '" + vendor_datail[0].VendorContact + "',";
                itemdata += "'MobileNo': '" + vendor_datail[0].MobileNo + "',";
                itemdata += "'VendormailID':'" + vendor_datail[0].VendormailID + "',";
                itemdata += "'DesignationId': '" + vendor_datail[0].Designation + "',";
                itemdata += "'VendorAddress': '" + vendor_datail[0].VendorAddress + "',";
                itemdata += "'City':'" + vendor_datail[0].City + "',";
                itemdata += "'StatesId': '" + vendor_datail[0].States + "',";
                itemdata += "'CountryId': '" + vendor_datail[0].Country + "',";
                itemdata += "'PanCardNo': '" + vendor_datail[0].PanCardNo + "',";
                itemdata += "'GstNo':'" + vendor_datail[0].GstNo + "',";
                itemdata += "'Remark':'" + vendor_datail[0].Remark + "'";

                returnID = vendor.saveVendor(clientContext, itemdata);

                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        var postedFile = files[i];
                        string Docdata = "'libraryIdId' : " + returnID;
                        Docdata += ",'DocumentPath' : '" + files[i].FileName + "'";
                        vendor.UploadDocument(clientContext, postedFile, Docdata);

                    }

                }
            }
                return Json(returnID, JsonRequestBehavior.AllowGet);
            
            
        }



        [SharePointContextFilter]
        public ActionResult UpdateInfo(FormCollection formCollection)
        {
            string returnID = "0";
            var name = formCollection["VendorDetails"];
            vendor_datail = JsonConvert.DeserializeObject<List<Emp_VendorMasterDetailsModel>>(name);
            string EditVendorID = Request.Cookies["EditVendorID"].Value;
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                    string itemdata = "'VendorCompany': '" + vendor_datail[0].VendorCompany + "',";
                    itemdata += "'VendorName': '" + vendor_datail[0].VendorName + "',";
                    itemdata += "'VendorContact': '" + vendor_datail[0].VendorContact + "',";
                    itemdata += "'MobileNo': '" + vendor_datail[0].MobileNo + "',";
                    itemdata += "'VendormailID': '" + vendor_datail[0].VendormailID + "',";
                    itemdata += "'DesignationId': '" + vendor_datail[0].Designation + "',";
                    itemdata += "'VendorAddress': '" + vendor_datail[0].VendorAddress + "',";
                    itemdata += "'City': '" + vendor_datail[0].City + "',";
                    itemdata += "'StatesId':" + vendor_datail[0].States + ",";
                    itemdata += "'CountryId':" + vendor_datail[0].Country + ",";
                    itemdata += "'PanCardNo': '" + vendor_datail[0].PanCardNo + "',";
                    itemdata += "'GstNo': '" + vendor_datail[0].GstNo + "',";
                    itemdata += "'Remark': '" + vendor_datail[0].Remark + "'";
                
                    returnID=vendor.UpdateVendor(clientContext, itemdata, EditVendorID);
                       if (Request.Files.Count > 0)
                        {
                          HttpFileCollectionBase files = Request.Files;
                             for (int i = 0; i < files.Count; i++)
                             {
                                  var postedFile = files[i];
                                  string docdata = "'libraryIdId' : " + returnID;
                                  docdata += ",'DocumentPath' : '" + files[i].FileName + "'";
                                  vendor.UploadDocument(clientContext, postedFile, docdata);

                    }
                }
            }
            
            return Json(returnID, JsonRequestBehavior.AllowGet);
        }


    }
}
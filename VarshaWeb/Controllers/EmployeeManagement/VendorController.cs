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
    public class VendorController : Controller
    {
        List<Emp_VendorMasterDetailsModel> vendorModel = new List<Emp_VendorMasterDetailsModel>();
        Emp_VendorMasterDetailsBal vendorBal = new Emp_VendorMasterDetailsBal();

        List<Emp_Vendor_DocumentModel> vendor_Doc = new List<Emp_Vendor_DocumentModel>();
        Emp_Vendor_DocumentBal vendorDocument = new Emp_Vendor_DocumentBal();

        List<Emp_CountryModel> vendor_country = new List<Emp_CountryModel>();
        Emp_CountryBal country = new Emp_CountryBal();

        Emp_StateMasterBal emp_StateMasterBal = new Emp_StateMasterBal();

        //get designation
        List<Emp_DesignationModel> vendor_designation = new List<Emp_DesignationModel>();
        Emp_DesignationBal designation = new Emp_DesignationBal();
        // GET: Vendor
        public ActionResult Index()
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                ViewBag.designation = designation.GetDesignation(clientContext);
                ViewBag.country = country.GetAllCountries(clientContext);
            }
            return View();
        }
        public ActionResult getVendordata(int id)
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                vendorModel = vendorBal.GetAllVendor(clientContext);
            }
            return Json(vendorModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getVendordataById(string VId)
        {
         
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                var path = spContext.SPHostUrl.Scheme + "://" + spContext.SPHostUrl.Host.ToString();
                vendorModel = vendorBal.GetVendorById(clientContext, VId);
                vendor_Doc = vendorDocument.GetVendorDocumentById(clientContext, VId, path);

            }
            return Json(vendorModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getVendorDocument(string VId)
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                var path = spContext.SPHostUrl.Scheme + "://" + spContext.SPHostUrl.Host.ToString();
                vendor_Doc = vendorDocument.GetVendorDocumentById(clientContext, VId, path);
            }
            return Json(vendor_Doc, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetState(Emp_StateMasterModel emp_State)
        {
            List<Emp_StateMasterModel> emp_StateMasterModels = new List<Emp_StateMasterModel>();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_StateMasterModels = emp_StateMasterBal.GetStateById(clientContext, emp_State.CountryNameID);
            }
            return Json(emp_StateMasterModels, JsonRequestBehavior.AllowGet);
        }

        [SharePointContextFilter]
        public ActionResult SaveInfo(FormCollection formCollection)
        {
            string returnID = "0";

            var name = formCollection["VendorDetails"];

            vendorModel = JsonConvert.DeserializeObject<List<Emp_VendorMasterDetailsModel>>(name);

            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);

            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string itemdata = "'VendorCompany' : '" + vendorModel[0].VendorCompany + "',";
                itemdata += "'VendorName': '" + vendorModel[0].VendorName + "',";
                itemdata += "'VendorContact': '" + vendorModel[0].VendorContact + "',";
                itemdata += "'MobileNo': '" + vendorModel[0].MobileNo + "',";
                itemdata += "'VendormailID':'" + vendorModel[0].VendormailID + "',";
                itemdata += "'DesignationId': '" + vendorModel[0].Designation + "',";
                itemdata += "'VendorAddress': '" + vendorModel[0].VendorAddress + "',";
                itemdata += "'City':'" + vendorModel[0].City + "',";
                itemdata += "'VendorStateId': " + vendorModel[0].VendorState + ",";
                itemdata += "'CountryId':" + vendorModel[0].Country + ",";
                itemdata += "'PanCardNo': '" + vendorModel[0].PanCardNo + "',";
                itemdata += "'GstNo':'" + vendorModel[0].GstNo + "',";
                itemdata += "'Remark':'" + vendorModel[0].Remark + "'";

                returnID = vendorBal.saveVendor(clientContext, itemdata);

                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        var postedFile = files[i];
                        string Docdata = "'libraryIdId' : " + returnID;
                        Docdata += ",'DocumentPath' : '" + files[i].FileName + "'";
                        vendorBal.UploadDocument(clientContext, postedFile, Docdata);

                    }

                }
            }
            return Json(returnID, JsonRequestBehavior.AllowGet);


        }

    /*  [SharePointContextFilter]
             public ActionResult UpdateInfo(FormCollection formCollection)
             {
                 string returnID = "0";
                 var name = formCollection["VendorDetails"];
                 vendorModel = JsonConvert.DeserializeObject<List<Emp_VendorMasterDetailsModel>>(name);
              string ID = vendorModel[0].ID.ToString();

              // string EditVendorID = Request.Cookies["EditVendorID"].Value;
              var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
                 using (var clientContext = spContext.CreateUserClientContextForSPHost())
                 {
                     string itemdata = "'VendorCompany': '" + vendorModel[0].VendorCompany + "',";
                     itemdata += "'VendorName': '" + vendorModel[0].VendorName + "',";
                     itemdata += "'VendorContact': '" + vendorModel[0].VendorContact + "',";
                     itemdata += "'MobileNo': '" + vendorModel[0].MobileNo + "',";
                     itemdata += "'VendormailID': '" + vendorModel[0].VendormailID + "',";
                     itemdata += "'DesignationId': '" + vendorModel[0].Designation + "',";
                     itemdata += "'VendorAddress': '" + vendorModel[0].VendorAddress + "',";
                     itemdata += "'City': '" + vendorModel[0].City + "',";
                     itemdata += "'StatesId':" + vendorModel[0].VendorStateId + ",";
                     itemdata += "'CountryId':" + vendorModel[0].Country + ",";
                     itemdata += "'PanCardNo': '" + vendorModel[0].PanCardNo + "',";
                     itemdata += "'GstNo': '" + vendorModel[0].GstNo + "',";
                     itemdata += "'Remark': '" + vendorModel[0].Remark + "'";

                     returnID = vendorBal.UpdateVendor(clientContext, itemdata, ID);
                     if (Request.Files.Count > 0)
                     {
                         HttpFileCollectionBase files = Request.Files;
                         for (int i = 0; i < files.Count; i++)
                         {
                             var postedFile = files[i];
                             string docdata = "'libraryIdId' : " + returnID;
                             docdata += ",'DocumentPath' : '" + files[i].FileName + "'";
                             vendorBal.UploadDocument(clientContext, postedFile, docdata);
                         }
                     }
                 }
                 return Json(returnID, JsonRequestBehavior.AllowGet);
             }   */

    }
}
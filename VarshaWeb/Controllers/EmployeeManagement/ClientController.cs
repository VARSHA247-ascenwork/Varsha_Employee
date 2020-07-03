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
    public class ClientController : Controller
    {
        List<Emp_ClientMasterDetailsModel> ClientModel = new List<Emp_ClientMasterDetailsModel>();
        Emp_ClientMasterDetailsBal ClientBal = new Emp_ClientMasterDetailsBal();

        List<Emp_Client_DocumentModel> Client_Doc = new List<Emp_Client_DocumentModel>();
        Emp_Client_DocumentBal ClientDocument = new Emp_Client_DocumentBal();

        List<Emp_CountryModel> Client_country = new List<Emp_CountryModel>();
        Emp_CountryBal country = new Emp_CountryBal();

        Emp_StateMasterBal emp_StateMasterBal = new Emp_StateMasterBal();

        //get designation
        List<Emp_DesignationModel> Client_designation = new List<Emp_DesignationModel>();
        Emp_DesignationBal designation = new Emp_DesignationBal();
        // GET: Client
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

        public ActionResult getClientdata(int id)
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                ClientModel = ClientBal.GetAllClient(clientContext);
            }
            return Json(ClientModel, JsonRequestBehavior.AllowGet);
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
        public ActionResult getClientdataById(string CId)
        {

            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
              //  var path = spContext.SPHostUrl.Scheme + "://" + spContext.SPHostUrl.Host.ToString();
                ClientModel = ClientBal.GetClientById(clientContext, CId);
              //  Client_Doc = ClientDocument.GetClientDocumentById(clientContext, CId, path);

            }
            Session["ClientID"] = CId;
            return Json(ClientModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getClientDocument(string CId)
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                var path = spContext.SPHostUrl.Scheme + "://" + spContext.SPHostUrl.Host.ToString();
                Client_Doc = ClientDocument.GetClientDocumentById(clientContext, CId, path);
            }
            return Json(Client_Doc, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteDocument(string DId)
        {
            string id = Session["ClientID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                ClientDocument.DeleteClientDocument(clientContext, DId);
                var path = spContext.SPHostUrl.Scheme + "://" + spContext.SPHostUrl.Host.ToString();
                Client_Doc = ClientDocument.GetClientDocumentById(clientContext, id, path);
            }
            return Json(Client_Doc, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveInfo(FormCollection formCollection)
        {
            string returnID = "0";
            var name = formCollection["ClientDetails"];
            ClientModel = JsonConvert.DeserializeObject<List<Emp_ClientMasterDetailsModel>>(name);

            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string itemdata = "'ClientName' : '" + ClientModel[0].ClientName + "',";
                itemdata += "'ClientAddress': '" + ClientModel[0].ClientAddress + "',";
                itemdata += "'ClientContact': '" + ClientModel[0].ClientContact + "',";
                itemdata += "'ClientStateId': " + ClientModel[0].ClientState + ",";
                itemdata += "'ClientCountryId': " + ClientModel[0].ClientCountry + ",";
                itemdata += "'ClientDesignationId': " + ClientModel[0].ClientDesignation + ",";
                itemdata += "'ClientMailID': '" + ClientModel[0].ClientMailID + "',";
                itemdata += "'ClientGSTNO': '" + ClientModel[0].ClientGSTNO + "',";
                itemdata += "'ClientPanCardNo': '" + ClientModel[0].ClientPanCardNo + "',";
                itemdata += "'ClientRemark': '" + ClientModel[0].ClientRemark + "',";
                itemdata += "'ClientCity': '" + ClientModel[0].ClientCity + "',";
                itemdata += "'MobileNo': '" + ClientModel[0].MobileNo + "'";
                returnID = ClientBal.SaveClient(clientContext, itemdata);

                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        var postedFile = files[i];
                        string item = "'libraryIdId' : " + returnID;
                        item += ",'DocumentPath' : '" + files[i].FileName + "'";
                        ClientBal.UploadDocument(clientContext, postedFile, item);

                    }

                }
            }

            return Json(returnID, JsonRequestBehavior.AllowGet);
        }

        [SharePointContextFilter]
        public ActionResult UpdateInfo(FormCollection formCollection)
        {
            string returnID = "0";
            var name = formCollection["ClientDetails"];
            ClientModel = JsonConvert.DeserializeObject<List<Emp_ClientMasterDetailsModel>>(name);
            string ID = ClientModel[0].ID.ToString();

            // string EditVendorID = Request.Cookies["EditVendorID"].Value;
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string itemdata = "'ClientName' : '" + ClientModel[0].ClientName + "',";
                itemdata += "'ClientAddress': '" + ClientModel[0].ClientAddress + "',";
                itemdata += "'ClientContact': '" + ClientModel[0].ClientContact + "',";
                itemdata += "'ClientStateId': " + ClientModel[0].ClientState + ",";
                itemdata += "'ClientCountryId': " + ClientModel[0].ClientCountry + ",";
                itemdata += "'ClientDesignationId': " + ClientModel[0].ClientDesignation + ",";
                itemdata += "'ClientMailID': '" + ClientModel[0].ClientMailID + "',";
                itemdata += "'ClientGSTNO': '" + ClientModel[0].ClientGSTNO + "',";
                itemdata += "'ClientPanCardNo': '" + ClientModel[0].ClientPanCardNo + "',";
                itemdata += "'ClientRemark': '" + ClientModel[0].ClientRemark + "',";
                itemdata += "'ClientCity': '" + ClientModel[0].ClientCity + "',";
                itemdata += "'MobileNo': '" + ClientModel[0].MobileNo + "'";
                returnID = ClientBal.UpdateClient(clientContext, itemdata, ID);
                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {

                        var postedFile = files[i];
                        string docdata = "'libraryIdId' : " + ID;
                        docdata += ",'DocumentPath' : '" + files[i].FileName + "'";
                        ClientBal.UploadDocument(clientContext, postedFile, docdata);
                    }


                }
            }
            return Json(returnID, JsonRequestBehavior.AllowGet);
        }
    }
}
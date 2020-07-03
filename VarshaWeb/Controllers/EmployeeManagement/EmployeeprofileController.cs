using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using WizrrWeb.BAL.EmployeeManagement;
using WizrrWeb.Models.EmployeeManagement;
using WizrrWeb.Models.EmployeeManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WizrrWeb.Controllers.EmployeeManagement
{
    public class EmployeeprofileController : Controller
    {

        #region All References
        //Models References
        List<Emp_BasicInfoModel> emp_BasicInfos = new List<Emp_BasicInfoModel>();
        List<EmpProfilePicModel> empProfilePics = new List<EmpProfilePicModel>();
        List<Emp_CompanyMasterModel> emp_CompanyMasterModels = new List<Emp_CompanyMasterModel>();
        List<Emp_EducationalDetailsModel> emp_EducationalDetails = new List<Emp_EducationalDetailsModel>();
        List<Emp_WorkExperienceModel> emp_WorkExperienceModels = new List<Emp_WorkExperienceModel>();
        List<Emp_AwardsDetailsModel> emp_AwardsDetailsModels = new List<Emp_AwardsDetailsModel>();
        List<Emp_PublicationDetailsModel> emp_PublicationDetailsModels = new List<Emp_PublicationDetailsModel>();
        List<Emp_ReferenceDetailsModel> emp_ReferenceDetailsModels = new List<Emp_ReferenceDetailsModel>();
        List<Emp_DisciplinaryDetailsModel> emp_DisciplinaryDetailsModels = new List<Emp_DisciplinaryDetailsModel>();
        List<Emp_VisaInformationModel> emp_VisaInformationModels = new List<Emp_VisaInformationModel>();
        List<Emp_BankingDetailsModel> emp_BankingDetailsModels = new List<Emp_BankingDetailsModel>();
        List<Emp_AddressModel> emp_AddressModel = new List<Emp_AddressModel>();
        List<Emp_EmergencyContactModel> emp_EmergencyContactModels = new List<Emp_EmergencyContactModel>();
        List<Emp_FamilyDetailsModel> emp_FamilyDetailsModels = new List<Emp_FamilyDetailsModel>();
        VM_EmployeeProfileModel vM_EmployeeProfileModel = new VM_EmployeeProfileModel();


        //BAL References
        Emp_BasicInfoBal emp_BasicInfoBal = new Emp_BasicInfoBal();
        EmpProfilePicBal empProfilePicBal = new EmpProfilePicBal();
        Emp_CompanyMasterBal emp_CompanyMasterBal = new Emp_CompanyMasterBal();
        Emp_EducationalDetailsBal emp_EducationalDetailsBal = new Emp_EducationalDetailsBal();
        Emp_WorkExperienceModelBal emp_WorkExperienceModelBal = new Emp_WorkExperienceModelBal();
        Emp_AwardsDetailsBal emp_AwardsDetailsBal = new Emp_AwardsDetailsBal();
        Emp_PublicationDetailsBal emp_PublicationDetailsBal = new Emp_PublicationDetailsBal();
        Emp_ReferenceDetailsBal emp_ReferenceDetailsBal = new Emp_ReferenceDetailsBal();
        Emp_DisciplinaryDetailsBal emp_DisciplinaryDetailsBal = new Emp_DisciplinaryDetailsBal();
        Emp_VisaInformationBal emp_VisaInformationBal = new Emp_VisaInformationBal();
        Emp_BankingDetailsBal emp_BankingDetailsBal = new Emp_BankingDetailsBal();
        Emp_CountryBal emp_CountryBal = new Emp_CountryBal();
        Emp_StateMasterBal emp_StateMasterBal = new Emp_StateMasterBal();
        Emp_AddressBal emp_AddressBal = new Emp_AddressBal();
        Emp_EmergencyContactBal emp_EmergencyContactBal = new Emp_EmergencyContactBal();
        Emp_FamilyDetailsBal emp_FamilyDetailsBal = new Emp_FamilyDetailsBal();
        Emp_MedicalInformationBal emp_MedicalInformationBal = new Emp_MedicalInformationBal(); 

        #endregion

        // GET: Employeeprofile
        public ActionResult Index()
        {
            string UserId = Session["UserID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                var path = spContext.SPHostUrl.Scheme + "://" + spContext.SPHostUrl.Host.ToString();
                emp_BasicInfos = emp_BasicInfoBal.GeEmployeeUserID(clientContext, UserId);
                string id= emp_BasicInfos[0].ID.ToString();
                var emp_Basic = emp_BasicInfos.Where(s => s.ID == Convert.ToInt32(id)).FirstOrDefault();

                Session["EMPID"] = id;
                empProfilePics = empProfilePicBal.GetEmpProfileById(clientContext, id, path);
                var empProfile = empProfilePics.Where(s => s.EmpCodeId == id).FirstOrDefault();

                string CompanyId= emp_BasicInfos[0].CompanyId.ToString();
                emp_CompanyMasterModels = emp_CompanyMasterBal.GetCompanyByID(clientContext, CompanyId);
                var empCompany = emp_CompanyMasterModels.Where(s => s.ID == Convert.ToInt32(CompanyId)).FirstOrDefault();
                
                vM_EmployeeProfileModel.emp_BasicInfo = emp_Basic;
                vM_EmployeeProfileModel.empProfilePic = empProfile;
                vM_EmployeeProfileModel.emp_CompanyMaster = empCompany;
                vM_EmployeeProfileModel.emp_EducationalDetails = emp_EducationalDetailsBal.GetEducationDetailsByEmpID(clientContext, id);
                vM_EmployeeProfileModel.emp_WorkExperienceModels = emp_WorkExperienceModelBal.GetWorkExperienceByEmpID(clientContext, id); 
                vM_EmployeeProfileModel.emp_AwardsDetails = emp_AwardsDetailsBal.GetAwardsDetailsByEmpID(clientContext, id);
                vM_EmployeeProfileModel.emp_PublicationDetails = emp_PublicationDetailsBal.GetPublicationDetailsByEmpID(clientContext, id);
                vM_EmployeeProfileModel.emp_ReferenceDetails = emp_ReferenceDetailsBal.GetReferenceDetailsByEmpID(clientContext, id);
                vM_EmployeeProfileModel.emp_DisciplinaryDetails = emp_DisciplinaryDetailsBal.GetDisciplinaryDetailsByEmpID(clientContext, id);
                vM_EmployeeProfileModel.emp_VisaInformation = emp_VisaInformationBal.GetVisaInformationByEmpID(clientContext, id);
                vM_EmployeeProfileModel.emp_BankingDetails = emp_BankingDetailsBal.GetBankingDetailsByEmpID(clientContext, id);
                vM_EmployeeProfileModel.emp_Countries = emp_CountryBal.GetAllCountries(clientContext);
                vM_EmployeeProfileModel.emp_Addresses = emp_AddressBal.GetEmpAddressByEmpID(clientContext, id);
                vM_EmployeeProfileModel.emp_emergencyContactModels = emp_EmergencyContactBal.GetEmergencyContactByEmpID(clientContext,id);
                vM_EmployeeProfileModel.emp_FamilyDetails = emp_FamilyDetailsBal.GetFamilyDetailsByEmpID(clientContext,id);
                vM_EmployeeProfileModel.emp_MedicalInformation = emp_MedicalInformationBal.GetMedicalInformationByEmpID(clientContext,id);
            }
            return View(vM_EmployeeProfileModel);
        }


        #region CRUD Action Methods For Educational Details

        public ActionResult SaveEducationDetails(Emp_EducationalDetailsModel emp_Educational)
        {
            string EmpId = Session["EMPID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = emp_Educational.UniversityInstitute == null ? "'UniversityInstitute' : null" : "'UniversityInstitute' : '" + emp_Educational.UniversityInstitute + "'";
                items += emp_Educational.Degree==null? ",'Degree' : null" : ",'Degree' : '" + emp_Educational.Degree + "'";
                items += emp_Educational.From == "Invalid date" ? ",'From' : null" : ",'From' : '" + emp_Educational.From + "'";
                items += emp_Educational.To == "Invalid date" ? ",'To' : null" : ",'To' : '" + emp_Educational.To + "'";
                items += emp_Educational.MarksInPercentage == null ? ",'MarksInPercentage' : null" : ",'MarksInPercentage' : '" + emp_Educational.MarksInPercentage + "'";
                items += emp_Educational.Subject == null ? ",'Subject' : null" : ",'Subject' : '" + emp_Educational.Subject + "'";
                items += ",'EmpCodeId' : " + EmpId;
                emp_EducationalDetailsBal.SaveEducationDetails(clientContext, items);

                vM_EmployeeProfileModel.emp_EducationalDetails = emp_EducationalDetailsBal.GetEducationDetailsByEmpID(clientContext, EmpId);

            }
            return PartialView("_PV_Emp_EducationDetailsListView", vM_EmployeeProfileModel.emp_EducationalDetails);
        }
        public ActionResult DeleteEducationDetails(Emp_EducationalDetailsModel emp_Educational)
        {
            string returnId = "";
            string EmpId = Session["EMPID"].ToString();
            string id = emp_Educational.ID.ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                returnId = emp_EducationalDetailsBal.DeleteEducationDetails(clientContext, id);
                vM_EmployeeProfileModel.emp_EducationalDetails = emp_EducationalDetailsBal.GetEducationDetailsByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_EducationDetailsListView", vM_EmployeeProfileModel.emp_EducationalDetails);
        }

        public ActionResult EditEducationDetails(Emp_EducationalDetailsModel emp)
        {
            Session["EducationID"] = emp.ID.ToString();
            Emp_EducationalDetailsModel emp_Educational = new Emp_EducationalDetailsModel();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_EducationalDetails = emp_EducationalDetailsBal.GetEducationDetailsByID(clientContext, emp.ID.ToString());
                emp_Educational = emp_EducationalDetails.Where(s => s.ID == Convert.ToInt32(emp.ID.ToString())).FirstOrDefault();
            }
            return PartialView("_PV_Emp_EducationEditView", emp_Educational);
        }

        public ActionResult UpdateEducationDetails(Emp_EducationalDetailsModel emp_Educational)
        {
            string id = Session["EducationID"].ToString();
            string EmpId = Session["EMPID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = emp_Educational.UniversityInstitute == null ? "'UniversityInstitute' : null" : "'UniversityInstitute' : '" + emp_Educational.UniversityInstitute + "'";
                items += emp_Educational.Degree == null ? ",'Degree' : null" : ",'Degree' : '" + emp_Educational.Degree + "'";
                items += emp_Educational.From == "Invalid date" ? ",'From' : null" : ",'From' : '" + emp_Educational.From + "'";
                items += emp_Educational.To == "Invalid date" ? ",'To' : null" : ",'To' : '" + emp_Educational.To + "'";
                items += emp_Educational.MarksInPercentage == null ? ",'MarksInPercentage' : null" : ",'MarksInPercentage' : '" + emp_Educational.MarksInPercentage + "'";
                items += emp_Educational.Subject == null ? ",'Subject' : null" : ",'Subject' : '" + emp_Educational.Subject + "'";
                items += ",'EmpCodeId' : " + EmpId;
                emp_EducationalDetailsBal.UpdateEducationDetails(clientContext, items, id);

                vM_EmployeeProfileModel.emp_EducationalDetails = emp_EducationalDetailsBal.GetEducationDetailsByEmpID(clientContext, EmpId);

            }
            return PartialView("_PV_Emp_EducationDetailsListView", vM_EmployeeProfileModel.emp_EducationalDetails);
        }

        #endregion


        #region CRUD Action Methods for Work Experience

        public ActionResult SaveWorkExperience(Emp_WorkExperienceModel emp_Work)
        {
            string EmpId = Session["EMPID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {

                string items =emp_Work.OrganizationName==null? "'OrganizationName':null" : "'OrganizationName':'" + emp_Work.OrganizationName + "'";
                items += emp_Work.Industry == null ? ",'Industry':null" : ",'Industry':'" + emp_Work.Industry + "'";
                items += emp_Work.Designation == null ? ",'Designation':null" : ",'Designation':'" + emp_Work.Designation + "'";
                items += emp_Work.FromDate == "Invalid date" ? ",'FromDate':null" : ",'FromDate':'" + emp_Work.FromDate + "'";
                items += emp_Work.ToDate == "Invalid date" ? ",'ToDate':null" : ",'ToDate':'" + emp_Work.ToDate + "'";
                items += emp_Work.Notes == null ? ",'Notes':null" : ",'Notes':'" + emp_Work.Notes + "'";
                items += emp_Work.AnnualSalary == null ? ",'AnnualSalary':null" : ",'AnnualSalary':'" + emp_Work.AnnualSalary + "'";
                items += emp_Work.ContactNumber == null ? ",'ContactNumber':null" : ",'ContactNumber':'" + emp_Work.ContactNumber + "'";
                items += ",'EmpCodeId':" + EmpId;
                emp_WorkExperienceModelBal.SaveEmp_WorkExperience(clientContext, items);
                vM_EmployeeProfileModel.emp_WorkExperienceModels = emp_WorkExperienceModelBal.GetWorkExperienceByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_WorkExperienceListView", vM_EmployeeProfileModel.emp_WorkExperienceModels) ;
        }

        public ActionResult DeleteWorkExperience(Emp_WorkExperienceModel emp_WorkExperience)
        {
            string EmpId = Session["EMPID"].ToString();
            string id = emp_WorkExperience.ID.ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_WorkExperienceModelBal.DeleteEmp_WorkExperience(clientContext, id);
                vM_EmployeeProfileModel.emp_WorkExperienceModels = emp_WorkExperienceModelBal.GetWorkExperienceByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_WorkExperienceListView", vM_EmployeeProfileModel.emp_WorkExperienceModels);
        }

        public ActionResult EditWorkExperience(Emp_WorkExperienceModel emp_Work)
        {
            Session["WorkExperienceID"] = emp_Work.ID.ToString();
            Emp_WorkExperienceModel emp_WorkExperience = new Emp_WorkExperienceModel();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_WorkExperienceModels = emp_WorkExperienceModelBal.GetWorkExperienceByID(clientContext, emp_Work.ID.ToString());
               emp_WorkExperience = emp_WorkExperienceModels.Where(s => s.ID == Convert.ToInt32(emp_Work.ID.ToString())).FirstOrDefault();
            }
            return PartialView("_PV_Emp_WorkExperienceEditView", emp_WorkExperience);
        }

        public ActionResult DetailsWorkExperience(Emp_WorkExperienceModel emp_Work)
        {
            Emp_WorkExperienceModel emp_WorkExperience = new Emp_WorkExperienceModel();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_WorkExperienceModels = emp_WorkExperienceModelBal.GetWorkExperienceByID(clientContext, emp_Work.ID.ToString());
                emp_WorkExperience = emp_WorkExperienceModels.Where(s => s.ID == Convert.ToInt32(emp_Work.ID.ToString())).FirstOrDefault();
            }
            return PartialView("_PV_Emp_WorkExperienceDetailsView", emp_WorkExperience);
        }

        public ActionResult UpdateWorkExperiences(Emp_WorkExperienceModel emp_Work)
        {
            string id = Session["WorkExperienceID"].ToString();
            string EmpId = Session["EMPID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = emp_Work.OrganizationName == null ? "'OrganizationName':null" : "'OrganizationName':'" + emp_Work.OrganizationName + "'";
                items += emp_Work.Industry == null ? ",'Industry':null" : ",'Industry':'" + emp_Work.Industry + "'";
                items += emp_Work.Designation == null ? ",'Designation':null" : ",'Designation':'" + emp_Work.Designation + "'";
                items += emp_Work.FromDate == null ? ",'FromDate':null" : ",'FromDate':'" + emp_Work.FromDate + "'";
                items += emp_Work.ToDate == null ? ",'ToDate':null" : ",'ToDate':'" + emp_Work.ToDate + "'";
                items += emp_Work.Notes == null ? ",'Notes':null" : ",'Notes':'" + emp_Work.Notes + "'";
                items += emp_Work.AnnualSalary == null ? ",'AnnualSalary':null" : ",'AnnualSalary':'" + emp_Work.AnnualSalary + "'";
                items += emp_Work.ContactNumber == null ? ",'ContactNumber':null" : ",'ContactNumber':'" + emp_Work.ContactNumber + "'";
                items += ",'EmpCodeId':" + EmpId;
                emp_WorkExperienceModelBal.UpdateEmp_WorkExperience(clientContext, items, id);
                vM_EmployeeProfileModel.emp_WorkExperienceModels = emp_WorkExperienceModelBal.GetWorkExperienceByEmpID(clientContext, EmpId);

            }
            return PartialView("_PV_Emp_WorkExperienceListView", vM_EmployeeProfileModel.emp_WorkExperienceModels);
        }

        #endregion


        #region CRUD Action Methods for Award Details

        public ActionResult SaveAwardDetails(Emp_AwardsDetailsModel emp_Awards)
        {
            string EmpId = Session["EMPID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Awards.Award==null? ",'Award':null" : ",'Award':'" + emp_Awards.Award + "'";
                items += emp_Awards.AwardedBy == null ? ",'AwardedBy':null" : ",'AwardedBy':'" + emp_Awards.AwardedBy + "'";
                items += emp_Awards.AwardedOn == "Invalid date" ? ",'AwardedOn':null" : ",'AwardedOn':'" + emp_Awards.AwardedOn + "'";
                items += emp_Awards.Description == null ? ",'Description':null" : ",'Description':'" + emp_Awards.Description + "'";
                emp_AwardsDetailsBal.SaveAwardsDetails(clientContext, items);
                vM_EmployeeProfileModel.emp_AwardsDetails = emp_AwardsDetailsBal.GetAwardsDetailsByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_AwardsDetailsListView", vM_EmployeeProfileModel.emp_AwardsDetails);
        }

        public ActionResult DeleteAwardDetails(Emp_AwardsDetailsModel emp_Awards)
        {
            string EmpId = Session["EMPID"].ToString();
            string id = emp_Awards.ID.ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_AwardsDetailsBal.DeleteAwardsDetails(clientContext, id);
                vM_EmployeeProfileModel.emp_AwardsDetails = emp_AwardsDetailsBal.GetAwardsDetailsByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_AwardsDetailsListView", vM_EmployeeProfileModel.emp_AwardsDetails);
        }


        public ActionResult EditAwardDetails(Emp_AwardsDetailsModel emp_Awards)
        {
            Session["AwardDetailsID"] = emp_Awards.ID.ToString();
            Emp_AwardsDetailsModel emp_AwardsDetails = new Emp_AwardsDetailsModel();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_AwardsDetailsModels = emp_AwardsDetailsBal.GetAwardsDetailsByID(clientContext, emp_Awards.ID.ToString());
                emp_AwardsDetails = emp_AwardsDetailsModels.Where(s => s.ID == Convert.ToInt32(emp_Awards.ID.ToString())).FirstOrDefault();
            }
            return PartialView("_PV_Emp_AwardsDetailsEditView", emp_AwardsDetails);
        }

        public ActionResult UpdateAwardDetails(Emp_AwardsDetailsModel emp_Awards)
        {
            string EmpId = Session["EMPID"].ToString();
            string id = Session["AwardDetailsID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Awards.Award == null ? ",'Award':null" : ",'Award':'" + emp_Awards.Award + "'";
                items += emp_Awards.AwardedBy == null ? ",'AwardedBy':null" : ",'AwardedBy':'" + emp_Awards.AwardedBy + "'";
                items += emp_Awards.AwardedOn == "Invalid date" ? ",'AwardedOn':null" : ",'AwardedOn':'" + emp_Awards.AwardedOn + "'";
                items += emp_Awards.Description == null ? ",'Description':null" : ",'Description':'" + emp_Awards.Description + "'";
                emp_AwardsDetailsBal.UpdateAwardsDetails(clientContext, items,id);
                vM_EmployeeProfileModel.emp_AwardsDetails = emp_AwardsDetailsBal.GetAwardsDetailsByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_AwardsDetailsListView", vM_EmployeeProfileModel.emp_AwardsDetails);
        }
        #endregion


        #region CRUD Action Methods for Publication Details

        public ActionResult SavePublication(Emp_PublicationDetailsModel emp_Publication)
        {
            string EmpId = Session["EMPID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Publication.PublishedOn == "Invalid date" ? ",'PublishedOn':null" : ",'PublishedOn':'" + emp_Publication.PublishedOn+ "'";
                items += emp_Publication.Publication == null ? ",'Publication':null" : ",'Publication':'" + emp_Publication.Publication + "'";
                items += emp_Publication.Description == null ? ",'Description':null" : ",'Description':'" + emp_Publication.Description + "'";
                items += emp_Publication.Url == null ? ",'Url':null" : ",'Url':'" + emp_Publication.Url + "'";
                emp_PublicationDetailsBal.SavePublicationDetails(clientContext, items);
                vM_EmployeeProfileModel.emp_PublicationDetails = emp_PublicationDetailsBal.GetPublicationDetailsByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_PublicationListView", vM_EmployeeProfileModel.emp_PublicationDetails);
        }

        public ActionResult DeletePublication(Emp_PublicationDetailsModel emp_Publication)
        {
            string EmpId = Session["EMPID"].ToString();
            string id = emp_Publication.ID.ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_PublicationDetailsBal.DeletePublicationDetails(clientContext, id);
                vM_EmployeeProfileModel.emp_PublicationDetails = emp_PublicationDetailsBal.GetPublicationDetailsByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_PublicationListView", vM_EmployeeProfileModel.emp_PublicationDetails);
        }

        public ActionResult EditPublication(Emp_PublicationDetailsModel emp_Publication)
        {
            Session["PublicationDetailsID"] = emp_Publication.ID.ToString();
            Emp_PublicationDetailsModel publicationDetailsModel = new Emp_PublicationDetailsModel();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_PublicationDetailsModels = emp_PublicationDetailsBal.GetPublicationDetailsByID(clientContext, emp_Publication.ID.ToString());
                publicationDetailsModel = emp_PublicationDetailsModels.Where(s => s.ID == Convert.ToInt32(emp_Publication.ID.ToString())).FirstOrDefault();
            }
            return PartialView("_PV_Emp_PublicationEditView", publicationDetailsModel);
        }

        public ActionResult UpdatePublication(Emp_PublicationDetailsModel emp_Publication)
        {
            string EmpId = Session["EMPID"].ToString();
            string id = Session["PublicationDetailsID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Publication.PublishedOn == "Invalid date" ? ",'PublishedOn':null" : ",'PublishedOn':'" + emp_Publication.PublishedOn + "'";
                items += emp_Publication.Publication == null ? ",'Publication':null" : ",'Publication':'" + emp_Publication.Publication + "'";
                items += emp_Publication.Description == null ? ",'Description':null" : ",'Description':'" + emp_Publication.Description + "'";
                items += emp_Publication.Url == null ? ",'Url':null" : ",'Url':'" + emp_Publication.Url + "'";
                emp_PublicationDetailsBal.UpdatePublicationDetails(clientContext, items,id);
                vM_EmployeeProfileModel.emp_PublicationDetails = emp_PublicationDetailsBal.GetPublicationDetailsByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_PublicationListView", vM_EmployeeProfileModel.emp_PublicationDetails);
        }

        #endregion


        #region CRUD Action Methods for Reference Details

        public ActionResult SaveReferenceDetails(Emp_ReferenceDetailsModel emp_Reference)
        {
            string EmpId = Session["EMPID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Reference.Person == null ? ",'Person':null" : ",'Person':'" + emp_Reference.Person + "'";
                items += emp_Reference.Designation == null ? ",'Designation':null" : ",'Designation':'" + emp_Reference.Designation + "'";
                items += emp_Reference.Company == null ? ",'Company':null" : ",'Company':'" + emp_Reference.Company + "'";
                items += emp_Reference.Email == null ? ",'Email':null" : ",'Email':'" + emp_Reference.Email + "'";
                items += emp_Reference.Phone == null ? ",'Phone':null" : ",'Phone':'" + emp_Reference.Phone + "'";
                items += emp_Reference.HowDoYouKnowPerson == null ? ",'HowDoYouKnowPerson':null" : ",'HowDoYouKnowPerson':'" + emp_Reference.HowDoYouKnowPerson + "'";
                items += emp_Reference.Notes == null ? ",'Notes':null" : ",'Notes':'" + emp_Reference.Notes + "'";
                items += emp_Reference.Address == null ? ",'Address':null" : ",'Address':'" + emp_Reference.Address + "'";
                emp_ReferenceDetailsBal.SaveReferenceDetails(clientContext, items);
                vM_EmployeeProfileModel.emp_ReferenceDetails = emp_ReferenceDetailsBal.GetReferenceDetailsByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_ReferenceListView", vM_EmployeeProfileModel.emp_ReferenceDetails);
        }

        public ActionResult DeleteReferenceDetails(Emp_ReferenceDetailsModel emp_Reference)
        {
            string EmpId = Session["EMPID"].ToString();
            string id = emp_Reference.ID.ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_ReferenceDetailsBal.DeleteReferenceDetails(clientContext, id);
                vM_EmployeeProfileModel.emp_ReferenceDetails = emp_ReferenceDetailsBal.GetReferenceDetailsByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_ReferenceListView", vM_EmployeeProfileModel.emp_ReferenceDetails);
        }

        public ActionResult ReferenceDetails(Emp_ReferenceDetailsModel emp_Reference)
        {
            Emp_ReferenceDetailsModel emp_ReferenceDetails = new Emp_ReferenceDetailsModel();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_ReferenceDetailsModels = emp_ReferenceDetailsBal.GetReferenceDetailsByID(clientContext, emp_Reference.ID.ToString());
                emp_ReferenceDetails = emp_ReferenceDetailsModels.Where(s => s.ID == Convert.ToInt32(emp_Reference.ID.ToString())).FirstOrDefault();
            }
            return PartialView("_PV_Emp_ReferenceDetailsView", emp_ReferenceDetails);
        }

        public ActionResult EditReferenceDetails(Emp_ReferenceDetailsModel emp_Reference)
        {
            Session["ReferenceDetailsID"] = emp_Reference.ID.ToString();
            Emp_ReferenceDetailsModel emp_ReferenceDetails = new Emp_ReferenceDetailsModel();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_ReferenceDetailsModels = emp_ReferenceDetailsBal.GetReferenceDetailsByID(clientContext, emp_Reference.ID.ToString());
                emp_ReferenceDetails = emp_ReferenceDetailsModels.Where(s => s.ID == Convert.ToInt32(emp_Reference.ID.ToString())).FirstOrDefault();
            }
            return PartialView("_PV_Emp_ReferenceEditView", emp_ReferenceDetails);
        }

        public ActionResult UpdateReferenceDetails(Emp_ReferenceDetailsModel emp_Reference)
        {
            string EmpId = Session["EMPID"].ToString();
            string id = Session["ReferenceDetailsID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Reference.Person == null ? ",'Person':null" : ",'Person':'" + emp_Reference.Person + "'";
                items += emp_Reference.Designation == null ? ",'Designation':null" : ",'Designation':'" + emp_Reference.Designation + "'";
                items += emp_Reference.Company == null ? ",'Company':null" : ",'Company':'" + emp_Reference.Company + "'";
                items += emp_Reference.Email == null ? ",'Email':null" : ",'Email':'" + emp_Reference.Email + "'";
                items += emp_Reference.Phone == null ? ",'Phone':null" : ",'Phone':'" + emp_Reference.Phone + "'";
                items += emp_Reference.HowDoYouKnowPerson == null ? ",'HowDoYouKnowPerson':null" : ",'HowDoYouKnowPerson':'" + emp_Reference.HowDoYouKnowPerson + "'";
                items += emp_Reference.Notes == null ? ",'Notes':null" : ",'Notes':'" + emp_Reference.Notes + "'";
                items += emp_Reference.Address == null ? ",'Address':null" : ",'Address':'" + emp_Reference.Address + "'";
                emp_ReferenceDetailsBal.UpdateReferenceDetails(clientContext, items, id);
                vM_EmployeeProfileModel.emp_ReferenceDetails = emp_ReferenceDetailsBal.GetReferenceDetailsByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_ReferenceListView", vM_EmployeeProfileModel.emp_ReferenceDetails);
        }
        

        #endregion


        #region CRUD Action Methods for Disciplinary Details

        public ActionResult SaveDisciplinaryDetails(Emp_DisciplinaryDetailsModel emp_Disciplinary)
        {
            string EmpId = Session["EMPID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Disciplinary.Description == null ? ",'Description':null" : ",'Description':'" + emp_Disciplinary.Description + "'";
                items += emp_Disciplinary.ActionOn == null ? ",'ActionOn':null" : ",'ActionOn':'" + emp_Disciplinary.ActionOn + "'";
                emp_DisciplinaryDetailsBal.SaveDisciplinaryDetails(clientContext, items);
                vM_EmployeeProfileModel.emp_DisciplinaryDetails = emp_DisciplinaryDetailsBal.GetDisciplinaryDetailsByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_DisciplinaryListView", vM_EmployeeProfileModel.emp_DisciplinaryDetails);
        }

        public ActionResult DeleteDisciplinaryDetails(Emp_DisciplinaryDetailsModel emp_Disciplinary)
        {
            string EmpId = Session["EMPID"].ToString();
            string id = emp_Disciplinary.ID.ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_DisciplinaryDetailsBal.DeleteDisciplinaryDetails(clientContext, id);
                vM_EmployeeProfileModel.emp_DisciplinaryDetails = emp_DisciplinaryDetailsBal.GetDisciplinaryDetailsByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_DisciplinaryListView", vM_EmployeeProfileModel.emp_DisciplinaryDetails);
        }

        public ActionResult EditDisciplinaryDetails(Emp_DisciplinaryDetailsModel emp_Disciplinary)
        {
            Session["DisciplinaryDetailsID"] = emp_Disciplinary.ID.ToString();
            Emp_DisciplinaryDetailsModel emp_DisciplinaryDetails = new Emp_DisciplinaryDetailsModel();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_DisciplinaryDetailsModels = emp_DisciplinaryDetailsBal.GetDisciplinaryDetailsByID(clientContext, emp_Disciplinary.ID.ToString());
                emp_DisciplinaryDetails = emp_DisciplinaryDetailsModels.Where(s => s.ID == Convert.ToInt32(emp_Disciplinary.ID.ToString())).FirstOrDefault();
            }
            return PartialView("_PV_Emp_DisciplinaryEditView", emp_DisciplinaryDetails);
        }

        public ActionResult DisciplinaryAddView(string action)
        {
            Emp_DisciplinaryDetailsModel emp_Disciplinary = new Emp_DisciplinaryDetailsModel();
            return PartialView("_PV_Emp_DisciplinaryEditView", emp_Disciplinary);
        }

        public ActionResult UpdateDisciplinaryDetails(Emp_DisciplinaryDetailsModel emp_Disciplinary)
        {
            string EmpId = Session["EMPID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Disciplinary.Description == null ? ",'Description':null" : ",'Description':'" + emp_Disciplinary.Description + "'";
                items += emp_Disciplinary.ActionOn == "Invalid date" ? ",'ActionOn':null" : ",'ActionOn':'" + emp_Disciplinary.ActionOn + "'";
                emp_DisciplinaryDetailsBal.SaveDisciplinaryDetails(clientContext, items);
                vM_EmployeeProfileModel.emp_DisciplinaryDetails = emp_DisciplinaryDetailsBal.GetDisciplinaryDetailsByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_DisciplinaryListView", vM_EmployeeProfileModel.emp_DisciplinaryDetails);
        }

        #endregion


        #region CRUD Action Methods for Visa Details

        public ActionResult SaveVisaDetails(Emp_VisaInformationModel emp_Visa)
        {
            string EmpId = Session["EMPID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Visa.VisaType == null ? ",'VisaType':null" : ",'VisaType':'" + emp_Visa.VisaType + "'";
                items += emp_Visa.Country == null ? ",'CountryId':null" : ",'CountryId':" + emp_Visa.Country;
                items += emp_Visa.ValidUntil == "Invalid date" ? ",'ValidUntil':null" : ",'ValidUntil':'" + emp_Visa.ValidUntil + "'";
                emp_VisaInformationBal.SaveVisaInformationsDetails(clientContext, items);
                vM_EmployeeProfileModel.emp_VisaInformation = emp_VisaInformationBal.GetVisaInformationByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_VisaListView", vM_EmployeeProfileModel.emp_VisaInformation);
        }

        public ActionResult DeleteVisaDetails(Emp_VisaInformationModel emp_Visa)
        {
            string EmpId = Session["EMPID"].ToString();
            string id = emp_Visa.ID.ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_VisaInformationBal.DeleteVisaInformationsDetails(clientContext, id);
                vM_EmployeeProfileModel.emp_VisaInformation = emp_VisaInformationBal.GetVisaInformationByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_VisaListView", vM_EmployeeProfileModel.emp_VisaInformation);
        }

        public ActionResult EditVisaDetails(Emp_VisaInformationModel emp_Visa)
        {
            Session["VisaDetailsID"] = emp_Visa.ID.ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_VisaInformationModels = emp_VisaInformationBal.GetVisaInformationByID(clientContext, emp_Visa.ID.ToString());
                vM_EmployeeProfileModel.emp_Visa = emp_VisaInformationModels.Where(s => s.ID == Convert.ToInt32(emp_Visa.ID.ToString())).FirstOrDefault();
                vM_EmployeeProfileModel.emp_Countries = emp_CountryBal.GetAllCountries(clientContext);
                vM_EmployeeProfileModel.SelectedCountryId = Convert.ToInt32(vM_EmployeeProfileModel.emp_Visa.CountryID);
            }
            return PartialView("_PV_Emp_VisaEditView", vM_EmployeeProfileModel);
        }

        public ActionResult VisaDetailsAddView(string action)
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                vM_EmployeeProfileModel.emp_Countries = emp_CountryBal.GetAllCountries(clientContext);
            }
            return PartialView("_PV_Emp_VisaEditView", vM_EmployeeProfileModel);
        }

        public ActionResult UpdateVisaDetails(Emp_VisaInformationModel emp_Visa)
        {
            string EmpId = Session["EMPID"].ToString();
            string id = Session["VisaDetailsID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Visa.VisaType == null ? ",'VisaType':null" : ",'VisaType':'" + emp_Visa.VisaType + "'";
                items += emp_Visa.Country == null ? ",'CountryId':null" : ",'CountryId':" + emp_Visa.Country;
                items += emp_Visa.ValidUntil == "Invalid date" ? ",'ValidUntil':null" : ",'ValidUntil':'" + emp_Visa.ValidUntil + "'";
                emp_VisaInformationBal.SaveVisaInformationsDetails(clientContext, items);
                vM_EmployeeProfileModel.emp_VisaInformation = emp_VisaInformationBal.GetVisaInformationByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_VisaListView", vM_EmployeeProfileModel.emp_VisaInformation);
        }

        #endregion


        #region CRUD Action Methods for Banking Details

        public ActionResult SaveBankingDetails(Emp_BankingDetailsModel emp_Banking)
        {
            string EmpId = Session["EMPID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Banking.BankName==null? ",'BankName':null" : ",'BankName':'" + emp_Banking.BankName  + "'";
                items += emp_Banking.Branch==null? ",'Branch':null" : ",'Branch':'" + emp_Banking.Branch + "'";
                items += emp_Banking.Country==null? ",'CountryId':null" : ",'CountryId':" + emp_Banking.Country;
                items += emp_Banking.State==null? ",'StateId':null" : ",'StateId':" + emp_Banking.State;
                items += emp_Banking.City==null? ",'City':null" : ",'City':'" + emp_Banking.City + "'";
                items += emp_Banking.AccountNumber==null? ",'AccountNumber': null" : ",'AccountNumber':'" + emp_Banking.AccountNumber + "'";
                items += emp_Banking.AccountType==null? ",'AccountType': null" : ",'AccountType':'" + emp_Banking.AccountType + "'";
                items += emp_Banking.RoutingNumber == null? ",'RoutingNumber': null" : ",'RoutingNumber':'" + emp_Banking.RoutingNumber + "'";
                emp_BankingDetailsBal.SaveBankingDetails(clientContext, items);
                vM_EmployeeProfileModel.emp_BankingDetails = emp_BankingDetailsBal.GetBankingDetailsByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_BankingListView", vM_EmployeeProfileModel.emp_BankingDetails);
        }

        public ActionResult DeleteBankingDetails(Emp_BankingDetailsModel emp_Banking)
        {
            string EmpId = Session["EMPID"].ToString();
            string id = emp_Banking.ID.ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_BankingDetailsBal.DeleteBankingDetails(clientContext, id);
                vM_EmployeeProfileModel.emp_BankingDetails = emp_BankingDetailsBal.GetBankingDetailsByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_BankingListView", vM_EmployeeProfileModel.emp_BankingDetails);
        }

        public ActionResult EditBankingDetails(Emp_BankingDetailsModel emp_Banking)
        {
            Session["BankingDetailsID"] = emp_Banking.ID.ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_BankingDetailsModels = emp_BankingDetailsBal.GetBankingDetailsByID(clientContext, emp_Banking.ID.ToString());
                vM_EmployeeProfileModel.emp_Bankings = emp_BankingDetailsModels.Where(s => s.ID == Convert.ToInt32(emp_Banking.ID.ToString())).FirstOrDefault();
                vM_EmployeeProfileModel.emp_Countries = emp_CountryBal.GetAllCountries(clientContext);
                vM_EmployeeProfileModel.SelectedCountryId = Convert.ToInt32(vM_EmployeeProfileModel.emp_Bankings.CountryID);
                vM_EmployeeProfileModel.emp_States = emp_StateMasterBal.GetStateById(clientContext, vM_EmployeeProfileModel.emp_Bankings.CountryID);
                if (vM_EmployeeProfileModel.emp_Bankings.StateID !="")
                {
                    vM_EmployeeProfileModel.SelectedStateID = Convert.ToInt32(vM_EmployeeProfileModel.emp_Bankings.StateID);
                }
            }
            return PartialView("_PV_Emp_BankingEditView", vM_EmployeeProfileModel);
        }

        public ActionResult BankingDetailsAddView(string action)
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                vM_EmployeeProfileModel.emp_Countries = emp_CountryBal.GetAllCountries(clientContext);
            }
            return PartialView("_PV_Emp_BankingEditView", vM_EmployeeProfileModel);
        }

        public ActionResult UpdateBankingDetails(Emp_BankingDetailsModel emp_Banking)
        {
            string EmpId = Session["EMPID"].ToString();
            string ID = Session["BankingDetailsID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Banking.BankName == null ? ",'BankName':null" : ",'BankName':'" + emp_Banking.BankName + "'";
                items += emp_Banking.Branch == null ? ",'Branch':null" : ",'Branch':'" + emp_Banking.Branch + "'";
                items += emp_Banking.Country == null ? ",'CountryId':null" : ",'CountryId':" + emp_Banking.Country;
                items += emp_Banking.State == null ? ",'StateId':null" : ",'StateId':" + emp_Banking.State;
                items += emp_Banking.City == null ? ",'City':null" : ",'City':'" + emp_Banking.City + "'";
                items += emp_Banking.AccountNumber == null ? ",'AccountNumber': null" : ",'AccountNumber':'" + emp_Banking.AccountNumber + "'";
                items += emp_Banking.AccountType == null ? ",'AccountType': null" : ",'AccountType':'" + emp_Banking.AccountType + "'";
                items += emp_Banking.RoutingNumber == null ? ",'RoutingNumber': null" : ",'RoutingNumber':'" + emp_Banking.RoutingNumber + "'";
                emp_BankingDetailsBal.UpdateBankingDetails(clientContext, items,ID);
                vM_EmployeeProfileModel.emp_BankingDetails = emp_BankingDetailsBal.GetBankingDetailsByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_BankingListView", vM_EmployeeProfileModel.emp_BankingDetails);
        }

        public ActionResult DetailsBankingDetails(Emp_BankingDetailsModel emp_Banking)
        {
            Emp_BankingDetailsModel emp_BankingDetails = new Emp_BankingDetailsModel();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_BankingDetailsModels = emp_BankingDetailsBal.GetBankingDetailsByID(clientContext, emp_Banking.ID.ToString());
                emp_BankingDetails = emp_BankingDetailsModels.Where(s => s.ID == Convert.ToInt32(emp_Banking.ID.ToString())).FirstOrDefault();
            }
            return PartialView("_PV_Emp_BankingDetailsView", emp_BankingDetails);
        }
        #endregion


        #region CRUD Action Methods for Emergency Contact

        public ActionResult SaveEmergencyContact(Emp_EmergencyContactModel emp_Emergency)
        {
            string EmpId = Session["EMPID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Emergency.PersonName == null ? ",'PersonName':null" : ",'PersonName':'" + emp_Emergency.PersonName + "'";
                items += emp_Emergency.Relationship == null ? ",'Relationship':null" : ",'Relationship':'" + emp_Emergency.Relationship + "'";
                items += emp_Emergency.PrimaryPhoneNumber == null ? ",'PrimaryPhoneNumber':null" : ",'PrimaryPhoneNumber':'" + emp_Emergency.PrimaryPhoneNumber + "'";
                items += emp_Emergency.AlternatePhoneNumber == null ? ",'AlternatePhoneNumber':null" : ",'AlternatePhoneNumber':'" + emp_Emergency.AlternatePhoneNumber + "'";
                items += emp_Emergency.Address == null ? ",'Address':null" : ",'Address':'" + emp_Emergency.Address + "'";
                
                emp_EmergencyContactBal.SaveEmergencyContact(clientContext, items);
                vM_EmployeeProfileModel.emp_emergencyContactModels = emp_EmergencyContactBal.GetEmergencyContactByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_EmergencyContactListView", vM_EmployeeProfileModel.emp_emergencyContactModels);
        }

        public ActionResult DeleteEmergencyContact(Emp_EmergencyContactModel emp_Emergency)
        {
            string EmpId = Session["EMPID"].ToString();
            string id = emp_Emergency.ID.ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_EmergencyContactBal.DeleteEmergencyContact(clientContext, id);
                vM_EmployeeProfileModel.emp_emergencyContactModels = emp_EmergencyContactBal.GetEmergencyContactByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_EmergencyContactListView", vM_EmployeeProfileModel.emp_emergencyContactModels);
        }

        public ActionResult EditEmergencyContact(Emp_EmergencyContactModel emp_Emergency)
        {
            Session["EmergencyContactID"] = emp_Emergency.ID.ToString();
            Emp_EmergencyContactModel emp_EmergencyContact = new Emp_EmergencyContactModel();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_EmergencyContactModels = emp_EmergencyContactBal.GetEmergencyContactByID(clientContext, emp_Emergency.ID.ToString());
                emp_EmergencyContact = emp_EmergencyContactModels.Where(s => s.ID == Convert.ToInt32(emp_Emergency.ID.ToString())).FirstOrDefault();
            }
            return PartialView("_PV_Emp_EmergencyContactEditView", emp_EmergencyContact);
        }

        public ActionResult EmergencyContactAddView(string action)
        {
            Emp_EmergencyContactModel emp_Emergency = new Emp_EmergencyContactModel();
            return PartialView("_PV_Emp_EmergencyContactEditView", emp_Emergency);
        }

        public ActionResult UpdateEmergencyContact(Emp_EmergencyContactModel emp_Emergency)
        {
            string ID = Session["EmergencyContactID"].ToString();
            string EmpId = Session["EMPID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Emergency.PersonName == null ? ",'PersonName':null" : ",'PersonName':'" + emp_Emergency.PersonName + "'";
                items += emp_Emergency.Relationship == null ? ",'Relationship':null" : ",'Relationship':'" + emp_Emergency.Relationship + "'";
                items += emp_Emergency.PrimaryPhoneNumber == null ? ",'PrimaryPhoneNumber':null" : ",'PrimaryPhoneNumber':'" + emp_Emergency.PrimaryPhoneNumber+"'";
                items += emp_Emergency.AlternatePhoneNumber == null ? ",'AlternatePhoneNumber':null" : ",'AlternatePhoneNumber':'" + emp_Emergency.AlternatePhoneNumber+"'";
                items += emp_Emergency.Address == null ? ",'Address':null" : ",'Address':'" + emp_Emergency.Address + "'";

                emp_EmergencyContactBal.UpdateEmergencyContact(clientContext, items,ID);
                vM_EmployeeProfileModel.emp_emergencyContactModels = emp_EmergencyContactBal.GetEmergencyContactByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_EmergencyContactListView", vM_EmployeeProfileModel.emp_emergencyContactModels);
        }

        public ActionResult EmergencyContactDetails(Emp_EmergencyContactModel emp_Emergency)
        {
            Emp_EmergencyContactModel emp_EmergencyContact = new Emp_EmergencyContactModel();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_EmergencyContactModels = emp_EmergencyContactBal.GetEmergencyContactByID(clientContext, emp_Emergency.ID.ToString());
                emp_EmergencyContact = emp_EmergencyContactModels.Where(s => s.ID == Convert.ToInt32(emp_Emergency.ID.ToString())).FirstOrDefault();
            }
            return PartialView("_PV_Emp_EmergencyContactDetailsView", emp_EmergencyContact);
        }

        #endregion


        #region CRUD Action Methods for Employee Address

        public ActionResult SaveAddress(Emp_AddressModel emp_Address)
        {
            string EmpId = Session["EMPID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Address.AddressType == null ? ",'AddressType':null" : ",'AddressType':'" + emp_Address.AddressType + "'";
                items += emp_Address.Country == null ? ",'CountryId':null" : ",'CountryId': " + emp_Address.Country;
                items += emp_Address.State == null ? ",'StateId':null" : ",'StateId': " + emp_Address.State;
                items += emp_Address.City == null ? ",'City':null" : ",'City':'" + emp_Address.City + "'";
                items += emp_Address.Address == null ? ",'Address':null" : ",'Address':'" + emp_Address.Address + "'";
                items += emp_Address.Landmark == null ? ",'Landmark':null" : ",'Landmark':'" + emp_Address.Landmark + "'";
                items += emp_Address.PostalCode == null ? ",'PostalCode':null" : ",'PostalCode':'" + emp_Address.PostalCode + "'";
                items += emp_Address.ResidingSince == "Invalid date" ? ",'ResidingSince':null" : ",'ResidingSince':'" + emp_Address.ResidingSince + "'";

                emp_AddressBal.SaveEmp_Address(clientContext, items);
                vM_EmployeeProfileModel.emp_Addresses = emp_AddressBal.GetEmpAddressByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_AddressListView", vM_EmployeeProfileModel.emp_Addresses);
        }

        public ActionResult DeleteAddress(Emp_AddressModel emp_Address)
        {
            string EmpId = Session["EMPID"].ToString();
            string id = emp_Address.ID.ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_AddressBal.DeleteEmpAddress(clientContext, id);
                vM_EmployeeProfileModel.emp_Addresses = emp_AddressBal.GetEmpAddressByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_AddressListView", vM_EmployeeProfileModel.emp_Addresses);
        }

        public ActionResult EditAddress(Emp_AddressModel emp_Address)
        {
            Session["AddressID"] = emp_Address.ID.ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_AddressModel = emp_AddressBal.GetEmpAddressByID(clientContext, emp_Address.ID.ToString());
                vM_EmployeeProfileModel.Addresses = emp_AddressModel.Where(s => s.ID == Convert.ToInt32(emp_Address.ID.ToString())).FirstOrDefault();
                vM_EmployeeProfileModel.emp_Countries = emp_CountryBal.GetAllCountries(clientContext);
                vM_EmployeeProfileModel.SelectedCountryId = Convert.ToInt32(vM_EmployeeProfileModel.Addresses.CountryId);
                vM_EmployeeProfileModel.emp_States = emp_StateMasterBal.GetStateById(clientContext, vM_EmployeeProfileModel.Addresses.CountryId);
                if (vM_EmployeeProfileModel.Addresses.StateId != "")
                {
                    vM_EmployeeProfileModel.SelectedStateID = Convert.ToInt32(vM_EmployeeProfileModel.Addresses.StateId);
                }
            }
            return PartialView("_PV_Emp_AddressEditView", vM_EmployeeProfileModel);
        }

        public ActionResult AddressAddView(string action)
        {
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                vM_EmployeeProfileModel.emp_Countries = emp_CountryBal.GetAllCountries(clientContext);
            }
            return PartialView("_PV_Emp_AddressEditView", vM_EmployeeProfileModel);
        }

        public ActionResult UpdateAddress(Emp_AddressModel emp_Address)
        {
            string ID = Session["AddressID"].ToString();
            string EmpId = Session["EMPID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Address.AddressType == null ? ",'AddressType':null" : ",'AddressType':'" + emp_Address.AddressType + "'";
                items += emp_Address.Country == null ? ",'CountryId':null" : ",'CountryId': " + emp_Address.Country;
                items += emp_Address.State == null ? ",'StateId':null" : ",'StateId': " + emp_Address.State;
                items += emp_Address.City == null ? ",'City':null" : ",'City':'" + emp_Address.City + "'";
                items += emp_Address.Address == null ? ",'Address':null" : ",'Address':'" + emp_Address.Address + "'";
                items += emp_Address.Landmark == null ? ",'Landmark':null" : ",'Landmark':'" + emp_Address.Landmark + "'";
                items += emp_Address.PostalCode == null ? ",'PostalCode':null" : ",'PostalCode':'" + emp_Address.PostalCode + "'";
                items += emp_Address.ResidingSince == "Invalid date" ? ",'ResidingSince':null" : ",'ResidingSince':'" + emp_Address.ResidingSince + "'";

                emp_AddressBal.UpdateEmp_Address(clientContext, items, ID);
                vM_EmployeeProfileModel.emp_Addresses = emp_AddressBal.GetEmpAddressByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_AddressListView", vM_EmployeeProfileModel.emp_Addresses);
        }

        public ActionResult AddressDetails(Emp_AddressModel emp_Address)
        {
            Emp_AddressModel empAddress = new Emp_AddressModel();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_AddressModel = emp_AddressBal.GetEmpAddressByID(clientContext, emp_Address.ID.ToString());
                empAddress = emp_AddressModel.Where(s => s.ID == Convert.ToInt32(emp_Address.ID.ToString())).FirstOrDefault();
            }
            return PartialView("_PV_Emp_AddressDetailsView", empAddress);
        }

        #endregion


        #region CRUD Action Methods for Family Details 

        public ActionResult SaveFamilyDetails(Emp_FamilyDetailsModel emp_Family)
        {
            string EmpId = Session["EMPID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Family.PersonName == null ? ",'PersonName':null" : ",'PersonName':'" + emp_Family.PersonName + "'";
                items += emp_Family.Gender == null ? ",'Gender':null" : ",'Gender': '" + emp_Family.Gender+"'";
                items += emp_Family.Relationship == null ? ",'Relationship':null" : ",'Relationship': '" + emp_Family.Relationship+"'";
                items += emp_Family.MediclaimCovered == null ? ",'MediclaimCovered':null" : ",'MediclaimCovered':'" + emp_Family.MediclaimCovered + "'";
                items += emp_Family.BirthDate == "Invalid date" ? ",'BirthDate':null" : ",'BirthDate':'" + emp_Family.BirthDate + "'";
                items += emp_Family.BirthPlace == null ? ",'BirthPlace':null" : ",'BirthPlace':'" + emp_Family.BirthPlace + "'";
                items += emp_Family.Age == null ? ",'Age':null" : ",'Age':'" + emp_Family.Age + "'";
                items += emp_Family.ContactNumber == null ? ",'ContactNumber':null" : ",'ContactNumber':'" + emp_Family.ContactNumber + "'";

                emp_FamilyDetailsBal.SaveFamilyDetails(clientContext, items);
                vM_EmployeeProfileModel.emp_FamilyDetails = emp_FamilyDetailsBal.GetFamilyDetailsByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_FamilyListView", vM_EmployeeProfileModel.emp_FamilyDetails);
        }

        public ActionResult DeleteFamilyDetails(Emp_FamilyDetailsModel emp_Family)
        {
            string EmpId = Session["EMPID"].ToString();
            string id = emp_Family.ID.ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_FamilyDetailsBal.DeleteFamilyDetails(clientContext, id);
                vM_EmployeeProfileModel.emp_FamilyDetails = emp_FamilyDetailsBal.GetFamilyDetailsByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_FamilyListView", vM_EmployeeProfileModel.emp_FamilyDetails);
        }

        public ActionResult EditFamilyDetails(Emp_FamilyDetailsModel emp_Family)
        {
            Session["FamilyID"] = emp_Family.ID.ToString();
            Emp_FamilyDetailsModel emp_FamilyDetails = new Emp_FamilyDetailsModel();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_FamilyDetailsModels = emp_FamilyDetailsBal.GetFamilyDetailsByID(clientContext, emp_Family.ID.ToString());
                emp_FamilyDetails = emp_FamilyDetailsModels.Where(s => s.ID == Convert.ToInt32(emp_Family.ID.ToString())).FirstOrDefault();
            }
            return PartialView("_PV_Emp_FamilyDetailsEditView", emp_FamilyDetails);
        }

        public ActionResult FamilyDetailsAddView(string action)
        {
            Emp_FamilyDetailsModel emp_FamilyDetails = new Emp_FamilyDetailsModel();
            return PartialView("_PV_Emp_FamilyDetailsEditView", emp_FamilyDetails);
        }

        public ActionResult UpdateFamilyDetails(Emp_FamilyDetailsModel emp_Family)
        {
            string ID = Session["FamilyID"].ToString();
            string EmpId = Session["EMPID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Family.PersonName == null ? ",'PersonName':null" : ",'PersonName':'" + emp_Family.PersonName + "'";
                items += emp_Family.Gender == null ? ",'Gender':null" : ",'Gender': '" + emp_Family.Gender + "'";
                items += emp_Family.Relationship == null ? ",'Relationship':null" : ",'Relationship': '" + emp_Family.Relationship + "'";
                items += emp_Family.MediclaimCovered == null ? ",'MediclaimCovered':null" : ",'MediclaimCovered':'" + emp_Family.MediclaimCovered + "'";
                items += emp_Family.BirthDate == "Invalid date" ? ",'BirthDate':null" : ",'BirthDate':'" + emp_Family.BirthDate + "'";
                items += emp_Family.BirthPlace == null ? ",'BirthPlace':null" : ",'BirthPlace':'" + emp_Family.BirthPlace + "'";
                items += emp_Family.Age == null ? ",'Age':null" : ",'Age':'" + emp_Family.Age + "'";
                items += emp_Family.ContactNumber == null ? ",'ContactNumber':null" : ",'ContactNumber':'" + emp_Family.ContactNumber + "'";

                emp_FamilyDetailsBal.UpdateFamilyDetails(clientContext, items, ID);
                vM_EmployeeProfileModel.emp_FamilyDetails = emp_FamilyDetailsBal.GetFamilyDetailsByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_FamilyListView", vM_EmployeeProfileModel.emp_FamilyDetails);
        }

        public ActionResult FamilyDetails(Emp_FamilyDetailsModel emp_Family)
        {
            Emp_FamilyDetailsModel emp_FamilyDetails = new Emp_FamilyDetailsModel();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_FamilyDetailsModels = emp_FamilyDetailsBal.GetFamilyDetailsByID(clientContext, emp_Family.ID.ToString());
                emp_FamilyDetails = emp_FamilyDetailsModels.Where(s => s.ID == Convert.ToInt32(emp_Family.ID.ToString())).FirstOrDefault();
            }
            return PartialView("_PV_Emp_FamilyDetailsView", emp_FamilyDetails);
        }

        #endregion


        #region CRUD Action Methods for  Medical Information  

        public ActionResult SaveMedicalInformation(Emp_MedicalInformationModel emp_Medical)
        {
            string EmpId = Session["EMPID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Medical.MedicalConditionName == null ? ",'MedicalConditionName':null" : ",'MedicalConditionName':'" + emp_Medical.MedicalConditionName + "'";
                items += emp_Medical.MedicalConditionFrom == null ? ",'MedicalConditionFrom':null" : ",'MedicalConditionFrom': '" + emp_Medical.MedicalConditionFrom + "'";
                items += emp_Medical.CurrentlyActive == null ? ",'CurrentlyActive':null" : ",'CurrentlyActive': '" + emp_Medical.CurrentlyActive + "'";
                items += emp_Medical.NeedSpecialAttention == null ? ",'NeedSpecialAttention':null" : ",'NeedSpecialAttention':'" + emp_Medical.NeedSpecialAttention + "'";
                items += emp_Medical.Notes == null ? ",'Notes':null" : ",'Notes':'" + emp_Medical.Notes + "'";

                emp_MedicalInformationBal.SaveMedicalInformation(clientContext, items);
                vM_EmployeeProfileModel.emp_MedicalInformation = emp_MedicalInformationBal.GetMedicalInformationByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_MedicalInformationListView", vM_EmployeeProfileModel.emp_MedicalInformation);
        }

        public ActionResult DeleteMedicalInformation(Emp_MedicalInformationModel emp_Medical)
        {
            string EmpId = Session["EMPID"].ToString();
            string id = emp_Medical.ID.ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                emp_MedicalInformationBal.DeleteMedicalInformation(clientContext, id);
                vM_EmployeeProfileModel.emp_MedicalInformation = emp_MedicalInformationBal.GetMedicalInformationByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_MedicalInformationListView", vM_EmployeeProfileModel.emp_MedicalInformation);
        }

        public ActionResult EditMedicalInformation(Emp_MedicalInformationModel emp_Medical)
        {
            Session["MedicalInformation"] = emp_Medical.ID.ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                vM_EmployeeProfileModel.emp_MedicalInformation = emp_MedicalInformationBal.GetMedicalInformationByID(clientContext, emp_Medical.ID.ToString());
                vM_EmployeeProfileModel.emp_Medical = vM_EmployeeProfileModel.emp_MedicalInformation.Where(s => s.ID == Convert.ToInt32(emp_Medical.ID.ToString())).FirstOrDefault();
                vM_EmployeeProfileModel.MedicalConditionFrom = GetYear();
            }
            return PartialView("_PV_Emp_MedicalInformationEditView", vM_EmployeeProfileModel);
        }

        public ActionResult MedicalInformationAddView(string action)
        {
            Emp_MedicalInformationModel emp_Medical = new Emp_MedicalInformationModel();
            vM_EmployeeProfileModel.MedicalConditionFrom=GetYear();
            vM_EmployeeProfileModel.emp_Medical = emp_Medical;
            return PartialView("_PV_Emp_MedicalInformationEditView", vM_EmployeeProfileModel);
        }

        public ActionResult UpdateMedicalInformation(Emp_MedicalInformationModel emp_Medical)
        {
            string ID = Session["MedicalInformation"].ToString();
            string EmpId = Session["EMPID"].ToString();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                string items = "'EmpCodeId' : " + EmpId;
                items += emp_Medical.MedicalConditionName == null ? ",'MedicalConditionName':null" : ",'MedicalConditionName':'" + emp_Medical.MedicalConditionName + "'";
                items += emp_Medical.MedicalConditionFrom == null ? ",'MedicalConditionFrom':null" : ",'MedicalConditionFrom': '" + emp_Medical.MedicalConditionFrom + "'";
                items += emp_Medical.CurrentlyActive == null ? ",'CurrentlyActive':null" : ",'CurrentlyActive': '" + emp_Medical.CurrentlyActive + "'";
                items += emp_Medical.NeedSpecialAttention == null ? ",'NeedSpecialAttention':null" : ",'NeedSpecialAttention':'" + emp_Medical.NeedSpecialAttention + "'";
                items += emp_Medical.Notes == null ? ",'Notes':null" : ",'Notes':'" + emp_Medical.Notes + "'";
                emp_MedicalInformationBal.UpdateMedicalInformation(clientContext, items, ID);
                vM_EmployeeProfileModel.emp_MedicalInformation = emp_MedicalInformationBal.GetMedicalInformationByEmpID(clientContext, EmpId);
            }
            return PartialView("_PV_Emp_MedicalInformationListView", vM_EmployeeProfileModel.emp_MedicalInformation);
        }

        public ActionResult MedicalInformation(Emp_MedicalInformationModel emp_Medical)
        {
            Emp_MedicalInformationModel emp_MedicalInformation = new Emp_MedicalInformationModel();
            var spContext = SharePointContextProvider.Current.GetSharePointContext(HttpContext);
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                vM_EmployeeProfileModel.emp_MedicalInformation = emp_MedicalInformationBal.GetMedicalInformationByID(clientContext, emp_Medical.ID.ToString());
                emp_MedicalInformation = vM_EmployeeProfileModel.emp_MedicalInformation.Where(s => s.ID == Convert.ToInt32(emp_Medical.ID.ToString())).FirstOrDefault();
            }
            return PartialView("_PV_Emp_MedicalInformationDetailsView", emp_MedicalInformation);
        }

        #endregion



        #region Get Years 
        public IList<SelectListItem> GetYear()
        {
            const int numberOfYears = 30;
            var startYear = DateTime.Now.Year;
            var endYear = startYear - numberOfYears;
            var yearList = new List<SelectListItem>();
            for (var i = endYear; i <= startYear; i++)
            {
                yearList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
            }
            return yearList;
        } 
        #endregion



        #region Get State by Country ID
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

        #endregion



    }
}
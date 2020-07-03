using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WizrrWeb.Models.EmployeeManagement.ViewModels
{
    public class VM_EmployeeProfileModel
    {
        public Emp_BasicInfoModel emp_BasicInfo { get; set; }
        public Emp_CompanyMasterModel emp_CompanyMaster { get; set; }
        public EmpProfilePicModel empProfilePic { get; set; }
        public IEnumerable<Emp_EducationalDetailsModel> emp_EducationalDetails { get; set; }
        public IEnumerable<Emp_WorkExperienceModel> emp_WorkExperienceModels { get; set; }
        public IEnumerable<Emp_AwardsDetailsModel> emp_AwardsDetails { get; set; }
        public IEnumerable<Emp_PublicationDetailsModel> emp_PublicationDetails { get; set; }
        public IEnumerable<Emp_ReferenceDetailsModel> emp_ReferenceDetails { get; set; }
        public IEnumerable<Emp_DisciplinaryDetailsModel> emp_DisciplinaryDetails { get; set; }
        public IEnumerable<Emp_VisaInformationModel> emp_VisaInformation { get; set; }
        public Emp_VisaInformationModel emp_Visa { get; set; }
        public IEnumerable<Emp_BankingDetailsModel> emp_BankingDetails { get; set; }
        public Emp_BankingDetailsModel emp_Bankings { get; set; }
        public IEnumerable<Emp_CountryModel> emp_Countries { get; set; }
        public int SelectedCountryId { get; set; }
        public IEnumerable<Emp_StateMasterModel> emp_States { get; set; }
        public int SelectedStateID { get; set; }
        public IEnumerable<Emp_AddressModel> emp_Addresses { get; set; }
        public Emp_AddressModel Addresses { get; set; }
        public IEnumerable<Emp_EmergencyContactModel> emp_emergencyContactModels { get; set; }
        public IEnumerable<Emp_FamilyDetailsModel> emp_FamilyDetails { get; set; }
        public IEnumerable<Emp_MedicalInformationModel> emp_MedicalInformation { get; set; }
        public Emp_MedicalInformationModel emp_Medical { get; set; }
        public IList<SelectListItem> MedicalConditionFrom { get; set; }
    }
}
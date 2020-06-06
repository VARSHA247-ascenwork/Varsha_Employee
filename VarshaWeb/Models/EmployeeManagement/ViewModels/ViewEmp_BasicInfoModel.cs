using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarshaWeb.Models.EmployeeManagement
{
    public class ViewEmp_BasicInfoModel
    {
        public int SelectedCompanyID { get; set; }
      
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmpCode { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string DOB { get; set; }
        public string JoiningDate { get; set; }
        public string OnProbationTill { get; set; }
        public string ProbationStatus { get; set; }
        public string OfficeEmail { get; set; }
        public string ContactNumber { get; set; }
        public string EmpStatus { get; set; }
        public IEnumerable<Emp_CompanyMasterModel> company { get; set; }
        public IEnumerable<Emp_DesignationModel> Designation { get; set; }
        public IEnumerable<Emp_DepartmentModel> Department { get; set; }
        public IEnumerable<Emp_DivisionModel> Division { get; set; }
        public IEnumerable<Emp_RegionModel> Region { get; set; }
        public IEnumerable<Emp_BranchModel> Branch { get; set; }
        public IEnumerable<UserGroupModel> User_Name { get; set; }
        public string ManagerCode { get; set; }
        
        public string UserNameId { get; set; }
        public string Manager { get; set; }
        public int ManagerId { get; set; }
        public string Profile_pic_url { get; set; }
        public string Facebook { get; set; }
        public string Google { get; set; }
        public string Linkedln { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Tiktok { get; set; }
        public string DOB_Months { get; set; }
        public string JoiningDate_Month { get; set; }

      
        // public IEnumerable<Emp_CompanyMasterModel> CompanyName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarshaWeb.Models.EmployeeManagement.ViewModels
{
    public class VM_Emp_BasicInfoModel
    {
        public Emp_BasicInfoModel VMEmpdata { get; set; }
        public int SelectedEmpID { get; set; }
        public int SelectedCompanyID { get; set; }
        public int SelectedDesignationID { get; set; }
        public int SelectedDepartmentID { get; set; }
        public int SelectedDivisionID { get; set; }
        public int SelectedRegionID { get; set; }
        public int SelectedBranchID { get; set; }
        public string SelectedUserID { get; set; }
        public int SelectetManagerID { get; set; }
        
           public string FirstName { get; set; }
       public string manager { get; set; }
        public IEnumerable<Emp_BasicInfoModel> Empdata { get; set; }
        public IEnumerable<Emp_CompanyMasterModel> company { get; set; }
        public IEnumerable<Emp_DesignationModel> Designation { get; set; }
        public IEnumerable<Emp_DepartmentModel> DepartmentName { get; set; }
        public IEnumerable<Emp_DivisionModel> Division { get; set; }
        public IEnumerable<Emp_RegionModel> Region { get; set; }
        public IEnumerable<Emp_BranchModel> Branch { get; set; }
        public IEnumerable<UserGroupModel> User_Name { get; set; }
       
        
      
    }
}
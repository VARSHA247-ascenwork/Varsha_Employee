using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarshaWeb.Models.EmployeeManagement
{
    public class ViewEmp_CompanyMasterModel
    {
        public int SelectedCompanyID { get; set; }
        public IEnumerable<Emp_CompanyMasterModel> CompanyName { get; set; }
        public IEnumerable<Emp_CompanyMasterModel> CompanyOwner { get; set; }
        public IEnumerable<Emp_CompanyMasterModel> CompanyDescription { get; set; }
        public IEnumerable<Emp_CompanyMasterModel> ContactPerson { get; set; }
        public IEnumerable<Emp_CompanyMasterModel> ContactNumber { get; set; }
        public IEnumerable<Emp_CompanyMasterModel> Website { get; set; }
        public IEnumerable<Emp_CompanyMasterModel> EmailID { get; set; }
        public IEnumerable<Emp_CompanyMasterModel> GSTNumber { get; set; }
        public IEnumerable<Emp_CompanyMasterModel> PANNumber { get; set; }
        public IEnumerable<Emp_CompanyMasterModel> City { get; set; }
        public IEnumerable<Emp_CompanyMasterModel> State { get; set; }
        public IEnumerable<Emp_CompanyMasterModel> Country { get; set; }
      
        // public IEnumerable<Emp_CompanyMasterModel> CompanyName { get; set; }
    }
}
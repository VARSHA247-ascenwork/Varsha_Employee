using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WizrrWeb.Models.EmployeeManagement
{
    public class Emp_InsuranceDetailsModel
    {
        public int ID { get; set; }
        public string InsuredName { get; set; }
        public string InsuredUntil { get; set; }
        public string InsuranceCompany { get; set; }
        public string InsuranceAmount { get; set; }
        public string Balance { get; set; }
        public string PolicyNumber { get; set; }
        public string EmpCode { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WizrrWeb.Models.EmployeeManagement
{
    public class Emp_WorkExperienceModel
    {
        public int ID { get; set; }
        public string OrganizationName { get; set; }
        public string Designation { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Notes { get; set; }
        public string ContactNumber { get; set; }
        public string AnnualSalary { get; set; }
        public string Address { get; set; }
        public string ReasonForLeaving { get; set; }
        public string DutiesResponsibilities { get; set; }
        public string Time { get; set; }
        public string EmpCode { get; set; }
        public string Industry { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WizrrWeb.Models.EmployeeManagement
{
    public class Emp_RelativesInStaffModel
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public string EmpCode { get; set; }
        public string Relationship { get; set; }
        public string StaffName { get; set; }
        public string Designation { get; set; }

    }
}
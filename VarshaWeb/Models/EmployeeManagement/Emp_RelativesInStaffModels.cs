using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarshaWeb.Models.EmployeeManagement
{
    public class Emp_RelativesInStaffModels
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public string Emp_Code { get; set; }
        public string Relationship { get; set; }
        public string StaffName { get; set; }
        public string Designation { get; set; }

    }
}
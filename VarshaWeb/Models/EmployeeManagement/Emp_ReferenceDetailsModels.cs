using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarshaWeb.Models.EmployeeManagement
{
    public class Emp_ReferenceDetailsModels
    {
        public int ID { get; set; }
        public string Person { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Designation { get; set; }
        public string Notes { get; set; }
        public string Address { get; set; }
        public string HowDoYouKnowPerson { get; set; }
        public string EmpCode { get; set; }
    }
}
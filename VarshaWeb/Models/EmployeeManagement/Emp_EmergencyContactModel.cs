using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WizrrWeb.Models.EmployeeManagement
{
    public class Emp_EmergencyContactModel
    {
        public int ID { get; set; }
        public string PersonName { get; set; }
        public string Relationship { get; set; }
        public string PrimaryPhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public string Address { get; set; }
        public string EmpCode { get; set; }

    }
}
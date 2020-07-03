using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WizrrWeb.Models.EmployeeManagement
{
    public class Emp_VisaInformationModel
    {
        public int ID { get; set; }
        public string VisaType { get; set; }
        public string ValidUntil { get; set; }
        public string Country { get; set; }
        public string CountryID { get; set; }
        public string EmpCode { get; set; }
    }
}
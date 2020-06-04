using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarshaWeb.Models.EmployeeManagement
{
    public class Emp_CompanyMasterModel
    {
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyOwner { get; set; }
        public string CompanyDescription { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string Website { get; set; }
        public string EmailID { get; set; }
        public string GSTNumber { get; set; }
        public string PANNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
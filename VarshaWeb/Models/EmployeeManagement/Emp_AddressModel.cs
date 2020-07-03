using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WizrrWeb.Models.EmployeeManagement
{
    public class Emp_AddressModel
    {
        public int ID { get; set; }
        public string AddressType { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string CountryId { get; set; }
        public string State { get; set; }
        public string StateId { get; set; }
        public string City { get; set; }
        public string Landmark { get; set; }
        public string PostalCode { get; set; }
        public string ResidingSince { get; set; }
        public string EmpCode { get; set; }
    }
}
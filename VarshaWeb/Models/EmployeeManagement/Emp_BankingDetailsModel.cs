using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WizrrWeb.Models.EmployeeManagement
{
    public class Emp_BankingDetailsModel
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public string EmpCode { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string AccountNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string RoutingNumber { get; set; }
        public string AccountType { get; set; }
        public string Country { get; set; }
        public string CountryID { get; set; }
        public string StateID { get; set; }
    }
}
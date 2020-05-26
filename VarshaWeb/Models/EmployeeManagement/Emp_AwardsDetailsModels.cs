using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarshaWeb.Models.EmployeeManagement
{
    public class Emp_AwardsDetailsModels
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public string Emp_Code { get; set; }
        public string Award { get; set; }
        public string AwardedBy { get; set; }
        public string AwardedOn { get; set; }
        public string Description { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarshaWeb.Models.EmployeeManagement
{
    public class Emp_StateMasterModel
    {
        public int ID { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string CountryNameID { get; set; }

    }
}
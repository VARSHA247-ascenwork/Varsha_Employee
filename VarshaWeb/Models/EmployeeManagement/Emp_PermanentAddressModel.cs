﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WizrrWeb.Models.EmployeeManagement
{
    public class Emp_PermanentAddressModel
    {
        public int ID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string EmpCode { get; set; }
        public string Country { get; set; }
    }
}
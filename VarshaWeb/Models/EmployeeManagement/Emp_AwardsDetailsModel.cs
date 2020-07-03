﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WizrrWeb.Models.EmployeeManagement
{
    public class Emp_AwardsDetailsModel
    {
        public int ID { get; set; }
        public string EmpCode { get; set; }
        public string Award { get; set; }
        public string AwardedBy { get; set; }
        public string AwardedOn { get; set; }
        public string Description { get; set; }
    }
}
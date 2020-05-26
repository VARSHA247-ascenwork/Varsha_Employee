using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarshaWeb.Models.EmployeeManagement
{
    public class Emp_DisciplinaryDetailsModels
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string ActionOn { get; set; }
        public string EmpCode { get; set; }
    }
}
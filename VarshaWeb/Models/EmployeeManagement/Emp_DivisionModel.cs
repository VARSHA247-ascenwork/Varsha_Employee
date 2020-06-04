using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarshaWeb.Models.EmployeeManagement
{
    public class Emp_DivisionModel
    {
        public int ID { get; set; }
        public string Division { get; set; }
        public string HeadOfDivision { get; set; }
        public string Description { get; set; }
    }
}
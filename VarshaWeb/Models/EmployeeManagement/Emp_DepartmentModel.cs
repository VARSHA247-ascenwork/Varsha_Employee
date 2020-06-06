using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VarshaWeb.Models.EmployeeManagement
{
    public class Emp_DepartmentModel
    {
        public int ID { get; set; }
       public string DepartmentName { get; set; }
        public string HeadOfDepartment { get; set; }
        public string Description { get; set; }

       // public IEnumerable<SelectListItem> DepartmentName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarshaWeb.Models.EmployeeManagement
{
    public class Emp_PublicationDetailsModel
    {
        public int ID { get; set; }
        public string PublishedOn { get; set; }
        public string Publication { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string EmpCode { get; set; }

    }
}
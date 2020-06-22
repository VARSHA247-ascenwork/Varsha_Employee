using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarshaWeb.Models.EmployeeManagement
{
    public class Emp_Vendor_DocumentModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string libraryId { get; set; }
        public string DocumentPath { get; set; }
    }
}
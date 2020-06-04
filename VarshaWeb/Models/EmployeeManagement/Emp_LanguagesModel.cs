using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarshaWeb.Models.EmployeeManagement
{
    public class Emp_LanguagesModel
    {
        public int ID { get; set; }
        public string Language { get; set; }
        public string CanSpeak { get; set; }
        public string CanRead { get; set; }
        public string CanWrite { get; set; }
        public string EmpCode { get; set; }
    }
}
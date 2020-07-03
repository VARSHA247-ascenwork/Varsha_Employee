using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WizrrWeb.Models.EmployeeManagement
{
    public class Emp_EducationalDetailsModel
    {
        public int ID { get; set; }
        public string UniversityInstitute { get; set; }
        public string Degree { get; set; }
        public string MarksInPercentage { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string EmpCode { get; set; }

    }
}
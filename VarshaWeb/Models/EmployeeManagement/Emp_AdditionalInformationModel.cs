using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarshaWeb.Models.EmployeeManagement
{
    public class Emp_AdditionalInformationModel
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public string Emp_Code { get; set; }
        public string ESIC { get; set; }
        public string MonthlyVariableApplicable { get; set; }
        public string AnniversaryDate { get; set; }

    }
}
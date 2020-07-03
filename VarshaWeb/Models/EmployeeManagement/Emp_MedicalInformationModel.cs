using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WizrrWeb.Models.EmployeeManagement
{
    public class Emp_MedicalInformationModel
    {
        public int ID { get; set; }
        public string Notes { get; set; }
        public string EmpCode { get; set; }
        public string MedicalConditionName { get; set; }
        public string MedicalConditionFrom { get; set; }
        public string CurrentlyActive { get; set; }
        public string NeedSpecialAttention { get; set; }

    }
}
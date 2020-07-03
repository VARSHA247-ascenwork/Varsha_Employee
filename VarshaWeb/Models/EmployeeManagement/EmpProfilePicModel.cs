using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WizrrWeb.Models.EmployeeManagement
{
    public class EmpProfilePicModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string EmpCodeId { get; set; }
        public string DocumentPath { get; set; }
        public string Active { get; set; }
    }
}
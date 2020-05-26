using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VarshaWeb.Models.EmployeeManagement
{
    public class Emp_VendorMasterDetailsModels
    {
        public int ID { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public string VendorContact { get; set; }
        public string VendormailID { get; set; }
        public string VendorCompany { get; set; }
        public string City { get; set; }
        public string GstNo { get; set; }
        public string PanCardNo { get; set; }
        public string Remark { get; set; }
        public string States { get; set; }
    }
}
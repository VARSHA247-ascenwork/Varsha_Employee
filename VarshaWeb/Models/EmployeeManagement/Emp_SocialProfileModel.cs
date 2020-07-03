using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WizrrWeb.Models.EmployeeManagement
{
    public class Emp_SocialProfileModel
    {
        public int ID { get; set; }
        public string Facebook { get; set; }
        public string Google { get; set; }
        public string LinkedIn { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Tiktok { get; set; }
        public string EmpCode { get; set; }
    }
}
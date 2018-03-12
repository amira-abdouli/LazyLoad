using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace BLL.Models
{  
    public class Employees:BaseTable
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public virtual ICollection<Departments> Departments { get; set; }
    }

    public class Departments
    {
        public int ID { get; set; }
        public int EmployeeId { get; set; }
        public string DepartmentName { get; set; }
        public string  Description { get; set; }
        public virtual Employees Employees { get; set; }
    }
   
}
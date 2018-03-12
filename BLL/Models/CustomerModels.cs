using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace BLL.Models
{
    public class Customers
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual  ICollection<Invoices> Invoices { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

    public class Invoices
    {
        public int ID { get; set; }
        public int CustomersID { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual ICollection<InviceDetails> InviceDetails { get; set; }
    }

    public class InviceDetails
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
        public int InvoicesID { get; set; }
        public virtual Invoices Invoices { get; set; }
    }


}
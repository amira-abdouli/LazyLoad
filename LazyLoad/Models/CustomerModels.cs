using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace LazyLoad.Models
{
    public class Customers : BLL.Models.BaseTable
    {
        public string Name { get; set; }
        public virtual  ICollection<Invoices> Invoices { get; set; }
    }

    public class Invoices: BLL.Models.BaseTable
    {
        public int CustomersID { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual ICollection<InviceDetails> InviceDetails { get; set; }
    }

    public class InviceDetails: BLL.Models.BaseTable
    {
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
        public int InvoicesID { get; set; }
        public virtual Invoices Invoices { get; set; }
    }


}
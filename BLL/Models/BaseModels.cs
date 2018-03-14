﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public interface IBaseTable
    {
        int ID { get; set; }
        bool Deleted { get; set; }
        DateTime CreateDate { get; set; }
        DateTime UpdateDate { get; set; }
        string UserID { get; set; }
        ApplicationUser User { get; set; }
    }
    public class BaseTable:IBaseTable
    {
        public int ID { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.LoggerModels
{
    public class Logger : Models.BaseTable
    {
        [Key]
        public string Code { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string Thread { get; set; }
    }
}

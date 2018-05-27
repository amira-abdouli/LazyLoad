using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class DataStoreException : Exception
    {
        public DataStoreException(string message) : base(message)
        {
        }
        public DataStoreException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}

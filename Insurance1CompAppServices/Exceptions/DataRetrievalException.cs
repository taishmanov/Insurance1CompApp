using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance1CompAppServices.Exceptions
{
    public class DataRetrievalException : Exception
    {
        public DataRetrievalException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

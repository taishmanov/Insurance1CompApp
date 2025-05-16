using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance1CompAppServices.Exceptions
{
    public class CalculationException : Exception
    {
        public CalculationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

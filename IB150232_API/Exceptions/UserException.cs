using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IB150232_API.Exceptions
{
    public class UserException : Exception
    {
        public UserException(string message)
            : base(message)
        {

        }
    }
}

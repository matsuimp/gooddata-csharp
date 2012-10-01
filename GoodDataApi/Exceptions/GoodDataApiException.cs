using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoodDataApi.Exceptions
{
    public class GoodDataApiException : Exception
    {
        public GoodDataApiException(string message) : base(message)
        {
            
        }
    }
}

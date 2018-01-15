using System;
using System.Collections.Generic;
using System.Text;

namespace DataAuthority.Domain.Exceptions
{
    public class DataAuthorityDomainException : Exception
    {
        public DataAuthorityDomainException()
        { }

        public DataAuthorityDomainException(string message)
            : base(message)
        { }

        public DataAuthorityDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNews.Domain.Exceptions
{
    public class SportNewsDomainException : Exception
    {
        public SportNewsDomainException()
        { }

        public SportNewsDomainException(string message)
            : base(message)
        { }

        public SportNewsDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

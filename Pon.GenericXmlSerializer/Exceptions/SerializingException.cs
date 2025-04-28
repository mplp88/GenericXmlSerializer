using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pon.GenericXmlSerializer.Exceptions
{
    internal class SerializingException : Exception
    {
        public SerializingException() : base() { }

        public SerializingException(string? message)
            : base(message) { }

        public SerializingException(string? message, Exception? innerException)
            : base(message, innerException) { }
    }
}

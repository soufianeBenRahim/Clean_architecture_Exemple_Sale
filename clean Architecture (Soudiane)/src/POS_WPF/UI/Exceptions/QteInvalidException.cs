using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Exceptions
{
    [System.Serializable]
    public class QteInvalidException : Exception
    {
        public QteInvalidException() { }
        public QteInvalidException(string message) : base(message) { }
        public QteInvalidException(string message, Exception inner) : base(message, inner) { }
        protected QteInvalidException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

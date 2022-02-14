using System;
using System.Runtime.Serialization;

namespace POS.Exceptions
{
    [Serializable]
    public class BarCodeNotFondException : Exception
    {
        public BarCodeNotFondException()
        {
        }

        public BarCodeNotFondException(string message) : base(message)
        {
        }

        public BarCodeNotFondException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BarCodeNotFondException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
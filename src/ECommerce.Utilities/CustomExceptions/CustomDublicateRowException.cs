using System;
using System.Runtime.Serialization;

namespace ECommerce.Utilities.CustomExceptions
{
    public class CustomDublicateRowException : CustomException
    {
        public CustomDublicateRowException(string message) : base(message)
        {
        }

        public CustomDublicateRowException(object model) : base(model)
        {
        }

        public CustomDublicateRowException(string message, object model) : base(message, model)
        {
        }

        public CustomDublicateRowException(string message, Exception innerException, object model) : base(message, innerException, model)
        {
        }

        protected CustomDublicateRowException(SerializationInfo info, StreamingContext context, object model) : base(info, context, model)
        {
        }

        public CustomDublicateRowException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

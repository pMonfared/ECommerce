using System;
using System.Runtime.Serialization;

namespace ECommerce.Utilities.CustomExceptions
{
    public class CustomDbException : CustomException
    {
        public CustomDbException(object model) : base(model)
        {
        }

        public CustomDbException(string message) : base(message)
        {
        }

        public CustomDbException(string message, object model) : base(message, model)
        {
        }

        public CustomDbException(string message, Exception innerException, object model) : base(message, innerException, model)
        {
        }

        protected CustomDbException(SerializationInfo info, StreamingContext context, object model) : base(info, context, model)
        {
        }

        public CustomDbException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

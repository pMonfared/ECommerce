using System;
using System.Runtime.Serialization;

namespace ECommerce.Utilities.CustomExceptions
{
    public class CustomArgumentOutOfRangeException : CustomException
    {
        public CustomArgumentOutOfRangeException(object model) : base(model)
        {
        }

        public CustomArgumentOutOfRangeException(string message) : base(message)
        {
        }

        public CustomArgumentOutOfRangeException(string message, object model) : base(message, model)
        {
        }

        public CustomArgumentOutOfRangeException(string message, Exception innerException, object model) : base(message, innerException, model)
        {
        }

        protected CustomArgumentOutOfRangeException(SerializationInfo info, StreamingContext context, object model) : base(info, context, model)
        {
        }

        public CustomArgumentOutOfRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
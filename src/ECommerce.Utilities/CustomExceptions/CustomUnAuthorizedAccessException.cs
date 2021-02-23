using System;
using System.Runtime.Serialization;

namespace ECommerce.Utilities.CustomExceptions
{
    public class CustomForbiddenException : CustomException
    {
        public CustomForbiddenException(object model) : base(model)
        {
        }

        public CustomForbiddenException(string message) : base(message)
        {
        }

        public CustomForbiddenException(string message, object model) : base(message, model)
        {
        }

        public CustomForbiddenException(string message, Exception innerException, object model) : base(message, innerException, model)
        {
        }

        protected CustomForbiddenException(SerializationInfo info, StreamingContext context, object model) : base(info, context, model)
        {
        }

        public CustomForbiddenException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

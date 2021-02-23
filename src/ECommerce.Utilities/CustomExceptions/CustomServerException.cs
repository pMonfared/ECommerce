using System;
using System.Runtime.Serialization;

namespace ECommerce.Utilities.CustomExceptions
{
    public class CustomServerException : CustomException
    {
        public CustomServerException(string message) : base(message)
        {
        }

        public CustomServerException(object model) : base(model)
        {
        }

        public CustomServerException(string message, object model) : base(message, model)
        {
        }

        public CustomServerException(string message, Exception innerException, object model) : base(message, innerException, model)
        {
        }

        protected CustomServerException(SerializationInfo info, StreamingContext context, object model) : base(info, context, model)
        {
        }

        public CustomServerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

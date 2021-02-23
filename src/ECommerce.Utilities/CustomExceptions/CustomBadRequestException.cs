using System;
using System.Runtime.Serialization;

namespace ECommerce.Utilities.CustomExceptions
{
    public class CustomBadRequestException : CustomException
    {
        
        
        public CustomBadRequestException(string message) : base(message)
        {
        }

        public CustomBadRequestException(object model) : base(model)
        {
        }

        public CustomBadRequestException(string message, object model) : base(message, model)
        {
        }

        public CustomBadRequestException(string message, Exception innerException, object model) : base(message, innerException, model)
        {
        }

        protected CustomBadRequestException(SerializationInfo info, StreamingContext context, object model) : base(info, context, model)
        {
        }

        public CustomBadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

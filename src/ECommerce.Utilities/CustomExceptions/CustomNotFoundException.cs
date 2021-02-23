using System;
using System.Runtime.Serialization;

namespace ECommerce.Utilities.CustomExceptions
{
    public class CustomNotFoundException : CustomException

    {
        public CustomNotFoundException(string message) : base(message)
        {
        }

        public CustomNotFoundException(object model) : base(model)
        {
        }

        public CustomNotFoundException(string message, object model) : base(message, model)
        {
        }

        public CustomNotFoundException(string message, Exception innerException, object model) : base(message, innerException,
            model)
        {
        }

        protected CustomNotFoundException(SerializationInfo info, StreamingContext context, object model) : base(info,
            context, model)
        {
        }

        public CustomNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
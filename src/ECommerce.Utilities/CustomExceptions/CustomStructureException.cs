using System;
using System.Runtime.Serialization;

namespace ECommerce.Utilities.CustomExceptions
{
    public class CustomStructureException : CustomException
    {
        public CustomStructureException(object model) : base(model)
        {
        }

        public CustomStructureException(string message) : base(message)
        {
        }

        public CustomStructureException(string message, object model) : base(message, model)
        {
        }

        public CustomStructureException(string message, Exception innerException, object model) : base(message, innerException, model)
        {
        }

        protected CustomStructureException(SerializationInfo info, StreamingContext context, object model) : base(info, context, model)
        {
        }

        public CustomStructureException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

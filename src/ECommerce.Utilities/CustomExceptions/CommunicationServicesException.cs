using System;
using System.Runtime.Serialization;

namespace ECommerce.Utilities.CustomExceptions
{
    public class CommunicationServicesException : CustomException
    {
        public CommunicationServicesException(string message) : base(message)
        {
        }

        public CommunicationServicesException(object model) : base(model)
        {
        }

        public CommunicationServicesException(string message, object model) : base(message, model)
        {
        }

        public CommunicationServicesException(string message, Exception innerException, object model) : base(message, innerException, model)
        {
        }

        public CommunicationServicesException(SerializationInfo info, StreamingContext context, object model) : base(info, context, model)
        {
        }

        public CommunicationServicesException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
using System;
using System.Runtime.Serialization;

namespace ECommerce.Utilities.CustomExceptions
{
    public class CustomException : Exception
    {
        private object Model { get; set; }
        public ExceptionDetails ExceptionDetails { get; set; } = new ExceptionDetails();
        private Guid ExceptionId { get; set; } = Guid.NewGuid();

        public string Id => ExceptionId.ToString();

        protected CustomException(string message) : base(message)
        {
        }

        protected CustomException(object model)
        {
            Model = model;
        }

        protected CustomException(string message, object model) : base(message)
        {
            Model = model;
        }

        protected CustomException(string message, Exception innerException, object model) : base(message,
            innerException)
        {
            Model = model;
        }

        protected CustomException(SerializationInfo info, StreamingContext context, object model) : base(info,
            context)
        {
            Model = model;
        }

        protected CustomException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
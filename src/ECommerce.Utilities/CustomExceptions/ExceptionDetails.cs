namespace ECommerce.Utilities.CustomExceptions
{
    public class ExceptionDetails
    {
        public string Message { get; set; }
        public string Detail { get; set; }
        public string ErrorCode { get; set; }
        public string Property { get; set; }
        public bool IsHighPriority { get; set; }
        public string OccurAddress { get; set; }

        public ExceptionDetails()
        {
        }
        public ExceptionDetails(string property)
        {
            Property = property;
        }
        
        public ExceptionDetails(string message, string errorCode,string detail)
        {
            Message = message;
            Detail = detail;
            ErrorCode = errorCode;
        }
        
        public ExceptionDetails(string message, string errorCode, string detail, string property)
        {
            Message = message;
            ErrorCode = errorCode;
            Property = property;
            Detail = detail;
        }

        public ExceptionDetails(string message, int errorCode, string detail)
        {
            Message = message;
            Detail = detail;
            ErrorCode = errorCode.ToString();
        }

    }
}
namespace ECommerce.Utilities.Consts
{
    public static class Errors
    {
        public static class General
        {
            public static class NotFound
            {
                public const string Message = "NotFound";
                public const string Detail = "The item does not exist";
                public const string ErrorCode = "general-1";
            }

            public static class ForbiddenAccessDenied
            {
                public const string Message = "ForbiddenAccessDenied";
                public const string Detail = "The request is understood, but it has been refused or access is not allowed";
                public const string ErrorCode = "general-2";
            }

            public static class InternalServer
            {
                public const string Message = "InternalServer";
                public const string Detail = "There is an internal server error";
                public const string ErrorCode = "general-3";
            }

            public static class ArgumentOutOfRange
            {
                public const string Message = "ArgumentOutOfRange";
                public const string Detail = "Request params are out of range";
                public const string ErrorCode = "general-4";
            }
            
            public static class TooManyRequests
            {
                public const string Message = "TooManyRequests";
                public const string Detail = "The request cannot be served due to the rate limit having been exhausted for the resource";
                public const string ErrorCode = "general-5";
            }
        }
    }
}
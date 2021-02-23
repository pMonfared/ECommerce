namespace ECommerce.Api.Search.Consts
{
    public static class SearchErrors
    {
        public static class Search
        {
            public static class CustomerIdOutOfRange
            {
                public const string Message = "Search.CustomerIdOutOfRange";
                public const string Detail = "The attribute cannot be zero or less than zero";
                public const string ErrorCode = "search-1";
            }
            
            public static class CustomerCannotFind
            {
                public const string Message = "Search.CustomerCannotFind";
                public const string ErrorCode = "search-2";
            }
        }
    }
}
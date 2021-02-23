namespace ECommerce.Utilities.DomainHelpers.Queries
{
    public class QueryParam
    {
        public string Search { get; protected set; }
        public int? Offset { get; protected set; }
        public int? Limit { get; protected set; }
        
        public QueryParam(int? offset, int? limit, string search)
        {
            Offset = offset;
            Limit = limit;
            Search = search;

            if (Offset != null && offset < 0)
            {
                Offset = 0;
            }

            if (Limit != null && Limit <= 0)
            {
                Limit = 10;
            }
        }
    }
}

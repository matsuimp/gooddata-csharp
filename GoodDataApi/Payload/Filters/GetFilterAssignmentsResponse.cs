namespace GoodDataApi.Payload.Filters
{
    public class FilterMapping
    {
        public string User;
        public string[] UserFilters;
    }

    public class FilterAssignments
    {
        public int Count;
        public FilterMapping[] Items;
        public int Length;
        public int Offset;
    }

    public class GetFilterAssignmentsResponse
    {
        public FilterAssignments UserFilters;
    }
}
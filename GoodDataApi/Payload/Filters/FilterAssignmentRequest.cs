namespace GoodDataApi.Payload.Filters
{
    public class FilterAssignment
    {
        public string User;
        public string[] UserFilters;
    }

    public class AssignmentsContainer
    {
        public FilterAssignment[] Items;
    }

    public class FilterAssignmentRequest
    {
        public AssignmentsContainer UserFilters;
    }
}
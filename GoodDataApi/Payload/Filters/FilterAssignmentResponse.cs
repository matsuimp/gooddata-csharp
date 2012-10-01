namespace GoodDataApi.Payload.Filters
{
    public class UserFilterAssignemtnResults
    {
        public string[] Failed;
        public string[] Successful;
    }

    public class FilterAssignmentResponse
    {
        public UserFilterAssignemtnResults UserFiltersUpdateResult;
    }
}
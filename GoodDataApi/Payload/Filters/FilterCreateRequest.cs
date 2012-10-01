namespace GoodDataApi.Payload.Filters
{
    public class FilterCreateRequest
    {
        public CreateUserFilter UserFilter;

        public FilterCreateRequest(string expression, string title)
        {
            UserFilter = new CreateUserFilter
                             {
                                 Content = new CreateUserFilterContent {Expression = expression},
                                 Meta = new CreateUserFilterMetadata {Category = "userFilter", Title = title}
                             };
        }

        public FilterCreateRequest()
        {
            
        }
    }

    public class CreateUserFilter
    {
        public CreateUserFilterContent Content;
        public CreateUserFilterMetadata Meta;
    }

    public class CreateUserFilterContent
    {
        public string Expression;
    }

    public class CreateUserFilterMetadata
    {
        public string Category;
        public string Title;
    }
}
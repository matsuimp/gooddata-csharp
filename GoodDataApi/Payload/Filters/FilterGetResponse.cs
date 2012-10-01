using System;

namespace GoodDataApi.Payload.Filters
{
    public class UserFilterGetResponse
    {
        public UserFilter UserFilter;
    }

    public class UserFilter
    {
        public UserFilterContent Content;
        public UserFilterMetadata Meta;
    }

    public class UserFilterContent
    {
        public string Expression;
        public UserFilterObject[] Objects;
    }

    public class UserFilterObject
    {
        public string Link;
        public string Author;
        public string Tags;
        public DateTime Created;
        public bool Deprecated;
        public string Summary;
        public string Title;
        public string Category;
        public DateTime Updated;
        public string Contributor;
        public string Uri;
        public string AttributeUri;
    }

    public class UserFilterMetadata
    {
        public string Author;
        public string Category;
        public string Contributor;
        public DateTime Created;
        public bool Deprecated;
        public string Identifier;
        public string Tags;
        public string Title;
        public DateTime Updated;
        public string Uri;
    }
}
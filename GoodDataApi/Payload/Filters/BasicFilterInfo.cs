using System;

namespace GoodDataApi.Payload.Filters
{
    public class BasicFilterInfo
    {
        public string Name;
        public string Uri;
    }

    public class FilterQueryContainer
    {
        public FilterQuery Query;
    }

    public class FilterQuery
    {
        public Entry[] Entries;
        public Metadata Meta;
    }

    public class Entry
    {
        public string Author;
        public string Category;
        public string Contributor;
        public DateTime Created;
        public bool Deprecated;
        public string Link;
        public string Summary;
        public string Tags;
        public DateTime Updated;
    }

    public class Metadata
    {
        public string Category;
        public string Summary;
        public string Title;
    }
}
namespace GoodDataApi.Payload.Attribute
{
	public class ElementsMetadata
	{
		public int Count;
		public string Mode;
		public string Filter;
		public string Records;
		public string Prompt;
		public string Attribute;
		public string Order;
		public string AttributeDisplayForm;
		public string Offset;
	}

	public class Element
	{
		public string Title;
		public string Uri;
	}

	public class ElementsInfo
	{
		public ElementsMetadata ElementsMeta;
		public Element[] Elements;
	}

	public class GetElementsResponse
	{
		public ElementsInfo AttributeElements;
	}
}
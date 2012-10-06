using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoodDataApi.Payload.Attribute
{
	public class AttributePrimaryKey
	{
		public string Data ;
		public string Type ;
	}

	public class AttributeContent
	{
		public string FormOf ;
		public string Expression ;
		public string Ldmexpression ;
	}

	public class AttributeLinks
	{
		public string Elements ;
	}

	public class AttributeMetadata
	{
		public string Author ;
		public string Uri ;
		public string Tags ;
		public string Created ;
		public string Identifier ;
		public string Deprecated ;
		public string Summary ;
		public string Title ;
		public string Category ;
		public string Updated ;
		public string Contributor ;
	}

	public class AttributeDisplayForm
	{
		public AttributeContent Content ;
		public AttributeLinks Links ;
		public AttributeMetadata Meta ;
	}

	public class AttributeForeignKey
	{
		public string Data ;
		public string Type ;
	}

	public class AttributeContent2
	{
		public AttributePrimaryKey[] Pk ;
		public string Dimension ;
		public AttributeDisplayForm[] DisplayForms ;
		public AttributeForeignKey[] Fk ;
	}

	public class AttributeMetadata2
	{
		public string Author ;
		public string Uri ;
		public string Tags ;
		public string Created ;
		public string Identifier ;
		public string Deprecated ;
		public string Summary ;
		public string Title ;
		public string Category ;
		public string Updated ;
		public string Contributor ;
	}

	public class Attribute
	{
		public AttributeContent2 Content ;
		public AttributeMetadata2 Meta ;
	}

	public class AttributeResponse
	{
		public Attribute Attribute ;
	}
}

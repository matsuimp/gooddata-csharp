using GoodDataApi.Payload;
using GoodDataApi.Payload.Attribute;

namespace GoodDataApi.Resources
{
	public interface IGoodDataAttribute
	{
		GoodDataResponse<AttributeResponse> Get(string attributeUri);
		GoodDataResponse<GetElementsResponse> GetElements(string attributeUri);
	}

	internal sealed class Attribute : IGoodDataAttribute
	{
		private readonly IGoodDataConnection _connection;

		public Attribute(IGoodDataConnection connection)
		{
			_connection = connection;
		}

		public GoodDataResponse<AttributeResponse> Get(string attributeUri)
		{
			return _connection.Get<AttributeResponse>(attributeUri);
		}

		public GoodDataResponse<GetElementsResponse> GetElements(string attributeUri)
		{
			return _connection.Get<GetElementsResponse>(attributeUri);
		}


	}
}
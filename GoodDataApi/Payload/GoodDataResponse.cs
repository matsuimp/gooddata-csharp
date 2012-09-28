using System.Net;

namespace GoodDataApi.Payload
{
	public class GoodDataResponse<T>
	{
		public HttpStatusCode Status { get; set; }
		public string Body { get; set; }
		public T Content { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace GoodDataApi.Payload
{
	public class GoodDataResponse<T>
	{
		public HttpStatusCode Status { get; set; }
		public string Body { get; set; }
		public T Result { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Net;
using GoodDataApi.Exceptions;

namespace GoodDataApi.Payload
{
	public class GoodDataResponse<T>
	{
		public HttpStatusCode Status { get; set; }
		public string Body { get; set; }
		public T Content { get; set; }

		private static readonly HashSet<HttpStatusCode> SuccessResponses = new HashSet<HttpStatusCode>
			                                                                   {
				                                                                   HttpStatusCode.OK,
				                                                                   HttpStatusCode.Created,
			                                                                   };
		public T AssertSuccess()
		{
			if (!SuccessResponses.Contains(Status))
				throw new GoodDataApiException(string.Format("Invalid reponse trying to retrieve {0}. HttpStatus={1}{2}{3}", typeof(T).FullName, Status, Environment.NewLine, Body));

			return Content;
		}
	}
}

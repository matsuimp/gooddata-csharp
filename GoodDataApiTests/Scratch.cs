using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using GoodDataApi.Payload.User;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace GoodDataApiTests
{
	internal static class MimeType
	{
		public const string Json = "application/json";

	}
	[TestFixture]
	public class Scratch
	{
		private const string DevProjectId = @"us03pustmnl2z7c9jm9vy1f9qiy2ve36";
		private const string GetAllUserFilters = @"/gdc/md/us03pustmnl2z7c9jm9vy1f9qiy2ve36/userfilters";
		private const string Login = @"/gdc/account/login";
		private const string Token = "/gdc/account/token";

		private readonly JsonSerializerSettings Settings = new JsonSerializerSettings
			                                                   {
				                                                   ContractResolver = new CamelCasePropertyNamesContractResolver(),
				                                                   NullValueHandling = NullValueHandling.Ignore,

			                                                   };
		[Test]
		public void GetAllFilters()
		{
			var handler = new HttpClientHandler { UseCookies = true };
			using (var client = new HttpClient(handler) {BaseAddress = new Uri(@"https://secure.gooddata.com")})
			{
				//var body = new AuthenticationRequest {PostUserLogin = new PostUserLogin {Login = test", Password = "test"}};
				
				//var bodyStr = JsonConvert.SerializeObject(body, Settings);
				//var result = client
				//	.PostAsync(Login, new StringContent(bodyStr, Encoding.UTF8, MimeType.Json))
				//	.Result;

				var token = client.GetAsync(Token).Result;
				//var cookie = result.Headers.GetValues("Set-Cookie").ToArray();
				//var strResult = result.Content.ReadAsStringAsync().Result;

				var filters = client.GetAsync(GetAllUserFilters).Result;

				var moreFilters = client.GetAsync(GetAllUserFilters).Result;
				
				
				

				//Console.Out.WriteLine(result);


				
			}
		}
	}
}
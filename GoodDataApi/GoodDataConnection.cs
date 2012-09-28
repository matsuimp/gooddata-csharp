using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using GoodDataApi.Payload;
using GoodDataApi.Payload.User;
using GoodDataApi.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using log4net;

namespace GoodDataApi
{
	internal interface IGoodDataConnection
	{
		GoodDataResponse<T> Post<T>(string uri, object payload, bool checkAuthentication=true);
		GoodDataResponse<T> Get<T>(string uri, bool checkAuthentication = true);
	}

	internal static class MimeType
	{
		public const string Json = @"application/json";
	}

	public class GoodDataConnection : IDisposable, IGoodDataConnection
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly string _email;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly string _password;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly int _remember;

		private bool _loggedIn = false;
		private bool _tokenAcquired = false;

		private readonly HttpClientHandler _httpHandler;
		private readonly HttpClient _client;


		private static readonly JsonSerializerSettings SerializationSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = NullValueHandling.Ignore, };
		private static readonly ILog Logger = LogManager.GetLogger(typeof (GoodDataConnection));
		public IMandatoryUserFilter MandatoryUserFilter { get; private set; }
		public IGoodDataUser User { get; private set; }

		public GoodDataConnection(string email, string password, int remember=0)
		{
			_email = email;
			_password = password;
			_remember = remember;

			_httpHandler = new HttpClientHandler {UseCookies = true};
			_client = new HttpClient(_httpHandler){BaseAddress = new Uri(Urls.Base)};

			MandatoryUserFilter = new MandatoryUserFilter(this);
			User = new User(this);
		}

		public GoodDataConnection() : this (AppConfig.Instance.Login, AppConfig.Instance.Password)
		{
			
		}

		GoodDataResponse<T> IGoodDataConnection.Post<T>(string uri, object payload, bool checkAuthentication = true)
		{
			if (checkAuthentication)
				Authenticate();

			var json = JsonConvert.SerializeObject(payload, SerializationSettings);
			var payloadContent = new StringContent(json, Encoding.UTF8, MimeType.Json);

			var result = AuthenticationRetry(client => client.PostAsync(uri, payloadContent).Result);
			var resultContent = result.Content.ReadAsStringAsync().Result;
			var desirializedResult = default(T);

			try
			{
				desirializedResult = JsonConvert.DeserializeObject<T>(resultContent);
			}
			catch (Exception e)
			{
				Logger.Error(string.Format("Could not deserialize type {0}",  typeof(T).FullName), e);	
			}


			return new GoodDataResponse<T>
				            {
					            Status = result.StatusCode,
					            Body = resultContent,
					            Result = desirializedResult,
				            };
		}

		GoodDataResponse<T> IGoodDataConnection.Get<T>(string uri, bool checkAuthentication = true)
		{
			if (checkAuthentication)
				Authenticate();

			var result = AuthenticationRetry(client => client.GetAsync(uri).Result);
			var resultContent = result.Content.ReadAsStringAsync().Result;
			var desirializedResult = default(T);

			try
			{
				desirializedResult = JsonConvert.DeserializeObject<T>(resultContent);
			}
			catch (Exception e)
			{
				Logger.Error(string.Format("Could not deserialize type {0}", typeof(T).FullName), e);
			}


			return new GoodDataResponse<T>
			{
				Status = result.StatusCode,
				Body = resultContent,
				Result = desirializedResult,
			};
		}

		private HttpResponseMessage AuthenticationRetry (Func<HttpClient, HttpResponseMessage> request )
		{
			var response = request(_client);
			if (_loggedIn && _tokenAcquired && response.StatusCode == HttpStatusCode.Unauthorized)
			{
				GetToken();
			}
			return request(_client);
		}

		public void Dispose()
		{
			// close the connection if open
			// dispose of the http client
			if (null != _client)
			{

				_client.Dispose();
			}
				
		}

		private void Authenticate()
		{
			if (!_loggedIn)
				Login();

			if (!_tokenAcquired)
				GetToken();
		}

		private void Login()
		{
			var result = Post<string>(Urls.Login, new AuthenticationRequest {PostUserLogin = new PostUserLogin {Login = _email, Password = _password, Remember = _remember}});
			_loggedIn = result.Status == HttpStatusCode.Created;
		}

		private void LogOut()
		{
			
		}

		private void GetToken()
		{
			var result = Get<AuthenticationResponse>(Urls.Token);
			_tokenAcquired = result.Status == HttpStatusCode.OK;
		}


		private static class Urls
		{
			public static string Base
			{
				get { return ConfigurationManager.AppSettings.ValueOrDefault("GoodData.BaseUrl", @"https://secure.gooddata.com"); }
			}

			public static string Login
			{
				get { return ConfigurationManager.AppSettings.ValueOrDefault("GoodData.LoginUrl", @"/gdc/account/login"); }
			}

			public static string Token
			{
				get { return ConfigurationManager.AppSettings.ValueOrDefault("GoodData.TokenUrl", @"/gdc/account/token"); }
			}
		}
	}
}

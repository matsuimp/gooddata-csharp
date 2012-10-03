using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using GoodDataApi.Exceptions;
using GoodDataApi.Payload;
using GoodDataApi.Payload.User;
using GoodDataApi.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using log4net;
using ProjectUser = GoodDataApi.Resources.ProjectUser;

namespace GoodDataApi
{
	internal interface IGoodDataConnection
	{
		string ProfileId { get; }
		GoodDataResponse<T> Post<T>(string uri, object payload, bool checkAuthentication = true, params JsonConverter[] converters);
		GoodDataResponse<T> Get<T>(string uri, bool checkAuthentication = true, params JsonConverter[] converters);
		GoodDataResponse<T> Delete<T>(string uri, bool checkAuthentication = true, params JsonConverter[] converters);
		GoodDataResponse<T> Put<T>(string uri, object payload, bool checkAuthentication = true, params JsonConverter[] converters);
	}

	public class GoodDataConnection : IDisposable, IGoodDataConnection
	{
		private static readonly ILog Logger = LogManager.GetLogger(typeof (GoodDataConnection));
		private readonly HttpClient _client;
		private readonly HttpClientHandler _httpHandler;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly string _email;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly string _password;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly int _remember;

		private bool _loggedIn;
		private string _profileId;
		private bool _tokenAcquired;

		public GoodDataConnection(string email, string password, int remember = 0)
		{
			_email = email;
			_password = password;
			_remember = remember;

			_httpHandler = new HttpClientHandler {UseCookies = true};
			_client = new HttpClient(_httpHandler) {BaseAddress = new Uri(Urls.Base)};
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MimeType.Json));

			MandatoryUserFilter = new MandatoryUserFilter(this);
			DomainUser = new DomainUser(this);
			ProjectUser = new ProjectUser(this);
			Project = new Project(this);
			Role = new Role(this);
		}

		public GoodDataConnection() : this(AppConfig.Instance.Login, AppConfig.Instance.Password)
		{
		}

		public IMandatoryUserFilter MandatoryUserFilter { get; private set; }
		public IGoodDataProjectUser ProjectUser { get; private set; }
		public IGoodDataDomainUser DomainUser { get; private set; }
		public IProject Project { get; private set; }
		public IRole Role { get; private set; }

		private IGoodDataConnection Conn
		{
			get { return this; }
		}

		public void Dispose()
		{
			if (null != _client)
			{
				Logout();
				_client.Dispose();
			}
		}

		public string ProfileId
		{
			get
			{
				Authenticate();
				return _profileId;
			}
		}

		GoodDataResponse<T> IGoodDataConnection.Post<T>(string uri, object payload, bool checkAuthentication, params JsonConverter[] converters)
		{
			if (checkAuthentication)
				Authenticate();

			var convertArgs = converters == null || converters.Length == 0
				                  ? new List<JsonConverter> {new BoolConverter()}
				                  : converters.ToList();

			var settings = new JsonSerializerSettings
				               {
					               ContractResolver = new CamelCasePropertyNamesContractResolver(),
					               NullValueHandling = NullValueHandling.Ignore,
					               Converters = convertArgs
				               };

			var result = AuthenticationRetry(client =>
				                                 {
					                                 var json = JsonConvert.SerializeObject(payload, settings);
					                                 var payloadContent = new StringContent(json, Encoding.UTF8, MimeType.Json);
					                                 return client.PostAsync(uri, payloadContent).Result;
				                                 });
			var resultContent = result.Content.ReadAsStringAsync().Result;
			var desirializedResult = default(T);

			try
			{
				desirializedResult = JsonConvert.DeserializeObject<T>(resultContent, settings);
			}
			catch (Exception e)
			{
				Logger.Error(string.Format("Could not deserialize type {0}", typeof (T).FullName), e);
			}


			return new GoodDataResponse<T>
				       {
					       Status = result.StatusCode,
					       Body = resultContent,
					       Content = desirializedResult,
				       };
		}

		GoodDataResponse<T> IGoodDataConnection.Put<T>(string uri, object payload, bool checkAuthentication, params JsonConverter[] converters)
		{
			if (checkAuthentication)
				Authenticate();

			var convertArgs = converters == null || converters.Length == 0
				                  ? new List<JsonConverter> {new BoolConverter()}
				                  : converters.ToList();

			var settings = new JsonSerializerSettings
				               {
					               ContractResolver = new CamelCasePropertyNamesContractResolver(),
					               NullValueHandling = NullValueHandling.Ignore,
					               Converters = convertArgs
				               };

			var result = AuthenticationRetry(client =>
				                                 {
					                                 var json = JsonConvert.SerializeObject(payload, settings);
					                                 var payloadContent = new StringContent(json, Encoding.UTF8, MimeType.Json);
					                                 return client.PutAsync(uri, payloadContent).Result;
				                                 });
			var resultContent = result.Content.ReadAsStringAsync().Result;
			var desirializedResult = default(T);

			try
			{
				desirializedResult = JsonConvert.DeserializeObject<T>(resultContent, settings);
			}
			catch (Exception e)
			{
				Logger.Error(string.Format("Could not deserialize type {0}", typeof (T).FullName), e);
			}


			return new GoodDataResponse<T>
				       {
					       Status = result.StatusCode,
					       Body = resultContent,
					       Content = desirializedResult,
				       };
		}

		GoodDataResponse<T> IGoodDataConnection.Get<T>(string uri, bool checkAuthentication, params JsonConverter[] converters)
		{
			if (checkAuthentication)
				Authenticate();

			var result = AuthenticationRetry(client => client.GetAsync(uri).Result);
			var resultContent = result.Content.ReadAsStringAsync().Result;
			var desirializedResult = default(T);

			var convertArgs = converters == null || converters.Length == 0
				                  ? new List<JsonConverter> {new BoolConverter()}
				                  : converters.ToList();

			var settings = new JsonSerializerSettings
				               {
					               ContractResolver = new CamelCasePropertyNamesContractResolver(),
					               NullValueHandling = NullValueHandling.Ignore,
					               Converters = convertArgs
				               };

			try
			{
				desirializedResult = JsonConvert.DeserializeObject<T>(resultContent, settings);
			}
			catch (Exception e)
			{
				Logger.Error(string.Format("Could not deserialize type {0}", typeof (T).FullName), e);
			}


			return new GoodDataResponse<T>
				       {
					       Status = result.StatusCode,
					       Body = resultContent,
					       Content = desirializedResult,
				       };
		}

		GoodDataResponse<T> IGoodDataConnection.Delete<T>(string uri, bool checkAuthentication, params JsonConverter[] converters)
		{
			if (checkAuthentication)
				Authenticate();

			var result = AuthenticationRetry(client => client.DeleteAsync(uri).Result);
			var resultContent = result.Content.ReadAsStringAsync().Result;
			var desirializedResult = default(T);

			var convertArgs = converters == null || converters.Length == 0
				                  ? new List<JsonConverter> {new BoolConverter()}
				                  : converters.ToList();

			var settings = new JsonSerializerSettings
				               {
					               ContractResolver = new CamelCasePropertyNamesContractResolver(),
					               NullValueHandling = NullValueHandling.Ignore,
					               Converters = convertArgs
				               };


			try
			{
				desirializedResult = JsonConvert.DeserializeObject<T>(resultContent, settings);
			}
			catch (Exception e)
			{
				Logger.Error(string.Format("Could not deserialize type {0}", typeof (T).FullName), e);
			}


			return new GoodDataResponse<T>
				       {
					       Status = result.StatusCode,
					       Body = resultContent,
					       Content = desirializedResult,
				       };
		}

		private HttpResponseMessage AuthenticationRetry(Func<HttpClient, HttpResponseMessage> request)
		{
			var response = request(_client);
			if (_loggedIn && _tokenAcquired && response.StatusCode == HttpStatusCode.Unauthorized)
			{
				GetToken();
				return request(_client);
			}
			return response;
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
			var payload = new AuthenticationRequest {PostUserLogin = new PostUserLogin {Login = _email, Password = _password, Remember = _remember}};
			var result = Conn.Post<AuthenticationResponse>(Urls.Login, payload, false);
			_profileId = GoodDataStrings.IdFromUri(result.Content.UserLogin.Profile);
			_loggedIn = result.Status == HttpStatusCode.OK;

			if (!_loggedIn)
				throw new GoodDataApiException(string.Format("Unabled to login to GoodData. Status Code = {0}", result.Status));
		}

		private void Logout()
		{
			// i don't know why this doesn't work? it returns a 404.
			//if (!_loggedIn || !_tokenAcquired)
			//	return;

			//Conn.Delete<string>(Urls.Logout(ProfileId), false);
		}

		private void GetToken()
		{
			var result = Conn.Get<object>(Urls.Token, false);
			_tokenAcquired = result.Status == HttpStatusCode.OK;

			if (!_tokenAcquired)
				throw new GoodDataApiException(string.Format("Unabled to acquire token to communicate with GoodData. Status Code = {0}", result.Status));
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

			public static string Logout(string profileId)
			{
				return string.Format(ConfigurationManager.AppSettings.ValueOrDefault("GoodData.LogoutUrl", @"/gdc/account/{0}"), profileId);
			}
		}
	}
}
using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
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
        GoodDataResponse<T> Post<T>(string uri, object payload, bool checkAuthentication = true, Func<string, T> transform = null);
        GoodDataResponse<T> Get<T>(string uri, bool checkAuthentication = true, Func<string, T> transform = null);
    }

    internal static class MimeType
    {
        public const string Json = @"application/json";
    }

    public class GoodDataConnection : IDisposable, IGoodDataConnection
    {
        private static readonly JsonSerializerSettings SerializationSettings = new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = NullValueHandling.Ignore,};
        private static readonly ILog Logger = LogManager.GetLogger(typeof (GoodDataConnection));
        private readonly HttpClient _client;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly string _email;
        private readonly HttpClientHandler _httpHandler;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly string _password;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)] private readonly int _remember;

        private bool _loggedIn;
        private bool _tokenAcquired;

        public GoodDataConnection(string email, string password, int remember = 0)
        {
            _email = email;
            _password = password;
            _remember = remember;

            _httpHandler = new HttpClientHandler {UseCookies = true};
            _client = new HttpClient(_httpHandler) {BaseAddress = new Uri(Urls.Base)};

            MandatoryUserFilter = new MandatoryUserFilter(this);
            User = new User(this);
        }

        public GoodDataConnection() : this(AppConfig.Instance.Login, AppConfig.Instance.Password)
        {
        }

        public IMandatoryUserFilter MandatoryUserFilter { get; private set; }
        public IGoodDataUser User { get; private set; }

        private IGoodDataConnection Conn
        {
            get { return this; }
        }

        public void Dispose()
        {
            if (null != _client)
            {
                _client.Dispose();
            }
        }

        GoodDataResponse<T> IGoodDataConnection.Post<T>(string uri, object payload, bool checkAuthentication, Func<string, T> transform )
        {
            if (checkAuthentication)
                Authenticate();

            var result = AuthenticationRetry(client =>
                                                 {
                                                     var json = JsonConvert.SerializeObject(payload, SerializationSettings);
                                                     var payloadContent = new StringContent(json, Encoding.UTF8, MimeType.Json);
                                                     return client.PostAsync(uri, payloadContent).Result;
                                                 });
            var resultContent = result.Content.ReadAsStringAsync().Result;
            var desirializedResult = default(T);

            try
            {
                if (null != transform)
                    desirializedResult = transform(resultContent);
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

        GoodDataResponse<T> IGoodDataConnection.Get<T>(string uri, bool checkAuthentication, Func<string, T> transform )
        {
            if (checkAuthentication)
                Authenticate();

            var result = AuthenticationRetry(client =>
                                                 {
                                                     client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MimeType.Json));
                                                     return client.GetAsync(uri).Result;
                                                 });
            var resultContent = result.Content.ReadAsStringAsync().Result;
            var desirializedResult = default(T);

            try
            {
                if (null != transform)
                    desirializedResult = transform(resultContent);
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
            var result = Conn.Post<string>(Urls.Login, payload, false);
            _loggedIn = result.Status == HttpStatusCode.OK;
        }

        private void LogOut()
        {
        }

        private void GetToken()
        {
            var result = Conn.Get<AuthenticationResponse>(Urls.Token, false);
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
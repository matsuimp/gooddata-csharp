using System.Collections.Generic;
using System.Configuration;
using System.Net;
using GoodDataApi.Exceptions;
using GoodDataApi.Payload;
using GoodDataApi.Payload.User;
using Newtonsoft.Json;

namespace GoodDataApi.Resources
{
    public interface IGoodDataProjectUser
    {
        GoodDataResponse<ProjectUsersResponse> GetProjectUsers(string projectId);
        GoodDataResponse<ProjectUserResponse> CreateProjectUser(ProjectUserRequest request, string projectId);
        
        GoodDataResponse<ProjectUserPayload> GetProjectUser(string projectId, string profileId);
        GoodDataResponse<ProjectUserResponse> UpdateProjectUser(string projectId, ProjectUserRequest request);
    }

    public interface IGoodDataDomainUser
    {
        GoodDataResponse<object> DeleteDomainUser(string profileId);
        GoodDataResponse<CreateDomainUserResponse> CreateDomainUser(CreateDomainUserRequest request, string domainName = null);
        GoodDataResponse<DomainUsersResponse> GetDomainUsers(int offset = 0, int limit = 1000, string domainName = null);
        GoodDataResponse<object> UpdateDomainUser(UpdateUserRequest request, string profileId);
        IEnumerable<DomainUserAccountSettings> AllDomainUsers(string domainName = null);
    }

    internal sealed class User : IGoodDataProjectUser, IGoodDataDomainUser
	{
		private readonly IGoodDataConnection _connection;

		public User(IGoodDataConnection connection)
		{
			_connection = connection;
		}

		public GoodDataResponse<ProjectUsersResponse> GetProjectUsers(string projectId)
		{
            return _connection.Get < ProjectUsersResponse>(Urls.ProjectUsers(projectId), converters:new EnabledDisabledBoolConvert());
		}

		public GoodDataResponse<ProjectUserResponse> CreateProjectUser(ProjectUserRequest request, string projectId)
		{
            return _connection.Post < ProjectUserResponse>(Urls.ProjectUsers(projectId), request, converters:new EnabledDisabledBoolConvert());
		}

        public GoodDataResponse<ProjectUserPayload> GetProjectUser(string projectId, string profileId)
        {
            return _connection.Get < ProjectUserPayload>(Urls.SpecificProjectUser(projectId, profileId), converters: new EnabledDisabledBoolConvert());
        }

        public GoodDataResponse<ProjectUserResponse> UpdateProjectUser(string projectId, ProjectUserRequest request)
        {
            return _connection.Post<ProjectUserResponse>(Urls.ProjectUsers(projectId), request, converters: new EnabledDisabledBoolConvert());
        }

		public GoodDataResponse<object> UpdateDomainUser(UpdateUserRequest request, string profileId)
		{
			return _connection.Put<object>(Urls.User(profileId), request);
		}

		public GoodDataResponse<object> DeleteDomainUser(string profileId)
		{
			return _connection.Delete<object>(Urls.User(profileId));
		}

		public GoodDataResponse<CreateDomainUserResponse> CreateDomainUser(CreateDomainUserRequest request, string domainName=null)
		{
			if (null == domainName)
				domainName = AppConfig.Instance.DomainName;

			return _connection.Post<CreateDomainUserResponse>(Urls.CreateDomainUser(domainName), request);
		}

		public GoodDataResponse<DomainUsersResponse> GetDomainUsers(int offset=0, int limit=1000, string domainName=null)
		{
			if (null == domainName)
				domainName = AppConfig.Instance.DomainName;

			return _connection.Get<DomainUsersResponse>(Urls.GetAllDomainUsers(offset, limit, domainName));
		}

        public IEnumerable<DomainUserAccountSettings> AllDomainUsers(string domainName=null)
        {
            if (null == domainName)
                domainName = AppConfig.Instance.DomainName;

            string next = "anything";
            var result = GetDomainUsers(domainName: domainName);
            while (!string.IsNullOrWhiteSpace(next))
            {
                if (result.Status != HttpStatusCode.OK)
                    throw new GoodDataApiException(result.Body);

                foreach (var container in result.Content.AccountSettings.Items)
                    yield return container.AccountSetting;

                next = result.Content.AccountSettings.Paging.Next;
                result = _connection.Get<DomainUsersResponse>(next);
            }
        }

		private static class Urls
		{
            public static string SpecificProjectUser(string projectId, string profileId)
            {
                return string.Format(ConfigurationManager.AppSettings.ValueOrDefault("GoodData.User.SpecificProjectUser", @"/gdc/projects/{0}/users/{1}"), projectId, profileId);
            }

			public static string ProjectUsers(string projectId)
			{
				return string.Format(ConfigurationManager.AppSettings.ValueOrDefault("GoodData.ProjectUsersUrl", @"/gdc/projects/{0}/users"), projectId);
			}

			public static string CreateDomainUser(string domainName)
			{
				return string.Format(ConfigurationManager.AppSettings.ValueOrDefault("GoodData.CreateDomainUserUrl", @"/gdc/account/domains/{0}/users"), domainName);
			}

			public static string GetAllDomainUsers(int offset, int limit, string domainName)
			{
				return string.Format(ConfigurationManager.AppSettings.ValueOrDefault("GoodData.GetAllDomainUsersUrl", @"/gdc/account/domains/{0}/users?offset={1}&limit={2}"), domainName, offset, limit);
			}

			public static string User(string profileId)
			{
				return string.Format(ConfigurationManager.AppSettings.ValueOrDefault("GoodData.UserUrl", @"/gdc/account/profile/{0}"), profileId);
			}
		}
	}
}
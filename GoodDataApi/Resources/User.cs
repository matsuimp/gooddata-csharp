using System.Configuration;
using GoodDataApi.Payload;
using GoodDataApi.Payload.User;
using Newtonsoft.Json;

namespace GoodDataApi.Resources
{
	public interface IGoodDataUser
	{
		GoodDataResponse<ProjectUsersResponse> ProjectUsers(string projectId);
		GoodDataResponse<CreateDomainUserResponse> AddDomainUser(CreateDomainUserRequest request, string domainName=null);
		GoodDataResponse<DomainUsersResponse> DomainUsers(int offset=0, int limit=1000, string domainName=null);
		GoodDataResponse<ProjectUserResponse> AddProjectUser(ProjectUserRequest request, string profileId);
		GoodDataResponse<object> UpdateDomainUser(UpdateUserRequest request, string profileId);
		GoodDataResponse<object> DeleteDomainUser(string profileId);
	}

	internal sealed class User : IGoodDataUser
	{
		private readonly IGoodDataConnection _connection;

		public User(IGoodDataConnection connection)
		{
			_connection = connection;
		}

		public GoodDataResponse<ProjectUsersResponse> ProjectUsers(string projectId)
		{
			return _connection.Get(Urls.ProjectUsers(projectId), transform: content => JsonConvert.DeserializeObject<ProjectUsersResponse>(content, new EnabledDisabledBoolConvert()));
		}

		public GoodDataResponse<ProjectUserResponse> AddProjectUser(ProjectUserRequest request, string profileId)
		{
			return _connection.Post(Urls.ProjectUsers(profileId), request, transform: content => JsonConvert.DeserializeObject<ProjectUserResponse>(content, new EnabledDisabledBoolConvert()));
		}

		public GoodDataResponse<object> UpdateDomainUser(UpdateUserRequest request, string profileId)
		{
			return _connection.Put<object>(Urls.User(profileId), request);
		}

		public GoodDataResponse<object> DeleteDomainUser(string profileId)
		{
			return _connection.Delete<object>(Urls.User(profileId));
		}

		public GoodDataResponse<CreateDomainUserResponse> AddDomainUser(CreateDomainUserRequest request, string domainName=null)
		{
			if (null == domainName)
				domainName = AppConfig.Instance.DomainName;

			return _connection.Post<CreateDomainUserResponse>(Urls.CreateDomainUser(domainName), request);
		}

		public GoodDataResponse<DomainUsersResponse> DomainUsers(int offset=0, int limit=1000, string domainName=null)
		{
			if (null == domainName)
				domainName = AppConfig.Instance.DomainName;

			return _connection.Get<DomainUsersResponse>(Urls.GetAllDomainUsers(offset, limit, domainName));
		}

		private static class Urls
		{
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
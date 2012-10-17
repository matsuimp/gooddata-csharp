using System.Collections.Generic;
using System.Configuration;
using System.Net;
using GoodDataApi.Exceptions;
using GoodDataApi.Payload;
using GoodDataApi.Payload.User;

namespace GoodDataApi.Resources
{
	public interface IGoodDataDomainUser
	{
		GoodDataResponse<object> Delete(string profileId);
		GoodDataResponse<CreateDomainUserResponse> Create(CreateDomainUserRequest request, string domainName = null);
		GoodDataResponse<DomainUsersResponse> GetUsers(int offset = 0, int limit = 1000, string domainName = null);
		GoodDataResponse<object> Update(UpdateUserRequest request, string profileId);
		IEnumerable<DomainUserAccountSettings> GetAllUsers(string domainName = null);
	}

	internal sealed class DomainUser : IGoodDataDomainUser
	{
		private readonly IInternalGoodDataConnection _connection;

		public DomainUser(IInternalGoodDataConnection connection)
		{
			_connection = connection;
		}

		public GoodDataResponse<object> Update(UpdateUserRequest request, string profileId)
		{
			return _connection.Put<object>(UserUrls.User(profileId), request);
		}

		public GoodDataResponse<object> Delete(string profileId)
		{
			return _connection.Delete<object>(UserUrls.User(profileId));
		}

		public GoodDataResponse<CreateDomainUserResponse> Create(CreateDomainUserRequest request, string domainName = null)
		{
			if (null == domainName)
				domainName = AppConfig.Instance.DomainName;

			return _connection.Post<CreateDomainUserResponse>(UserUrls.CreateDomainUser(domainName), request);
		}

		public GoodDataResponse<DomainUsersResponse> GetUsers(int offset = 0, int limit = 1000, string domainName = null)
		{
			if (null == domainName)
				domainName = AppConfig.Instance.DomainName;

			return _connection.Get<DomainUsersResponse>(UserUrls.GetAllDomainUsers(offset, limit, domainName));
		}

		public IEnumerable<DomainUserAccountSettings> GetAllUsers(string domainName = null)
		{
			if (null == domainName)
				domainName = AppConfig.Instance.DomainName;

			string nextUrl = null;
			var result = GetUsers(domainName: domainName).AssertSuccess();
			do
			{
				if (!string.IsNullOrWhiteSpace(nextUrl))
				{
					result = _connection.Get<DomainUsersResponse>(nextUrl).AssertSuccess();
				}
					
				foreach (var container in result.AccountSettings.Items)
					yield return container.AccountSetting;

				nextUrl = result.AccountSettings.Paging.Next;

			} while (!string.IsNullOrWhiteSpace(nextUrl));
		}
	}

	internal static class UserUrls
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
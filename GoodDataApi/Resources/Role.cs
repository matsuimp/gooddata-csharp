using System;
using System.Configuration;
using System.Linq;
using System.Net;
using GoodDataApi.Exceptions;
using GoodDataApi.Payload;
using GoodDataApi.Payload.Role;

namespace GoodDataApi.Resources
{
	public interface IRole
	{
		GoodDataResponse<AllProjectRolesResponse> All(string projectId);
		GoodDataResponse<ProjectRoleResponse> Get(string roleUri);
		string Find(string projectId, string roleIdentifier);
	}

	internal sealed class Role : IRole
	{
		private readonly IGoodDataConnection _connection;

		public Role(IGoodDataConnection connection)
		{
			_connection = connection;
		}

		public GoodDataResponse<AllProjectRolesResponse> All(string projectId)
		{
			return _connection.Get<AllProjectRolesResponse>(Urls.All(projectId));
		}

		public GoodDataResponse<ProjectRoleResponse> Get(string roleUri)
		{
			return _connection.Get<ProjectRoleResponse>(roleUri);
		}

		/// <summary>
		/// Returns the URI of the role with the specified roleIdentifier. Will thrown an exception if not found.
		/// </summary>
		public string Find(string projectId, string roleIdentifier)
		{
			var all = All(projectId);
			if (all.Status != HttpStatusCode.OK)
				throw new GoodDataApiException(string.Format("Could not retreived the user roles for the project, '{0}'{1}{2}", projectId, Environment.NewLine, all.Body));

			var found = all.Content.ProjectRoles.Roles
				.Select(x =>
				        new
					        {
						        Uri = x,
						        RoleResponse = Get(x)
					        })
				.First(pair => pair.RoleResponse.Content.ProjectRole.Meta.Identifier == roleIdentifier);

			return found.Uri;
		}


		private static class Urls
		{
			public static string All(string projectId)
			{
				return string.Format(ConfigurationManager.AppSettings.ValueOrDefault("GoodData.Role.AllUrl", @"/gdc/projects/{0}/roles"), projectId);
			}
		}
	}
}
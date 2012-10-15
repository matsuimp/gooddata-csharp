using System.Configuration;
using GoodDataApi.Payload;
using GoodDataApi.Payload.Project;

namespace GoodDataApi.Resources
{
	public interface IProject
	{
		GoodDataResponse<ProjectResponse> Get(string projectId);
		GoodDataResponse<AllProjectsResponse> All();
	}

	internal sealed class Project : IProject
	{
		private readonly IInternalGoodDataConnection _connection;

		public Project(IInternalGoodDataConnection connection)
		{
			_connection = connection;
		}

		public GoodDataResponse<ProjectResponse> Get(string projectId)
		{
			return _connection.Get<ProjectResponse>(Urls.GetProject(projectId));
		}

		public GoodDataResponse<AllProjectsResponse> All()
		{
			return _connection.Get<AllProjectsResponse>(Urls.AllProjects(_connection.ProfileId));
		}


		private static class Urls
		{
			public static string GetProject(string projectId)
			{
				return string.Format(ConfigurationManager.AppSettings.ValueOrDefault("GoodData.ProjectUrl", "/gdc/projects/{0}"), projectId);
			}

			public static string AllProjects(string profileId)
			{
				return string.Format(ConfigurationManager.AppSettings.ValueOrDefault("GoodData.AllProjectsUrl", "/gdc/account/profile/{0}/projects"), profileId);
			}
		}
	}
}
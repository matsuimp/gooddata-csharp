using GoodDataApi.Payload;
using GoodDataApi.Payload.User;

namespace GoodDataApi.Resources
{
	public interface IGoodDataProjectUser
	{
		GoodDataResponse<ProjectUsersResponse> GetUsers(string projectId);
		GoodDataResponse<ProjectUserResponse> Create(ProjectUserRequest request, string projectId);
		GoodDataResponse<ProjectUserPayload> Get(string projectId, string profileId);
		GoodDataResponse<ProjectUserResponse> Update(string projectId, ProjectUserRequest request);
	}

	internal sealed class ProjectUser : IGoodDataProjectUser
	{
		private readonly IGoodDataConnection _connection;

		public ProjectUser(IGoodDataConnection connection)
		{
			_connection = connection;
		}

		public GoodDataResponse<ProjectUsersResponse> GetUsers(string projectId)
		{
			return _connection.Get<ProjectUsersResponse>(UserUrls.ProjectUsers(projectId), converters: new EnabledDisabledBoolConvert());
		}

		public GoodDataResponse<ProjectUserResponse> Create(ProjectUserRequest request, string projectId)
		{
			return _connection.Post<ProjectUserResponse>(UserUrls.ProjectUsers(projectId), request, converters: new EnabledDisabledBoolConvert());
		}

		public GoodDataResponse<ProjectUserPayload> Get(string projectId, string profileId)
		{
			return _connection.Get<ProjectUserPayload>(UserUrls.SpecificProjectUser(projectId, profileId), converters: new EnabledDisabledBoolConvert());
		}

		public GoodDataResponse<ProjectUserResponse> Update(string projectId, ProjectUserRequest request)
		{
			return _connection.Post<ProjectUserResponse>(UserUrls.ProjectUsers(projectId), request, converters: new EnabledDisabledBoolConvert());
		}
	}
}
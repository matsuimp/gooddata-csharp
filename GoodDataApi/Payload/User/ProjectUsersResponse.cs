using System;
using System.Collections.Generic;

namespace GoodDataApi.Payload.User
{
	public class ProjectUsersResponse
	{
		public ProjectUserPayload[] Users;
	}

	public class ProjectUserPayload
	{
		public ProjectUser User;
	}

	public class ProjectUser
	{
		public ProjectUserContent Content;
		public ProjectUserLinks Links;
		public ProjectUserMeta Meta;
	}

	public class ProjectUserContent
	{
		public string Email;
		public string FirstName;
		public List<string> UserRoles;
		public string PhoneNumber;
		/// <summary>
		/// True for enabled in the project, false for disabled
		/// </summary>
		public bool Status;
		public string LastName;
		public string Login;
	}

	public class ProjectUserLinks
	{
		public string Roles;
		public string Permissions;
		public string Groups;
		public string Self;
		public string Invitations;
		public string Projects;
		public string ProjectRelUri;
	}

	public class ProjectUserMeta
	{
		public DateTime Created;
		public DateTime Updated;
		public string Author;
		public string Title;
		public string Contributor;
	}
}
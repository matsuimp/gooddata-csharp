using System;

namespace GoodDataApi.Payload.Project
{
	public class ProjectResponse
	{
		public Project Project;
	}

	public class AllProjectsResponse
	{
		public ProjectContainer[] Projects;
	}

	public class ProjectContainer
	{
		public Project Project;
	}

	public class Project
	{
		public ProjectContent Content;
		public ProjectLinks Links;
		public ProjectMetadata Meta;
	}

	public class ProjectContent
	{
		public bool GuidedNavigation;
		public bool IsPublic;
		public string State;
	}

	public class ProjectLinks
	{
		public string Roles;
		public string Ldm_Thumbnail;
		public string Connectors;
		public string Self;
		public string Invitations;
		public string Users;
		public string Ldm;
		public string PublicArtifacts;
		public string Metadata;
		public string Templates;
	}

	public class ProjectMetadata
	{
		public DateTime Created;
		public string Summary;
		public DateTime Updated;
		public string Author;
		public string Title;
		public string Contributor;
	}
}
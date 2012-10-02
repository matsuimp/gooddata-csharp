using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoodDataApi.Payload.Role
{
	public class ProjectRolesLinks
	{
		public string Project;
	}

	public class AllProjectRoles
	{
		public string[] Roles;
		public ProjectRolesLinks ProjectRolesLinks;
	}

	public class AllProjectRolesResponse
	{
		public AllProjectRoles ProjectRoles;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoodDataApi.Payload.User
{
	public class ProjectUserResponse
	{
		public ProjectUsersUpdateResult ProjectUsersUpdateResult;
	}

	public class ProjectUsersUpdateResult
	{
		public string[] Successful;
		public string[] Failed;
	}
}

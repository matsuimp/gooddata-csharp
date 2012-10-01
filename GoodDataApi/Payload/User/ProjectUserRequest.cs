using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoodDataApi.Payload.User
{
	public class ProjectUserRequest
	{
		public CreateProjectUser User;

		public ProjectUserRequest()
		{
			
		}

		public ProjectUserRequest(string profileUri, bool enabled, string role)
		{
			User = new CreateProjectUser
				       {
					       Content = new CreateProjectUserContent {Status = enabled, UserRoles = new List<string>{role}},
					       Links = new CreateProjectUserLinks {Self = profileUri}
				       };
		}

        public ProjectUserRequest(string profileUri, bool enabled, List<string> roles)
        {
            User = new CreateProjectUser
            {
                Content = new CreateProjectUserContent { Status = enabled, UserRoles = roles },
                Links = new CreateProjectUserLinks { Self = profileUri }
            };
        }
	}

	public class CreateProjectUser
	{
		public CreateProjectUserContent Content;
		public CreateProjectUserLinks Links;
	}

	public class CreateProjectUserContent
	{
		public bool Status;
		public List<string> UserRoles;
	}

	public class CreateProjectUserLinks
	{
		public string Self;
	}
}

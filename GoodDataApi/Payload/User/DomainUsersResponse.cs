using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoodDataApi.Payload.User
{
	public class DomainUsersResponse
	{
		public DomainUsersResponseSettings AccountSettings;
	}

	public class DomainUsersResponseSettings
	{
		public DomainUsersResponsePaging Paging;
		public DomainUserAccountSettingsContainer[] Items;
	}

	public class DomainUsersResponsePaging
	{
		public int Offset;
		public int Count;
		public string Next;
	}

	public class DomainUserAccountSettingsContainer
	{
		public DomainUserAccountSettings AccountSetting;
	}

	public class DomainUserAccountSettings
	{
		public string CompanyName;
		public string Country;
		public DateTime Created;
		public string FirstName;
		public string LastName;
		public string Login;
		public string PhoneNumber;
		public string Position;
		public string TimeZone;
		public DateTime Updated;
		public DomainUserReponseLinks Links;
		public string Email;
	}

	public class DomainUserReponseLinks
	{
		public string Projects;
		public string Self;
	}

}

using System;

namespace GoodDataApi.Payload.User
{
	public class DomainUserQuery
	{
	}

	public class AccountSettings
	{
		public Paging Paging;
		public AccountSetting[] Items;
	}

	public class Paging
	{
		public int Offset;
		public int Count;
	}

	public class AccountSetting
	{
		public string CompanyName;
		public string Country;
		public DateTime Created;
		public string FirstName;
		public string LastName;
		public string Login;
		public string PhoneNumber;
		public string Position;
		public string Settings;
		public string TimeZone;
		public DateTime Updated;
		public Links Links;
	}

	public class Links
	{
		public string[] Projects;
		public string Self;
	}
}
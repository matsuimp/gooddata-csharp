namespace GoodDataApi.Payload.User
{
	public class CreateDomainUserResponse
	{
		public string Uri;
	}

	public class CreateDomainUserRequest
	{
		public CreateDomainUserAcccountSetting AccountSetting;

		public CreateDomainUserRequest()
		{
		}

		public CreateDomainUserRequest(string email, string password, string firstName, string lastName, string ssoProvider = null)
		{
			AccountSetting = new CreateDomainUserAcccountSetting
								 {
									 Login = email,
									 Email = email,
									 Password = password,
									 VerifyPassword = password,
									 FirstName = firstName,
									 LastName = lastName,
									 SsoProvider = ssoProvider,
								 };
		}
	}

	public class CreateDomainUserAcccountSetting
	{
		public string Login;
		public string Password;
		public string Email;
		public string VerifyPassword;
		public string FirstName;
		public string LastName;
		public string SsoProvider;
	}
}
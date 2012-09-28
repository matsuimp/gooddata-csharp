namespace GoodDataApi.Payload.User
{
	public class UpdateProjectUserAccountSettings
	{
		public string FirstName;
		public string LastName;
		public string SsoProvider;
		public string Password;
		public string VerifyPassword;
	}

	public class UpdateUserRequest
	{
		public UpdateProjectUserAccountSettings AccountSetting;
	}
}
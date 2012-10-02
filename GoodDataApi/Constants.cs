using System.Collections.Generic;

namespace GoodDataApi
{
	public static class Constants
	{
		public static class RolesIdentifiers
		{
			public const string Admin = "adminRole";
			public const string Editor = "editorRole";
			public const string DashboardOnly = "dashboardOnlyRole";
			public const string UnverifiedAdmin = "unverifiedAdminRole";
			public const string ReadOnly = "readOnlyUserRole";

			public static IEnumerable<string> All()
			{
				return new[]
					       {
						       Admin, Editor, DashboardOnly, UnverifiedAdmin, ReadOnly
					       };
			}
		}
	}
}
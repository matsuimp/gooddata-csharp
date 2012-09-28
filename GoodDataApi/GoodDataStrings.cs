using System;

namespace GoodDataApi
{
	public static class GoodDataStrings
	{
		public static string IdFromUri(string uri)
		{
			var parts = uri.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
			return parts[parts.Length - 1];
		}
	}
}
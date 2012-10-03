using System;
using System.Collections.Specialized;
using GoodDataService.Configuration;
using log4net;

namespace GoodDataApi
{
	internal class AppConfig
	{
		public static AppConfig Instance = new AppConfig();
		private static readonly ILog Logger = LogManager.GetLogger(typeof (AppConfig));

		// hopefully temporary until all the functionality in GoodDataService is migrated over to GoodDataApi
		public AppConfig()
		{
			try
			{
				var config = GoodDataConfigurationSection.GetConfig();
				Login = config.Login;
				Password = config.Password;
				DomainName = config.Domain;
			}
			catch (Exception e)
			{
				Logger.Error(e);
				Login = null;
				Password = null;
				DomainName = null;
			}
		}

		public string Login { get; private set; }
		public string Password { get; private set; }
		public string DomainName { get; private set; }
	}

	internal static class ConfigurationManagerExtensions
	{
		public static string ValueOrDefault(this NameValueCollection config, string key, string defaultValue = default(string))
		{
			var values = config.GetValues(key);

			if (null == values || values.Length == 0)
				return defaultValue;

			return values[0];
		}
	}
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;

namespace GoodDataApi
{
	internal class AppConfig : ConfigurationSection
	{
		public static AppConfig Instance
		{
			get { return ConfigurationManager.GetSection("gooddata") as AppConfig; }
		}

		[ConfigurationProperty("login", IsRequired = true)]
		public virtual string Login
		{
			get { return (string)this["login"]; }
			set { this["login"] = value; }
		}

		[ConfigurationProperty("password", IsRequired = true)]
		public virtual string Password
		{
			get { return (string)this["password"]; }
			set { this["password"] = value; }
		}
	}

	internal static class ConfigurationManagerExtensions
	{
		public static string ValueOrDefault(this NameValueCollection config, string key, string defaultValue=default(string))
		{
			var values = config.GetValues(key);

			if (null == values || values.Length == 0)
				return defaultValue;

			return values[0];
		}
	}
}

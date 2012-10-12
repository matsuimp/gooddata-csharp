using System;
using System.Diagnostics;
using System.Web;
using GoodDataService.Configuration;
using Newtonsoft.Json;

namespace GoodDataService.SSO
{
	public class SsoProvider
	{
		private static readonly object Locker = new object();
		private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		private const int ThirtySixHours = (60*36);

		public SsoProvider()
		{
			Config = GoodDataConfigurationSection.GetConfig();
		}

		public GoodDataConfigurationSection Config { get; set; }

		public string GenerateToken(string email, int validaityOffsetInMinutes = 10)
		{
			var userData = CreateUserData(email, validaityOffsetInMinutes);
			Trace.Write(userData);

			lock (Locker)
	        {
	        	var gpg = new GnuPgpProcessor();
	        	var signedData = gpg.Sign(Config.Passphrase, userData);
	        	Trace.Write(signedData);
	        	var encryptedData = gpg.Encrypt(Config.Recipient, signedData);
	        	Trace.Write(encryptedData);
	        	return EncodeUserData(encryptedData);
	        }
		}


		private static string CreateUserData(string email, int validityOffsetInMinutes = 10)
		{
			if (validityOffsetInMinutes > ThirtySixHours)
				throw new InvalidOperationException("GoodData does not support sessions longer than 36 hours. See, http://developer.gooddata.com/core-concepts/integrations-sso-user-provisioning/single-sign-on");

			var validity = Convert.ToInt32((DateTime.UtcNow.AddMinutes(validityOffsetInMinutes) - UnixEpoch).TotalSeconds);
			return JsonConvert.SerializeObject(new {email, validity});
		}

		private static string EncodeUserData(string input)
		{
			return HttpUtility.UrlEncode(input);
		}
	}
}
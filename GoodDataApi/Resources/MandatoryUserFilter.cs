using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Xml.XPath;
using GoodDataApi.Payload;
using GoodDataApi.Payload.Filters;
using Newtonsoft.Json;

namespace GoodDataApi.Resources
{
	public interface IMandatoryUserFilter 
	{
        GoodDataResponse<FilterQueryContainer> All(string projectId);
        
	}

	internal sealed class MandatoryUserFilter : IMandatoryUserFilter
	{
		private readonly IGoodDataConnection _connection;

		public MandatoryUserFilter(IGoodDataConnection connection)
		{
			_connection = connection;
		}

        public GoodDataResponse<FilterQueryContainer> All(string projectId)
        {
            var uri = Urls.All(projectId);
            return _connection.Get(uri, transform: content => JsonConvert.DeserializeObject<FilterQueryContainer>(content, new BoolConverter()));
        }


        private static class Urls
        {
            public static string All(string projectId)
            {
                return string.Format(ConfigurationManager.AppSettings.ValueOrDefault("GoodData.MandatoryUserFilter.All", "/gdc/md/{0}/query/userfilters"), projectId);
            }
        }
	}
}
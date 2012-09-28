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

	internal class MandatoryUserFilter : IMandatoryUserFilter
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

        private static IEnumerable<BasicFilterInfo> ParseFilters(string html, string projectId)
        {
            var doc = XElement.Parse(html);
            var namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaceManager.AddNamespace("GC", Urls.HtmlNameSpace);
            var expression = string.Format(".//GC:a[starts-with(@href, '{0}')]", Urls.AllXPathPattern(projectId));

            var elements =  doc.XPathSelectElements(expression, namespaceManager);
            return elements.Select(element => new BasicFilterInfo {Name = element.Value, Uri = element.Attribute(XName.Get("href")).Value});
        }



        private static class Urls
        {
            public static string All(string projectId)
            {
                return string.Format(ConfigurationManager.AppSettings.ValueOrDefault("GoodData.MandatoryUserFilter.All", "/gdc/md/{0}/query/userfilters"), projectId);
            }

            public static string AllXPathPattern(string projectId)
            {
                return string.Format(ConfigurationManager.AppSettings.ValueOrDefault("GoodData.MandatoryUserFilter.AllXPathPattern", "/gdc/md/{0}/obj/"), projectId);
            }

            public static string HtmlNameSpace
            {
                get { return ConfigurationManager.AppSettings.ValueOrDefault("GoodDataApi.MandatoryUserFilter.AllHtmlNamespace", "http://www.w3.org/1999/xhtml"); }
            }
        }
	}
}
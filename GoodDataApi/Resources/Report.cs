using System.Configuration;
using GoodDataApi.Payload;
using GoodDataApi.Payload.Report;

namespace GoodDataApi.Resources
{
	public interface IGoodDataReport
	{
		GoodDataResponse<GetReportsResponse> GetAll(string projectId);
		GoodDataResponse<ExecuteReportResponse> Execute(ExecuteReportRequest request);
		GoodDataResponse<ExportReportResponse> Export(ExportReportRequest request);
		GoodDataResponse<GetReportResponse> Get(string reportUri);
	}

	internal class Report : IGoodDataReport
	{
		private readonly IInternalGoodDataConnection _connection;

		public Report(IInternalGoodDataConnection connection)
		{
			_connection = connection;
		}

		public GoodDataResponse<GetReportsResponse> GetAll(string projectId)
		{
			return _connection.Get<GetReportsResponse>(Urls.All(projectId));
		}

		public GoodDataResponse<GetReportResponse> Get(string reportUri)
		{
			return _connection.Get<GetReportResponse>(reportUri);
		}

		public GoodDataResponse<ExecuteReportResponse> Execute(ExecuteReportRequest request)
		{
			return _connection.Post<ExecuteReportResponse>(Urls.Execute, request);
		}

		public GoodDataResponse<ExportReportResponse> Export(ExportReportRequest request)
		{
			return _connection.Post<ExportReportResponse>(Urls.Export, request);
		}

		/// <summary>
		/// internal bc i'm still figuring out how i want to expose this w/ polling to see if the report is exported, etc.
		/// </summary>
		internal void Download(string exportUri, string targetPath)
		{
			_connection.Download(exportUri, targetPath);
		}

		private static class Urls
		{
			public static string All(string projectId)
			{
				return string.Format(ConfigurationManager.AppSettings.ValueOrDefault("GoodData.Report.GetAll", "/gdc/md/{0}/query/reports"), projectId);
			}

			public static string Execute { get { return ConfigurationManager.AppSettings.ValueOrDefault("GoodData.Report.Execute", "/gdc/xtab2/executor3"); } }
			public static string Export { get { return ConfigurationManager.AppSettings.ValueOrDefault("GoodData.Report.Export", "/gdc/exporter/executor"); } }
		}
	}
}
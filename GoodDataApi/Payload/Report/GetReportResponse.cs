namespace GoodDataApi.Payload.Report
{
	public class ReportContent
	{
		public string DefaultReportDefinition;
		public string[] Domains;
		public object[] Results;
	}

	public class ReportMetadata
	{
		public string Author;
		public string Uri;
		public string Tags;
		public string Created;
		public string Identifier;
		public string Deprecated;
		public string Summary;
		public string Title;
		public string Category;
		public string Updated;
		public string Contributor;
	}

	public class ReportInfo
	{
		public ReportContent Content;
		public ReportMetadata Meta;
	}

	public class GetReportResponse
	{
		public ReportInfo Report;
	}
}
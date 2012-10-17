namespace GoodDataApi.Payload.Report
{
	public class ExportReportInfo
	{
		public string Report;
		public string Format;
	}

	public class ExportReportRequest
	{
		public ExportReportInfo result_req;

		public ExportReportRequest()
		{
			
		}

		public ExportReportRequest(string reportUri, string format="csv")
		{
			result_req = new ExportReportInfo {Format = format, Report = reportUri};
		}
	}
}
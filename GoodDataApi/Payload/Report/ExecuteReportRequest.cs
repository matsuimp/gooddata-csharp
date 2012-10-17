namespace GoodDataApi.Payload.Report
{
	public class ReportToExport
	{
		/// <summary>
		/// the uri for the report
		/// </summary>
		public string ReportDefinition;
	}

	public class ExecuteReportRequest
	{
		public ReportToExport report_req;

		public ExecuteReportRequest()
		{
			
		}

		public ExecuteReportRequest(string reportUri)
		{
			report_req = new ReportToExport { ReportDefinition = reportUri };
		}
	}
}
namespace GoodDataApi.Payload.Report
{
	public class ReportResponse
	{
		public string Link ;
		public string Author ;
		public string Tags ;
		public string Created ;
		public string Deprecated ;
		public string Summary ;
		public string Title ;
		public string Category ;
		public string Updated ;
		public string Contributor ;
	}

	public class ReportsMetadata
	{
		public string Summary ;
		public string Title ;
		public string Category ;
	}

	public class ReportsResponse
	{
		public ReportResponse[] Entries ;
		public ReportsMetadata Meta ;
	}

	public class GetReportsResponse
	{
		public ReportsResponse Query ;
	}
}
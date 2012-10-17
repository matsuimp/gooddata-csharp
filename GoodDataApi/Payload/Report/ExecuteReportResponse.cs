using System;

namespace GoodDataApi.Payload.Report
{
	public class ReportResultAttributeHeaderLocator
	{
		public string Uri;
	}

	public class ReportResultLocator
	{
		public ReportResultAttributeHeaderLocator AttributeHeaderLocator;
	}

	public class ReportResultColumnWidth
	{
		public int Width;
		public ReportResultLocator[] Locator;
	}

	public class ReportResultRow
	{
		public string DisplayFormId;
		public string AttributeTitle;
		public string BaseElementUri;
		public string Title;
		public string AttributeId;
		public string[][] Totals;
		public string DrillDownStepAttributeDf;
		public string Sort;
	}

	public class ReportResultSort
	{
		public string[] Columns;
		public string[] Rows;
	}

	public class ReportResultLabels
	{
	}

	public class ReportResultOneNumber
	{
		public ReportResultLabels Labels;
	}

	public class ReportMetric
	{
		public string Format;
		public string Title;
		public string MetricId;
	}

	public class ReportResultView
	{
		public string ReportName;
		public ReportResultColumnWidth[] ColumnWidths;
		public string[] Filters;
		public ReportResultRow[] Rows;
		public ReportResultSort Sort;
		public ReportResultOneNumber OneNumber;
		public string Format;
		public string[] Columns;
		public ReportMetric[] Metrics;
	}

	public class ReportResultContent
	{
		public string ReportDefinition;
		public ReportResultView ReportView;
		public string ParentReport;
		public string DataResult;
	}

	public class ReportResultMetadata
	{
		public string Author;
		public string Uri;
		public string Tags;
		public DateTime Created;
		public string Identifier;
		public bool Deprecated;
		public string Summary;
		public string Title;
		public string Category;
		public DateTime Updated;
		public string Contributor;
	}

	public class ReportResult
	{
		public ReportResultContent Content;
		public ReportResultMetadata Meta;
	}

	public class ExecuteReportResponse
	{
		public ReportResult ReportResult2;
	}
}
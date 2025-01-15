using System;
using System.Globalization;
using System.IO;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V1
{
	// Token: 0x02000FB0 RID: 4016
	internal class AdobeAnalyticsRequestV1 : AdobeAnalyticsRequestBase
	{
		// Token: 0x0600699C RID: 27036 RVA: 0x0016B209 File Offset: 0x00169409
		private AdobeAnalyticsRequestV1(string queryName, string companyName)
			: base("1.4", companyName, "https://api.omniture.com", "admin/1.4/rest/", string.Format(CultureInfo.InvariantCulture, "method={0}", queryName))
		{
			this.QueryName = queryName;
			this.companyName = companyName;
		}

		// Token: 0x17001E5E RID: 7774
		// (get) Token: 0x0600699D RID: 27037 RVA: 0x0016B23F File Offset: 0x0016943F
		// (set) Token: 0x0600699E RID: 27038 RVA: 0x0016B247 File Offset: 0x00169447
		public string QueryName { get; private set; }

		// Token: 0x0600699F RID: 27039 RVA: 0x00019E61 File Offset: 0x00018061
		protected override Value CreateContent()
		{
			return RecordValue.Empty;
		}

		// Token: 0x060069A0 RID: 27040 RVA: 0x0016B250 File Offset: 0x00169450
		protected override RecordValue CreateHeaders()
		{
			if (!string.IsNullOrEmpty(this.companyName))
			{
				return RecordValue.New(AdobeAnalyticsRequestV1.HeaderKeys, new Value[] { TextValue.New(this.companyName) });
			}
			return RecordValue.Empty;
		}

		// Token: 0x060069A1 RID: 27041 RVA: 0x0016B283 File Offset: 0x00169483
		public static AdobeAnalyticsRequestBase NewEmptyRequest(string queryName, string companyName)
		{
			return new AdobeAnalyticsRequestV1(queryName, companyName);
		}

		// Token: 0x060069A2 RID: 27042 RVA: 0x0016B28C File Offset: 0x0016948C
		public static AdobeAnalyticsRequestBase NewMetadataRequest(string queryName, string reportSuiteId, string companyName)
		{
			return new AdobeAnalyticsRequestV1.AdobeAnalyticsMetadataRequest(queryName, companyName, reportSuiteId);
		}

		// Token: 0x060069A3 RID: 27043 RVA: 0x0016B296 File Offset: 0x00169496
		public static AdobeAnalyticsRequestBase NewSegmentRequest(string reportSuiteId, string companyName)
		{
			return new AdobeAnalyticsRequestV1.AdobeAnalyticsSegmentRequest(companyName);
		}

		// Token: 0x060069A4 RID: 27044 RVA: 0x0016B29E File Offset: 0x0016949E
		public static AdobeAnalyticsRequestBase NewReportRequest(AdobeAnalyticsReportDescriptionV1 description, string companyName)
		{
			return new AdobeAnalyticsRequestV1.AdobeAnalyticsReportRequest(description, companyName);
		}

		// Token: 0x060069A5 RID: 27045 RVA: 0x0016B2A7 File Offset: 0x001694A7
		public static AdobeAnalyticsRequestBase NewReportRequest(TextValue nativeQuery, string companyName)
		{
			return new AdobeAnalyticsRequestV1.AdobeAnalyticsNativeQueryRequest(nativeQuery, companyName);
		}

		// Token: 0x060069A6 RID: 27046 RVA: 0x0016B2B0 File Offset: 0x001694B0
		public static AdobeAnalyticsRequestBase NewPollingRequest(Value pollingId, string companyName)
		{
			return new AdobeAnalyticsRequestV1.AdobeAnalyticsPollingRequest(pollingId, companyName);
		}

		// Token: 0x04003A6A RID: 14954
		public const string QueueRequestQuery = "Report.Queue";

		// Token: 0x04003A6B RID: 14955
		public const string GetRequestQuery = "Report.Get";

		// Token: 0x04003A6C RID: 14956
		public const string GetSegmentsQuery = "Segments.Get";

		// Token: 0x04003A6D RID: 14957
		private static readonly Keys ReportSuiteIdKey = Keys.New("reportSuiteID");

		// Token: 0x04003A6E RID: 14958
		private static readonly Keys SegmentsKey = Keys.New("accessLevel");

		// Token: 0x04003A6F RID: 14959
		private static readonly Keys HeaderKeys = Keys.New("X-ADOBE-DMA-COMPANY");

		// Token: 0x04003A70 RID: 14960
		private readonly string companyName;

		// Token: 0x02000FB1 RID: 4017
		private class AdobeAnalyticsMetadataRequest : AdobeAnalyticsRequestV1
		{
			// Token: 0x060069A8 RID: 27048 RVA: 0x0016B2E8 File Offset: 0x001694E8
			public AdobeAnalyticsMetadataRequest(string queryName, string companyName, string reportSuiteId)
				: base(queryName, companyName)
			{
				this.reportSuiteId = reportSuiteId;
			}

			// Token: 0x060069A9 RID: 27049 RVA: 0x0016B2F9 File Offset: 0x001694F9
			protected override Value CreateContent()
			{
				return RecordValue.New(AdobeAnalyticsRequestV1.ReportSuiteIdKey, new Value[] { TextValue.New(this.reportSuiteId) });
			}

			// Token: 0x04003A72 RID: 14962
			private readonly string reportSuiteId;
		}

		// Token: 0x02000FB2 RID: 4018
		private class AdobeAnalyticsSegmentRequest : AdobeAnalyticsRequestV1
		{
			// Token: 0x060069AA RID: 27050 RVA: 0x0016B319 File Offset: 0x00169519
			public AdobeAnalyticsSegmentRequest(string companyName)
				: base("Segments.Get", companyName)
			{
			}

			// Token: 0x060069AB RID: 27051 RVA: 0x0016B327 File Offset: 0x00169527
			protected override Value CreateContent()
			{
				return RecordValue.New(AdobeAnalyticsRequestV1.SegmentsKey, new Value[] { TextValue.New("shared") });
			}
		}

		// Token: 0x02000FB3 RID: 4019
		private class AdobeAnalyticsReportRequest : AdobeAnalyticsRequestV1
		{
			// Token: 0x060069AC RID: 27052 RVA: 0x0016B346 File Offset: 0x00169546
			public AdobeAnalyticsReportRequest(AdobeAnalyticsReportDescriptionV1 description, string companyName)
				: base("Report.Queue", companyName)
			{
				this.description = description;
			}

			// Token: 0x060069AD RID: 27053 RVA: 0x0016B35B File Offset: 0x0016955B
			protected override Value CreateContent()
			{
				return this.description.Value;
			}

			// Token: 0x04003A73 RID: 14963
			private readonly AdobeAnalyticsReportDescriptionV1 description;
		}

		// Token: 0x02000FB4 RID: 4020
		private class AdobeAnalyticsNativeQueryRequest : AdobeAnalyticsRequestV1
		{
			// Token: 0x060069AE RID: 27054 RVA: 0x0016B368 File Offset: 0x00169568
			public AdobeAnalyticsNativeQueryRequest(TextValue nativeQuery, string companyName)
				: base("Report.Queue", companyName)
			{
				this.nativeQuery = nativeQuery;
			}

			// Token: 0x060069AF RID: 27055 RVA: 0x0016B380 File Offset: 0x00169580
			protected override Value CreateContent()
			{
				Value value;
				using (TextReader textReader = new StringReader(this.nativeQuery.AsString))
				{
					value = JsonParser.Parse(textReader, null);
				}
				return value;
			}

			// Token: 0x04003A74 RID: 14964
			private readonly TextValue nativeQuery;
		}

		// Token: 0x02000FB5 RID: 4021
		private class AdobeAnalyticsPollingRequest : AdobeAnalyticsRequestV1
		{
			// Token: 0x060069B0 RID: 27056 RVA: 0x0016B3C4 File Offset: 0x001695C4
			public AdobeAnalyticsPollingRequest(Value pollingId, string companyName)
				: base("Report.Get", companyName)
			{
				this.pollingId = pollingId;
			}

			// Token: 0x060069B1 RID: 27057 RVA: 0x0016B3D9 File Offset: 0x001695D9
			protected override Value CreateContent()
			{
				return this.pollingId;
			}

			// Token: 0x04003A75 RID: 14965
			private readonly Value pollingId;
		}
	}
}

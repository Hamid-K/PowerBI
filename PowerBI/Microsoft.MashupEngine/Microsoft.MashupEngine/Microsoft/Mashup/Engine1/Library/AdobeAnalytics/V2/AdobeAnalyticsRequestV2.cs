using System;
using System.Globalization;
using System.IO;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V2
{
	// Token: 0x02000F9A RID: 3994
	internal class AdobeAnalyticsRequestV2 : AdobeAnalyticsRequestBase
	{
		// Token: 0x06006930 RID: 26928 RVA: 0x0016966C File Offset: 0x0016786C
		protected override RecordValue CreateHeaders()
		{
			return RecordValue.New(Keys.New("x-proxy-global-company-id", "x-api-key", "content-type"), new Value[]
			{
				TextValue.New(this.company),
				TextValue.New(this.clientId),
				TextValue.New("application/json")
			});
		}

		// Token: 0x06006931 RID: 26929 RVA: 0x001696C1 File Offset: 0x001678C1
		private AdobeAnalyticsRequestV2(string clientId, string route, string companyId, string query = null)
			: base("2.0", companyId, string.Format(CultureInfo.InvariantCulture, "https://analytics.adobe.io/api/{0}", companyId), route, query)
		{
			this.clientId = clientId;
		}

		// Token: 0x06006932 RID: 26930 RVA: 0x000020FA File Offset: 0x000002FA
		protected override Value CreateContent()
		{
			return null;
		}

		// Token: 0x06006933 RID: 26931 RVA: 0x001696E9 File Offset: 0x001678E9
		public static AdobeAnalyticsRequestV2 NewCollectionsRequest(string clientId, string companyId)
		{
			return new AdobeAnalyticsRequestV2.AdobeAnalyticsCollectionsRequest(clientId, companyId);
		}

		// Token: 0x06006934 RID: 26932 RVA: 0x001696F2 File Offset: 0x001678F2
		public static AdobeAnalyticsRequestV2 NewMetadataRequest(string clientId, string queryName, string reportSuiteId, string companyId)
		{
			return new AdobeAnalyticsRequestV2.AdobeAnalyticsMetadataRequest(clientId, queryName, companyId, reportSuiteId);
		}

		// Token: 0x06006935 RID: 26933 RVA: 0x001696FD File Offset: 0x001678FD
		public static AdobeAnalyticsRequestV2 NewCalculatedMetricsRequest(string clientId, string reportSuiteId, string companyId)
		{
			return new AdobeAnalyticsRequestV2.AdobeAnalyticsCalculatedMetricsRequest(clientId, companyId, reportSuiteId);
		}

		// Token: 0x06006936 RID: 26934 RVA: 0x00169707 File Offset: 0x00167907
		public static AdobeAnalyticsRequestV2 NewSegmentRequest(string clientId, string reportSuiteId, string companyId, int page)
		{
			return new AdobeAnalyticsRequestV2.AdobeAnalyticsSegmentRequest(clientId, companyId, reportSuiteId, page);
		}

		// Token: 0x06006937 RID: 26935 RVA: 0x00169712 File Offset: 0x00167912
		public static AdobeAnalyticsRequestV2 NewReportRequest(string clientId, Value reportValue, string companyId)
		{
			return new AdobeAnalyticsRequestV2.AdobeAnalyticsReportRequest(clientId, reportValue, companyId);
		}

		// Token: 0x06006938 RID: 26936 RVA: 0x0016971C File Offset: 0x0016791C
		public static AdobeAnalyticsRequestV2 NewReportRequest(string clientId, TextValue nativeQuery, string companyId)
		{
			return new AdobeAnalyticsRequestV2.AdobeAnalyticsNativeQueryRequest(clientId, nativeQuery, companyId);
		}

		// Token: 0x06006939 RID: 26937 RVA: 0x00169726 File Offset: 0x00167926
		public static AdobeAnalyticsRequestV2 NewTopItemsRequest(string clientId, string companyId, string rsid, string dimension, int count = 10, int page = 0)
		{
			return new AdobeAnalyticsRequestV2.AdobeAnalyticsTopItemsRequest(clientId, companyId, rsid, dimension, count, page);
		}

		// Token: 0x04003A1D RID: 14877
		public const string ReportRoute = "reports";

		// Token: 0x04003A1E RID: 14878
		public const string GetSegmentsRoute = "segments";

		// Token: 0x04003A1F RID: 14879
		private const string GlobalCompanyIdHeaderKey = "x-proxy-global-company-id";

		// Token: 0x04003A20 RID: 14880
		private const string ApiHeaderKey = "x-api-key";

		// Token: 0x04003A21 RID: 14881
		private const string ContentTypeHeaderKey = "content-type";

		// Token: 0x04003A22 RID: 14882
		private const string ContentType = "application/json";

		// Token: 0x04003A23 RID: 14883
		private readonly string clientId;

		// Token: 0x02000F9B RID: 3995
		private class AdobeAnalyticsCollectionsRequest : AdobeAnalyticsRequestV2
		{
			// Token: 0x0600693A RID: 26938 RVA: 0x00169735 File Offset: 0x00167935
			public AdobeAnalyticsCollectionsRequest(string clientId, string companyId)
				: base(clientId, "collections/suites", companyId, "limit=0")
			{
			}
		}

		// Token: 0x02000F9C RID: 3996
		private class AdobeAnalyticsMetadataRequest : AdobeAnalyticsRequestV2
		{
			// Token: 0x0600693B RID: 26939 RVA: 0x00169749 File Offset: 0x00167949
			public AdobeAnalyticsMetadataRequest(string clientId, string route, string companyId, string reportSuiteId)
				: base(clientId, route, companyId, string.Format(CultureInfo.InvariantCulture, "rsid={0}&expansion=allowedForReporting", reportSuiteId))
			{
			}
		}

		// Token: 0x02000F9D RID: 3997
		private class AdobeAnalyticsCalculatedMetricsRequest : AdobeAnalyticsRequestV2
		{
			// Token: 0x0600693C RID: 26940 RVA: 0x00169765 File Offset: 0x00167965
			public AdobeAnalyticsCalculatedMetricsRequest(string clientId, string companyId, string reportSuiteId)
				: base(clientId, "calculatedmetrics", companyId, string.Format(CultureInfo.InvariantCulture, "toBeUsedInRsid={0}&includeType=shared&limit=0", reportSuiteId))
			{
			}
		}

		// Token: 0x02000F9E RID: 3998
		private class AdobeAnalyticsSegmentRequest : AdobeAnalyticsRequestV2
		{
			// Token: 0x0600693D RID: 26941 RVA: 0x00169784 File Offset: 0x00167984
			public AdobeAnalyticsSegmentRequest(string clientId, string companyId, string reportSuiteId, int page)
				: base(clientId, "segments", companyId, string.Format(CultureInfo.InvariantCulture, "rsid={0}&includeType=shared&page={1}", reportSuiteId, page))
			{
			}
		}

		// Token: 0x02000F9F RID: 3999
		private class AdobeAnalyticsReportRequest : AdobeAnalyticsRequestV2
		{
			// Token: 0x0600693E RID: 26942 RVA: 0x001697AA File Offset: 0x001679AA
			public AdobeAnalyticsReportRequest(string clientId, Value reportValue, string companyId)
				: base(clientId, "reports", companyId, null)
			{
				this.reportValue = reportValue;
			}

			// Token: 0x0600693F RID: 26943 RVA: 0x001697C1 File Offset: 0x001679C1
			protected override Value CreateContent()
			{
				return this.reportValue;
			}

			// Token: 0x04003A24 RID: 14884
			private readonly Value reportValue;
		}

		// Token: 0x02000FA0 RID: 4000
		private class AdobeAnalyticsNativeQueryRequest : AdobeAnalyticsRequestV2
		{
			// Token: 0x06006940 RID: 26944 RVA: 0x001697C9 File Offset: 0x001679C9
			public AdobeAnalyticsNativeQueryRequest(string clientId, TextValue nativeQuery, string companyId)
				: base(clientId, "reports", companyId, null)
			{
				this.nativeQuery = nativeQuery;
			}

			// Token: 0x06006941 RID: 26945 RVA: 0x001697E0 File Offset: 0x001679E0
			protected override Value CreateContent()
			{
				Value value;
				using (TextReader textReader = new StringReader(this.nativeQuery.AsString))
				{
					value = JsonParser.Parse(textReader, null);
				}
				return value;
			}

			// Token: 0x04003A25 RID: 14885
			private readonly TextValue nativeQuery;
		}

		// Token: 0x02000FA1 RID: 4001
		private class AdobeAnalyticsTopItemsRequest : AdobeAnalyticsRequestV2
		{
			// Token: 0x06006942 RID: 26946 RVA: 0x00169824 File Offset: 0x00167A24
			public AdobeAnalyticsTopItemsRequest(string clientId, string companyId, string rsid, string dimension, int count = 10, int page = 0)
				: base(clientId, "reports/topItems", companyId, string.Format(CultureInfo.InvariantCulture, "rsid={rsid}&dimension={0}&limit={1}&page={2}", dimension, count, page))
			{
			}
		}
	}
}

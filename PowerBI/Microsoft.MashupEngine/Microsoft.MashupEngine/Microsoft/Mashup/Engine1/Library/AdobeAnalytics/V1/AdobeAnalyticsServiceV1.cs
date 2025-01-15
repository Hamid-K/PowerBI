using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V1
{
	// Token: 0x02000FB7 RID: 4023
	internal sealed class AdobeAnalyticsServiceV1 : AdobeAnalyticsService
	{
		// Token: 0x060069BF RID: 27071 RVA: 0x00169BD1 File Offset: 0x00167DD1
		public AdobeAnalyticsServiceV1(IEngineHost host, AdobeAnalyticsOptions options)
			: base(host, options)
		{
		}

		// Token: 0x060069C0 RID: 27072 RVA: 0x0016B82C File Offset: 0x00169A2C
		public override IList<AdobeAnalyticsCube> GetCubes(AdobeAnalyticsCompany company)
		{
			ListValue asList = this.ExecuteRequest(AdobeAnalyticsRequestV1.NewEmptyRequest("Company.GetReportSuites", company.Name))["report_suites"].AsList;
			IList<AdobeAnalyticsCube> list = new List<AdobeAnalyticsCube>();
			foreach (IValueReference valueReference in asList)
			{
				Value value = valueReference.Value;
				list.Add(new AdobeAnalyticsCube(this, value["rsid"].AsString, value["site_title"].AsString, company.Name));
			}
			return list;
		}

		// Token: 0x060069C1 RID: 27073 RVA: 0x0016B8D0 File Offset: 0x00169AD0
		public override IList<AdobeAnalyticsDimension> GetDimensions(AdobeAnalyticsCube cube)
		{
			IList<AdobeAnalyticsDimension> list = new List<AdobeAnalyticsDimension>();
			list.AddRange(AdobeAnalyticsDateGranularityHierarchyV1.Hierarchy);
			foreach (IValueReference valueReference in this.ExecuteRequest(AdobeAnalyticsRequestV1.NewMetadataRequest("Report.GetElements", cube.Id, cube.Company)).AsList)
			{
				Value value = valueReference.Value;
				if (value["subrelation"].AsBoolean)
				{
					list.Add(AdobeAnalyticsDimension.New(value["name"].AsString, value["id"].AsString));
				}
			}
			return list;
		}

		// Token: 0x060069C2 RID: 27074 RVA: 0x0016B988 File Offset: 0x00169B88
		public override IList<AdobeAnalyticsMeasure> GetMeasures(AdobeAnalyticsCube cube)
		{
			ListValue asList = this.ExecuteRequest(AdobeAnalyticsRequestV1.NewMetadataRequest("Report.GetMetrics", cube.Id, cube.Company)).AsList;
			IList<AdobeAnalyticsMeasure> list = new List<AdobeAnalyticsMeasure>();
			foreach (IValueReference valueReference in asList)
			{
				Value value = valueReference.Value;
				list.Add(AdobeAnalyticsMeasure.New(value["name"].AsString, value["id"].AsString));
			}
			return list;
		}

		// Token: 0x060069C3 RID: 27075 RVA: 0x0016BA20 File Offset: 0x00169C20
		public Value GetReport(AdobeAnalyticsRequestBase request, string companyName = null)
		{
			Value value = this.ExecuteRequest(request);
			return this.ExecuteRequest(AdobeAnalyticsRequestV1.NewPollingRequest(value, companyName));
		}

		// Token: 0x060069C4 RID: 27076 RVA: 0x0016BA44 File Offset: 0x00169C44
		public override IList<AdobeAnalyticsSegment> GetSegments(AdobeAnalyticsCube cube)
		{
			IList<AdobeAnalyticsSegment> list = new List<AdobeAnalyticsSegment>();
			foreach (IValueReference valueReference in this.ExecuteRequest(AdobeAnalyticsRequestV1.NewSegmentRequest(cube.Id, cube.Company)).AsList)
			{
				list.Add(AdobeAnalyticsSegment.New(valueReference.Value));
			}
			return list;
		}

		// Token: 0x060069C5 RID: 27077 RVA: 0x0016BAB8 File Offset: 0x00169CB8
		protected override Value ExecuteRequest(AdobeAnalyticsRequest request)
		{
			ResourceCredentialCollection credentials = base.GetCredentials();
			Value value = null;
			IPersistentCache persistentCache = this.host.GetPersistentCache();
			Stream stream;
			if (persistentCache.TryGetValue(request.CacheKey, out stream))
			{
				using (TextReader textReader = new StreamReader(stream))
				{
					value = JsonParser.Parse(textReader, null);
				}
				stream.Dispose();
			}
			else
			{
				Request request2 = Request.Create(this.host, "AdobeAnalytics", null, TextValue.New(request.Uri.ToString()), null, request.Content, null, request.Headers, null, null, null, null, null, this.retryPolicy);
				Encoding encoding = Encoding.UTF8;
				string text = null;
				Value value2;
				for (int i = 0; i < this.options.RetryCount + 1; i++)
				{
					try
					{
						Response response2;
						Response response = (response2 = request2.GetResponse(credentials, null, false));
						try
						{
							if (!string.IsNullOrEmpty(response.CharacterSet))
							{
								encoding = Encoding.GetEncoding(response.CharacterSet);
							}
							using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), encoding))
							{
								text = streamReader.ReadToEnd();
								using (TextReader textReader2 = new StringReader(text))
								{
									value = JsonParser.Parse(textReader2, null);
								}
							}
						}
						finally
						{
							if (response2 != null)
							{
								((IDisposable)response2).Dispose();
							}
						}
					}
					catch (ResponseException ex)
					{
						Message2 message = Strings.WebRequestFailed(request2.ResourceKind, ex.InnerException.Message);
						throw DataSourceException.NewDataSourceError<Message2>(this.host, message, AdobeAnalyticsService.Resource, "ErrorUri", TextValue.New(request2.Uri.OriginalString), TypeValue.Text, null);
					}
					if (!value.IsRecord || !value.AsRecord.TryGetValue("error", out value2))
					{
						break;
					}
					if (!(value2.AsString == "report_not_ready"))
					{
						string asString = value["error_description"].AsString;
						Value @null;
						if (!value.AsRecord.TryGetValue("error_uri", out @null))
						{
							@null = Value.Null;
						}
						throw DataSourceException.NewDataSourceError(this.host, asString, AdobeAnalyticsService.Resource, "ErrorUri", @null, NullableTypeValue.Text, null);
					}
					Thread.Sleep(this.options.RetryDelay);
				}
				if (text == null)
				{
					throw DataSourceException.NewDataSourceError<Message0>(this.host, Strings.AdobeReportNotAvailable, AdobeAnalyticsService.Resource, "Request", TextValue.New(encoding.GetString(request.Content.AsBytes)), TypeValue.Text, null);
				}
				if (value.IsRecord && value.TryGetValue("error", out value2))
				{
					throw DataSourceException.NewDataSourceError<Message0>(this.host, Strings.AdobeReportNotReady, AdobeAnalyticsService.Resource, "Request", TextValue.New(encoding.GetString(request.Content.AsBytes)), TypeValue.Text, null);
				}
				Stream stream2 = persistentCache.BeginAdd();
				byte[] bytes = encoding.GetBytes(text);
				stream2.Write(bytes, 0, bytes.Length);
				persistentCache.EndAdd(request.CacheKey, stream2);
			}
			return value;
		}

		// Token: 0x04003A7D RID: 14973
		public const string SiteTitleKey = "site_title";

		// Token: 0x04003A7E RID: 14974
		public const string ReportSuitesKey = "report_suites";

		// Token: 0x04003A7F RID: 14975
		public const string RsidKey = "rsid";
	}
}

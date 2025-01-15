using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V2
{
	// Token: 0x02000FA3 RID: 4003
	internal sealed class AdobeAnalyticsServiceV2 : AdobeAnalyticsService
	{
		// Token: 0x0600694F RID: 26959 RVA: 0x00169BD1 File Offset: 0x00167DD1
		public AdobeAnalyticsServiceV2(IEngineHost host, AdobeAnalyticsOptions options)
			: base(host, options)
		{
		}

		// Token: 0x06006950 RID: 26960 RVA: 0x00169BDC File Offset: 0x00167DDC
		public override IList<AdobeAnalyticsCube> GetCubes(AdobeAnalyticsCompany company)
		{
			if (company.Id == "__defaultCompany")
			{
				IList<AdobeAnalyticsCube> list = new List<AdobeAnalyticsCube>();
				foreach (AdobeAnalyticsCompany adobeAnalyticsCompany in this.GetCompanies())
				{
					list.AddRange(this.GetCubesForCompany(adobeAnalyticsCompany.Id));
				}
				return list;
			}
			return this.GetCubesForCompany(company.Id);
		}

		// Token: 0x06006951 RID: 26961 RVA: 0x00169C5C File Offset: 0x00167E5C
		private IList<AdobeAnalyticsCube> GetCubesForCompany(string companyId)
		{
			ListValue asList = this.ExecuteRequest(AdobeAnalyticsRequestV2.NewCollectionsRequest(base.ClientId, companyId))["content"].AsList;
			IList<AdobeAnalyticsCube> list = new List<AdobeAnalyticsCube>();
			foreach (IValueReference valueReference in asList)
			{
				Value value = valueReference.Value;
				Value value2;
				if (!value.TryGetValue("name", out value2))
				{
					value2 = value["rsid"];
				}
				list.Add(new AdobeAnalyticsCube(this, value["rsid"].AsString, value2.AsString, companyId));
			}
			return list;
		}

		// Token: 0x06006952 RID: 26962 RVA: 0x00169D08 File Offset: 0x00167F08
		public override IList<AdobeAnalyticsDimension> GetDimensions(AdobeAnalyticsCube cube)
		{
			IList<AdobeAnalyticsDimension> list = new List<AdobeAnalyticsDimension>();
			list.Add(AdobeAnalyticsReportDescriptionV2.SegmentDimension);
			list.AddRange(AdobeAnalyticsDateGranularityHierarchyV2.Hierarchy);
			foreach (IValueReference valueReference in this.ExecuteRequest(AdobeAnalyticsRequestV2.NewMetadataRequest(base.ClientId, "dimensions", cube.Id, cube.Company)).AsList)
			{
				Value value = valueReference.Value;
				if (value["allowedForReporting"].AsBoolean && !AdobeAnalyticsDateGranularityHierarchyV2.IsGranularity(value["id"].AsString))
				{
					string asString = value["id"].AsString;
					string asString2 = value["name"].AsString;
					list.Add(AdobeAnalyticsDimension.New(asString2, asString));
				}
			}
			return list;
		}

		// Token: 0x06006953 RID: 26963 RVA: 0x00169DEC File Offset: 0x00167FEC
		public override IList<AdobeAnalyticsMeasure> GetMeasures(AdobeAnalyticsCube cube)
		{
			ListValue asList = this.ExecuteRequest(AdobeAnalyticsRequestV2.NewMetadataRequest(base.ClientId, "metrics", cube.Id, cube.Company)).AsList;
			IList<AdobeAnalyticsMeasure> list = new List<AdobeAnalyticsMeasure>();
			foreach (IValueReference valueReference in asList)
			{
				Value value = valueReference.Value;
				if (value["allowedForReporting"].AsBoolean)
				{
					list.Add(AdobeAnalyticsMeasure.New(value["name"].AsString, value["id"].AsString));
				}
			}
			foreach (IValueReference valueReference2 in this.ExecuteRequest(AdobeAnalyticsRequestV2.NewCalculatedMetricsRequest(base.ClientId, cube.Id, cube.Company)).AsRecord["content"].AsList)
			{
				Value value2 = valueReference2.Value;
				list.Add(AdobeAnalyticsMeasure.New(value2["name"].AsString, value2["id"].AsString));
			}
			return list;
		}

		// Token: 0x06006954 RID: 26964 RVA: 0x00169F2C File Offset: 0x0016812C
		public Value GetReport(AdobeAnalyticsReportDescriptionV2 report, string companyId)
		{
			return ListValue.New(report.GetReport(this, companyId));
		}

		// Token: 0x06006955 RID: 26965 RVA: 0x00169F3B File Offset: 0x0016813B
		public Value GetReport(AdobeAnalyticsRequestV2 request)
		{
			return this.ExecuteRequest(request);
		}

		// Token: 0x06006956 RID: 26966 RVA: 0x00169F44 File Offset: 0x00168144
		public override IList<AdobeAnalyticsSegment> GetSegments(AdobeAnalyticsCube cube)
		{
			IList<AdobeAnalyticsSegment> list = new List<AdobeAnalyticsSegment>();
			int num = 0;
			bool asBoolean;
			do
			{
				Value value = this.ExecuteRequest(AdobeAnalyticsRequestV2.NewSegmentRequest(base.ClientId, cube.Id, cube.Company, num));
				foreach (IValueReference valueReference in value["content"].AsList)
				{
					list.Add(AdobeAnalyticsSegment.New(valueReference.Value));
				}
				asBoolean = value["lastPage"].AsBoolean;
				num++;
			}
			while (!asBoolean);
			return list;
		}

		// Token: 0x06006957 RID: 26967 RVA: 0x00169FF0 File Offset: 0x001681F0
		public IList<string> GetTopItemIds(string companyId, string rsid, string dimension, int count = 0)
		{
			IList<string> list = new List<string>();
			int num = 0;
			int num2 = 0;
			Value value;
			do
			{
				value = this.ExecuteRequest(AdobeAnalyticsRequestV2.NewTopItemsRequest(base.ClientId, companyId, rsid, dimension, count, num));
				foreach (IValueReference valueReference in value["rows"].AsList)
				{
					Value value2 = (Value)valueReference;
					list.Add(value2["itemId"].AsString);
					num2++;
					if (count > 0 && num2 >= count)
					{
						break;
					}
				}
			}
			while (!value["lastPage"].AsBoolean && (count == 0 || num2 < count));
			return list;
		}

		// Token: 0x06006958 RID: 26968 RVA: 0x0016A0BC File Offset: 0x001682BC
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
				Request request2 = Request.Create(this.host, "AdobeAnalytics", null, TextValue.New(request.Uri.ToString()), null, request.Content, null, request.Headers, null, null, null, null, null, null);
				Encoding encoding = Encoding.UTF8;
				string text = null;
				int i = 0;
				Value value2;
				while (i < this.options.RetryCount + 1)
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
							if (response.StatusCode == 500)
							{
								text = null;
								goto IL_01E9;
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
					goto IL_0180;
					IL_01E9:
					i++;
					continue;
					IL_0180:
					if (value.IsRecord && value.AsRecord.TryGetValue("error", out value2))
					{
						string asString = value["error_description"].AsString;
						Value @null;
						if (!value.AsRecord.TryGetValue("error_uri", out @null))
						{
							@null = Value.Null;
						}
						throw DataSourceException.NewDataSourceError(this.host, asString, AdobeAnalyticsService.Resource, "ErrorUri", @null, NullableTypeValue.Text, null);
					}
					break;
				}
				if (text == null)
				{
					throw DataSourceException.NewDataSourceError<Message0>(this.host, Strings.AdobeAnalyticsServerError, AdobeAnalyticsService.Resource, "Request", TextValue.New(encoding.GetString((request.Content ?? BinaryValue.Empty).AsBytes)), TypeValue.Text, null);
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

		// Token: 0x06006959 RID: 26969 RVA: 0x0016A3D8 File Offset: 0x001685D8
		private string GetCacheKey(string commandText, Value content)
		{
			return PersistentCacheKey.AdobeAnalytics.Qualify(commandText);
		}

		// Token: 0x04003A2F RID: 14895
		public const string ReportSuitesKey = "content";

		// Token: 0x04003A30 RID: 14896
		public const string RsidKey = "rsid";

		// Token: 0x04003A31 RID: 14897
		public const string SiteTitleKey = "name";
	}
}

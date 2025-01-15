using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E21 RID: 3617
	internal class CdpaService
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06006169 RID: 24937 RVA: 0x0014CDF0 File Offset: 0x0014AFF0
		// (remove) Token: 0x0600616A RID: 24938 RVA: 0x0014CE24 File Offset: 0x0014B024
		internal static event EventHandler<CdpaService.QueryExecutedEventArgs> QueryExecuted;

		// Token: 0x0600616B RID: 24939 RVA: 0x0014CE58 File Offset: 0x0014B058
		public CdpaService(IEngineHost engineHost, string tenantUri)
		{
			this.endpoints = new CdpaService.Endpoints(tenantUri);
			this.engineHost = engineHost;
			CdpaEndpointConfig cdpaEndpointConfig;
			if (!CdpaEndpointConfig.TryGetEndpointConfig(this.endpoints.TenantUri, out cdpaEndpointConfig))
			{
				throw this.NewServiceError(Strings.Cdpa_UriNotSupported(this.endpoints.TenantUri), null);
			}
		}

		// Token: 0x17001CA2 RID: 7330
		// (get) Token: 0x0600616C RID: 24940 RVA: 0x0014CEAF File Offset: 0x0014B0AF
		public IEngineHost EngineHost
		{
			get
			{
				return this.engineHost;
			}
		}

		// Token: 0x17001CA3 RID: 7331
		// (get) Token: 0x0600616D RID: 24941 RVA: 0x0014CEB7 File Offset: 0x0014B0B7
		public string Tenant
		{
			get
			{
				return this.endpoints.Tenant;
			}
		}

		// Token: 0x17001CA4 RID: 7332
		// (get) Token: 0x0600616E RID: 24942 RVA: 0x0014CEC4 File Offset: 0x0014B0C4
		public IResource Resource
		{
			get
			{
				return Microsoft.Mashup.Engine1.Library.Resource.New("CDPA", this.endpoints.TenantUri);
			}
		}

		// Token: 0x0600616F RID: 24943 RVA: 0x0014CEDB File Offset: 0x0014B0DB
		public Value ExecuteMetadataQuery()
		{
			return this.ExecuteQuery(this.endpoints.MetadataEndpointUri, null, true);
		}

		// Token: 0x06006170 RID: 24944 RVA: 0x0014CEF0 File Offset: 0x0014B0F0
		public Value ExecuteTimeSeriesQuery(string query)
		{
			return this.ExecuteQuery(this.endpoints.TimeSeriesEndpointUri, query, false);
		}

		// Token: 0x06006171 RID: 24945 RVA: 0x0014CF05 File Offset: 0x0014B105
		public Value ExecuteSignalsQuery(string query)
		{
			return this.ExecuteQuery(this.endpoints.SignalsEndpointUri, query, false);
		}

		// Token: 0x06006172 RID: 24946 RVA: 0x0014CF1A File Offset: 0x0014B11A
		public Value ExecutePropertiesQuery(string propertyName, string query)
		{
			return this.ExecuteQuery(this.endpoints.GetPropertiesEndpointUri(propertyName), query, false);
		}

		// Token: 0x06006173 RID: 24947 RVA: 0x0014CF30 File Offset: 0x0014B130
		public Value ExecuteFunnelQuery(string query)
		{
			return this.ExecuteQuery(this.endpoints.FunnelEndpointUri, query, false);
		}

		// Token: 0x06006174 RID: 24948 RVA: 0x0014CF48 File Offset: 0x0014B148
		public ValueException NewServiceError(string message, Exception innerException = null)
		{
			return DataSourceException.NewDataSourceError(this.EngineHost, message, this.Resource, new RecordKeyDefinition[]
			{
				new RecordKeyDefinition("TenantUri", TextValue.New(this.endpoints.TenantUri), TypeValue.Text)
			}, innerException);
		}

		// Token: 0x06006175 RID: 24949 RVA: 0x0014CF94 File Offset: 0x0014B194
		private Value ExecuteQuery(string endpointUri, string query, bool isMetadata)
		{
			RecordValue dynamicHeaders = this.GetDynamicHeaders();
			CdpaService.OnQueryExecuted(dynamicHeaders, query);
			ResourceCredentialCollection resourceCredentialCollection = HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.engineHost, this.Resource, null);
			Request request = Request.Create(this.engineHost, this.Resource.Kind, endpointUri, TextValue.New(endpointUri), Value.Null, (query == null) ? null : Library.Text.ToBinary.Invoke(TextValue.New(query)).AsBinary, null, CdpaService.staticHeaders.Concatenate(dynamicHeaders).AsRecord, null, null, null, null, null, null);
			Request request2 = request;
			Value[] array = dynamicHeaders.Keys.Select((string k) => TextValue.New(k)).ToArray<TextValue>();
			request2.ExcludedHeaders = ListValue.New(array);
			request.IsMetadata = isMetadata;
			Response response = request.GetResponse(resourceCredentialCollection, null, false);
			Value value2;
			using (Stream responseStream = response.GetResponseStream())
			{
				using (TextReader textReader = new StreamReader(responseStream))
				{
					string text = response.Headers["X-Request-ID"];
					if (request.IsFailedStatusCode(response))
					{
						string text2 = textReader.ReadToEnd();
						text = text ?? Strings.Cdpa_RequestId_None;
						throw this.NewServiceError(Strings.Cdpa_RequestError(text, text2), null);
					}
					Value value = JsonParser.Parse(textReader, null);
					if (text != null)
					{
						value = BinaryOperator.NewMeta.Invoke(value, UnaryOperator.Meta.Invoke(value).Concatenate(RecordValue.New(Keys.New("X-Request-ID"), new Value[] { TextValue.New(text) })).AsRecord);
					}
					value2 = value;
				}
			}
			return value2;
		}

		// Token: 0x06006176 RID: 24950 RVA: 0x0014D150 File Offset: 0x0014B350
		private RecordValue GetDynamicHeaders()
		{
			RecordValue recordValue = RecordValue.Empty;
			IEvaluationConstants evaluationConstants = this.EngineHost.QueryService<IEvaluationConstants>();
			if (evaluationConstants != null)
			{
				try
				{
					string correlationId = evaluationConstants.CorrelationId;
					if (correlationId != null)
					{
						using (TextReader textReader = new StringReader(correlationId))
						{
							Value value = JsonParser.Parse(textReader, null);
							HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
							RecordBuilder recordBuilder = new RecordBuilder(4);
							CdpaService.ExtractCdpaHeaders(hashSet, ref recordBuilder, value);
							recordValue = recordBuilder.ToRecord();
						}
					}
				}
				catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
				{
				}
			}
			return recordValue;
		}

		// Token: 0x06006177 RID: 24951 RVA: 0x0014D1F4 File Offset: 0x0014B3F4
		private static void OnQueryExecuted(RecordValue dynamicHeaders, string query)
		{
			EventHandler<CdpaService.QueryExecutedEventArgs> queryExecuted = CdpaService.QueryExecuted;
			if (queryExecuted != null)
			{
				queryExecuted(null, new CdpaService.QueryExecutedEventArgs(dynamicHeaders, query));
			}
		}

		// Token: 0x06006178 RID: 24952 RVA: 0x0014D218 File Offset: 0x0014B418
		private static void ExtractCdpaHeaders(HashSet<string> headerKeys, ref RecordBuilder builder, Value value)
		{
			ValueKind kind = value.Kind;
			if (kind != ValueKind.List)
			{
				if (kind == ValueKind.Record)
				{
					CdpaService.ExtractCdpaHeaders(headerKeys, ref builder, value.AsRecord);
					return;
				}
			}
			else
			{
				CdpaService.ExtractCdpaHeaders(headerKeys, ref builder, value.AsList);
			}
		}

		// Token: 0x06006179 RID: 24953 RVA: 0x0014D254 File Offset: 0x0014B454
		private static void ExtractCdpaHeaders(HashSet<string> headerKeys, ref RecordBuilder builder, RecordValue record)
		{
			foreach (string text in record.Keys)
			{
				Value value = record[text];
				if (text.StartsWith("CDPA-X-", StringComparison.OrdinalIgnoreCase))
				{
					if (value.IsText)
					{
						string text2 = CdpaService.ReplaceFirst(text, "CDPA-X-", "X-", StringComparison.OrdinalIgnoreCase);
						if (headerKeys.Add(text2))
						{
							builder.Add(text2, value, TypeValue.Any);
						}
					}
				}
				else
				{
					CdpaService.ExtractCdpaHeaders(headerKeys, ref builder, value);
				}
			}
		}

		// Token: 0x0600617A RID: 24954 RVA: 0x0014D2F0 File Offset: 0x0014B4F0
		private static string ReplaceFirst(string value, string oldSubstring, string newSubstring, StringComparison stringComparison)
		{
			int num = value.IndexOf(oldSubstring, stringComparison);
			if (num != -1)
			{
				string text = value.Substring(0, num);
				string text2 = value.Substring(num + oldSubstring.Length);
				return text + newSubstring + text2;
			}
			return value;
		}

		// Token: 0x0600617B RID: 24955 RVA: 0x0014D32C File Offset: 0x0014B52C
		private static void ExtractCdpaHeaders(HashSet<string> headerKeys, ref RecordBuilder builder, ListValue list)
		{
			foreach (IValueReference valueReference in list)
			{
				CdpaService.ExtractCdpaHeaders(headerKeys, ref builder, valueReference.Value);
			}
		}

		// Token: 0x0400350D RID: 13581
		public const string RequestIdHeader = "X-Request-ID";

		// Token: 0x0400350E RID: 13582
		private const string contextCdpaHeaderPrefix = "CDPA-X-";

		// Token: 0x0400350F RID: 13583
		private const string requestCdpaHeaderPrefix = "X-";

		// Token: 0x04003510 RID: 13584
		private static readonly RecordValue staticHeaders = RecordValue.New(Keys.New("Content-Type"), new Value[] { TextValue.New("application/json") });

		// Token: 0x04003512 RID: 13586
		private readonly CdpaService.Endpoints endpoints;

		// Token: 0x04003513 RID: 13587
		private readonly IEngineHost engineHost;

		// Token: 0x02000E22 RID: 3618
		internal class QueryExecutedEventArgs : EventArgs
		{
			// Token: 0x0600617D RID: 24957 RVA: 0x0014D3A5 File Offset: 0x0014B5A5
			public QueryExecutedEventArgs(RecordValue headers, string query)
			{
				this.headers = headers;
				this.query = query;
			}

			// Token: 0x17001CA5 RID: 7333
			// (get) Token: 0x0600617E RID: 24958 RVA: 0x0014D3BB File Offset: 0x0014B5BB
			public RecordValue Headers
			{
				get
				{
					return this.headers;
				}
			}

			// Token: 0x17001CA6 RID: 7334
			// (get) Token: 0x0600617F RID: 24959 RVA: 0x0014D3C3 File Offset: 0x0014B5C3
			public string Query
			{
				get
				{
					return this.query;
				}
			}

			// Token: 0x04003514 RID: 13588
			private readonly RecordValue headers;

			// Token: 0x04003515 RID: 13589
			private readonly string query;
		}

		// Token: 0x02000E23 RID: 3619
		private class Endpoints
		{
			// Token: 0x06006180 RID: 24960 RVA: 0x0014D3CC File Offset: 0x0014B5CC
			public Endpoints(string tenantUri)
			{
				try
				{
					this.tenantUri = tenantUri;
					Uri uri = new Uri(this.tenantUri);
					this.authority = uri.GetLeftPart(UriPartial.Authority);
					string[] array = uri.GetLeftPart(UriPartial.Path).Substring(this.authority.Length).Split(new char[] { '/' });
					this.tenant = array[3];
				}
				catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
				{
					throw ValueException.NewExpressionError<Message1>(Strings.Cdpa_UriNotSupported(tenantUri), null, null);
				}
			}

			// Token: 0x17001CA7 RID: 7335
			// (get) Token: 0x06006181 RID: 24961 RVA: 0x0014D468 File Offset: 0x0014B668
			public string TenantUri
			{
				get
				{
					return this.tenantUri;
				}
			}

			// Token: 0x17001CA8 RID: 7336
			// (get) Token: 0x06006182 RID: 24962 RVA: 0x0014D470 File Offset: 0x0014B670
			public string Tenant
			{
				get
				{
					return this.tenant;
				}
			}

			// Token: 0x17001CA9 RID: 7337
			// (get) Token: 0x06006183 RID: 24963 RVA: 0x0014D478 File Offset: 0x0014B678
			public string MetadataEndpointUri
			{
				get
				{
					return string.Format(CultureInfo.InvariantCulture, "{0}/v1/pbimetadata/tenants/{1}", this.authority, this.tenant);
				}
			}

			// Token: 0x17001CAA RID: 7338
			// (get) Token: 0x06006184 RID: 24964 RVA: 0x0014D495 File Offset: 0x0014B695
			public string TimeSeriesEndpointUri
			{
				get
				{
					return string.Format(CultureInfo.InvariantCulture, "{0}/v1/tenants/{1}/metric/inline/series", this.authority, this.tenant);
				}
			}

			// Token: 0x17001CAB RID: 7339
			// (get) Token: 0x06006185 RID: 24965 RVA: 0x0014D4B2 File Offset: 0x0014B6B2
			public string SignalsEndpointUri
			{
				get
				{
					return string.Format(CultureInfo.InvariantCulture, "{0}/v1/tenants/{1}/metric/inline/signals", this.authority, this.tenant);
				}
			}

			// Token: 0x17001CAC RID: 7340
			// (get) Token: 0x06006186 RID: 24966 RVA: 0x0014D4CF File Offset: 0x0014B6CF
			public string FunnelEndpointUri
			{
				get
				{
					return string.Format(CultureInfo.InvariantCulture, "{0}/v1/tenants/{1}/metric/funnel", this.authority, this.tenant);
				}
			}

			// Token: 0x06006187 RID: 24967 RVA: 0x0014D4EC File Offset: 0x0014B6EC
			public string GetPropertiesEndpointUri(string propertyName)
			{
				string text = Uri.EscapeUriString(propertyName);
				return string.Format(CultureInfo.InvariantCulture, "{0}/v1/tenants/{1}/metric/inline/properties/{2}", this.authority, this.tenant, text);
			}

			// Token: 0x04003516 RID: 13590
			private const string metadataEndpointTemplate = "{0}/v1/pbimetadata/tenants/{1}";

			// Token: 0x04003517 RID: 13591
			private const string timeSeriesEndpointTemplate = "{0}/v1/tenants/{1}/metric/inline/series";

			// Token: 0x04003518 RID: 13592
			private const string signalsEndpointTemplate = "{0}/v1/tenants/{1}/metric/inline/signals";

			// Token: 0x04003519 RID: 13593
			private const string funnelEndpointTemplate = "{0}/v1/tenants/{1}/metric/funnel";

			// Token: 0x0400351A RID: 13594
			private const string propertiesEndpointTemplate = "{0}/v1/tenants/{1}/metric/inline/properties/{2}";

			// Token: 0x0400351B RID: 13595
			private readonly string tenantUri;

			// Token: 0x0400351C RID: 13596
			private readonly string authority;

			// Token: 0x0400351D RID: 13597
			private readonly string tenant;
		}
	}
}

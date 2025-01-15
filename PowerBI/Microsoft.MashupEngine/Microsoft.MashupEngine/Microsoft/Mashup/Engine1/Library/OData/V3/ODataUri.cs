using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Data.Experimental.OData.Query;
using Microsoft.Data.OData;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008DF RID: 2271
	internal static class ODataUri
	{
		// Token: 0x060040CD RID: 16589 RVA: 0x000D8774 File Offset: 0x000D6974
		public static bool TryParseODataUri(IEngineHost host, IResource resource, Uri queryUri, Uri serviceUri, out QueryDescriptorQueryToken result)
		{
			bool flag;
			try
			{
				result = QueryDescriptorQueryToken.ParseUri(queryUri, serviceUri);
				result = new QueryDescriptorQueryToken(result.Path, result.Filter, result.OrderByTokens, result.Select, result.Expand, result.Skip, result.Top, result.InlineCount, result.Format, ODataUri.EncodeQueryOptions(result.QueryOptions));
				flag = true;
			}
			catch (ODataException ex)
			{
				using (IHostTrace hostTrace = TracingService.CreateTrace(host, "Engine/OData/ODataUri/TryParseODataUri", TraceEventType.Information, resource))
				{
					hostTrace.Add("ServiceUri", serviceUri, true);
					hostTrace.Add("QueryUri", queryUri, true);
					hostTrace.Add(ex, true);
				}
				result = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x000D8850 File Offset: 0x000D6A50
		public static List<QueryOptionQueryToken> EncodeQueryOptions(IEnumerable<QueryOptionQueryToken> queryOptions)
		{
			List<QueryOptionQueryToken> list = new List<QueryOptionQueryToken>();
			foreach (QueryOptionQueryToken queryOptionQueryToken in queryOptions)
			{
				QueryOptionQueryToken queryOptionQueryToken2 = new QueryOptionQueryToken((queryOptionQueryToken.Name != null) ? Uri.EscapeDataString(queryOptionQueryToken.Name) : null, (queryOptionQueryToken.Value != null) ? Uri.EscapeDataString(queryOptionQueryToken.Value) : null);
				list.Add(queryOptionQueryToken2);
			}
			return list;
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x000D88D4 File Offset: 0x000D6AD4
		public static QueryDescriptorQueryToken ParseODataUri(IEngineHost host, IResource resource, Uri queryUri, Uri serviceUri)
		{
			QueryDescriptorQueryToken queryDescriptorQueryToken;
			using (IHostTrace hostTrace = TracingService.CreateTrace(host, "Engine/OData/ODataUri/ParseODataUri", TraceEventType.Information, resource))
			{
				hostTrace.Add("ServiceUri", serviceUri, true);
				hostTrace.Add("QueryUri", queryUri, true);
				try
				{
					queryDescriptorQueryToken = QueryDescriptorQueryToken.ParseUri(queryUri, serviceUri);
				}
				catch (ODataException ex)
				{
					hostTrace.Add(ex, true);
					throw ex;
				}
			}
			return queryDescriptorQueryToken;
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x000D8948 File Offset: 0x000D6B48
		public static Uri CreateMetadataUri(Uri serviceUri)
		{
			QueryDescriptorQueryToken queryDescriptorQueryToken = new QueryDescriptorQueryToken(new KeywordSegmentQueryToken(KeywordKind.Metadata, null), null, null, null, null, null, null, null, null, null);
			return MashupODataUriBuilder.CreateUri(serviceUri, queryDescriptorQueryToken);
		}
	}
}

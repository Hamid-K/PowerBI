using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;

namespace System.Web.Http.Batch
{
	// Token: 0x02000113 RID: 275
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class BatchHttpRequestMessageExtensions
	{
		// Token: 0x0600075A RID: 1882 RVA: 0x000122A4 File Offset: 0x000104A4
		public static void CopyBatchRequestProperties(this HttpRequestMessage subRequest, HttpRequestMessage batchRequest)
		{
			if (subRequest == null)
			{
				throw new ArgumentNullException("subRequest");
			}
			if (batchRequest == null)
			{
				throw new ArgumentNullException("batchRequest");
			}
			foreach (KeyValuePair<string, object> keyValuePair in batchRequest.Properties)
			{
				if (!BatchHttpRequestMessageExtensions.BatchRequestPropertyExclusions.Contains(keyValuePair.Key))
				{
					subRequest.Properties.Add(keyValuePair);
				}
			}
			HttpRequestContext requestContext = subRequest.GetRequestContext();
			if (requestContext != null)
			{
				BatchHttpRequestContext batchHttpRequestContext = new BatchHttpRequestContext(requestContext)
				{
					Url = new UrlHelper(subRequest)
				};
				subRequest.SetRequestContext(batchHttpRequestContext);
			}
		}

		// Token: 0x040001DD RID: 477
		private const string HttpBatchContextKey = "MS_HttpBatchContext";

		// Token: 0x040001DE RID: 478
		private static readonly string[] BatchRequestPropertyExclusions = new string[]
		{
			HttpPropertyKeys.HttpRouteDataKey,
			HttpPropertyKeys.DisposableRequestResourcesKey,
			HttpPropertyKeys.SynchronizationContextKey,
			HttpPropertyKeys.HttpConfigurationKey,
			"MS_RoutingContext",
			"MS_HttpBatchContext"
		};
	}
}

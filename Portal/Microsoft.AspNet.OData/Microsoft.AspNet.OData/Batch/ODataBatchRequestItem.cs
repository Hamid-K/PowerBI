using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Batch
{
	// Token: 0x020001D0 RID: 464
	public abstract class ODataBatchRequestItem : IDisposable
	{
		// Token: 0x06000F50 RID: 3920 RVA: 0x0003EDF8 File Offset: 0x0003CFF8
		public static async Task<HttpResponseMessage> SendMessageAsync(HttpMessageInvoker invoker, HttpRequestMessage request, CancellationToken cancellationToken, Dictionary<string, string> contentIdToLocationMapping)
		{
			if (invoker == null)
			{
				throw Error.ArgumentNull("invoker");
			}
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (contentIdToLocationMapping != null)
			{
				string text = ContentIdHelpers.ResolveContentId(request.RequestUri.OriginalString, contentIdToLocationMapping);
				request.RequestUri = new Uri(text);
				request.SetODataContentIdMapping(contentIdToLocationMapping);
			}
			HttpResponseMessage httpResponseMessage = await invoker.SendAsync(request, cancellationToken);
			string odataContentId = request.GetODataContentId();
			if (contentIdToLocationMapping != null && odataContentId != null)
			{
				ODataBatchRequestItem.AddLocationHeaderToMapping(httpResponseMessage, contentIdToLocationMapping, odataContentId);
			}
			return httpResponseMessage;
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0003EE55 File Offset: 0x0003D055
		private static void AddLocationHeaderToMapping(HttpResponseMessage response, IDictionary<string, string> contentIdToLocationMapping, string contentId)
		{
			if (response.Headers.Location != null)
			{
				contentIdToLocationMapping.Add(contentId, response.Headers.Location.AbsoluteUri);
			}
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0003EE81 File Offset: 0x0003D081
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000F53 RID: 3923
		public abstract IEnumerable<IDisposable> GetResourcesForDisposal();

		// Token: 0x06000F54 RID: 3924
		public abstract Task<ODataBatchResponseItem> SendRequestAsync(HttpMessageInvoker invoker, CancellationToken cancellationToken);

		// Token: 0x06000F55 RID: 3925
		protected abstract void Dispose(bool disposing);
	}
}

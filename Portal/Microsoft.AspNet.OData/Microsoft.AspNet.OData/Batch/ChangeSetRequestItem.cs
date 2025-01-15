using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Batch
{
	// Token: 0x020001D3 RID: 467
	public class ChangeSetRequestItem : ODataBatchRequestItem
	{
		// Token: 0x06000F64 RID: 3940 RVA: 0x0003F0C2 File Offset: 0x0003D2C2
		public ChangeSetRequestItem(IEnumerable<HttpRequestMessage> requests)
		{
			if (requests == null)
			{
				throw Error.ArgumentNull("requests");
			}
			this.Requests = requests;
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0003F0DF File Offset: 0x0003D2DF
		// (set) Token: 0x06000F66 RID: 3942 RVA: 0x0003F0E7 File Offset: 0x0003D2E7
		public IEnumerable<HttpRequestMessage> Requests { get; private set; }

		// Token: 0x06000F67 RID: 3943 RVA: 0x0003F0F0 File Offset: 0x0003D2F0
		public override async Task<ODataBatchResponseItem> SendRequestAsync(HttpMessageInvoker invoker, CancellationToken cancellationToken)
		{
			if (invoker == null)
			{
				throw Error.ArgumentNull("invoker");
			}
			Dictionary<string, string> contentIdToLocationMapping = new Dictionary<string, string>();
			List<HttpResponseMessage> responses = new List<HttpResponseMessage>();
			try
			{
				foreach (HttpRequestMessage httpRequestMessage in this.Requests)
				{
					HttpResponseMessage httpResponseMessage = await ODataBatchRequestItem.SendMessageAsync(invoker, httpRequestMessage, cancellationToken, contentIdToLocationMapping);
					if (!httpResponseMessage.IsSuccessStatusCode)
					{
						ChangeSetRequestItem.DisposeResponses(responses);
						responses.Clear();
						responses.Add(httpResponseMessage);
						return new ChangeSetResponseItem(responses);
					}
					responses.Add(httpResponseMessage);
				}
				IEnumerator<HttpRequestMessage> enumerator = null;
			}
			catch
			{
				ChangeSetRequestItem.DisposeResponses(responses);
				throw;
			}
			return new ChangeSetResponseItem(responses);
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0003F148 File Offset: 0x0003D348
		public override IEnumerable<IDisposable> GetResourcesForDisposal()
		{
			List<IDisposable> list = new List<IDisposable>();
			foreach (HttpRequestMessage httpRequestMessage in this.Requests)
			{
				if (httpRequestMessage != null)
				{
					list.AddRange(HttpRequestMessageExtensions.GetResourcesForDisposal(httpRequestMessage));
				}
			}
			return list;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0003F1A4 File Offset: 0x0003D3A4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (HttpRequestMessage httpRequestMessage in this.Requests)
				{
					if (httpRequestMessage != null)
					{
						httpRequestMessage.Dispose();
					}
				}
			}
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0003F1F8 File Offset: 0x0003D3F8
		internal static void DisposeResponses(List<HttpResponseMessage> responses)
		{
			foreach (HttpResponseMessage httpResponseMessage in responses)
			{
				if (httpResponseMessage != null)
				{
					httpResponseMessage.Dispose();
				}
			}
		}
	}
}

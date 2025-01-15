using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Batch
{
	// Token: 0x020001D4 RID: 468
	public class OperationRequestItem : ODataBatchRequestItem
	{
		// Token: 0x06000F6B RID: 3947 RVA: 0x0003F248 File Offset: 0x0003D448
		public OperationRequestItem(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			this.Request = request;
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x0003F265 File Offset: 0x0003D465
		// (set) Token: 0x06000F6D RID: 3949 RVA: 0x0003F26D File Offset: 0x0003D46D
		public HttpRequestMessage Request { get; private set; }

		// Token: 0x06000F6E RID: 3950 RVA: 0x0003F278 File Offset: 0x0003D478
		public override async Task<ODataBatchResponseItem> SendRequestAsync(HttpMessageInvoker invoker, CancellationToken cancellationToken)
		{
			if (invoker == null)
			{
				throw Error.ArgumentNull("invoker");
			}
			TaskAwaiter<HttpResponseMessage> taskAwaiter = ODataBatchRequestItem.SendMessageAsync(invoker, this.Request, cancellationToken, null).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<HttpResponseMessage> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<HttpResponseMessage>);
			}
			return new OperationResponseItem(taskAwaiter.GetResult());
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0003F2CD File Offset: 0x0003D4CD
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Request.Dispose();
			}
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0003F2DD File Offset: 0x0003D4DD
		public override IEnumerable<IDisposable> GetResourcesForDisposal()
		{
			return HttpRequestMessageExtensions.GetResourcesForDisposal(this.Request);
		}
	}
}

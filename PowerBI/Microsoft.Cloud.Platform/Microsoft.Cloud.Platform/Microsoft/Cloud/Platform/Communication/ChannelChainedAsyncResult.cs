using System;
using System.ServiceModel;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000491 RID: 1169
	public class ChannelChainedAsyncResult<T> : ChainedAsyncResult<WorkTicket>
	{
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x0008146F File Offset: 0x0007F66F
		// (set) Token: 0x06002405 RID: 9221 RVA: 0x00081477 File Offset: 0x0007F677
		public OperationContext OperationContext { get; private set; }

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06002406 RID: 9222 RVA: 0x00081480 File Offset: 0x0007F680
		// (set) Token: 0x06002407 RID: 9223 RVA: 0x00081488 File Offset: 0x0007F688
		public Sequencer.AsyncEndFunction EndExecuteOperation { get; private set; }

		// Token: 0x06002408 RID: 9224 RVA: 0x00081491 File Offset: 0x0007F691
		public ChannelChainedAsyncResult(AsyncCallback callback, object context, OperationContext operationContext, Sequencer.AsyncEndFunction endExecuteOperation)
			: base(callback, context, null, null)
		{
			this.OperationContext = operationContext;
			this.EndExecuteOperation = endExecuteOperation;
		}
	}
}

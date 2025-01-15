using System;
using Microsoft.HostIntegration.MqClient.StrictResources.ClassLibrary;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B57 RID: 2903
	public class TransactionalQueue : Queue
	{
		// Token: 0x06005BF5 RID: 23541 RVA: 0x0017B216 File Offset: 0x00179416
		public TransactionalQueue(string queueAlias)
			: base(queueAlias, true)
		{
		}

		// Token: 0x06005BF6 RID: 23542 RVA: 0x0017B220 File Offset: 0x00179420
		public TransactionalQueue(string name, TransactionalQueueManager queueManager)
			: base(name, queueManager, true)
		{
		}

		// Token: 0x06005BF7 RID: 23543 RVA: 0x0017B22C File Offset: 0x0017942C
		internal override void CheckTransactions(QueueManager queueManager)
		{
			if (!(queueManager is TransactionalQueueManager))
			{
				ArgumentException ex = new ArgumentException(SR.TransactionalQNeedsTransactionalQm, "queueManager");
				if (this.tracePoint.IsEnabled(TraceFlags.Error))
				{
					this.tracePoint.Trace(TraceFlags.Error, ex);
				}
				throw ex;
			}
		}
	}
}

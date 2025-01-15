using System;
using System.Threading;
using System.Transactions;
using Microsoft.HostIntegration.MqClient.StrictResources.ClassLibrary;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B59 RID: 2905
	public class TransactionalQueueManager : QueueManager
	{
		// Token: 0x06005C36 RID: 23606 RVA: 0x0017CCEA File Offset: 0x0017AEEA
		public TransactionalQueueManager(string queueManagerAlias)
			: base(true, queueManagerAlias)
		{
			this.SetFirstThread();
		}

		// Token: 0x06005C37 RID: 23607 RVA: 0x0017CD05 File Offset: 0x0017AF05
		public TransactionalQueueManager(string name, string channelName, string host)
			: base(true, name, channelName, host)
		{
			this.SetFirstThread();
		}

		// Token: 0x06005C38 RID: 23608 RVA: 0x0017CD22 File Offset: 0x0017AF22
		public TransactionalQueueManager(string name, string channelName, string host, int port)
			: base(true, name, channelName, host, port)
		{
			this.SetFirstThread();
		}

		// Token: 0x06005C39 RID: 23609 RVA: 0x0017CD41 File Offset: 0x0017AF41
		public TransactionalQueueManager(string name, string channelName, string host, int port, bool sslUse)
			: base(true, name, channelName, host, port, sslUse)
		{
			this.SetFirstThread();
		}

		// Token: 0x06005C3A RID: 23610 RVA: 0x0017CD62 File Offset: 0x0017AF62
		public TransactionalQueueManager(string name, string channelName, string host, int port, bool sslUse, string connectAs)
			: base(true, name, channelName, host, port, sslUse, connectAs)
		{
			this.SetFirstThread();
		}

		// Token: 0x06005C3B RID: 23611 RVA: 0x0017CD88 File Offset: 0x0017AF88
		public TransactionalQueueManager(string name, string channelName, string host, int port, bool sslUse, string connectAs, string dynamicQueueNamePrefix)
			: base(true, name, channelName, host, port, sslUse, connectAs, dynamicQueueNamePrefix)
		{
			this.SetFirstThread();
		}

		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x06005C3C RID: 23612 RVA: 0x00002B16 File Offset: 0x00000D16
		internal override bool IsTransactional
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005C3D RID: 23613 RVA: 0x0017CDB8 File Offset: 0x0017AFB8
		internal override void CheckTransactions()
		{
			if (Transaction.Current == null)
			{
				CustomMqClientException ex = new CustomMqClientException(SR.TransactionalNotInTransaction);
				if (this.tracePoint.IsEnabled(TraceFlags.Error))
				{
					this.tracePoint.Trace(TraceFlags.Error, ex);
				}
				throw ex;
			}
			object obj = this.lockThreadObject;
			lock (obj)
			{
				int managedThreadId = Thread.CurrentThread.ManagedThreadId;
				if (this.firstOperationThreadId != managedThreadId)
				{
					CustomMqClientException ex2 = new CustomMqClientException(SR.TransactionalWrongThread);
					if (this.tracePoint.IsEnabled(TraceFlags.Error))
					{
						this.tracePoint.Trace(TraceFlags.Error, ex2);
					}
					throw ex2;
				}
			}
		}

		// Token: 0x06005C3E RID: 23614 RVA: 0x0017CE68 File Offset: 0x0017B068
		private void SetFirstThread()
		{
			object obj = this.lockThreadObject;
			lock (obj)
			{
				this.firstOperationThreadId = Thread.CurrentThread.ManagedThreadId;
			}
		}

		// Token: 0x04004849 RID: 18505
		private object lockThreadObject = new object();

		// Token: 0x0400484A RID: 18506
		private int firstOperationThreadId;
	}
}

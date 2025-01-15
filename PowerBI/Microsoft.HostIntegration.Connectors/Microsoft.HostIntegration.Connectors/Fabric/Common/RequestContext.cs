using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Xml;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000459 RID: 1113
	internal class RequestContext : OperationContext
	{
		// Token: 0x060026F4 RID: 9972 RVA: 0x00077218 File Offset: 0x00075418
		public RequestContext(Message msgToBeSent, IOutputChannel channel, Dictionary<UniqueId, RequestContext> hashTable, object hashTableLock, AsyncCallback callback, object state, TimeSpan timeout)
			: base(callback, state, timeout)
		{
			this.m_msgToBeSent = msgToBeSent;
			this.m_channel = channel;
			this.m_receivedMsg = null;
			this.m_requestMessageId = msgToBeSent.Headers.MessageId;
			if (this.m_requestMessageId == null)
			{
				this.m_requestMessageId = new UniqueId();
				msgToBeSent.Headers.MessageId = this.m_requestMessageId;
			}
			this.m_requestAction = msgToBeSent.Headers.Action;
			this.m_hashTable = hashTable;
			this.m_hashTableLock = hashTableLock;
			lock (this.m_hashTableLock)
			{
				this.m_hashTable.Add(this.m_requestMessageId, this);
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060026F5 RID: 9973 RVA: 0x000772DC File Offset: 0x000754DC
		public Message ReceivedMessage
		{
			get
			{
				return this.m_receivedMsg;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x060026F6 RID: 9974 RVA: 0x000772E4 File Offset: 0x000754E4
		public UniqueId RequestMessageId
		{
			get
			{
				return this.m_requestMessageId;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x060026F7 RID: 9975 RVA: 0x000772EC File Offset: 0x000754EC
		public string RequestAction
		{
			get
			{
				return this.m_requestAction;
			}
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x000772F4 File Offset: 0x000754F4
		public void Send(TimeSpan timeout)
		{
			this.m_msgToBeSent.Properties.AllowOutputBatching = false;
			this.m_channel.Send(this.m_msgToBeSent, timeout);
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x00077319 File Offset: 0x00075519
		public void BeginSend()
		{
			this.m_msgToBeSent.Properties.AllowOutputBatching = false;
			this.m_channel.BeginSend(this.m_msgToBeSent, TimeSpan.MaxValue, RequestContext.MessageContextCallback, this);
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x0007734C File Offset: 0x0007554C
		private static void ProcessMessageContext(IAsyncResult ar)
		{
			RequestContext requestContext = (RequestContext)ar.AsyncState;
			IOutputChannel channel = requestContext.m_channel;
			requestContext.m_channel = null;
			bool completedSynchronously = ar.CompletedSynchronously;
			Exception ex = null;
			try
			{
				channel.EndSend(ar);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			finally
			{
				requestContext.OnMessageSent(completedSynchronously, ex);
			}
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x000773B4 File Offset: 0x000755B4
		public void RemoveContext()
		{
			object hashTableLock = this.m_hashTableLock;
			if (hashTableLock != null)
			{
				lock (hashTableLock)
				{
					if (this.m_hashTableLock != null)
					{
						this.Remove();
					}
				}
			}
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x000773FC File Offset: 0x000755FC
		public void Remove()
		{
			bool flag = this.m_hashTable.Remove(this.m_requestMessageId);
			ReleaseAssert.IsTrue(flag);
			this.m_hashTableLock = null;
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x00077428 File Offset: 0x00075628
		protected override void OnOperationComplete(bool completedSynchronously)
		{
			this.RemoveContext();
			base.OnOperationComplete(completedSynchronously);
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x00077437 File Offset: 0x00075637
		protected void OnMessageSent(bool sendCompletedSynchronously, Exception e)
		{
			if (e != null)
			{
				base.OperationCompleted(sendCompletedSynchronously, e);
			}
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x00077444 File Offset: 0x00075644
		public void MessageReceived(Message receivedMsg)
		{
			this.m_receivedMsg = receivedMsg;
			base.OperationCompleted(false, null);
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x00077458 File Offset: 0x00075658
		public static RequestContext FindAndRemoveContext(UniqueId replyId, Dictionary<UniqueId, RequestContext> hashTable, object hashTableLock)
		{
			RequestContext requestContext;
			lock (hashTableLock)
			{
				hashTable.TryGetValue(replyId, out requestContext);
				if (requestContext != null)
				{
					requestContext.Remove();
				}
			}
			return requestContext;
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x0007749C File Offset: 0x0007569C
		public static RequestContext FindContext(UniqueId messageId, Dictionary<UniqueId, RequestContext> hashTable)
		{
			RequestContext requestContext;
			hashTable.TryGetValue(messageId, out requestContext);
			return requestContext;
		}

		// Token: 0x04001713 RID: 5907
		private Message m_msgToBeSent;

		// Token: 0x04001714 RID: 5908
		private UniqueId m_requestMessageId;

		// Token: 0x04001715 RID: 5909
		private string m_requestAction;

		// Token: 0x04001716 RID: 5910
		private IOutputChannel m_channel;

		// Token: 0x04001717 RID: 5911
		private Message m_receivedMsg;

		// Token: 0x04001718 RID: 5912
		private Dictionary<UniqueId, RequestContext> m_hashTable;

		// Token: 0x04001719 RID: 5913
		private object m_hashTableLock;

		// Token: 0x0400171A RID: 5914
		private static AsyncCallback MessageContextCallback = new AsyncCallback(RequestContext.ProcessMessageContext);
	}
}

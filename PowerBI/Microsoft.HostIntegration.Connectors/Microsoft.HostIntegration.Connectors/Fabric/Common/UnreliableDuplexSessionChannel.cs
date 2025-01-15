using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000431 RID: 1073
	internal class UnreliableDuplexSessionChannel : ChannelBase, IDuplexSessionChannel, IDuplexChannel, IInputChannel, IOutputChannel, IChannel, ICommunicationObject, ISessionChannel<IDuplexSession>
	{
		// Token: 0x0600254A RID: 9546 RVA: 0x00072598 File Offset: 0x00070798
		public UnreliableDuplexSessionChannel(ChannelManagerBase manager, IDuplexSessionChannel nestedChannel)
			: base(manager)
		{
			this.m_nestedChannel = nestedChannel;
			this.m_nestedChannel.Faulted += this.OnNestedChannelFaulted;
			this.m_id = nestedChannel.RemoteAddress.Uri.AbsolutePath;
			this.m_pendingCount = 1;
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x0600254B RID: 9547 RVA: 0x000725E7 File Offset: 0x000707E7
		// (set) Token: 0x0600254C RID: 9548 RVA: 0x000725EF File Offset: 0x000707EF
		public string Id
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x000725F8 File Offset: 0x000707F8
		private void OnNestedChannelFaulted(object sender, EventArgs e)
		{
			base.Fault();
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x00072600 File Offset: 0x00070800
		public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.m_nestedChannel.BeginReceive(timeout, callback, state);
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x00072610 File Offset: 0x00070810
		public IAsyncResult BeginReceive(AsyncCallback callback, object state)
		{
			return this.m_nestedChannel.BeginReceive(callback, state);
		}

		// Token: 0x06002550 RID: 9552 RVA: 0x0007261F File Offset: 0x0007081F
		public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.m_nestedChannel.BeginTryReceive(timeout, callback, state);
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x0007262F File Offset: 0x0007082F
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.m_nestedChannel.BeginWaitForMessage(timeout, callback, state);
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x0007263F File Offset: 0x0007083F
		public Message EndReceive(IAsyncResult result)
		{
			return this.m_nestedChannel.EndReceive(result);
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x0007264D File Offset: 0x0007084D
		public bool EndTryReceive(IAsyncResult result, out Message message)
		{
			return this.m_nestedChannel.EndTryReceive(result, out message);
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x0007265C File Offset: 0x0007085C
		public bool EndWaitForMessage(IAsyncResult result)
		{
			return this.EndWaitForMessage(result);
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x00072665 File Offset: 0x00070865
		public EndpointAddress LocalAddress
		{
			get
			{
				return this.m_nestedChannel.LocalAddress;
			}
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x00072672 File Offset: 0x00070872
		public Message Receive(TimeSpan timeout)
		{
			return this.m_nestedChannel.Receive(timeout);
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x00072680 File Offset: 0x00070880
		public Message Receive()
		{
			return this.m_nestedChannel.Receive();
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x0007268D File Offset: 0x0007088D
		public bool TryReceive(TimeSpan timeout, out Message message)
		{
			return this.m_nestedChannel.TryReceive(timeout, out message);
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x0007269C File Offset: 0x0007089C
		public bool WaitForMessage(TimeSpan timeout)
		{
			return this.m_nestedChannel.WaitForMessage(timeout);
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000726AC File Offset: 0x000708AC
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			TimeSpan delay = UnreliableTransportConfiguration.GetDelay(this.m_id, message);
			if (delay == TimeSpan.MinValue)
			{
				base.Fault();
				this.m_nestedChannel.Abort();
				EventLogWriter.WriteVerbose<string>("UnreliableTransport.Fault", "Faulting channel {0}", this.m_id);
				throw new CommunicationException("Channel faulted");
			}
			if (delay != TimeSpan.Zero)
			{
				UnreliableDuplexSessionChannel.SendContext sendContext = new UnreliableDuplexSessionChannel.SendContext(message, delay, this, timeout, callback, state);
				sendContext.StartTimer();
				return sendContext;
			}
			return this.m_nestedChannel.BeginSend(message, timeout, callback, state);
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x00072736 File Offset: 0x00070936
		public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
		{
			return this.BeginSend(message, base.DefaultSendTimeout, callback, state);
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x00072748 File Offset: 0x00070948
		public void EndSend(IAsyncResult result)
		{
			UnreliableDuplexSessionChannel.SendContext sendContext = result as UnreliableDuplexSessionChannel.SendContext;
			if (sendContext != null)
			{
				sendContext.End();
				return;
			}
			this.m_nestedChannel.EndSend(result);
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600255D RID: 9565 RVA: 0x00072772 File Offset: 0x00070972
		public EndpointAddress RemoteAddress
		{
			get
			{
				return this.m_nestedChannel.RemoteAddress;
			}
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x0007277F File Offset: 0x0007097F
		public void Send(Message message, TimeSpan timeout)
		{
			this.EndSend(this.BeginSend(message, timeout, null, null));
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x00072791 File Offset: 0x00070991
		public void Send(Message message)
		{
			this.EndSend(this.BeginSend(message, null, null));
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002560 RID: 9568 RVA: 0x000727A2 File Offset: 0x000709A2
		public Uri Via
		{
			get
			{
				return this.m_nestedChannel.Via;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002561 RID: 9569 RVA: 0x000727AF File Offset: 0x000709AF
		public IDuplexSession Session
		{
			get
			{
				return this.m_nestedChannel.Session;
			}
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000727BC File Offset: 0x000709BC
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			TimeSpan delay = UnreliableTransportConfiguration.GetDelay(this.m_id, null);
			if (delay == TimeSpan.MaxValue)
			{
				OperationContext operationContext = new OperationContext(callback, state, UnreliableTransportSpec.MaxDelay);
				operationContext.StartTimer();
				return operationContext;
			}
			return this.m_nestedChannel.BeginOpen(timeout, callback, state);
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x00072808 File Offset: 0x00070A08
		protected override void OnEndOpen(IAsyncResult result)
		{
			OperationContext operationContext = result as OperationContext;
			if (operationContext != null)
			{
				operationContext.End();
				return;
			}
			this.m_nestedChannel.EndOpen(result);
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x00072832 File Offset: 0x00070A32
		protected override void OnOpen(TimeSpan timeout)
		{
			this.OnEndOpen(this.OnBeginOpen(timeout, null, null));
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x00072843 File Offset: 0x00070A43
		protected override void OnAbort()
		{
			this.m_nestedChannel.Abort();
			this.m_nestedChannel.Faulted -= this.OnNestedChannelFaulted;
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x00072867 File Offset: 0x00070A67
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.DecrementPendingCount();
			return new SynchronousCompletionOperationContext(callback, state);
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x00072878 File Offset: 0x00070A78
		protected override void OnEndClose(IAsyncResult result)
		{
			SynchronousCompletionOperationContext synchronousCompletionOperationContext = (SynchronousCompletionOperationContext)result;
			synchronousCompletionOperationContext.End();
			this.m_nestedChannel.Faulted -= this.OnNestedChannelFaulted;
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x000728A9 File Offset: 0x00070AA9
		protected override void OnClose(TimeSpan timeout)
		{
			this.OnEndClose(this.OnBeginClose(timeout, null, null));
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x000728BA File Offset: 0x00070ABA
		private void IncrementPendingCount()
		{
			Interlocked.Increment(ref this.m_pendingCount);
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x000728C8 File Offset: 0x00070AC8
		private void DecrementPendingCount()
		{
			if (Interlocked.Decrement(ref this.m_pendingCount) == 0)
			{
				Utility.CloseChannelAsync(this.m_nestedChannel);
			}
		}

		// Token: 0x040016A9 RID: 5801
		private IDuplexSessionChannel m_nestedChannel;

		// Token: 0x040016AA RID: 5802
		private string m_id;

		// Token: 0x040016AB RID: 5803
		private int m_pendingCount;

		// Token: 0x02000432 RID: 1074
		private class SendContext : OperationContext
		{
			// Token: 0x0600256B RID: 9579 RVA: 0x000728E4 File Offset: 0x00070AE4
			public SendContext(Message msg, TimeSpan waitTime, UnreliableDuplexSessionChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
				: base(callback, state, timeout)
			{
				this.m_msg = msg;
				this.m_channel = channel;
				if (waitTime != TimeSpan.MaxValue)
				{
					this.m_channel.IncrementPendingCount();
					Timer timer = new Timer(UnreliableDuplexSessionChannel.SendContext.OnTimeout, this);
					timer.Enqueue(waitTime);
				}
				IOCompletionPortWorkQueue.NormalPriorityWorkQueue.QueueWorkItem(UnreliableDuplexSessionChannel.SendContext.CompleteCallback, this);
			}

			// Token: 0x0600256C RID: 9580 RVA: 0x00072948 File Offset: 0x00070B48
			private static void StaticCompleteCallback(object state)
			{
				UnreliableDuplexSessionChannel.SendContext sendContext = (UnreliableDuplexSessionChannel.SendContext)state;
				sendContext.CompleteOperation(false, null);
			}

			// Token: 0x0600256D RID: 9581 RVA: 0x00072964 File Offset: 0x00070B64
			private static void StaticOnTimeout(object state)
			{
				UnreliableDuplexSessionChannel.SendContext sendContext = (UnreliableDuplexSessionChannel.SendContext)state;
				try
				{
					sendContext.m_channel.m_nestedChannel.BeginSend(sendContext.m_msg, UnreliableDuplexSessionChannel.SendContext.OnSent, sendContext);
				}
				catch (Exception ex)
				{
					if (!Utility.IsCommunicationException(ex))
					{
						throw;
					}
				}
				finally
				{
					sendContext.m_channel.DecrementPendingCount();
				}
			}

			// Token: 0x0600256E RID: 9582 RVA: 0x000729D0 File Offset: 0x00070BD0
			private static void StaticOnSent(IAsyncResult ar)
			{
				UnreliableDuplexSessionChannel.SendContext sendContext = (UnreliableDuplexSessionChannel.SendContext)ar.AsyncState;
				try
				{
					sendContext.m_channel.m_nestedChannel.EndSend(ar);
				}
				catch (Exception ex)
				{
					if (!Utility.IsCommunicationException(ex))
					{
						throw;
					}
				}
			}

			// Token: 0x040016AC RID: 5804
			private Message m_msg;

			// Token: 0x040016AD RID: 5805
			private UnreliableDuplexSessionChannel m_channel;

			// Token: 0x040016AE RID: 5806
			private static WaitCallback CompleteCallback = new WaitCallback(UnreliableDuplexSessionChannel.SendContext.StaticCompleteCallback);

			// Token: 0x040016AF RID: 5807
			private static WaitCallback OnTimeout = new WaitCallback(UnreliableDuplexSessionChannel.SendContext.StaticOnTimeout);

			// Token: 0x040016B0 RID: 5808
			private static AsyncCallback OnSent = new AsyncCallback(UnreliableDuplexSessionChannel.SendContext.StaticOnSent);
		}
	}
}

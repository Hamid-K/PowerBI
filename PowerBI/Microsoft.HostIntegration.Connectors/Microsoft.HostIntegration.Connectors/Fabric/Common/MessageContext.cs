using System;
using System.ServiceModel.Channels;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000457 RID: 1111
	internal abstract class MessageContext : OperationContext, IDisposable
	{
		// Token: 0x060026D0 RID: 9936 RVA: 0x00076EB2 File Offset: 0x000750B2
		public MessageContext(Message msgToBeSent, Message receivedMsg, IOutputChannel channel, AsyncCallback callback, object state, TimeSpan timeout)
			: base(callback, state, timeout)
		{
			this.m_msgToBeSent = msgToBeSent;
			this.m_receivedMsg = receivedMsg;
			this.m_channel = channel;
			if (msgToBeSent != null)
			{
				this.m_msgState = 1;
				return;
			}
			this.m_msgState = 18;
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x060026D1 RID: 9937 RVA: 0x00076EE8 File Offset: 0x000750E8
		// (set) Token: 0x060026D2 RID: 9938 RVA: 0x00076EF0 File Offset: 0x000750F0
		public Message SentMessage
		{
			get
			{
				return this.m_msgToBeSent;
			}
			protected set
			{
				this.m_msgToBeSent = value;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060026D3 RID: 9939 RVA: 0x00076EF9 File Offset: 0x000750F9
		// (set) Token: 0x060026D4 RID: 9940 RVA: 0x00076F01 File Offset: 0x00075101
		public Message ReceivedMessage
		{
			get
			{
				return this.m_receivedMsg;
			}
			protected set
			{
				this.m_receivedMsg = value;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060026D5 RID: 9941 RVA: 0x00076F0A File Offset: 0x0007510A
		// (set) Token: 0x060026D6 RID: 9942 RVA: 0x00076F12 File Offset: 0x00075112
		public IOutputChannel OutputChannel
		{
			get
			{
				return this.m_channel;
			}
			protected set
			{
				this.m_channel = value;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060026D7 RID: 9943 RVA: 0x00076F1B File Offset: 0x0007511B
		public bool IsSendSideContext
		{
			get
			{
				return (this.m_msgState & 1) != 0;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060026D8 RID: 9944 RVA: 0x00076F2B File Offset: 0x0007512B
		public bool IsReceiveSideContext
		{
			get
			{
				return (this.m_msgState & 2) != 0;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060026D9 RID: 9945 RVA: 0x00076F3B File Offset: 0x0007513B
		public bool IsSent
		{
			get
			{
				return MessageContext.StaticIsSent(this.m_msgState);
			}
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x00076F48 File Offset: 0x00075148
		private static bool StaticIsSent(int msgState)
		{
			return (msgState & 8) != 0;
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060026DB RID: 9947 RVA: 0x00076F53 File Offset: 0x00075153
		public bool HasSendFaulted
		{
			get
			{
				return MessageContext.StaticHasSendFaulted(this.m_msgState);
			}
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x00076F60 File Offset: 0x00075160
		private static bool StaticHasSendFaulted(int msgState)
		{
			return (msgState & 4) != 0;
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060026DD RID: 9949 RVA: 0x00076F6B File Offset: 0x0007516B
		public bool IsReceived
		{
			get
			{
				return MessageContext.StaticIsReceived(this.m_msgState);
			}
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x00076F78 File Offset: 0x00075178
		private static bool StaticIsReceived(int msgState)
		{
			return (msgState & 16) != 0;
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060026DF RID: 9951 RVA: 0x00076F84 File Offset: 0x00075184
		public bool IsComplete
		{
			get
			{
				return base.IsOperationComplete || MessageContext.StaticIsComplete(this.m_msgState);
			}
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x00076F9B File Offset: 0x0007519B
		private static bool StaticIsComplete(int msgState)
		{
			return (MessageContext.StaticIsSent(msgState) || MessageContext.StaticHasSendFaulted(msgState)) && MessageContext.StaticIsReceived(msgState);
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x060026E1 RID: 9953 RVA: 0x00076FB5 File Offset: 0x000751B5
		public bool IsClosed
		{
			get
			{
				return MessageContext.StaticIsClosed(this.m_msgState);
			}
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x00076FC2 File Offset: 0x000751C2
		private static bool StaticIsClosed(int msgState)
		{
			return (msgState & 32) != 0;
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x060026E3 RID: 9955 RVA: 0x00076FCE File Offset: 0x000751CE
		public bool IsAborted
		{
			get
			{
				return MessageContext.StaticIsAborted(this.m_msgState);
			}
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x00076FDB File Offset: 0x000751DB
		private static bool StaticIsAborted(int msgState)
		{
			return (msgState & 64) != 0;
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x00076FE7 File Offset: 0x000751E7
		public void Abort()
		{
			this.Abort(new OperationContextAbortedException());
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x00076FF4 File Offset: 0x000751F4
		public override void Abort(Exception e)
		{
			int num = this.m_msgState;
			for (;;)
			{
				int num2 = num;
				if (MessageContext.StaticIsComplete(num) || MessageContext.StaticIsAborted(num) || MessageContext.StaticIsClosed(num))
				{
					break;
				}
				int num3 = num | 64;
				num = Interlocked.CompareExchange(ref this.m_msgState, num3, num2);
				if (num == num2)
				{
					goto Block_3;
				}
			}
			return;
			Block_3:
			this.OnAbort(e);
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x00077041 File Offset: 0x00075241
		public void Close()
		{
			this.Close(TimeSpan.MaxValue);
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x00077050 File Offset: 0x00075250
		public void Close(TimeSpan timeout)
		{
			int num = this.m_msgState;
			for (;;)
			{
				int num2 = num;
				if (MessageContext.StaticIsClosed(num) || MessageContext.StaticIsAborted(num))
				{
					break;
				}
				int num3 = num | 32;
				num = Interlocked.CompareExchange(ref this.m_msgState, num3, num2);
				if (num == num2)
				{
					goto Block_2;
				}
			}
			return;
			Block_2:
			this.OnClose(timeout);
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x00077095 File Offset: 0x00075295
		public void Dispose()
		{
			this.Close();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x000770A3 File Offset: 0x000752A3
		protected virtual void OnAbort(Exception e)
		{
			base.OperationCompleted(false, e);
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x000770AD File Offset: 0x000752AD
		protected virtual void OnClose(TimeSpan timespan)
		{
			base.OperationCompleted(false, null);
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x000770B7 File Offset: 0x000752B7
		public void Send()
		{
			this.Send(TimeSpan.MaxValue);
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x000770C4 File Offset: 0x000752C4
		public void Send(TimeSpan timeout)
		{
			this.m_msgToBeSent.Properties.AllowOutputBatching = false;
			this.m_channel.Send(this.m_msgToBeSent, timeout);
			this.OnMessageSent(true, null);
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x000770F1 File Offset: 0x000752F1
		public void BeginSend()
		{
			this.m_msgToBeSent.Properties.AllowOutputBatching = false;
			this.m_channel.BeginSend(this.m_msgToBeSent, TimeSpan.MaxValue, MessageContext.MessageContextCallback, this);
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x00077121 File Offset: 0x00075321
		public void MessageReceived(Message receivedMsg)
		{
			this.OnMessageReceived(receivedMsg);
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x0007712C File Offset: 0x0007532C
		protected virtual void OnMessageSent(bool sendCompletedSynchronously, Exception e)
		{
			int num = ((e == null) ? 8 : 4);
			int num2 = this.m_msgState;
			int num3;
			do
			{
				num3 = num2;
				int num4 = num2 | num;
				num2 = Interlocked.CompareExchange(ref this.m_msgState, num4, num3);
			}
			while (num2 != num3);
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x00077160 File Offset: 0x00075360
		protected virtual void OnMessageReceived(Message receivedMsg)
		{
			int num = this.m_msgState;
			while ((num & 16) == 0)
			{
				int num2 = num;
				int num3 = num | 16;
				num = Interlocked.CompareExchange(ref this.m_msgState, num3, num2);
				if (num == num2)
				{
					this.m_receivedMsg = receivedMsg;
					return;
				}
			}
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x0007719C File Offset: 0x0007539C
		private static void ProcessMessageContext(IAsyncResult ar)
		{
			MessageContext messageContext = (MessageContext)ar.AsyncState;
			IOutputChannel outputChannel = messageContext.OutputChannel;
			messageContext.OutputChannel = null;
			bool completedSynchronously = ar.CompletedSynchronously;
			Exception ex = null;
			try
			{
				outputChannel.EndSend(ar);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			finally
			{
				messageContext.OnMessageSent(completedSynchronously, ex);
			}
		}

		// Token: 0x04001706 RID: 5894
		private static AsyncCallback MessageContextCallback = new AsyncCallback(MessageContext.ProcessMessageContext);

		// Token: 0x04001707 RID: 5895
		private Message m_msgToBeSent;

		// Token: 0x04001708 RID: 5896
		private Message m_receivedMsg;

		// Token: 0x04001709 RID: 5897
		private IOutputChannel m_channel;

		// Token: 0x0400170A RID: 5898
		private int m_msgState;

		// Token: 0x02000458 RID: 1112
		[Flags]
		public enum MessageState
		{
			// Token: 0x0400170C RID: 5900
			SendSide = 1,
			// Token: 0x0400170D RID: 5901
			ReceiveSide = 2,
			// Token: 0x0400170E RID: 5902
			SendFaulted = 4,
			// Token: 0x0400170F RID: 5903
			Sent = 8,
			// Token: 0x04001710 RID: 5904
			Received = 16,
			// Token: 0x04001711 RID: 5905
			Closed = 32,
			// Token: 0x04001712 RID: 5906
			Aborted = 64
		}
	}
}

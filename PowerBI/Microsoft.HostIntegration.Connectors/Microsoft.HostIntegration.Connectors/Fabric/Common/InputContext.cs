using System;
using System.ServiceModel.Channels;
using System.Xml;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200045B RID: 1115
	internal class InputContext : IInputContext
	{
		// Token: 0x0600270D RID: 9997 RVA: 0x000774C7 File Offset: 0x000756C7
		public InputContext(Message receivedMsg, IInputConnection inputConnection, IOutputChannel channel)
		{
			this.m_receivedMsg = receivedMsg;
			this.m_requestMessageId = receivedMsg.Headers.MessageId;
			this.m_requestAction = receivedMsg.Headers.Action;
			this.m_inputConnection = inputConnection;
			this.m_outputChannel = channel;
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x0600270E RID: 9998 RVA: 0x00077506 File Offset: 0x00075706
		public UniqueId RequestId
		{
			get
			{
				return this.m_requestMessageId;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x0600270F RID: 9999 RVA: 0x0007750E File Offset: 0x0007570E
		public IInputConnection InputConnection
		{
			get
			{
				return this.m_inputConnection;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06002710 RID: 10000 RVA: 0x00077516 File Offset: 0x00075716
		public string RequestAction
		{
			get
			{
				return this.m_requestAction;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x0007751E File Offset: 0x0007571E
		public Message ReceivedMessage
		{
			get
			{
				return this.m_receivedMsg;
			}
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x000036A9 File Offset: 0x000018A9
		public void Accept()
		{
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x000036A9 File Offset: 0x000018A9
		public void Reject()
		{
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x00077526 File Offset: 0x00075726
		private void PrepareReplyMessage(Message msg)
		{
			msg.Headers.RelatesTo = this.m_requestMessageId;
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x00077539 File Offset: 0x00075739
		public void Reply(Message msg)
		{
			this.Reply(msg, TimeSpan.MaxValue);
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x00077547 File Offset: 0x00075747
		public void Reply(Message msg, TimeSpan timeout)
		{
			this.PrepareReplyMessage(msg);
			this.m_outputChannel.Send(msg, timeout);
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x0007755D File Offset: 0x0007575D
		public IAsyncResult BeginReply(Message msg, AsyncCallback callback, object state)
		{
			return this.BeginReply(msg, TimeSpan.MaxValue, callback, state);
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x0007756D File Offset: 0x0007576D
		public IAsyncResult BeginReply(Message msg, TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.PrepareReplyMessage(msg);
			return this.m_outputChannel.BeginSend(msg, timeout, callback, state);
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x00077586 File Offset: 0x00075786
		public void EndReply(IAsyncResult ar)
		{
			this.m_outputChannel.EndSend(ar);
		}

		// Token: 0x0400171B RID: 5915
		private Message m_receivedMsg;

		// Token: 0x0400171C RID: 5916
		private UniqueId m_requestMessageId;

		// Token: 0x0400171D RID: 5917
		private string m_requestAction;

		// Token: 0x0400171E RID: 5918
		private IInputConnection m_inputConnection;

		// Token: 0x0400171F RID: 5919
		private IOutputChannel m_outputChannel;
	}
}

using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000449 RID: 1097
	internal class MessageListener : TcpListener, IMessageListener, IListener, ITransportObject
	{
		// Token: 0x0600265F RID: 9823 RVA: 0x00075880 File Offset: 0x00073A80
		public MessageListener(IChannelListener<IDuplexSessionChannel> channelListener)
			: base(channelListener)
		{
			this.m_lock = new object();
			this.m_openSessions = new List<IInputSession>();
			this.m_defaultDispatcher = null;
			this.m_filterTable = new MessageFilterTable<MessageListener.MessageCallbackData>();
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x000758B4 File Offset: 0x00073AB4
		protected override bool OnClose()
		{
			try
			{
				base.WCFObject.Close();
			}
			catch (Exception ex)
			{
				if (!Utility.IsCommunicationException(ex))
				{
					throw;
				}
			}
			List<IInputSession> openSessions;
			lock (this.m_lock)
			{
				openSessions = this.m_openSessions;
				this.m_openSessions = null;
			}
			if (openSessions.Count > 0)
			{
				TransportsContext<IInputSession> transportsContext = new TransportsContext<IInputSession>(new AsyncCallback(MessageListener.StaticCloseCallback), this, TimeSpan.MaxValue);
				transportsContext.BeginClose(openSessions);
				if (!transportsContext.CompletedSynchronously)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x00075950 File Offset: 0x00073B50
		private static void StaticCloseCallback(IAsyncResult ar)
		{
			MessageListener messageListener = (MessageListener)ar.AsyncState;
			messageListener.CloseCallback(ar);
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x00075970 File Offset: 0x00073B70
		private void CloseCallback(IAsyncResult ar)
		{
			TransportsContext<IInputSession> transportsContext = (TransportsContext<IInputSession>)ar;
			Exception ex = null;
			try
			{
				transportsContext.EndClose(ar);
			}
			catch (Exception ex2)
			{
				if (ar.CompletedSynchronously)
				{
					throw;
				}
				ex = ex2;
			}
			if (!ar.CompletedSynchronously)
			{
				base.OnCloseCompleted(ex);
			}
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x000759C0 File Offset: 0x00073BC0
		private MessageFilterTable<MessageListener.MessageCallbackData> CopyFilterTable()
		{
			MessageFilterTable<MessageListener.MessageCallbackData> messageFilterTable = new MessageFilterTable<MessageListener.MessageCallbackData>();
			foreach (KeyValuePair<MessageFilter, MessageListener.MessageCallbackData> keyValuePair in this.m_filterTable)
			{
				messageFilterTable.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return messageFilterTable;
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x00075A24 File Offset: 0x00073C24
		public void RegisterFilter(string actionPrefix, ProcessReceivedMessage callback, object state)
		{
			MessageListener.MessageCallbackData messageCallbackData = new MessageListener.MessageCallbackData(callback, state);
			lock (this.m_lock)
			{
				if (actionPrefix == null)
				{
					this.m_defaultDispatcher = messageCallbackData;
				}
				else
				{
					ActionPrefixFilter actionPrefixFilter = new ActionPrefixFilter(new string[] { actionPrefix });
					MessageFilterTable<MessageListener.MessageCallbackData> messageFilterTable = this.CopyFilterTable();
					messageFilterTable.Add(actionPrefixFilter, messageCallbackData);
					this.m_filterTable = messageFilterTable;
				}
			}
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x00075A98 File Offset: 0x00073C98
		public bool UnregisterFilter(string actionPrefix)
		{
			bool flag2;
			lock (this.m_lock)
			{
				bool flag;
				if (actionPrefix == null)
				{
					flag = this.m_defaultDispatcher != null;
					this.m_defaultDispatcher = null;
				}
				else
				{
					MessageFilterTable<MessageListener.MessageCallbackData> messageFilterTable = this.CopyFilterTable();
					flag = messageFilterTable.Remove(new ActionPrefixFilter(new string[] { actionPrefix }));
					this.m_filterTable = messageFilterTable;
				}
				flag2 = flag;
			}
			return flag2;
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x00075B10 File Offset: 0x00073D10
		protected override void OnOpened(Exception openException)
		{
			base.OnOpened(openException);
			if (openException == null)
			{
				IAsyncResult asyncResult = this.SafeBeginAccept();
				if (asyncResult != null && asyncResult.CompletedSynchronously)
				{
					this.CompleteAccept(asyncResult);
				}
			}
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x00075B40 File Offset: 0x00073D40
		private static void StaticAcceptCallback(IAsyncResult ar)
		{
			if (ar.CompletedSynchronously)
			{
				return;
			}
			MessageListener messageListener = (MessageListener)ar.AsyncState;
			messageListener.CompleteAccept(ar);
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x00075B6C File Offset: 0x00073D6C
		private void CompleteAccept(IAsyncResult ar)
		{
			IInputSession inputSession = this.SafeEndAccept(ar);
			if (inputSession != null)
			{
				for (;;)
				{
					ar = this.SafeBeginAccept();
					if (ar == null || !ar.CompletedSynchronously)
					{
						break;
					}
					IInputSession inputSession2 = this.SafeEndAccept(ar);
					if (inputSession2 == null)
					{
						break;
					}
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.OpenInputSessionAsynchronously), inputSession2);
				}
				this.OpenInputSession(inputSession);
			}
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x00075BC0 File Offset: 0x00073DC0
		private IAsyncResult SafeBeginAccept()
		{
			IAsyncResult asyncResult;
			try
			{
				asyncResult = base.BeginAccept(TimeSpan.MaxValue, new AsyncCallback(MessageListener.StaticAcceptCallback), this);
			}
			catch (Exception ex)
			{
				if (!Utility.IsCommunicationException(ex))
				{
					throw;
				}
				asyncResult = null;
			}
			return asyncResult;
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x00075C08 File Offset: 0x00073E08
		private IInputSession SafeEndAccept(IAsyncResult ar)
		{
			IInputSession inputSession;
			try
			{
				inputSession = base.EndAccept(ar);
			}
			catch (Exception ex)
			{
				if (!Utility.IsCommunicationException(ex))
				{
					throw;
				}
				inputSession = null;
			}
			return inputSession;
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x00075C40 File Offset: 0x00073E40
		private void OpenInputSessionAsynchronously(object state)
		{
			this.OpenInputSession((IInputSession)state);
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x00075C50 File Offset: 0x00073E50
		private void OpenInputSession(IInputSession inputSession)
		{
			try
			{
				IAsyncResult asyncResult = inputSession.BeginOpen(TimeSpan.MaxValue, new AsyncCallback(this.CompleteOpenCallback), inputSession);
				if (asyncResult.CompletedSynchronously)
				{
					this.CompleteOpen(asyncResult);
				}
			}
			catch (Exception ex)
			{
				if (!Utility.IsCommunicationException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x00075CA4 File Offset: 0x00073EA4
		private void CompleteOpenCallback(IAsyncResult ar)
		{
			if (ar.CompletedSynchronously)
			{
				return;
			}
			this.CompleteOpen(ar);
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x00075CB8 File Offset: 0x00073EB8
		private void CompleteOpen(IAsyncResult ar)
		{
			IInputSession inputSession = (IInputSession)ar.AsyncState;
			try
			{
				inputSession.EndOpen(ar);
			}
			catch (Exception ex)
			{
				if (!Utility.IsCommunicationException(ex))
				{
					throw;
				}
				return;
			}
			this.StartListening(inputSession);
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x00075D00 File Offset: 0x00073F00
		private void StartListening(IInputSession inputSession)
		{
			bool flag = false;
			lock (this.m_lock)
			{
				flag = this.m_openSessions == null;
				if (!flag)
				{
					this.m_openSessions.Add(inputSession);
				}
			}
			if (flag)
			{
				ReleaseAssert.IsTrue(base.IsDead);
				TransportObject.CloseTransportAsynchronously(inputSession);
				return;
			}
			IAsyncResult asyncResult = this.SafeBeginReceive(inputSession);
			if (asyncResult != null && asyncResult.CompletedSynchronously)
			{
				this.CompleteMessageReceived(asyncResult);
			}
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x00075D80 File Offset: 0x00073F80
		private void MessageReceivedCallback(IAsyncResult ar)
		{
			if (ar.CompletedSynchronously)
			{
				return;
			}
			this.CompleteMessageReceived(ar);
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x00075D94 File Offset: 0x00073F94
		private void CompleteMessageReceived(IAsyncResult ar)
		{
			IInputSession inputSession = (IInputSession)ar.AsyncState;
			IInputContext inputContext = this.SafeEndReceive(inputSession, ar);
			if (inputContext != null)
			{
				for (;;)
				{
					ar = this.SafeBeginReceive(inputSession);
					if (ar == null || !ar.CompletedSynchronously)
					{
						break;
					}
					IInputContext inputContext2 = this.SafeEndReceive(inputSession, ar);
					if (inputContext2 == null)
					{
						break;
					}
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.DispatchMessageCallback), inputContext2);
				}
				this.DispatchMessage(inputContext);
			}
		}

		// Token: 0x06002672 RID: 9842 RVA: 0x00075DF8 File Offset: 0x00073FF8
		private IAsyncResult SafeBeginReceive(IInputSession inputSession)
		{
			IAsyncResult asyncResult;
			try
			{
				asyncResult = inputSession.BeginReceive(TimeSpan.MaxValue, new AsyncCallback(this.MessageReceivedCallback), inputSession);
			}
			catch (Exception ex)
			{
				if (!Utility.IsCommunicationException(ex))
				{
					throw;
				}
				this.CloseSession(inputSession);
				asyncResult = null;
			}
			return asyncResult;
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x00075E48 File Offset: 0x00074048
		private IInputContext SafeEndReceive(IInputSession inputSession, IAsyncResult ar)
		{
			IInputContext inputContext;
			try
			{
				inputContext = inputSession.EndReceive(ar);
				if (inputContext == null)
				{
					this.CloseSession(inputSession);
				}
			}
			catch (Exception ex)
			{
				if (!Utility.IsCommunicationException(ex))
				{
					throw;
				}
				this.CloseSession(inputSession);
				inputContext = null;
			}
			return inputContext;
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x00075E90 File Offset: 0x00074090
		public void CloseInputConnection(IInputContext inputContext)
		{
			IInputSession inputSession = inputContext.InputConnection as IInputSession;
			if (inputSession != null)
			{
				this.CloseSession(inputSession);
			}
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x00075EB4 File Offset: 0x000740B4
		private void CloseSession(IInputSession inputSession)
		{
			bool flag = false;
			lock (this.m_lock)
			{
				if (this.m_openSessions != null)
				{
					flag = this.m_openSessions.Remove(inputSession);
				}
			}
			if (flag)
			{
				TransportObject.CloseTransportAsynchronously(inputSession);
			}
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x00075F08 File Offset: 0x00074108
		private void DispatchMessageCallback(object state)
		{
			IInputContext inputContext = (IInputContext)state;
			this.DispatchMessage(inputContext);
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x00075F24 File Offset: 0x00074124
		private void DispatchMessage(IInputContext inputContext)
		{
			Message receivedMessage = inputContext.ReceivedMessage;
			if (receivedMessage.Headers.Action == "ping")
			{
				try
				{
					inputContext.BeginReply(receivedMessage, MessageListener.PingReplyCallback, inputContext);
				}
				catch (Exception ex)
				{
					if (!Utility.IsCommunicationException(ex))
					{
						throw;
					}
				}
				return;
			}
			MessageListener.MessageCallbackData defaultDispatcher = this.m_defaultDispatcher;
			if (defaultDispatcher != null)
			{
				defaultDispatcher.Callback(inputContext, defaultDispatcher.State);
				return;
			}
			try
			{
				this.m_filterTable.GetMatchingValue(receivedMessage, out defaultDispatcher);
			}
			catch (Exception ex2)
			{
				ReleaseAssert.IsTrue(!(ex2 is MultipleFilterMatchesException));
				throw;
			}
			if (defaultDispatcher != null)
			{
				defaultDispatcher.Callback(inputContext, defaultDispatcher.State);
			}
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x00075FE0 File Offset: 0x000741E0
		private static void StaticPingReplyCallback(IAsyncResult ar)
		{
			IInputContext inputContext = (IInputContext)ar.AsyncState;
			try
			{
				inputContext.EndReply(ar);
			}
			catch (Exception ex)
			{
				if (!Utility.IsCommunicationException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x040016EC RID: 5868
		private object m_lock;

		// Token: 0x040016ED RID: 5869
		private List<IInputSession> m_openSessions;

		// Token: 0x040016EE RID: 5870
		private MessageListener.MessageCallbackData m_defaultDispatcher;

		// Token: 0x040016EF RID: 5871
		private MessageFilterTable<MessageListener.MessageCallbackData> m_filterTable;

		// Token: 0x040016F0 RID: 5872
		private static AsyncCallback PingReplyCallback = new AsyncCallback(MessageListener.StaticPingReplyCallback);

		// Token: 0x0200044A RID: 1098
		private class MessageCallbackData
		{
			// Token: 0x0600267A RID: 9850 RVA: 0x00076033 File Offset: 0x00074233
			public MessageCallbackData(ProcessReceivedMessage callback, object state)
			{
				this.m_callback = callback;
				this.m_state = state;
			}

			// Token: 0x17000789 RID: 1929
			// (get) Token: 0x0600267B RID: 9851 RVA: 0x00076049 File Offset: 0x00074249
			public ProcessReceivedMessage Callback
			{
				get
				{
					return this.m_callback;
				}
			}

			// Token: 0x1700078A RID: 1930
			// (get) Token: 0x0600267C RID: 9852 RVA: 0x00076051 File Offset: 0x00074251
			public object State
			{
				get
				{
					return this.m_state;
				}
			}

			// Token: 0x040016F1 RID: 5873
			private ProcessReceivedMessage m_callback;

			// Token: 0x040016F2 RID: 5874
			private object m_state;
		}
	}
}

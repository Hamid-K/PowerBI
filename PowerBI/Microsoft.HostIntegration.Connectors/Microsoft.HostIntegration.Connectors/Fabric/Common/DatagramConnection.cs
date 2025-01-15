using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000442 RID: 1090
	internal class DatagramConnection : TransportObject, IOutputConnection, ITransportConnection, ITransportObject, IDatagramConnection
	{
		// Token: 0x06002614 RID: 9748 RVA: 0x00074A6A File Offset: 0x00072C6A
		internal DatagramConnection(Uri remoteAddress, TcpTransportFactory factory)
		{
			this.m_lock = new object();
			this.m_remoteAddress = remoteAddress;
			this.m_factory = factory;
			this.m_tcpSession = null;
			this.m_isPresentInHashTable = true;
			this.m_datagramMessages = null;
			this.m_datagramRequests = null;
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x00074AA7 File Offset: 0x00072CA7
		// (set) Token: 0x06002616 RID: 9750 RVA: 0x00074AAF File Offset: 0x00072CAF
		public IMessageHandler Handler
		{
			get
			{
				return this.m_handler;
			}
			set
			{
				this.m_handler = value;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06002617 RID: 9751 RVA: 0x00074AB8 File Offset: 0x00072CB8
		private TcpOutputSession ActiveTcpSession
		{
			get
			{
				TcpOutputSession tcpSession = this.m_tcpSession;
				if (tcpSession == null || tcpSession.State != TransportState.Opened)
				{
					return null;
				}
				return tcpSession;
			}
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x00074ADC File Offset: 0x00072CDC
		private TcpOutputSession ResetTcpSession()
		{
			TcpOutputSession tcpOutputSession = this.m_tcpSession;
			while (tcpOutputSession != null)
			{
				TcpOutputSession tcpOutputSession2 = tcpOutputSession;
				tcpOutputSession = Interlocked.CompareExchange<TcpOutputSession>(ref this.m_tcpSession, null, tcpOutputSession2);
				if (tcpOutputSession == tcpOutputSession2)
				{
					break;
				}
			}
			return tcpOutputSession;
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x00002B16 File Offset: 0x00000D16
		protected override bool OnOpen()
		{
			return true;
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x00074B08 File Offset: 0x00072D08
		protected override bool InitiateClose()
		{
			ReleaseAssert.IsTrue(this.m_isPresentInHashTable);
			if (!this.m_factory.ReleaseDatagramConnection(this, false))
			{
				return false;
			}
			this.m_isPresentInHashTable = false;
			return base.InitiateClose();
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x00002B16 File Offset: 0x00000D16
		protected override bool OnClose()
		{
			return true;
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x00074B34 File Offset: 0x00072D34
		protected override void OnAbort()
		{
			EventLogWriter.WriteInfo("DatagramConnection", "Aborting datagram connection to {0}", new object[] { this.m_remoteAddress });
			TcpOutputSession tcpOutputSession = this.ResetTcpSession();
			if (tcpOutputSession != null)
			{
				tcpOutputSession.Abort();
			}
			base.OnAbort();
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x00074B78 File Offset: 0x00072D78
		protected override void OnCleanup(Exception faultingException)
		{
			if (this.m_isPresentInHashTable)
			{
				this.m_factory.ReleaseDatagramConnection(this, true);
				this.m_isPresentInHashTable = false;
			}
			EventLogWriter.WriteInfo("DatagramConnection", "Closing datagram connection to {0}", new object[] { this.m_remoteAddress });
			TcpOutputSession tcpOutputSession = this.ResetTcpSession();
			DatagramConnection.CloseSession(tcpOutputSession);
			base.OnCleanup(faultingException);
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x0600261E RID: 9758 RVA: 0x00074BD6 File Offset: 0x00072DD6
		public Uri RemoteAddress
		{
			get
			{
				return this.m_remoteAddress;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x0600261F RID: 9759 RVA: 0x000189CC File Offset: 0x00016BCC
		public Uri LocalAddress
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x00074BE0 File Offset: 0x00072DE0
		private void SessionEventHandler(object sender, EventArgs args)
		{
			if ((args == TransportObject.FaultedEvent || args == TransportObject.ClosedEvent) && this.m_tcpSession == sender)
			{
				EventLogWriter.WriteInfo("DatagramConnection", "Nested channel {0} {1}", new object[]
				{
					this.m_remoteAddress,
					(args == TransportObject.FaultedEvent) ? "faulted" : "closed"
				});
				TcpOutputSession tcpOutputSession = (TcpOutputSession)sender;
				Interlocked.CompareExchange<TcpOutputSession>(ref this.m_tcpSession, null, tcpOutputSession);
			}
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x00074C52 File Offset: 0x00072E52
		private void RegisterForStateChange(TcpOutputSession tcpSession)
		{
			tcpSession.StateChange += this.SessionEventHandler;
			if (tcpSession.State == TransportState.Closed)
			{
				this.SessionEventHandler(tcpSession, TransportObject.ClosedEvent);
				return;
			}
			if (tcpSession.State == TransportState.Faulted)
			{
				this.SessionEventHandler(tcpSession, TransportObject.FaultedEvent);
			}
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x00074C94 File Offset: 0x00072E94
		private void CreateSession()
		{
			EventLogWriter.WriteInfo("DatagramConnection", "Trying to open nested channel {0}", new object[] { this.m_remoteAddress });
			Uri uri;
			if (this.m_remoteAddress.Scheme != this.m_factory.Scheme)
			{
				uri = new UriBuilder(this.m_remoteAddress)
				{
					Scheme = this.m_factory.Scheme
				}.Uri;
			}
			else
			{
				uri = this.m_remoteAddress;
			}
			TcpOutputSession tcpOutputSession = (TcpOutputSession)this.m_factory.CreateOutputSession(uri);
			tcpOutputSession.Handler = this.m_handler;
			tcpOutputSession.BeginOpen(new AsyncCallback(this.OpenCallback), tcpOutputSession);
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x00074D40 File Offset: 0x00072F40
		private void OpenCallback(IAsyncResult ar)
		{
			ReleaseAssert.IsTrue(this.m_isOpening);
			TcpOutputSession tcpOutputSession = (TcpOutputSession)ar.AsyncState;
			Exception ex = null;
			try
			{
				tcpOutputSession.EndOpen(ar);
				EventLogWriter.WriteInfo("DatagramConnection", "Nested channel {0} opened", new object[] { this.m_remoteAddress });
				if (!base.IsDead)
				{
					this.m_tcpSession = tcpOutputSession;
					this.RegisterForStateChange(tcpOutputSession);
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
				EventLogWriter.WriteInfo("DatagramConnection", "Nested channel {0} open failed {1}", new object[] { this.m_remoteAddress, ex2 });
			}
			this.m_isOpening = false;
			if (ex == null)
			{
				if (base.IsDead)
				{
					ex = base.CreateDeadTransportException();
					if (Interlocked.CompareExchange<TcpOutputSession>(ref this.m_tcpSession, null, tcpOutputSession) == tcpOutputSession)
					{
						DatagramConnection.CloseSession(tcpOutputSession);
					}
					tcpOutputSession = null;
				}
			}
			else
			{
				tcpOutputSession = null;
			}
			this.ProcessDatagramMessages(ar.CompletedSynchronously, ex, tcpOutputSession);
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x00074E28 File Offset: 0x00073028
		private void ProcessDatagramMessages(bool completedSynchronously, Exception openException, IOutputSession tcpSession)
		{
			ReleaseAssert.IsTrue((openException == null) ^ (tcpSession == null));
			List<DatagramConnection.DatagramMessageContext> datagramMessages;
			List<DatagramConnection.DatagramMessageContext> datagramRequests;
			lock (this.m_lock)
			{
				datagramMessages = this.m_datagramMessages;
				this.m_datagramMessages = null;
				datagramRequests = this.m_datagramRequests;
				this.m_datagramRequests = null;
			}
			if (datagramMessages != null)
			{
				foreach (DatagramConnection.DatagramMessageContext datagramMessageContext in datagramMessages)
				{
					if (!datagramMessageContext.IsCompleted)
					{
						bool flag = completedSynchronously && datagramMessageContext.CreatingThreadId == Thread.CurrentThread.ManagedThreadId;
						if (openException == null)
						{
							datagramMessageContext.CompletedSynchronously = flag;
							try
							{
								DatagramConnection.BeginSend(tcpSession, datagramMessageContext);
								continue;
							}
							catch (Exception ex)
							{
								datagramMessageContext.CompleteOperation(flag, ex);
								continue;
							}
						}
						datagramMessageContext.CompleteOperation(flag, openException);
					}
				}
			}
			if (datagramRequests != null)
			{
				foreach (DatagramConnection.DatagramMessageContext datagramMessageContext2 in datagramRequests)
				{
					if (!datagramMessageContext2.IsCompleted)
					{
						bool flag2 = completedSynchronously && datagramMessageContext2.CreatingThreadId == Thread.CurrentThread.ManagedThreadId;
						if (openException == null)
						{
							datagramMessageContext2.CompletedSynchronously = flag2;
							try
							{
								DatagramConnection.BeginSendReceive(tcpSession, datagramMessageContext2);
								continue;
							}
							catch (Exception ex2)
							{
								datagramMessageContext2.CompleteOperation(flag2, ex2);
								continue;
							}
						}
						datagramMessageContext2.CompleteOperation(flag2, openException);
					}
				}
			}
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x00074FB0 File Offset: 0x000731B0
		protected static void CloseSession(IOutputSession tcpSession)
		{
			if (tcpSession != null)
			{
				tcpSession.BeginClose(new AsyncCallback(DatagramConnection.StaticCloseCallback), tcpSession);
			}
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x00074FCC File Offset: 0x000731CC
		private static void StaticCloseCallback(IAsyncResult ar)
		{
			IOutputSession outputSession = (IOutputSession)ar.AsyncState;
			try
			{
				outputSession.EndClose(ar);
			}
			catch (Exception ex)
			{
				if (!Utility.IsCommunicationException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x0007500C File Offset: 0x0007320C
		private DatagramConnection.DatagramMessageContext EnqueueDatagramMessage(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			bool isOpening;
			DatagramConnection.DatagramMessageContext datagramMessageContext;
			lock (this.m_lock)
			{
				isOpening = this.m_isOpening;
				if (this.ActiveTcpSession != null)
				{
					return null;
				}
				if (!isOpening)
				{
					this.m_isOpening = true;
				}
				if (this.m_datagramMessages == null)
				{
					this.m_datagramMessages = new List<DatagramConnection.DatagramMessageContext>();
				}
				datagramMessageContext = new DatagramConnection.DatagramMessageContext(message, timeout, callback, state);
				this.m_datagramMessages.Add(datagramMessageContext);
			}
			if (!isOpening)
			{
				this.CreateSession();
			}
			return datagramMessageContext;
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x00075094 File Offset: 0x00073294
		private DatagramConnection.DatagramMessageContext EnqueueDatagramRequest(Message request, TimeSpan timeout, AsyncCallback callback, object state)
		{
			bool isOpening;
			DatagramConnection.DatagramMessageContext datagramMessageContext;
			lock (this.m_lock)
			{
				isOpening = this.m_isOpening;
				if (this.ActiveTcpSession != null)
				{
					return null;
				}
				if (!isOpening)
				{
					this.m_isOpening = true;
				}
				if (this.m_datagramRequests == null)
				{
					this.m_datagramRequests = new List<DatagramConnection.DatagramMessageContext>();
				}
				datagramMessageContext = new DatagramConnection.DatagramMessageContext(request, timeout, callback, state);
				this.m_datagramRequests.Add(datagramMessageContext);
			}
			if (!isOpening)
			{
				this.CreateSession();
			}
			return datagramMessageContext;
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x0007511C File Offset: 0x0007331C
		public void Send(Message message)
		{
			this.Send(message, TimeSpan.MaxValue);
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x0007512C File Offset: 0x0007332C
		public void Send(Message message, TimeSpan timeout)
		{
			IOutputSession activeTcpSession = this.ActiveTcpSession;
			if (activeTcpSession != null)
			{
				activeTcpSession.Send(message, timeout);
				return;
			}
			this.EndSend(this.BeginSend(message, timeout, null, null));
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x0007515C File Offset: 0x0007335C
		public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
		{
			return this.BeginSend(message, TimeSpan.MaxValue, callback, state);
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x0007516C File Offset: 0x0007336C
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			DatagramConnection.DatagramMessageContext datagramMessageContext;
			TransportState state2;
			for (;;)
			{
				IOutputSession activeTcpSession = this.ActiveTcpSession;
				if (activeTcpSession != null)
				{
					datagramMessageContext = new DatagramConnection.DatagramMessageContext(message, timeout, callback, state);
					try
					{
						DatagramConnection.BeginSend(activeTcpSession, datagramMessageContext);
						return datagramMessageContext;
					}
					catch (Exception ex)
					{
						datagramMessageContext.CompleteOperation(true, ex);
						return datagramMessageContext;
					}
				}
				state2 = base.State;
				if (state2 != TransportState.Opened)
				{
					break;
				}
				datagramMessageContext = this.EnqueueDatagramMessage(message, timeout, callback, state);
				if (datagramMessageContext != null)
				{
					goto Block_3;
				}
			}
			datagramMessageContext = new DatagramConnection.DatagramMessageContext(message, timeout, callback, state);
			datagramMessageContext.CompleteOperation(true, base.CreateInvalidTransportException(state2));
			return datagramMessageContext;
			Block_3:
			datagramMessageContext.StartTimer();
			return datagramMessageContext;
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x000751F0 File Offset: 0x000733F0
		private static void BeginSend(IOutputSession tcpSession, DatagramConnection.DatagramMessageContext datagramContext)
		{
			datagramContext.TcpSession = tcpSession;
			datagramContext.InnerAsyncResult = tcpSession.BeginSend(datagramContext.DatagramMessage, datagramContext.ExpirationTime.SafeRemainingDuration, DatagramConnection.SendCallback, datagramContext);
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x0007522C File Offset: 0x0007342C
		private static void StaticSendCallback(IAsyncResult ar)
		{
			DatagramConnection.DatagramMessageContext datagramMessageContext = (DatagramConnection.DatagramMessageContext)ar.AsyncState;
			datagramMessageContext.InnerAsyncResult = ar;
			Exception ex = null;
			try
			{
				datagramMessageContext.TcpSession.EndSend(ar);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			datagramMessageContext.CompleteOperation(ar.CompletedSynchronously, ex);
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x00075280 File Offset: 0x00073480
		private static void StaticSendReceiveCallback(IAsyncResult ar)
		{
			DatagramConnection.DatagramMessageContext datagramMessageContext = (DatagramConnection.DatagramMessageContext)ar.AsyncState;
			datagramMessageContext.InnerAsyncResult = ar;
			Exception ex = null;
			try
			{
				datagramMessageContext.ReceivedMessage = datagramMessageContext.TcpSession.EndSendReceive(ar);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			datagramMessageContext.CompleteOperation(ar.CompletedSynchronously, ex);
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x000752D8 File Offset: 0x000734D8
		public void EndSend(IAsyncResult ar)
		{
			DatagramConnection.DatagramMessageContext datagramMessageContext = (DatagramConnection.DatagramMessageContext)ar;
			datagramMessageContext.End();
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x000752F2 File Offset: 0x000734F2
		public Message SendReceive(Message message)
		{
			return this.SendReceive(message, TimeSpan.MaxValue);
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x00075300 File Offset: 0x00073500
		public Message SendReceive(Message message, TimeSpan timeout)
		{
			IOutputSession activeTcpSession = this.ActiveTcpSession;
			Message message2;
			if (activeTcpSession != null)
			{
				message2 = activeTcpSession.SendReceive(message, timeout);
			}
			else
			{
				message2 = this.EndSendReceive(this.BeginSendReceive(message, null, null));
			}
			return message2;
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x00075333 File Offset: 0x00073533
		public IAbortableAsyncResult BeginSendReceive(Message message, AsyncCallback callback, object state)
		{
			return this.BeginSendReceive(message, TimeSpan.MaxValue, callback, state);
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x00075344 File Offset: 0x00073544
		public IAbortableAsyncResult BeginSendReceive(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			DatagramConnection.DatagramMessageContext datagramMessageContext;
			TransportState state2;
			for (;;)
			{
				IOutputSession activeTcpSession = this.ActiveTcpSession;
				if (activeTcpSession != null)
				{
					datagramMessageContext = new DatagramConnection.DatagramMessageContext(message, timeout, callback, state);
					try
					{
						DatagramConnection.BeginSendReceive(activeTcpSession, datagramMessageContext);
						return datagramMessageContext;
					}
					catch (Exception ex)
					{
						datagramMessageContext.CompleteOperation(true, ex);
						return datagramMessageContext;
					}
				}
				state2 = base.State;
				if (state2 != TransportState.Opened)
				{
					break;
				}
				datagramMessageContext = this.EnqueueDatagramRequest(message, timeout, callback, state);
				if (datagramMessageContext != null)
				{
					goto Block_3;
				}
			}
			datagramMessageContext = new DatagramConnection.DatagramMessageContext(message, timeout, callback, state);
			datagramMessageContext.CompleteOperation(true, base.CreateInvalidTransportException(state2));
			return datagramMessageContext;
			Block_3:
			datagramMessageContext.StartTimer();
			return datagramMessageContext;
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x000753C8 File Offset: 0x000735C8
		private static void BeginSendReceive(IOutputSession tcpSession, DatagramConnection.DatagramMessageContext datagramContext)
		{
			datagramContext.TcpSession = tcpSession;
			datagramContext.InnerAsyncResult = tcpSession.BeginSendReceive(datagramContext.DatagramMessage, datagramContext.ExpirationTime.SafeRemainingDuration, DatagramConnection.SendReceiveCallback, datagramContext);
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x00075404 File Offset: 0x00073604
		public Message EndSendReceive(IAsyncResult ar)
		{
			DatagramConnection.DatagramMessageContext datagramMessageContext = (DatagramConnection.DatagramMessageContext)ar;
			datagramMessageContext.End();
			return datagramMessageContext.ReceivedMessage;
		}

		// Token: 0x040016DA RID: 5850
		private const string LogSource = "DatagramConnection";

		// Token: 0x040016DB RID: 5851
		private object m_lock;

		// Token: 0x040016DC RID: 5852
		private Uri m_remoteAddress;

		// Token: 0x040016DD RID: 5853
		private TcpTransportFactory m_factory;

		// Token: 0x040016DE RID: 5854
		private bool m_isOpening;

		// Token: 0x040016DF RID: 5855
		private TcpOutputSession m_tcpSession;

		// Token: 0x040016E0 RID: 5856
		private IMessageHandler m_handler;

		// Token: 0x040016E1 RID: 5857
		private bool m_isPresentInHashTable;

		// Token: 0x040016E2 RID: 5858
		private List<DatagramConnection.DatagramMessageContext> m_datagramMessages;

		// Token: 0x040016E3 RID: 5859
		private List<DatagramConnection.DatagramMessageContext> m_datagramRequests;

		// Token: 0x040016E4 RID: 5860
		private static AsyncCallback SendCallback = new AsyncCallback(DatagramConnection.StaticSendCallback);

		// Token: 0x040016E5 RID: 5861
		private static AsyncCallback SendReceiveCallback = new AsyncCallback(DatagramConnection.StaticSendReceiveCallback);

		// Token: 0x02000443 RID: 1091
		private class DatagramMessageContext : OperationContext
		{
			// Token: 0x06002638 RID: 9784 RVA: 0x00075448 File Offset: 0x00073648
			public DatagramMessageContext(Message datagramMessage, TimeSpan timeout, AsyncCallback callback, object state)
				: base(callback, state, timeout)
			{
				this.m_datagramMessage = datagramMessage;
				this.m_receivedMessage = null;
				this.m_tcpSession = null;
				this.m_innerResult = null;
			}

			// Token: 0x17000781 RID: 1921
			// (get) Token: 0x06002639 RID: 9785 RVA: 0x00075470 File Offset: 0x00073670
			public Message DatagramMessage
			{
				get
				{
					return this.m_datagramMessage;
				}
			}

			// Token: 0x17000782 RID: 1922
			// (get) Token: 0x0600263A RID: 9786 RVA: 0x00075478 File Offset: 0x00073678
			// (set) Token: 0x0600263B RID: 9787 RVA: 0x00075480 File Offset: 0x00073680
			public Message ReceivedMessage
			{
				get
				{
					return this.m_receivedMessage;
				}
				set
				{
					this.m_receivedMessage = value;
				}
			}

			// Token: 0x17000783 RID: 1923
			// (get) Token: 0x0600263C RID: 9788 RVA: 0x00075489 File Offset: 0x00073689
			// (set) Token: 0x0600263D RID: 9789 RVA: 0x00075491 File Offset: 0x00073691
			public IOutputSession TcpSession
			{
				get
				{
					return this.m_tcpSession;
				}
				set
				{
					this.m_tcpSession = value;
				}
			}

			// Token: 0x17000784 RID: 1924
			// (set) Token: 0x0600263E RID: 9790 RVA: 0x0007549A File Offset: 0x0007369A
			public IAsyncResult InnerAsyncResult
			{
				set
				{
					this.m_innerResult = value;
				}
			}

			// Token: 0x0600263F RID: 9791 RVA: 0x000754A4 File Offset: 0x000736A4
			public override void Abort(Exception exception)
			{
				IAbortableAsyncResult abortableAsyncResult = this.m_innerResult as IAbortableAsyncResult;
				if (abortableAsyncResult != null)
				{
					abortableAsyncResult.Abort(exception);
				}
				base.Abort(exception);
			}

			// Token: 0x040016E6 RID: 5862
			private Message m_datagramMessage;

			// Token: 0x040016E7 RID: 5863
			private IOutputSession m_tcpSession;

			// Token: 0x040016E8 RID: 5864
			private IAsyncResult m_innerResult;

			// Token: 0x040016E9 RID: 5865
			private Message m_receivedMessage;
		}
	}
}

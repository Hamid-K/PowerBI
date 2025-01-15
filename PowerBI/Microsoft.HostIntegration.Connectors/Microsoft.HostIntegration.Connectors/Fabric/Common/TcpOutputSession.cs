using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Xml;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000450 RID: 1104
	internal class TcpOutputSession : SafeCommunicationObject<IDuplexSessionChannel>, IOutputSession, IOutputConnection, ISession, ITransportConnection, ITransportObject
	{
		// Token: 0x0600268E RID: 9870 RVA: 0x00076130 File Offset: 0x00074330
		public TcpOutputSession(Uri remoteAddress, TcpTransportFactory factory)
			: base(factory.CreateChannel(remoteAddress))
		{
			this.m_factory = factory;
			this.m_requestHashTable = new Dictionary<UniqueId, RequestContext>();
			this.m_requestHashTableLock = new object();
			this.m_remoteAddress = remoteAddress;
			this.m_retryCount = 0;
			this.m_pingTimer = new Timer(TcpOutputSession.PingTimerCallback, this);
			this.m_pingNecessary = true;
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x0600268F RID: 9871 RVA: 0x00076062 File Offset: 0x00074262
		private IDuplexSessionChannel Channel
		{
			get
			{
				return base.WCFObject;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002690 RID: 9872 RVA: 0x0007618D File Offset: 0x0007438D
		// (set) Token: 0x06002691 RID: 9873 RVA: 0x00076195 File Offset: 0x00074395
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

		// Token: 0x06002692 RID: 9874 RVA: 0x0007619E File Offset: 0x0007439E
		protected override void OnOpened(Exception openException)
		{
			base.OnOpened(openException);
			if (openException == null)
			{
				this.StartPingTimer();
				this.StartListening();
			}
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x000761B6 File Offset: 0x000743B6
		private void StartPingTimer()
		{
			if (TcpOutputSession.PingInterval < TimeSpan.MaxValue)
			{
				this.m_pingTimer.Enqueue(TcpOutputSession.PingInterval);
			}
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000761DC File Offset: 0x000743DC
		protected override void OnCleanup(Exception faultingException)
		{
			Exception ex = faultingException;
			if (ex == null)
			{
				ex = new ObjectDisposedException(this.ToString());
			}
			this.CompleteOutstandingContexts(ex);
			base.OnCleanup(faultingException);
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x00076208 File Offset: 0x00074408
		private void CompleteOutstandingContexts(Exception faultingException)
		{
			RequestContext[] array;
			lock (this.m_requestHashTableLock)
			{
				int num = 0;
				array = new RequestContext[this.m_requestHashTable.Count];
				foreach (KeyValuePair<UniqueId, RequestContext> keyValuePair in this.m_requestHashTable)
				{
					array[num++] = keyValuePair.Value;
				}
				foreach (RequestContext requestContext in array)
				{
					requestContext.Remove();
				}
			}
			foreach (RequestContext requestContext2 in array)
			{
				requestContext2.CompleteOperation(false, faultingException);
			}
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000762E4 File Offset: 0x000744E4
		private static void StaticPingCallback(object state)
		{
			TcpOutputSession tcpOutputSession = (TcpOutputSession)state;
			tcpOutputSession.PingCallback();
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x00076300 File Offset: 0x00074500
		private void PingCallback()
		{
			if (this.m_pingNecessary)
			{
				try
				{
					Message message = Message.CreateMessage(MessageVersion.Default, "ping");
					this.BeginSend(message, TcpOutputSession.PingSendCallback, this);
					goto IL_003C;
				}
				catch (Exception ex)
				{
					if (!Utility.IsCommunicationException(ex))
					{
						throw;
					}
					goto IL_003C;
				}
			}
			this.m_pingNecessary = true;
			IL_003C:
			if (base.State == TransportState.Opened)
			{
				this.StartPingTimer();
			}
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x00076368 File Offset: 0x00074568
		private static void StaticPingSendCallback(IAsyncResult ar)
		{
			TcpOutputSession tcpOutputSession = (TcpOutputSession)ar.AsyncState;
			try
			{
				tcpOutputSession.EndSend(ar);
			}
			catch (Exception ex)
			{
				if (!Utility.IsCommunicationException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002699 RID: 9881 RVA: 0x000763A8 File Offset: 0x000745A8
		public Uri RemoteAddress
		{
			get
			{
				return this.m_remoteAddress;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x0600269A RID: 9882 RVA: 0x000763B0 File Offset: 0x000745B0
		public Uri LocalAddress
		{
			get
			{
				return this.Channel.LocalAddress.Uri;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x0600269B RID: 9883 RVA: 0x000763C2 File Offset: 0x000745C2
		public string Id
		{
			get
			{
				return this.Channel.Session.Id;
			}
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x000763D4 File Offset: 0x000745D4
		public void Send(Message message)
		{
			this.Send(message, TimeSpan.MaxValue);
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x000763E4 File Offset: 0x000745E4
		public void Send(Message message, TimeSpan timeout)
		{
			try
			{
				message.Properties.AllowOutputBatching = false;
				this.Channel.Send(message, timeout);
			}
			catch (Exception ex)
			{
				if (!Utility.IsException<TimeoutException>(ex) && !base.IsDead)
				{
					base.Fault(ex);
				}
				throw;
			}
			this.m_pingNecessary = false;
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x00076440 File Offset: 0x00074640
		public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
		{
			return this.BeginSend(message, TimeSpan.MaxValue, callback, state);
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x00076450 File Offset: 0x00074650
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult;
			try
			{
				message.Properties.AllowOutputBatching = false;
				asyncResult = this.Channel.BeginSend(message, timeout, callback, state);
			}
			catch (Exception ex)
			{
				if (!Utility.IsException<TimeoutException>(ex) && !base.IsDead)
				{
					base.Fault(ex);
				}
				TcpOutputSession.LocalContext localContext = new TcpOutputSession.LocalContext(callback, state);
				localContext.CompleteOperation(true, ex);
				asyncResult = localContext;
			}
			return asyncResult;
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x000764BC File Offset: 0x000746BC
		public void EndSend(IAsyncResult ar)
		{
			try
			{
				TcpOutputSession.LocalContext localContext = ar as TcpOutputSession.LocalContext;
				if (localContext != null)
				{
					localContext.End();
				}
				else
				{
					this.Channel.EndSend(ar);
				}
			}
			catch (Exception ex)
			{
				if (!Utility.IsException<TimeoutException>(ex) && !base.IsDead)
				{
					base.Fault(ex);
				}
				throw;
			}
			this.m_pingNecessary = false;
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x0007651C File Offset: 0x0007471C
		public Message SendReceive(Message message)
		{
			return this.SendReceive(message, TimeSpan.MaxValue);
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x0007652C File Offset: 0x0007472C
		public Message SendReceive(Message message, TimeSpan timeout)
		{
			base.ThrowIfInvalid();
			RequestContext requestContext = new RequestContext(message, this.Channel, this.m_requestHashTable, this.m_requestHashTableLock, null, null, timeout);
			UniqueId messageId = message.Headers.MessageId;
			requestContext.StartTimer();
			try
			{
				requestContext.Send(timeout);
			}
			catch (Exception ex)
			{
				if (!Utility.IsException<TimeoutException>(ex) && !base.IsDead)
				{
					base.Fault(ex);
				}
				RequestContext.FindAndRemoveContext(messageId, this.m_requestHashTable, this.m_requestHashTableLock);
				throw;
			}
			requestContext.End();
			this.m_pingNecessary = false;
			return requestContext.ReceivedMessage;
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x000765C8 File Offset: 0x000747C8
		public IAbortableAsyncResult BeginSendReceive(Message message, AsyncCallback callback, object state)
		{
			return this.BeginSendReceive(message, TimeSpan.MaxValue, callback, state);
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x000765D8 File Offset: 0x000747D8
		public IAbortableAsyncResult BeginSendReceive(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (base.State != TransportState.Opened)
			{
				return new SynchronousCompletionOperationContext(callback, state);
			}
			RequestContext requestContext = new RequestContext(message, this.Channel, this.m_requestHashTable, this.m_requestHashTableLock, callback, state, timeout);
			try
			{
				requestContext.BeginSend();
				requestContext.StartTimer();
			}
			catch (Exception ex)
			{
				if (!Utility.IsException<TimeoutException>(ex) && !base.IsDead)
				{
					base.Fault(ex);
				}
				requestContext.CompleteOperation(true, ex);
			}
			return requestContext;
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x00076658 File Offset: 0x00074858
		public Message EndSendReceive(IAsyncResult ar)
		{
			RequestContext requestContext = ar as RequestContext;
			if (requestContext != null)
			{
				requestContext.End();
				this.m_pingNecessary = false;
				return requestContext.ReceivedMessage;
			}
			if (ar is SynchronousCompletionOperationContext)
			{
				TransportState transportState = base.State;
				if (transportState == TransportState.Opened)
				{
					transportState = TransportState.Opening;
				}
				throw base.CreateInvalidTransportException(transportState);
			}
			throw new ArgumentException("Argument is invalid", "ar");
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x000766AF File Offset: 0x000748AF
		private bool ProcessException(Exception e)
		{
			if (Utility.IsCommunicationException(e))
			{
				if (!base.IsDead)
				{
					base.Fault(e);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x000766CC File Offset: 0x000748CC
		private void StartListening()
		{
			IAsyncResult asyncResult = this.SafeBeginReceive();
			if (asyncResult == null)
			{
				return;
			}
			if (asyncResult.CompletedSynchronously)
			{
				this.CompleteReceive(asyncResult);
			}
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x000766F4 File Offset: 0x000748F4
		private IAsyncResult SafeBeginReceive()
		{
			IAsyncResult asyncResult = null;
			try
			{
				asyncResult = this.Channel.BeginReceive(TimeSpan.MaxValue, new AsyncCallback(TcpOutputSession.StaticReceiveCallback), this);
			}
			catch (Exception ex)
			{
				if (!this.ProcessException(ex))
				{
					throw;
				}
			}
			return asyncResult;
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x00076744 File Offset: 0x00074944
		private Message SafeEndReceive(IAsyncResult ar)
		{
			Message message = null;
			try
			{
				message = this.Channel.EndReceive(ar);
			}
			catch (Exception ex)
			{
				if (!this.ProcessException(ex))
				{
					throw;
				}
			}
			return message;
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x00076784 File Offset: 0x00074984
		private static void StaticReceiveCallback(IAsyncResult ar)
		{
			if (ar.CompletedSynchronously)
			{
				return;
			}
			TcpOutputSession tcpOutputSession = (TcpOutputSession)ar.AsyncState;
			tcpOutputSession.CompleteReceive(ar);
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x000767B0 File Offset: 0x000749B0
		private void CompleteReceive(IAsyncResult ar)
		{
			Message message = this.SafeEndReceive(ar);
			if (message == null)
			{
				base.ForceClosure();
				return;
			}
			for (;;)
			{
				ar = this.SafeBeginReceive();
				if (ar == null || !ar.CompletedSynchronously)
				{
					goto IL_004D;
				}
				Message message2 = this.SafeEndReceive(ar);
				if (message2 == null)
				{
					break;
				}
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.DispatchMessageCallback), message2);
			}
			base.ForceClosure();
			IL_004D:
			this.DispatchMessage(message);
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x00076814 File Offset: 0x00074A14
		private void DispatchMessageCallback(object state)
		{
			Message message = (Message)state;
			this.DispatchMessage(message);
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x00076830 File Offset: 0x00074A30
		private void DispatchMessage(Message incomingMessage)
		{
			UniqueId relatesTo = incomingMessage.Headers.RelatesTo;
			if (!(relatesTo != null))
			{
				this.ProcessIncomingMessage(incomingMessage);
				return;
			}
			RequestContext requestContext = RequestContext.FindAndRemoveContext(relatesTo, this.m_requestHashTable, this.m_requestHashTableLock);
			if (requestContext != null)
			{
				requestContext.MessageReceived(incomingMessage);
				return;
			}
			this.ProcessRelatedMessage(incomingMessage);
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x00076880 File Offset: 0x00074A80
		private void ProcessRelatedMessage(Message relatedMessage)
		{
			EventLogWriter.WriteInfo("OutputSession", "Session to {0} dropped reply message action {1} RelatesTo {2}", new object[]
			{
				this.m_remoteAddress,
				relatedMessage.Headers.Action,
				relatedMessage.Headers.RelatesTo
			});
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x000768CC File Offset: 0x00074ACC
		private void ProcessIncomingMessage(Message incomingMessage)
		{
			if (incomingMessage.Headers.Action == "ping")
			{
				return;
			}
			IMessageHandler handler = this.m_handler;
			if (handler != null)
			{
				handler.ProcessMessage(incomingMessage);
				return;
			}
			EventLogWriter.WriteInfo("OutputSession", "Session to {0} dropped incoming message action {1}", new object[]
			{
				this.m_remoteAddress,
				incomingMessage.Headers.Action
			});
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x00076934 File Offset: 0x00074B34
		protected override bool OnOpen()
		{
			IAsyncResult asyncResult = base.WCFObject.BeginOpen(TimeSpan.MaxValue, new AsyncCallback(TcpOutputSession.StaticOpenCallback), this);
			return asyncResult.CompletedSynchronously;
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x00076968 File Offset: 0x00074B68
		private static void StaticOpenCallback(IAsyncResult ar)
		{
			TcpOutputSession tcpOutputSession = (TcpOutputSession)ar.AsyncState;
			tcpOutputSession.OpenCallback(ar);
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x00076988 File Offset: 0x00074B88
		private void OpenCallback(IAsyncResult ar)
		{
			Exception ex = null;
			bool flag = ar.CompletedSynchronously && this.m_retryCount == 0;
			try
			{
				base.WCFObject.EndOpen(ar);
			}
			catch (Exception ex2)
			{
				if (flag)
				{
					throw;
				}
				ex = ex2;
				if (TcpOutputSession.IsConnectionRejectionException(ex2))
				{
					if (this.m_retryCount == this.m_factory.MaxConnectionRetry)
					{
						EventLogWriter.WriteInfo("OutputSession", "Connnection to {0} {1} rejected", new object[] { this.m_remoteAddress, base.Context });
						ex = new RemoteDownException(ex2);
					}
					else if (this.m_retryCount < this.m_factory.MaxConnectionRetry)
					{
						this.m_retryCount++;
						Timer timer = new Timer(TcpOutputSession.RetryOpenCallback, this);
						timer.Enqueue(this.m_factory.ConnectionRetryInterval);
						return;
					}
				}
			}
			base.CompleteOpen(flag, ex);
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x00076A74 File Offset: 0x00074C74
		private static void StaticRetryOpen(object state)
		{
			TcpOutputSession tcpOutputSession = (TcpOutputSession)state;
			tcpOutputSession.RetryOpen();
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x00076A8E File Offset: 0x00074C8E
		private void RetryOpen()
		{
			base.WCFObject = this.m_factory.CreateChannel(this.m_remoteAddress);
			this.OnOpen();
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x00076AAE File Offset: 0x00074CAE
		protected override bool OnClose()
		{
			this.m_pingTimer.Dequeue();
			return base.OnClose();
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x00076AC2 File Offset: 0x00074CC2
		protected override void OnAbort()
		{
			this.m_pingTimer.Dequeue();
			base.OnAbort();
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x00076AD8 File Offset: 0x00074CD8
		private static bool IsConnectionRejectionException(Exception e)
		{
			while (e is OperationCompletedException)
			{
				e = e.InnerException;
			}
			if (e is EndpointNotFoundException)
			{
				SocketException ex = e.InnerException as SocketException;
				if (ex != null && ex.ErrorCode == 10061)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040016F3 RID: 5875
		internal const string PingAction = "ping";

		// Token: 0x040016F4 RID: 5876
		private const string LogSource = "OutputSession";

		// Token: 0x040016F5 RID: 5877
		private TcpTransportFactory m_factory;

		// Token: 0x040016F6 RID: 5878
		private Dictionary<UniqueId, RequestContext> m_requestHashTable;

		// Token: 0x040016F7 RID: 5879
		private object m_requestHashTableLock;

		// Token: 0x040016F8 RID: 5880
		private Uri m_remoteAddress;

		// Token: 0x040016F9 RID: 5881
		private IMessageHandler m_handler;

		// Token: 0x040016FA RID: 5882
		private int m_retryCount;

		// Token: 0x040016FB RID: 5883
		private Timer m_pingTimer;

		// Token: 0x040016FC RID: 5884
		private static AsyncCallback PingSendCallback = new AsyncCallback(TcpOutputSession.StaticPingSendCallback);

		// Token: 0x040016FD RID: 5885
		private static WaitCallback RetryOpenCallback = new WaitCallback(TcpOutputSession.StaticRetryOpen);

		// Token: 0x040016FE RID: 5886
		private static WaitCallback PingTimerCallback = new WaitCallback(TcpOutputSession.StaticPingCallback);

		// Token: 0x040016FF RID: 5887
		public static TimeSpan PingInterval = TimeSpan.FromMinutes(2.0);

		// Token: 0x04001700 RID: 5888
		private bool m_pingNecessary;

		// Token: 0x02000451 RID: 1105
		private class LocalContext : OperationContext
		{
			// Token: 0x060026B9 RID: 9913 RVA: 0x00076B73 File Offset: 0x00074D73
			internal LocalContext(AsyncCallback callback, object state)
				: base(callback, state)
			{
			}
		}
	}
}

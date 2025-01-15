using System;
using System.Collections;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002BC RID: 700
	internal abstract class WcfTransportChannel
	{
		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x0004BE70 File Offset: 0x0004A070
		// (set) Token: 0x06001994 RID: 6548 RVA: 0x0004BE78 File Offset: 0x0004A078
		private protected DataCacheSecurity DataCacheSecurity { protected get; private set; }

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001995 RID: 6549 RVA: 0x0004BE81 File Offset: 0x0004A081
		internal TimeSpan SendTimeout
		{
			get
			{
				return this._sendTimeout;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x0004BE89 File Offset: 0x0004A089
		internal TimeSpan ChannelOpenTimeout
		{
			get
			{
				return this._channelOpenTimeout;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001997 RID: 6551 RVA: 0x0004BE91 File Offset: 0x0004A091
		internal TimeSpan SendReceiveTimeout
		{
			get
			{
				return this._sendReceiveTimeout;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x0004BE99 File Offset: 0x0004A099
		internal TimeSpan ReceiveTimeout
		{
			get
			{
				return this._receiveTimeout;
			}
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0004BEA1 File Offset: 0x0004A0A1
		internal WcfTransportChannel(DataCacheSecurity dataCacheSecurity)
			: this(null, ConfigManager.CLIENT_CHANNEL_OPEN_WAIT, new TimeSpan(0, 0, 0, 0, ConfigManager.TIMEOUT), dataCacheSecurity)
		{
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0004BEBE File Offset: 0x0004A0BE
		internal WcfTransportChannel(OnMessageReceived defaultReceiveCallback, DataCacheSecurity dataCacheSecurity)
			: this(defaultReceiveCallback, ConfigManager.CLIENT_CHANNEL_OPEN_WAIT, new TimeSpan(0, 0, 0, 0, ConfigManager.TIMEOUT), dataCacheSecurity)
		{
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x0004BEDC File Offset: 0x0004A0DC
		internal WcfTransportChannel(OnMessageReceived defaultReceiveCallback, TimeSpan chnlTimeout, TimeSpan sendTimeout, DataCacheSecurity dataCacheSecurity)
		{
			this.SetupTimeouts(chnlTimeout, sendTimeout);
			this._defaultReceiveCallback = defaultReceiveCallback;
			this.DataCacheSecurity = dataCacheSecurity;
			this._receiveCallbackTable = new Hashtable();
			this._receiver = new AsyncCallback(this.Receive);
			this._lockObject = new object();
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0004BF30 File Offset: 0x0004A130
		private void SetupTimeouts(TimeSpan chnlOpenTimeout, TimeSpan sendTimeout)
		{
			this._receiveTimeout = ConfigManager.RECEIVE_TIMEOUT;
			TimeSpan minSendTimeout = ConfigManager.MinSendTimeout;
			if (sendTimeout < minSendTimeout)
			{
				sendTimeout = minSendTimeout;
			}
			this._sendTimeout = sendTimeout;
			this._sendReceiveTimeout = this._sendTimeout + this._sendTimeout;
			this._channelOpenTimeout = chnlOpenTimeout;
		}

		// Token: 0x0600199D RID: 6557
		protected abstract void AddSecurityBinding(BindingElementCollection bindingCollection);

		// Token: 0x0600199E RID: 6558
		protected abstract void AddCustomBindingElements(BindingElementCollection bindingCollection);

		// Token: 0x0600199F RID: 6559
		protected abstract TcpTransportBindingElement PrepareTcpTransportBindingElement();

		// Token: 0x060019A0 RID: 6560 RVA: 0x0004BF80 File Offset: 0x0004A180
		protected void SetupBindings()
		{
			BinaryMessageEncodingBindingElement binaryMessageEncodingBindingElement = new BinaryMessageEncodingBindingElement();
			XmlDictionaryReaderQuotas.Max.CopyTo(binaryMessageEncodingBindingElement.ReaderQuotas);
			TcpTransportBindingElement tcpTransportBindingElement = this.PrepareTcpTransportBindingElement();
			BindingElementCollection bindingElementCollection = new BindingElementCollection();
			bindingElementCollection.Add(binaryMessageEncodingBindingElement);
			this.AddSecurityBinding(bindingElementCollection);
			this.AddCustomBindingElements(bindingElementCollection);
			bindingElementCollection.Add(tcpTransportBindingElement);
			this._tcpBinding = new CustomBinding(bindingElementCollection);
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x0004BFD8 File Offset: 0x0004A1D8
		protected virtual bool InvokeCallback(Message message, IChannelContainer container)
		{
			OnMessageReceived onMessageReceived = this._receiveCallbackTable[message.Headers.Action] as OnMessageReceived;
			if (onMessageReceived != null)
			{
				IReplyContext replyContext = new ReplyContext(message, container, this);
				onMessageReceived(replyContext);
				return true;
			}
			return false;
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x0004C018 File Offset: 0x0004A218
		protected bool InvokeDefaultCallback(Message message, IChannelContainer container)
		{
			OnMessageReceived defaultReceiveCallback = this._defaultReceiveCallback;
			if (defaultReceiveCallback != null)
			{
				IReplyContext replyContext = new ReplyContext(message, container, this);
				defaultReceiveCallback(replyContext);
				return true;
			}
			return false;
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0004C042 File Offset: 0x0004A242
		protected void Receive(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				this.CompleteProcessing(result);
			}
		}

		// Token: 0x060019A4 RID: 6564
		protected abstract void CompleteProcessing(IAsyncResult result);

		// Token: 0x060019A5 RID: 6565 RVA: 0x0004C054 File Offset: 0x0004A254
		internal void CleanupChannel(IDuplexSessionChannel channel)
		{
			channel.Abort();
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "ChannelID = {0} Channel cleaned up.", new object[] { channel.GetHashCode() });
			}
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0004C095 File Offset: 0x0004A295
		protected bool LogAndCheckIfCommunicationException(Exception e, IDuplexChannel channel)
		{
			return WcfTransportChannel.StaticLogAndCheckIfCommunicationException(e, this._logSource, TraceLevel.Info, (channel != null) ? channel.GetHashCode() : (-1));
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0004C0B0 File Offset: 0x0004A2B0
		protected bool LogAndCheckIfCommunicationException(Exception e, TraceLevel traceLevel, IDuplexChannel channel)
		{
			return WcfTransportChannel.StaticLogAndCheckIfCommunicationException(e, this._logSource, traceLevel, (channel != null) ? channel.GetHashCode() : (-1));
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x0004C0CB File Offset: 0x0004A2CB
		internal static bool IsCommunicationException(Exception e)
		{
			return e is TimeoutException || e is CommunicationException || e is ObjectDisposedException || e is InvalidOperationException;
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x0004C0F0 File Offset: 0x0004A2F0
		internal static bool IsNotSupportedException(Exception e)
		{
			return e is NotSupportedException;
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x0004C0FB File Offset: 0x0004A2FB
		internal static bool StaticLogAndCheckIfCommunicationException(Exception e, string source, TraceLevel traceLevel, int id)
		{
			Utility.LogException(e, source, traceLevel, id);
			return WcfTransportChannel.IsCommunicationException(e);
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x0004C10C File Offset: 0x0004A30C
		public virtual void RegisterReceiveCallback(string action, OnMessageReceived callback)
		{
			if (string.IsNullOrEmpty(action))
			{
				throw new ArgumentNullException("action");
			}
			if (string.Equals(action, "http://schemas.microsoft.com/velocity/msgs/RequestReplyAction", StringComparison.Ordinal))
			{
				throw new InvalidOperationException();
			}
			if (string.Equals(action, "http://schemas.microsoft.com/velocity/msgs/VerificationAction", StringComparison.Ordinal))
			{
				throw new InvalidOperationException();
			}
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			lock (this._receiveCallbackTable.SyncRoot)
			{
				OnMessageReceived onMessageReceived = (OnMessageReceived)this._receiveCallbackTable[action];
				onMessageReceived = (OnMessageReceived)Delegate.Combine(onMessageReceived, callback);
				this._receiveCallbackTable[action] = onMessageReceived;
			}
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00003CAB File Offset: 0x00001EAB
		public virtual void RegisterReceiveCallback(MessageType messageType, OnMessageReceived callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x0004C1C0 File Offset: 0x0004A3C0
		public void UnregisterReceiveCallback(string action, OnMessageReceived callback)
		{
			if (string.IsNullOrEmpty(action))
			{
				throw new ArgumentNullException("action");
			}
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			lock (this._receiveCallbackTable.SyncRoot)
			{
				OnMessageReceived onMessageReceived = (OnMessageReceived)this._receiveCallbackTable[action];
				if (onMessageReceived != null)
				{
					onMessageReceived = (OnMessageReceived)Delegate.Remove(onMessageReceived, callback);
					if (onMessageReceived == null)
					{
						this._receiveCallbackTable.Remove(action);
					}
					else
					{
						this._receiveCallbackTable[action] = onMessageReceived;
					}
				}
			}
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x00003CAB File Offset: 0x00001EAB
		public virtual void UnregisterReceiveCallback(MessageType messageType, OnMessageReceived callback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x0004C260 File Offset: 0x0004A460
		public void UnregisterReceiveCallback(string action)
		{
			if (string.IsNullOrEmpty(action))
			{
				throw new ArgumentNullException("action");
			}
			lock (this._receiveCallbackTable.SyncRoot)
			{
				this._receiveCallbackTable.Remove(action);
			}
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x0004C2C0 File Offset: 0x0004A4C0
		public void RegisterDefaultReceiveCallback(OnMessageReceived callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			lock (this._lockObject)
			{
				this._defaultReceiveCallback = (OnMessageReceived)Delegate.Combine(this._defaultReceiveCallback, callback);
			}
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0004C320 File Offset: 0x0004A520
		public void UnregisterDefaultReceiveCallback(OnMessageReceived callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			lock (this._lockObject)
			{
				if (this._defaultReceiveCallback != null)
				{
					this._defaultReceiveCallback = (OnMessageReceived)Delegate.Remove(this._defaultReceiveCallback, callback);
				}
			}
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x0004C388 File Offset: 0x0004A588
		public void UnregisterDefaultReceiveCallback()
		{
			this._defaultReceiveCallback = null;
		}

		// Token: 0x04000DCF RID: 3535
		protected TimeSpan _sendTimeout;

		// Token: 0x04000DD0 RID: 3536
		protected TimeSpan _channelOpenTimeout;

		// Token: 0x04000DD1 RID: 3537
		protected TimeSpan _sendReceiveTimeout;

		// Token: 0x04000DD2 RID: 3538
		protected TimeSpan _receiveTimeout;

		// Token: 0x04000DD3 RID: 3539
		protected CustomBinding _tcpBinding;

		// Token: 0x04000DD4 RID: 3540
		protected Hashtable _receiveCallbackTable;

		// Token: 0x04000DD5 RID: 3541
		protected OnMessageReceived _defaultReceiveCallback;

		// Token: 0x04000DD6 RID: 3542
		protected AsyncCallback _receiver;

		// Token: 0x04000DD7 RID: 3543
		protected object _lockObject;

		// Token: 0x04000DD8 RID: 3544
		protected volatile bool _isClosed;

		// Token: 0x04000DD9 RID: 3545
		protected string _logSource;
	}
}

using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001A0 RID: 416
	internal class TcpClientChannel : IDisposable
	{
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000D9F RID: 3487 RVA: 0x0002E83E File Offset: 0x0002CA3E
		// (set) Token: 0x06000DA0 RID: 3488 RVA: 0x0002E846 File Offset: 0x0002CA46
		public DateTime LastSendTime { get; set; }

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0002E850 File Offset: 0x0002CA50
		public TcpClientChannel(Uri uri, IClientChannelFactory factory)
		{
			this._waitHandle = new ManualResetEvent(false);
			this._factory = factory;
			this._uri = uri;
			this.LastSendTime = DateTime.UtcNow;
			this._logSource = factory.ChannelLogPrefix + uri.ToString();
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x0002E8AA File Offset: 0x0002CAAA
		public Uri RemoteUri
		{
			get
			{
				return this._uri;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x0002E8B2 File Offset: 0x0002CAB2
		public ITcpChannel TcpChannel
		{
			get
			{
				return this._chnl;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0002E8BA File Offset: 0x0002CABA
		public WaitHandle ConnectionWaitHandle
		{
			get
			{
				return this._waitHandle;
			}
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0002E8C4 File Offset: 0x0002CAC4
		public void ConnectAsync()
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "Initiating connection to host {0}", new object[] { this._uri });
			}
			lock (this._syncObject)
			{
				if (this._connectionPending)
				{
					if (Provider.IsEnabled(TraceLevel.Info))
					{
						EventLogWriter.WriteInfo(this._logSource, "Connection request already pending, returning. {0}", new object[] { this._uri });
					}
					return;
				}
				this._connectionPending = true;
				this._chnl = null;
				this._waitHandle.Reset();
			}
			this._factory.BeginConnect(this._uri.Host, this._uri.Port, new AsyncCallback(this.ConnectionCallback), this);
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x0002E9A4 File Offset: 0x0002CBA4
		public Exception LastException
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0002E9AC File Offset: 0x0002CBAC
		private void ConnectionCallback(IAsyncResult asyncResult)
		{
			try
			{
				TcpConnectOperationResult tcpConnectOperationResult = this._factory.EndConnect(asyncResult);
				if (tcpConnectOperationResult.OperationSucceded)
				{
					tcpConnectOperationResult.Channel.TcpChannelClosed = new TcpChnlClosed(this.OnChannelClosed);
					tcpConnectOperationResult.Channel.ReceiveMessage();
					this.LastSendTime = DateTime.UtcNow;
					Interlocked.Exchange<ITcpChannel>(ref this._chnl, tcpConnectOperationResult.Channel);
					this.OnChannelOpened();
					if (Provider.IsEnabled(TraceLevel.Info))
					{
						EventLogWriter.WriteInfo(this._logSource, "Channel established successfully to host {0}", new object[] { this._uri });
					}
				}
				else
				{
					this._exception = tcpConnectOperationResult.ConnectionFailureException;
				}
			}
			finally
			{
				this._connectionPending = false;
				try
				{
					this._waitHandle.Set();
				}
				catch (ObjectDisposedException)
				{
				}
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0002EA84 File Offset: 0x0002CC84
		internal void OnChannelClosed(ITcpChannel chnl)
		{
			this._chnl = null;
			this._factory.ChannelClosed(chnl);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0002EA9E File Offset: 0x0002CC9E
		private void OnChannelOpened()
		{
			this._factory.ChannelOpened();
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0002EAB0 File Offset: 0x0002CCB0
		public void Dispose()
		{
			ITcpChannel chnl = this._chnl;
			if (chnl != null)
			{
				chnl.CloseGracefully();
			}
			this._waitHandle.Close();
		}

		// Token: 0x04000964 RID: 2404
		private readonly IClientChannelFactory _factory;

		// Token: 0x04000965 RID: 2405
		private Uri _uri;

		// Token: 0x04000966 RID: 2406
		private ITcpChannel _chnl;

		// Token: 0x04000967 RID: 2407
		private ManualResetEvent _waitHandle;

		// Token: 0x04000968 RID: 2408
		private bool _connectionPending;

		// Token: 0x04000969 RID: 2409
		private Exception _exception;

		// Token: 0x0400096A RID: 2410
		private string _logSource;

		// Token: 0x0400096B RID: 2411
		private readonly object _syncObject = new object();
	}
}

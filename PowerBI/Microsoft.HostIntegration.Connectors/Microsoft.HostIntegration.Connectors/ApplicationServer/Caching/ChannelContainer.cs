using System;
using System.Diagnostics;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002A1 RID: 673
	internal class ChannelContainer : IDisposable
	{
		// Token: 0x060018C9 RID: 6345 RVA: 0x0004A814 File Offset: 0x00048A14
		internal ChannelContainer(EndpointID endpoint, WcfClientChannel parent)
		{
			this._endOpen = new AsyncCallback(this.Opened);
			this._endpoint = endpoint;
			this._logSource = this._logSource + '.' + this._endpoint.URI.Host;
			this._parent = parent;
			this._isCallingDeadCallbackRequired = true;
			this._handle = new ManualResetEvent(false);
			this._currentResult = OperationResult.ChannelOpening;
			this.CreateChannel();
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x0004A8A9 File Offset: 0x00048AA9
		public IDuplexSessionChannel Channel
		{
			get
			{
				return this._innerContainer.Channel;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x0004A8B6 File Offset: 0x00048AB6
		public IChannelContainer InnerContainer
		{
			get
			{
				return this._innerContainer;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x0004A8BE File Offset: 0x00048ABE
		internal bool IsActive
		{
			get
			{
				return this._currentResult.IsSuccess && this.Channel.State == CommunicationState.Opened;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x0004A8DD File Offset: 0x00048ADD
		internal OperationResult Status
		{
			get
			{
				return this._currentResult;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x0004A8E5 File Offset: 0x00048AE5
		internal ManualResetEvent ChannelOpenHandle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0004A8F0 File Offset: 0x00048AF0
		private bool TryEnterRecycleLock()
		{
			bool flag = Interlocked.CompareExchange(ref this._lock, ChannelContainer.GetThreadId(), ChannelContainer._unlocked) == ChannelContainer._unlocked;
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<bool, ChannelContainer>(this._logSource, "TryEnterRecycleLock - {0} Channel Container = {1}.", flag, this);
			}
			return flag;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0004A935 File Offset: 0x00048B35
		private void ExitRecycleLock()
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<ChannelContainer>(this._logSource, "ExitRecycleLock - Channel Container = {0}.", this);
			}
			ReleaseAssert.IsTrue<ChannelContainer>(Interlocked.Exchange(ref this._lock, ChannelContainer._unlocked) != ChannelContainer._unlocked, "Exit done by multiple threads - {0}", this);
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x0004A978 File Offset: 0x00048B78
		private void CreateChannel()
		{
			ReleaseAssert.IsTrue(this._lock == ChannelContainer.GetThreadId());
			IDuplexSessionChannel duplexSessionChannel = null;
			try
			{
				duplexSessionChannel = this._parent.CreateChannel(this._endpoint);
			}
			catch (Exception ex)
			{
				if (!WcfTransportChannel.StaticLogAndCheckIfCommunicationException(ex, this._logSource, TraceLevel.Warning, -1))
				{
					throw;
				}
				this._currentResult = new OperationResult(OperationStatus.ChannelCreationFailed, ex);
				this.ExitRecycleLock();
				return;
			}
			if (duplexSessionChannel != null)
			{
				this._innerContainer = new InnerChannelContainer(duplexSessionChannel);
			}
			if (!this._parent.IsClosed)
			{
				this.Open();
				return;
			}
			if (this._innerContainer != null)
			{
				this.CleanupChannel();
			}
			this._currentResult = OperationResult.InstanceClosed;
			this.ExitRecycleLock();
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x0004AA28 File Offset: 0x00048C28
		private void Open()
		{
			this._currentResult = OperationResult.ChannelOpening;
			try
			{
				this.Channel.BeginOpen(this._endOpen, this);
			}
			catch (Exception ex)
			{
				if (!this.CheckOpenException(this, ex))
				{
					throw;
				}
			}
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x0004AA74 File Offset: 0x00048C74
		private void Opened(IAsyncResult ar)
		{
			ChannelContainer channelContainer = ar.AsyncState as ChannelContainer;
			try
			{
				channelContainer.Channel.EndOpen(ar);
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(this._logSource, "Channel open succeeded - {0}", new object[] { channelContainer });
				}
				ServerInformation serverInformation;
				channelContainer._currentResult = channelContainer._parent.VerifyAndStartReceiving(channelContainer._innerContainer, out serverInformation);
				if (serverInformation != null && channelContainer.Status == OperationResult.Success)
				{
					channelContainer._parent.ChannelOpened(channelContainer, serverInformation.InstanceAddressInternal);
				}
				channelContainer._handle.Set();
				channelContainer._isCallingDeadCallbackRequired = true;
				channelContainer.ExitRecycleLock();
			}
			catch (Exception ex)
			{
				if (!this.CheckOpenException(channelContainer, ex))
				{
					throw;
				}
			}
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x0004AB34 File Offset: 0x00048D34
		private bool CheckOpenException(ChannelContainer container, Exception e)
		{
			container._currentResult = new OperationResult(OperationStatus.ChannelOpenFailed, e);
			container.CleanupChannel();
			container._handle.Set();
			container.ExitRecycleLock();
			return WcfTransportChannel.StaticLogAndCheckIfCommunicationException(e, this._logSource, TraceLevel.Warning, (container.Channel == null) ? (-1) : container.Channel.GetHashCode());
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x0004AB90 File Offset: 0x00048D90
		internal void RecycleChannel(Exception e, bool forceCleanup)
		{
			if (this.TryEnterRecycleLock())
			{
				if (this.IsActive && !forceCleanup)
				{
					this.ExitRecycleLock();
					return;
				}
				this._handle.Reset();
				this.CleanupChannel();
				if (this._isCallingDeadCallbackRequired && !this._parent.IsClosed && this._innerContainer != null)
				{
					this._parent.InvokeDeadCallback(this._endpoint, this.Channel.GetHashCode(), e);
					this._isCallingDeadCallbackRequired = false;
				}
				this.CreateChannel();
			}
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x0004AC15 File Offset: 0x00048E15
		private void CleanupChannel()
		{
			if (this._innerContainer != null && this.Channel != null)
			{
				this._parent.CleanupChannel(this.Channel);
			}
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x0004AC38 File Offset: 0x00048E38
		public override string ToString()
		{
			string text = "ChannelCreationFailed";
			if (this._innerContainer != null)
			{
				text = this._innerContainer.Channel.State.ToString();
			}
			return string.Format(CultureInfo.InvariantCulture, "[Endpoint={0}; Lockstatus={1}; ChannelState={2}; {3}]", new object[] { this._endpoint, this._lock, text, this._currentResult });
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x0004ACA9 File Offset: 0x00048EA9
		private static int GetThreadId()
		{
			return Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x0004ACB5 File Offset: 0x00048EB5
		public void Dispose()
		{
			this._currentResult = OperationResult.InstanceClosed;
			this.CleanupChannel();
		}

		// Token: 0x04000D7A RID: 3450
		private AsyncCallback _endOpen;

		// Token: 0x04000D7B RID: 3451
		private string _logSource = "DistributedCache.ChannelContainer";

		// Token: 0x04000D7C RID: 3452
		private static int _unlocked = -1;

		// Token: 0x04000D7D RID: 3453
		private EndpointID _endpoint;

		// Token: 0x04000D7E RID: 3454
		private IChannelContainer _innerContainer;

		// Token: 0x04000D7F RID: 3455
		private int _lock = ChannelContainer.GetThreadId();

		// Token: 0x04000D80 RID: 3456
		private ManualResetEvent _handle;

		// Token: 0x04000D81 RID: 3457
		private WcfClientChannel _parent;

		// Token: 0x04000D82 RID: 3458
		private OperationResult _currentResult;

		// Token: 0x04000D83 RID: 3459
		private bool _isCallingDeadCallbackRequired;
	}
}

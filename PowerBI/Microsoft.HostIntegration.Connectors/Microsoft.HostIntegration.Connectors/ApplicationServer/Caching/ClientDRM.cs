using System;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200002D RID: 45
	internal class ClientDRM : DRM
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00006D64 File Offset: 0x00004F64
		internal ClientDRM(string cacheName, string[] endpointAddresses, string actionString, string id, TimeSpan channelOpenTimeout, SimpleSendReceiveModule tempSendReceiveModule, IClientProtocol protocol, CacheLookupTableTransfer initialLookupTable)
			: base(actionString, "Client" + id, tempSendReceiveModule)
		{
			this._protocol = protocol;
			this._endpointAddresses = endpointAddresses;
			this.InitializeCasClient(cacheName, initialLookupTable, id, channelOpenTimeout);
			this.refreshTimer = new Timer(new TimerCallback(this.RefreshTableTimerCallback), null, ClientDRM.RefreshTimerInterval, ClientDRM.RefreshTimerInterval);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006DC4 File Offset: 0x00004FC4
		internal void RefreshTableTimerCallback(object obj)
		{
			this._casClient.RefreshAll(ClientDRM.RefreshTimerInterval);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006DD8 File Offset: 0x00004FD8
		private void InitializeCasClient(string cacheName, CacheLookupTableTransfer initialLookupTable, string id, TimeSpan chnlOpenTimeout)
		{
			string[] array = new string[] { cacheName };
			this._casClient = new ThickClient(array, this._endpointAddresses, this._protocol, id, initialLookupTable);
			int num = (int)chnlOpenTimeout.TotalMilliseconds * this._endpointAddresses.Length + ConfigManager.TIMEOUT;
			TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0, num);
			int num2 = 0;
			while (!this._casClient.RefreshAll(timeSpan))
			{
				Exception ex = null;
				Exception lastException = this._casClient.LastException;
				if (lastException != null)
				{
					ex = lastException.InnerException;
				}
				DataCacheException ex2 = ex as DataCacheException;
				if (ex2 != null && ex2.ErrorCode.Equals(19))
				{
					throw ex2;
				}
				if (num2 > 0)
				{
					throw Utility.CreateClientException(string.Empty, 17, 5, ex);
				}
				Thread.Sleep(ConfigManager.CLIENT_CHANNEL_OPEN_WAIT);
				num2++;
				if (num2 > 1)
				{
					return;
				}
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006EAC File Offset: 0x000050AC
		protected override void AssignPrimaryTrackerData(RequestBody body, object context)
		{
			if (!this.ShouldAssignPrimaryTrackerData(body))
			{
				return;
			}
			if (context != null)
			{
				RequestTrackerOnPrimary requestTrackerOnPrimary = context as RequestTrackerOnPrimary;
				SendReceiveSynchronizer sendReceiveSynchronizer = body.Session as SendReceiveSynchronizer;
				if (requestTrackerOnPrimary != null && sendReceiveSynchronizer != null)
				{
					sendReceiveSynchronizer.AssociateTracker(requestTrackerOnPrimary);
				}
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006EE8 File Offset: 0x000050E8
		protected override void ProcessPartialResponse(ResponseBody resp, object session)
		{
			SendReceiveSynchronizer sendReceiveSynchronizer = (SendReceiveSynchronizer)session;
			sendReceiveSynchronizer.TryComplete(resp);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006F04 File Offset: 0x00005104
		internal override bool IsLocalRequest(EndpointID address)
		{
			return false;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006F08 File Offset: 0x00005108
		public override void Dispose()
		{
			lock (this)
			{
				if (this.refreshTimer != null)
				{
					this.refreshTimer.Dispose();
					this.refreshTimer = null;
				}
			}
			base.Dispose();
		}

		// Token: 0x040000B2 RID: 178
		private string[] _endpointAddresses;

		// Token: 0x040000B3 RID: 179
		private readonly IClientProtocol _protocol;

		// Token: 0x040000B4 RID: 180
		private static TimeSpan RefreshTimerInterval = new TimeSpan(0, 2, 0);

		// Token: 0x040000B5 RID: 181
		private Timer refreshTimer;
	}
}

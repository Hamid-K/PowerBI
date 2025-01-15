using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200000C RID: 12
	internal class ClientRoutingManager
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000039C4 File Offset: 0x00001BC4
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000039CC File Offset: 0x00001BCC
		public EndpointID Server
		{
			get
			{
				return this._server;
			}
			set
			{
				this._server = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000039D5 File Offset: 0x00001BD5
		public CacheLookupTable LookupTable
		{
			get
			{
				return this._cacheLookupTable;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000039E0 File Offset: 0x00001BE0
		public ClientRoutingManager(IClientProtocol transport, string cacheName, EndpointID server, CacheLookupTableTransfer initialLookupTable)
		{
			this._transport = transport;
			this._server = server;
			this._cacheName = cacheName;
			this._lookupRequestTimeout = ConfigManager.LookupTableRequestTimeout;
			this._refreshLookupLock = new object();
			int num = ConfigManager.TIMEOUT / 4;
			int num2 = 3000;
			if (num < num2)
			{
				num = num2;
			}
			this._lookupRequestTimeout = new TimeSpan(0, 0, 0, 0, num);
			if (initialLookupTable == null)
			{
				this._cacheLookupTable = new CacheLookupTable(this._cacheName);
				this.UpdateLookupTable();
				return;
			}
			this._cacheLookupTable = new CacheLookupTable(initialLookupTable, this._cacheName);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003A74 File Offset: 0x00001C74
		public EndpointID Lookup(int key)
		{
			string text = this._cacheLookupTable.LookupEndpointAddress(key);
			if (!string.IsNullOrEmpty(text))
			{
				return new EndpointID(text);
			}
			return null;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003AA0 File Offset: 0x00001CA0
		public void RefreshLookUpTableAsync(object state)
		{
			ClientRoutingManager.RefreshLookupTable refreshLookupTable = new ClientRoutingManager.RefreshLookupTable(this.UpdateLookupTable);
			refreshLookupTable.BeginInvoke(new AsyncCallback(this.EndRefreshLookupTable), state);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003AD0 File Offset: 0x00001CD0
		private void UpdateLookupTable()
		{
			if (Monitor.TryEnter(this._refreshLookupLock))
			{
				try
				{
					CacheLookupTableRequest vasCacheLookupTableRequest = Utility.GetVasCacheLookupTableRequest(this._cacheLookupTable);
					CacheLookupTableTransfer lookupTable = this._transport.GetLookupTable(this._server, vasCacheLookupTableRequest, this._lookupRequestTimeout);
					if (lookupTable != null && this._cacheLookupTable.IsNewer(lookupTable))
					{
						CacheLookupTable cacheLookupTable = new CacheLookupTable(lookupTable, this._cacheName);
						Interlocked.Exchange<CacheLookupTable>(ref this._cacheLookupTable, cacheLookupTable);
					}
				}
				catch (DataCacheException)
				{
					throw;
				}
				catch (Exception ex)
				{
					if (Provider.IsEnabled(TraceLevel.Warning))
					{
						EventLogWriter.WriteWarning("ClientRoutingManager", "Error in retrieving lookup table. Exception {0}", new object[] { ex.ToString() });
					}
				}
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003B8C File Offset: 0x00001D8C
		private void EndRefreshLookupTable(IAsyncResult result)
		{
			AsyncResult asyncResult = (AsyncResult)result;
			ClientRoutingManager.RefreshLookupTable refreshLookupTable = (ClientRoutingManager.RefreshLookupTable)asyncResult.AsyncDelegate;
			refreshLookupTable.EndInvoke(result);
		}

		// Token: 0x0400004E RID: 78
		private const string _logSource = "ClientRoutingManger";

		// Token: 0x0400004F RID: 79
		private readonly IClientProtocol _transport;

		// Token: 0x04000050 RID: 80
		private EndpointID _server;

		// Token: 0x04000051 RID: 81
		private string _cacheName;

		// Token: 0x04000052 RID: 82
		private CacheLookupTable _cacheLookupTable;

		// Token: 0x04000053 RID: 83
		private object _refreshLookupLock;

		// Token: 0x04000054 RID: 84
		private TimeSpan _lookupRequestTimeout;

		// Token: 0x0200000D RID: 13
		// (Invoke) Token: 0x0600006A RID: 106
		private delegate void RefreshLookupTable();
	}
}

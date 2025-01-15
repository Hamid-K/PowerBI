using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Caching;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000073 RID: 115
	internal sealed class GlobalDbConnectionPool : IDbConnectionPool
	{
		// Token: 0x0600047E RID: 1150 RVA: 0x00013804 File Offset: 0x00011A04
		internal GlobalDbConnectionPool()
			: this(TimeSpan.FromHours(Globals.Configuration.GlobalConnectionPoolEvictionTimeout))
		{
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001381B File Offset: 0x00011A1B
		internal GlobalDbConnectionPool(TimeSpan expiration)
		{
			this.m_expiration = expiration;
			this.m_asOnPremExpiration = TimeSpan.FromHours(Globals.Configuration.ASOnPremConnectionPoolEvictionTimeout);
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000DCA3 File Offset: 0x0000BEA3
		public static int SystemConnectionCount
		{
			get
			{
				return DbConnectionPoolLruCache.ConnectionCount;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000DCA3 File Offset: 0x0000BEA3
		public int ConnectionCount
		{
			get
			{
				return DbConnectionPoolLruCache.ConnectionCount;
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0001384C File Offset: 0x00011A4C
		public IDbConnection GetConnection(ConnectionKey connectionKey)
		{
			if (connectionKey == null)
			{
				return null;
			}
			object obj = this.m_connectionsCache.Get(connectionKey.GetKeyString());
			if (obj != null)
			{
				return this.FindConnection(connectionKey, (GlobalDbConnectionPool.EntryList)obj);
			}
			return null;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00013884 File Offset: 0x00011A84
		private IDbConnection FindConnection(ConnectionKey keyToFind, GlobalDbConnectionPool.EntryList connections)
		{
			bool flag = keyToFind.ShouldCheckIsAlive();
			while (connections.Entries.Count > 0)
			{
				ConnectionEntry connectionEntry = null;
				RSTrace.DataExtensionTracer.Trace(TraceLevel.Info, "Waiting to acquire connections lock inside FindConnection");
				lock (connections)
				{
					RSTrace.DataExtensionTracer.Trace(TraceLevel.Info, "Acquired connections lock inside FindConnection");
					int num = connections.Entries.Count - 1;
					if (num < 0)
					{
						return null;
					}
					connectionEntry = connections.Entries[num];
					connections.Entries.RemoveAt(num);
					connectionEntry.RequestFromPool();
				}
				if (connectionEntry != null)
				{
					DbConnectionPoolLruCache.RemoveConnectionEntry(connectionEntry);
				}
				try
				{
					if (connectionEntry.OKToGetFromPool && connectionEntry.Key.Equals(keyToFind) && (!flag || connectionEntry.Connection.IsAlive))
					{
						connectionEntry.Connection.IsFromPool = true;
						return connectionEntry.Connection;
					}
				}
				catch (Exception ex)
				{
					RSTrace.DataExtensionTracer.Trace(TraceLevel.Error, "Exception occurred getting the connection from the pool: {0}", new object[] { ex.ToString() });
				}
				this.CloseEntry(connectionEntry);
			}
			return null;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000139B8 File Offset: 0x00011BB8
		public bool PoolConnection(IDbPoolableConnection connection, ConnectionKey connectionKey)
		{
			if (connectionKey == null || connection == null)
			{
				return false;
			}
			this.PoolConnectionImpl(connection, connectionKey);
			return true;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000139CC File Offset: 0x00011BCC
		private void PoolConnectionImpl(IDbPoolableConnection connection, ConnectionKey connectionKey)
		{
			bool flag = false;
			try
			{
				bool isOnPremiseConnection = connectionKey.IsOnPremiseConnection;
				GlobalDbConnectionPool.EntryList entries = null;
				string keyString = connectionKey.GetKeyString();
				object obj = this.m_connectionsCache.Get(keyString);
				if (obj == null)
				{
					entries = new GlobalDbConnectionPool.EntryList();
					object obj2 = this.m_connectionsCache.Add(keyString, entries, null, Cache.NoAbsoluteExpiration, isOnPremiseConnection ? this.m_asOnPremExpiration : this.m_expiration, CacheItemPriority.Normal, new CacheItemRemovedCallback(this.EntriesRemovedCallBack));
					if (obj2 != null)
					{
						entries = (GlobalDbConnectionPool.EntryList)obj2;
					}
				}
				else
				{
					entries = (GlobalDbConnectionPool.EntryList)obj;
				}
				ConnectionEntry connectionEntry = new ConnectionEntry(connectionKey, connection, null);
				connectionEntry.OnConnectionClosed += this.connectionEntry_OnConnectionClosed;
				bool raceWithLRU = false;
				long num = PerformanceUtilities.ExecuteAndMeasureDurationMs(delegate
				{
					GlobalDbConnectionPool.EntryList entries2 = entries;
					lock (entries2)
					{
						if (!entries.IsBeingRemoved)
						{
							entries.Entries.Add(connectionEntry);
						}
						else
						{
							raceWithLRU = true;
						}
					}
				});
				RSTrace.DataExtensionTracer.Trace(TraceLevel.Info, "GlobalDbConnectionPool Entries update completed with duration={0}", new object[] { num });
				if (!raceWithLRU)
				{
					DbConnectionPoolLruCache.AddConnectionEntry(connectionEntry, true);
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			catch (Exception ex)
			{
				RSTrace.DataExtensionTracer.Trace(TraceLevel.Error, "Exception occurred pooling the connection: {0}", new object[] { ex.ToString() });
				flag = false;
			}
			if (!flag)
			{
				RSTrace.DataExtensionTracer.Trace(TraceLevel.Error, "GlobalDbConnectionPool: Could not pool connection. Closing the connection.");
				new ConnectionEntry(connectionKey, connection, null).CloseConnectionAsync();
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00013B48 File Offset: 0x00011D48
		public void CloseConnections()
		{
			DbConnectionPoolLruCache.CloseConnections();
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00013B4F File Offset: 0x00011D4F
		private void CloseEntry(ConnectionEntry entry)
		{
			entry.OnConnectionClosed -= this.connectionEntry_OnConnectionClosed;
			entry.CloseConnectionAsync();
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00013B6C File Offset: 0x00011D6C
		private void connectionEntry_OnConnectionClosed(object sender, EventArgs e)
		{
			ConnectionEntry connectionEntry = (ConnectionEntry)sender;
			string keyString = connectionEntry.Key.GetKeyString();
			object obj = this.m_connectionsCache.Get(keyString);
			if (obj != null)
			{
				GlobalDbConnectionPool.EntryList entryList = (GlobalDbConnectionPool.EntryList)obj;
				GlobalDbConnectionPool.EntryList entryList2 = entryList;
				lock (entryList2)
				{
					int num = entryList.Entries.IndexOf(connectionEntry);
					if (num >= 0)
					{
						entryList.Entries.RemoveAt(num);
					}
					if (entryList.Entries.Count == 0)
					{
						this.m_connectionsCache.Remove(keyString);
						entryList.IsBeingRemoved = true;
					}
				}
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00013C14 File Offset: 0x00011E14
		private void EntriesRemovedCallBack(string key, object value, CacheItemRemovedReason reason)
		{
			GlobalDbConnectionPool.EntryList entryList = (GlobalDbConnectionPool.EntryList)value;
			GlobalDbConnectionPool.EntryList entryList2 = entryList;
			lock (entryList2)
			{
				foreach (ConnectionEntry connectionEntry in entryList.Entries)
				{
					DbConnectionPoolLruCache.RemoveConnectionEntry(connectionEntry);
					this.CloseEntry(connectionEntry);
				}
			}
		}

		// Token: 0x04000237 RID: 567
		private readonly Cache m_connectionsCache = HttpRuntime.Cache;

		// Token: 0x04000238 RID: 568
		private readonly TimeSpan m_expiration;

		// Token: 0x04000239 RID: 569
		private readonly TimeSpan m_asOnPremExpiration;

		// Token: 0x02000443 RID: 1091
		private class EntryList
		{
			// Token: 0x04000F5B RID: 3931
			public List<ConnectionEntry> Entries = new List<ConnectionEntry>();

			// Token: 0x04000F5C RID: 3932
			public bool IsBeingRemoved;
		}
	}
}

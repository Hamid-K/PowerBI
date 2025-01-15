using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000034 RID: 52
	internal sealed class DbConnectionPool : IDbConnectionPool, IDisposable
	{
		// Token: 0x06000189 RID: 393 RVA: 0x0000DC89 File Offset: 0x0000BE89
		public DbConnectionPool(global::System.Action onConnectionCloseCallBack)
		{
			this.m_onConnectionCloseCallBack = onConnectionCloseCallBack;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000DCA3 File Offset: 0x0000BEA3
		public static int SystemConnectionCount
		{
			get
			{
				return DbConnectionPoolLruCache.ConnectionCount;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000DCAC File Offset: 0x0000BEAC
		public int ConnectionCount
		{
			get
			{
				LinkedList<ConnectionEntry> connections = this.m_connections;
				int count;
				lock (connections)
				{
					count = this.m_connections.Count;
				}
				return count;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000DCF4 File Offset: 0x0000BEF4
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000DCFD File Offset: 0x0000BEFD
		private void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				this.CloseConnections();
				this.m_disposed = true;
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000DD16 File Offset: 0x0000BF16
		private void ThrowIfDisposed()
		{
			if (this.m_disposed)
			{
				RSTrace.DataExtensionTracer.Assert(false, "Cannot access disposed DbConnectionPool");
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000DD30 File Offset: 0x0000BF30
		public bool PoolConnection(IDbPoolableConnection connection, ConnectionKey connectionKey)
		{
			this.ThrowIfDisposed();
			if (connectionKey == null || connection == null)
			{
				return false;
			}
			bool flag = false;
			try
			{
				LinkedList<ConnectionEntry> connections = this.m_connections;
				lock (connections)
				{
					this.CleanupClosedConnections();
					if (this.m_connections.Count < 5)
					{
						ConnectionEntry connectionEntry = new ConnectionEntry(connectionKey, connection, this.m_onConnectionCloseCallBack);
						this.m_connections.AddFirst(connectionEntry);
						DbConnectionPoolLruCache.AddConnectionEntry(connectionEntry, false);
						flag = true;
					}
				}
			}
			catch (Exception ex)
			{
				RSTrace.DataExtensionTracer.Trace(TraceLevel.Error, "Exception occurred pooling the connection: {0}", new object[] { ex.ToString() });
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000DDE8 File Offset: 0x0000BFE8
		private void CleanupClosedConnections()
		{
			LinkedList<ConnectionEntry> connections = this.m_connections;
			lock (connections)
			{
				if (this.m_connections.Count >= 5)
				{
					List<ConnectionEntry> list = new List<ConnectionEntry>();
					foreach (ConnectionEntry connectionEntry in this.m_connections)
					{
						if (connectionEntry.IsClosed)
						{
							list.Add(connectionEntry);
						}
					}
					foreach (ConnectionEntry connectionEntry2 in list)
					{
						this.m_connections.Remove(connectionEntry2);
					}
				}
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000DECC File Offset: 0x0000C0CC
		public IDbConnection GetConnection(ConnectionKey connectionKey)
		{
			this.ThrowIfDisposed();
			if (connectionKey == null)
			{
				return null;
			}
			IDbConnection dbConnection = null;
			LinkedList<ConnectionEntry> connections = this.m_connections;
			lock (connections)
			{
				if (this.m_connections.Count > 0)
				{
					List<ConnectionEntry> list = new List<ConnectionEntry>();
					foreach (ConnectionEntry connectionEntry in this.m_connections)
					{
						if (connectionEntry.IsClosed)
						{
							list.Add(connectionEntry);
						}
						else
						{
							try
							{
								if (connectionEntry.Key.Equals(connectionKey))
								{
									list.Add(connectionEntry);
									DbConnectionPoolLruCache.RemoveConnectionEntry(connectionEntry);
									if (connectionEntry.Connection.IsAlive)
									{
										connectionEntry.Connection.IsFromPool = true;
										dbConnection = connectionEntry.Connection;
										break;
									}
									connectionEntry.CloseConnection();
								}
							}
							catch (Exception ex)
							{
								connectionEntry.CloseConnection();
								RSTrace.DataExtensionTracer.Trace(TraceLevel.Error, "Exception occurred getting the connection from the pool: {0}", new object[] { ex.ToString() });
							}
						}
					}
					foreach (ConnectionEntry connectionEntry2 in list)
					{
						this.m_connections.Remove(connectionEntry2);
					}
				}
			}
			return dbConnection;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000E07C File Offset: 0x0000C27C
		public void CloseConnections()
		{
			this.ThrowIfDisposed();
			LinkedList<ConnectionEntry> connections = this.m_connections;
			lock (connections)
			{
				foreach (ConnectionEntry connectionEntry in this.m_connections)
				{
					connectionEntry.CloseConnection();
				}
				this.m_connections.Clear();
			}
		}

		// Token: 0x04000121 RID: 289
		private const int MAX_CONNECTIONS_PER_POOL = 5;

		// Token: 0x04000122 RID: 290
		private LinkedList<ConnectionEntry> m_connections = new LinkedList<ConnectionEntry>();

		// Token: 0x04000123 RID: 291
		private global::System.Action m_onConnectionCloseCallBack;

		// Token: 0x04000124 RID: 292
		private bool m_disposed;
	}
}

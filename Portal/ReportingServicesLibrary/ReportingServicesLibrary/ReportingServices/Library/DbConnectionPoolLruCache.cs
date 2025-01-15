using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000035 RID: 53
	internal static class DbConnectionPoolLruCache
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000E108 File Offset: 0x0000C308
		public static int ConnectionCount
		{
			get
			{
				LinkedLruCache lruConnectionCache = DbConnectionPoolLruCache.m_lruConnectionCache;
				int count;
				lock (lruConnectionCache)
				{
					count = DbConnectionPoolLruCache.m_lruConnectionCache.Count;
				}
				return count;
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000E150 File Offset: 0x0000C350
		public static void AddConnectionEntry(ConnectionEntry connectionEntry, bool closeEvictedConnectionsAsync)
		{
			List<ConnectionEntry> connectionsToClose = null;
			long num = PerformanceUtilities.ExecuteAndMeasureDurationMs(delegate
			{
				LinkedLruCache lruConnectionCache = DbConnectionPoolLruCache.m_lruConnectionCache;
				lock (lruConnectionCache)
				{
					connectionsToClose = DbConnectionPoolLruCache.EnsureCapacity();
					DbConnectionPoolLruCache.m_lruConnectionCache.Add(connectionEntry);
				}
			});
			RSTrace.DataExtensionTracer.Trace(TraceLevel.Info, "LRU cache capacity complete with duration={0}", new object[] { num });
			if (connectionsToClose != null)
			{
				long num2 = PerformanceUtilities.ExecuteAndMeasureDurationMs(delegate
				{
					foreach (ConnectionEntry connectionEntry2 in connectionsToClose)
					{
						if (!connectionEntry2.OKToGetFromPool)
						{
							if (closeEvictedConnectionsAsync)
							{
								connectionEntry2.CloseConnectionAsync();
							}
							else
							{
								connectionEntry2.CloseConnection();
							}
						}
					}
				});
				RSTrace.DataExtensionTracer.Trace(TraceLevel.Info, "Eviction of {0} connections completed with duration={1}", new object[] { connectionsToClose.Count, num2 });
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000E1F8 File Offset: 0x0000C3F8
		public static void RemoveConnectionEntry(ConnectionEntry connectionEntry)
		{
			LinkedLruCache lruConnectionCache = DbConnectionPoolLruCache.m_lruConnectionCache;
			lock (lruConnectionCache)
			{
				DbConnectionPoolLruCache.m_lruConnectionCache.Remove(connectionEntry);
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000E23C File Offset: 0x0000C43C
		private static List<ConnectionEntry> EnsureCapacity()
		{
			List<ConnectionEntry> list = null;
			LinkedLruCache lruConnectionCache = DbConnectionPoolLruCache.m_lruConnectionCache;
			lock (lruConnectionCache)
			{
				RSTrace.DataExtensionTracer.Trace(TraceLevel.Info, "DbConnectionPool has {0} connections with a limit of {1}", new object[]
				{
					DbConnectionPoolLruCache.m_lruConnectionCache.Count,
					500
				});
				if (DbConnectionPoolLruCache.m_lruConnectionCache.Count >= 500)
				{
					list = new List<ConnectionEntry>();
					while (DbConnectionPoolLruCache.m_lruConnectionCache.Count >= 500)
					{
						ConnectionEntry connectionEntry = (ConnectionEntry)DbConnectionPoolLruCache.m_lruConnectionCache.ExtractLRU();
						connectionEntry.RequestClosing();
						list.Add(connectionEntry);
					}
				}
			}
			return list;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000E2F4 File Offset: 0x0000C4F4
		internal static void CloseConnections()
		{
			LinkedLruCache lruConnectionCache = DbConnectionPoolLruCache.m_lruConnectionCache;
			lock (lruConnectionCache)
			{
				while (DbConnectionPoolLruCache.m_lruConnectionCache.Count > 0)
				{
					((ConnectionEntry)DbConnectionPoolLruCache.m_lruConnectionCache.ExtractLRU()).CloseConnection();
				}
			}
		}

		// Token: 0x04000125 RID: 293
		private const int MAX_POOLED_CONNECTIONS = 500;

		// Token: 0x04000126 RID: 294
		private static LinkedLruCache m_lruConnectionCache = new LinkedLruCache();
	}
}

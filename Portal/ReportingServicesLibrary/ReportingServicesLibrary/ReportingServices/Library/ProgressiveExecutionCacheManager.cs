using System;
using System.Diagnostics;
using System.Web;
using System.Web.Caching;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.ConnectionPool;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000189 RID: 393
	internal static class ProgressiveExecutionCacheManager
	{
		// Token: 0x06000E51 RID: 3665 RVA: 0x00034850 File Offset: 0x00032A50
		public static ProgressiveCacheEntry GetCacheEntry(IRenderEditSession session)
		{
			RSTrace.CatalogTrace.Assert(session != null, "GetCacheEntry: session != null");
			if (!session.IsSessionIdValid)
			{
				return null;
			}
			string text = session.MakeCacheKey();
			ProgressiveCacheEntry progressiveCacheEntry = ProgressiveExecutionCacheManager.m_cache.Get(text) as ProgressiveCacheEntry;
			RSTrace.CacheTracer.Trace(TraceLevel.Verbose, "CacheEntry - GET: " + session.SessionId);
			return progressiveCacheEntry;
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x000348AC File Offset: 0x00032AAC
		public static void PutCacheEntry(IRenderEditSession session, ProgressiveCacheEntry entry)
		{
			RSTrace.CatalogTrace.Assert(session != null && session.IsSessionIdValid, "PutCacheEntry: session != null && session.IsSessionIdValid");
			string text = session.MakeCacheKey();
			TimeSpan timeSpan = new TimeSpan(0, 0, Globals.Configuration.RdlxSessionTimeout);
			string text2 = ((session.ItemPath == null) ? null : session.ItemPath.ToString());
			RSTrace.CacheTracer.Trace(TraceLevel.Verbose, string.Concat(new string[]
			{
				"CacheEntry - PUT: ",
				session.SessionId,
				"; sliding timeout: ",
				timeSpan.ToString(),
				"; user: ",
				session.UserName,
				"; path: ",
				text2
			}));
			ProgressiveExecutionCacheManager.m_cache.Insert(text, entry, null, Cache.NoAbsoluteExpiration, timeSpan, CacheItemPriority.NotRemovable, ProgressiveExecutionCacheManager.m_cacheEvictionCallback);
			ProgressiveReportCounters.Current.ActiveSession.Increment();
			ProgressiveReportCounters.Current.NewSessionTotal.Increment();
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x00034998 File Offset: 0x00032B98
		public static void RemoveCacheEntry(IRenderEditSession session)
		{
			RSTrace.CatalogTrace.Assert(session != null, "RemoveCacheEntry: session != null");
			if (!session.IsSessionIdValid)
			{
				return;
			}
			string text = session.MakeCacheKey();
			RSTrace.CacheTracer.Trace(TraceLevel.Verbose, "CacheEntry - REMOVE: " + session.SessionId + "; user: " + session.UserName);
			if (ProgressiveExecutionCacheManager.m_cache.Remove(text) == null)
			{
				RSTrace.CacheTracer.Trace(TraceLevel.Verbose, "CacheEntry - REMOVE: " + session.SessionId + " not found in cache");
			}
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00034A1C File Offset: 0x00032C1C
		private static void CleanupEvictedCacheEntry(string key, object value, CacheItemRemovedReason reason)
		{
			Guid.NewGuid();
			Guid.NewGuid();
			ProgressiveCacheEntry progressiveCacheEntry = value as ProgressiveCacheEntry;
			if (progressiveCacheEntry != null)
			{
				string clientActivityId = progressiveCacheEntry.ClientActivityId;
			}
			else
			{
				Guid.Empty.ToString();
				RSTrace.CacheTracer.Trace(TraceLevel.Error, "Cache entry isn't a ProgressiveCacheEntry, logging eviction with Guid.Empty");
			}
			RSEventProvider.Current.NotifySessionEvicted(key, reason.ToString());
			RSTrace.CacheTracer.Trace(TraceLevel.Verbose, "CacheEntry - Cleanup: " + key + "; reason: " + reason.ToString());
			try
			{
				ProgressiveReportCounters.Current.ActiveSession.Decrement();
				((IDisposable)value).Dispose();
			}
			catch (Exception ex)
			{
				if (RSTrace.CacheTracer.TraceError)
				{
					RSTrace.CacheTracer.Trace(TraceLevel.Error, "Error removing cached item: " + ex.ToString());
				}
			}
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00034B04 File Offset: 0x00032D04
		public static IDbConnectionPool GetConnectionPool(IRenderEditSession session)
		{
			IDbConnectionPool dbConnectionPool = null;
			ProgressiveCacheEntry cacheEntry = ProgressiveExecutionCacheManager.GetCacheEntry(session);
			if (cacheEntry != null)
			{
				dbConnectionPool = cacheEntry.ConnectionPool;
			}
			return dbConnectionPool;
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00034B28 File Offset: 0x00032D28
		internal static bool TryGetOrCreateCacheEntry(IRenderEditSession session, string userName, string operationName, out ProgressiveCacheEntry entry, out bool entryCreated)
		{
			entryCreated = false;
			if (session.IsNewSession)
			{
				entry = ProgressiveExecutionCacheManager.CreateCacheEntry(userName, session.SessionType);
				entryCreated = true;
			}
			else if (!ProgressiveExecutionCacheManager.TryGetCacheEntry(session, userName, operationName, out entry))
			{
				entry = ProgressiveExecutionCacheManager.CreateCacheEntry(userName, session.SessionType);
				entryCreated = true;
				return false;
			}
			return true;
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00034B78 File Offset: 0x00032D78
		internal static bool TryGetCacheEntry(IRenderEditSession session, string userName, string operationName, out ProgressiveCacheEntry entry)
		{
			entry = ProgressiveExecutionCacheManager.GetCacheEntry(session);
			if (entry == null)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "{0}: session {1} not found", new object[] { operationName, session.SessionId });
				return false;
			}
			return entry.Validate(session.SessionId, userName, operationName);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00034BC5 File Offset: 0x00032DC5
		internal static ProgressiveCacheEntry CreateCacheEntry(string userName, PowerViewSessionType sessionType)
		{
			return ProgressiveExecutionCacheManager.CreateCacheEntry(userName, null, sessionType);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00034BCF File Offset: 0x00032DCF
		private static ProgressiveCacheEntry CreateCacheEntry(string userName)
		{
			return ProgressiveExecutionCacheManager.CreateCacheEntry(userName, null, PowerViewSessionType.RdlxRenderEdit);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00034BD9 File Offset: 0x00032DD9
		public static ProgressiveCacheEntry CreateCacheEntry(string userName, IDbConnectionPool connectionPool)
		{
			return ProgressiveExecutionCacheManager.CreateCacheEntry(userName, connectionPool, PowerViewSessionType.RdlxRenderEdit);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00034BE3 File Offset: 0x00032DE3
		private static ProgressiveCacheEntry CreateCacheEntry(string userName, IDbConnectionPool connectionPool, PowerViewSessionType sessionType)
		{
			ProgressiveCacheEntry progressiveCacheEntry = new ProgressiveCacheEntry(userName, sessionType);
			if (connectionPool == null)
			{
				connectionPool = ConnectionPoolManagerProvider.ConnectionPoolManager.CreateConnectionPool(new global::System.Action(ServerDataExtensionConnection.OnConnectionCloseAction));
			}
			progressiveCacheEntry.ConnectionPool = connectionPool;
			return progressiveCacheEntry;
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00034C10 File Offset: 0x00032E10
		internal static IDbConnectionPool LoadConnectionPool(ProgressiveCacheEntry entry)
		{
			RSTrace.CatalogTrace.Assert(entry != null, "LoadConnectionPool: entry == null");
			IDbConnectionPool connectionPool = entry.ConnectionPool;
			RSTrace.CatalogTrace.Assert(connectionPool != null, "LoadConnectionPool: connectionPool == null");
			return connectionPool;
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x00034C4B File Offset: 0x00032E4B
		internal static int ItemsInCache
		{
			get
			{
				return ProgressiveExecutionCacheManager.m_cache.Count;
			}
		}

		// Token: 0x040005ED RID: 1517
		private static Cache m_cache = HttpRuntime.Cache;

		// Token: 0x040005EE RID: 1518
		private static readonly CacheItemRemovedCallback m_cacheEvictionCallback = new CacheItemRemovedCallback(ProgressiveExecutionCacheManager.CleanupEvictedCacheEntry);
	}
}

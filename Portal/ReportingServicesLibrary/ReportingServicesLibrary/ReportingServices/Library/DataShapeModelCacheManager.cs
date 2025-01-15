using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Caching;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000174 RID: 372
	internal static class DataShapeModelCacheManager
	{
		// Token: 0x06000DAA RID: 3498 RVA: 0x00031BE8 File Offset: 0x0002FDE8
		internal static object GetCacheEntry(ModelCacheKey modelCacheKey, DateTime? modelLastUpdatedTime)
		{
			if (!modelCacheKey.Session.IsSessionIdValid)
			{
				return null;
			}
			RSTrace.CacheTracer.Trace(TraceLevel.Verbose, "ModelCache - CacheEntry - GET: Session Key:{0} , Model Key:{1}, Model last updated time={2}", new object[] { modelCacheKey.SessionKey, modelCacheKey.ModelKey, modelLastUpdatedTime });
			object obj = null;
			IDictionary<string, object> dictionary = DataShapeModelCacheManager.m_cache.Get(modelCacheKey.SessionKey) as IDictionary<string, object>;
			if (dictionary != null)
			{
				object obj2 = ((ICollection)dictionary).SyncRoot;
				lock (obj2)
				{
					if (!dictionary.TryGetValue(modelCacheKey.ModelKey, out obj))
					{
						return null;
					}
				}
				DataShapeModelCacheManager.CacheEntry cacheEntry = (DataShapeModelCacheManager.CacheEntry)obj;
				if (cacheEntry.ModelLastUpdatedTime != null != (modelLastUpdatedTime != null))
				{
					RSEventProvider.Current.NotifyModelCacheLastUpdatedTimeMismatch("GetCacheEntry", cacheEntry.ModelLastUpdatedTime != null, modelLastUpdatedTime != null);
					obj2 = ((ICollection)dictionary).SyncRoot;
					lock (obj2)
					{
						dictionary.Remove(modelCacheKey.ModelKey);
						return null;
					}
				}
				if (cacheEntry.ModelLastUpdatedTime < modelLastUpdatedTime)
				{
					obj2 = ((ICollection)dictionary).SyncRoot;
					lock (obj2)
					{
						if (dictionary.TryGetValue(modelCacheKey.ModelKey, out obj))
						{
							cacheEntry = (DataShapeModelCacheManager.CacheEntry)obj;
							if (cacheEntry.ModelLastUpdatedTime < modelLastUpdatedTime)
							{
								RSTrace.CacheTracer.Trace(TraceLevel.Info, "Local model cache: removing cached model due to ModelLastUpdatedTime change. Cached ModelLastModifiedTime={0}, new ModelLastModifiedTime={1}, modelKey={2}", new object[] { cacheEntry.ModelLastUpdatedTime, modelLastUpdatedTime, modelCacheKey.ModelKey });
								dictionary.Remove(modelCacheKey.ModelKey);
								return null;
							}
						}
					}
				}
				return cacheEntry.Value;
			}
			return null;
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00031E30 File Offset: 0x00030030
		internal static void PutCacheEntry(ModelCacheKey modelCacheKey, object entry, DateTime? modelLastUpdatedTime)
		{
			DataShapeModelCacheManager.InternalPutCacheEntry(modelCacheKey, entry, modelLastUpdatedTime);
			if (DataShapeModelCacheManager.ShouldCopyCacheItemToGlobalScope(modelCacheKey))
			{
				ModelCacheKey globalKey = modelCacheKey.GetGlobalKey();
				if (modelCacheKey != globalKey)
				{
					DataShapeModelCacheManager.InternalPutCacheEntry(globalKey, entry, modelLastUpdatedTime);
				}
			}
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00031E60 File Offset: 0x00030060
		private static void InternalPutCacheEntry(ModelCacheKey modelCacheKey, object entry, DateTime? modelLastUpdatedTime)
		{
			RSTrace.CatalogTrace.Assert(modelCacheKey.Session.IsSessionIdValid, "ModelCache - PutCacheEntry: session.IsSessionIdValid");
			TimeSpan slidingExpiration = modelCacheKey.SlidingExpiration;
			DateTime absoluteExpiration = modelCacheKey.AbsoluteExpiration;
			RSTrace.CacheTracer.Trace(TraceLevel.Verbose, "ModelCache - CacheEntry - PUT: Session Key:{0}, Model Key:{1}, Sliding timeout: {2}, Absolute timeout: {3}, Model last updated time: {4}", new object[]
			{
				modelCacheKey.SessionKey,
				modelCacheKey.ModelKey,
				slidingExpiration.ToString(),
				absoluteExpiration.ToString(),
				modelLastUpdatedTime
			});
			Dictionary<string, object> dictionary = DataShapeModelCacheManager.m_cache.Get(modelCacheKey.SessionKey) as Dictionary<string, object>;
			if (dictionary == null)
			{
				Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
				Cache cache = DataShapeModelCacheManager.m_cache;
				lock (cache)
				{
					dictionary = DataShapeModelCacheManager.m_cache.Get(modelCacheKey.SessionKey) as Dictionary<string, object>;
					if (dictionary == null)
					{
						dictionary = dictionary2;
						DataShapeModelCacheManager.m_cache.Add(modelCacheKey.SessionKey, dictionary, null, absoluteExpiration, slidingExpiration, CacheItemPriority.NotRemovable, DataShapeModelCacheManager.m_cacheEvictionCallback);
					}
				}
			}
			DataShapeModelCacheManager.CacheEntry cacheEntry = new DataShapeModelCacheManager.CacheEntry(entry, modelLastUpdatedTime);
			DataShapeModelCacheManager.CacheEntry cacheEntry2 = null;
			object syncRoot = ((ICollection)dictionary).SyncRoot;
			lock (syncRoot)
			{
				object obj;
				if (dictionary.TryGetValue(modelCacheKey.ModelKey, out obj))
				{
					cacheEntry2 = (DataShapeModelCacheManager.CacheEntry)obj;
					if (modelLastUpdatedTime == null || cacheEntry2.ModelLastUpdatedTime == null || cacheEntry2.ModelLastUpdatedTime < modelLastUpdatedTime)
					{
						dictionary[modelCacheKey.ModelKey] = cacheEntry;
					}
				}
				else
				{
					dictionary[modelCacheKey.ModelKey] = cacheEntry;
				}
			}
			if (cacheEntry2 != null && cacheEntry2.ModelLastUpdatedTime != null != (modelLastUpdatedTime != null))
			{
				RSEventProvider.Current.NotifyModelCacheLastUpdatedTimeMismatch("PutCacheEntry", cacheEntry2.ModelLastUpdatedTime != null, modelLastUpdatedTime != null);
			}
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0003206C File Offset: 0x0003026C
		internal static void RemoveAllCacheEntries(ModelCacheInfo modelCacheInfo)
		{
			foreach (object obj in Enum.GetValues(typeof(ModelCacheScope)))
			{
				string text = ModelCacheKey.MakeSessionKey(modelCacheInfo);
				RSTrace.CacheTracer.Trace(TraceLevel.Verbose, "ModelCache - CacheEntry - REMOVE: {0}; user: {1}, modelScope: {2}", new object[]
				{
					modelCacheInfo.Session.SessionId,
					modelCacheInfo.Session.UserName,
					obj
				});
				if (DataShapeModelCacheManager.m_cache.Remove(text) == null)
				{
					RSTrace.CacheTracer.Trace(TraceLevel.Verbose, "ModelCache - CacheEntry - REMOVE: {0} (modelScope: {1}) not found in cache", new object[]
					{
						modelCacheInfo.Session.SessionId,
						obj
					});
				}
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00032138 File Offset: 0x00030338
		internal static void RemoveCacheEntry(ModelCacheInfo modelCacheInfo)
		{
			if (!modelCacheInfo.Session.IsSessionIdValid || DataShapeModelCacheManager.DetermineModelCacheScope(modelCacheInfo) == ModelCacheScope.Global)
			{
				return;
			}
			string text = ModelCacheKey.MakeSessionKey(modelCacheInfo);
			RSTrace.CacheTracer.Trace(TraceLevel.Verbose, "ModelCache - CacheEntry - REMOVE: {0}; user: {1}", new object[]
			{
				modelCacheInfo.Session.SessionId,
				modelCacheInfo.Session.UserName
			});
			if (DataShapeModelCacheManager.m_cache.Remove(text) == null)
			{
				RSTrace.CacheTracer.Trace(TraceLevel.Verbose, "ModelCache - CacheEntry - REMOVE: {0} not found in cache", new object[] { modelCacheInfo.Session.SessionId });
			}
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x000321C5 File Offset: 0x000303C5
		public static void RemoveCacheEntry(IRenderEditSession session)
		{
			DataShapeModelCacheManager.RemoveCacheEntry(new ModelCacheInfo(session, string.Empty, "EntityDataSource", "2.0", null, false, false));
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x000321E4 File Offset: 0x000303E4
		private static void CleanupEvictedCacheEntry(string key, object value, CacheItemRemovedReason reason)
		{
			RSTrace.CacheTracer.Trace(TraceLevel.Verbose, "ModelCache - CacheEntry - Cleanup: {0}; reason: {1}", new object[]
			{
				key,
				reason.ToString()
			});
			try
			{
				IDictionary<string, object> dictionary = value as IDictionary<string, object>;
				if (dictionary != null)
				{
					object syncRoot = ((ICollection)dictionary).SyncRoot;
					lock (syncRoot)
					{
						dictionary.Clear();
					}
				}
			}
			catch (Exception ex)
			{
				if (RSTrace.CacheTracer.TraceError)
				{
					RSTrace.CacheTracer.Trace(TraceLevel.Error, "ModelCache - Error disposing cached item: " + ex.ToString());
				}
			}
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00032298 File Offset: 0x00030498
		private static bool ShouldCopyCacheItemToGlobalScope(ModelCacheKey modelCacheKey)
		{
			return DataShapeModelCacheManager.DetermineModelCacheScope(modelCacheKey.CacheInfo) == ModelCacheScope.User && modelCacheKey.CacheInfo.IsMultiDimensional;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x000322B8 File Offset: 0x000304B8
		internal static ModelCacheScope DetermineModelCacheScope(ModelCacheInfo cacheInfo)
		{
			bool flag = false;
			IRenderEditSession session = cacheInfo.Session;
			bool flag2 = session.SessionType == PowerViewSessionType.SemanticQuery;
			bool flag3 = session.SessionType == PowerViewSessionType.GetEntityDataModel;
			if (flag2 || flag3)
			{
				flag = true;
			}
			if (cacheInfo.IsMultiDimensional && flag3)
			{
				flag = false;
			}
			if (flag)
			{
				return ModelCacheScope.Global;
			}
			if (!flag2 && !flag3)
			{
				return ModelCacheScope.Session;
			}
			return ModelCacheScope.User;
		}

		// Token: 0x040005A0 RID: 1440
		private static Cache m_cache = HttpRuntime.Cache;

		// Token: 0x040005A1 RID: 1441
		private static readonly CacheItemRemovedCallback m_cacheEvictionCallback = new CacheItemRemovedCallback(DataShapeModelCacheManager.CleanupEvictedCacheEntry);

		// Token: 0x02000474 RID: 1140
		private sealed class CacheEntry
		{
			// Token: 0x06002391 RID: 9105 RVA: 0x00084D39 File Offset: 0x00082F39
			public CacheEntry(object value, DateTime? modelLastUpdatedTime)
			{
				this.Value = value;
				this.ModelLastUpdatedTime = modelLastUpdatedTime;
			}

			// Token: 0x17000A7D RID: 2685
			// (get) Token: 0x06002392 RID: 9106 RVA: 0x00084D4F File Offset: 0x00082F4F
			// (set) Token: 0x06002393 RID: 9107 RVA: 0x00084D57 File Offset: 0x00082F57
			public object Value { get; private set; }

			// Token: 0x17000A7E RID: 2686
			// (get) Token: 0x06002394 RID: 9108 RVA: 0x00084D60 File Offset: 0x00082F60
			// (set) Token: 0x06002395 RID: 9109 RVA: 0x00084D68 File Offset: 0x00082F68
			public DateTime? ModelLastUpdatedTime { get; private set; }
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Caching;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200027B RID: 635
	internal sealed class RSLocalCacheManager : MemoryAuditProxy
	{
		// Token: 0x06001689 RID: 5769 RVA: 0x00059BAA File Offset: 0x00057DAA
		private RSLocalCacheManager()
		{
			this.m_cachePolicy = new ClearAspNetCachePolicy<string, ICachedItem>();
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00059BC8 File Offset: 0x00057DC8
		public void RemovedCallback(string k, object v, CacheItemRemovedReason r)
		{
			try
			{
				ICachedItem cachedItem = v as ICachedItem;
				if (cachedItem != null)
				{
					this.m_cachePolicy.Remove(cachedItem.Key);
					Interlocked.Add(ref this.m_approximateKBytes, -cachedItem.SizeEstimateKb);
				}
				IHierarchicalCachedItem hierarchicalCachedItem = v as IHierarchicalCachedItem;
				if (hierarchicalCachedItem != null)
				{
					List<string> childKeys = hierarchicalCachedItem.ChildKeys;
					lock (childKeys)
					{
						foreach (string text in hierarchicalCachedItem.ChildKeys)
						{
							this.cache.Remove(text);
						}
					}
				}
				IDisposable disposable = v as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			catch (Exception ex)
			{
				if (RSTrace.CacheTracer.TraceError)
				{
					RSTrace.CacheTracer.Trace(TraceLevel.Error, "Error removing cached item: " + ex.ToString());
				}
			}
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00059CD4 File Offset: 0x00057ED4
		internal bool SaveCacheItem(ICachedItem item)
		{
			return item != null && this.SaveToCache(item);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00059CE4 File Offset: 0x00057EE4
		internal object GetCacheItem(string key)
		{
			object obj = this.cache[key];
			ICachedItem cachedItem = obj as ICachedItem;
			if (cachedItem != null)
			{
				this.m_cachePolicy.MarkAsUsed(cachedItem.Key);
			}
			return obj;
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00059D18 File Offset: 0x00057F18
		private bool SaveToCache(ICachedItem item)
		{
			bool flag = false;
			if (this.cache[item.Key] == null)
			{
				Interlocked.Add(ref this.m_approximateKBytes, item.SizeEstimateKb);
				this.cache.Insert(item.Key, item, null, item.ExpirationDate, Cache.NoSlidingExpiration, CacheItemPriority.Normal, new CacheItemRemovedCallback(this.RemovedCallback));
				this.m_cachePolicy.Add(item.Key, item);
				flag = true;
			}
			IHierarchicalCachedItem hierarchicalCachedItem = item as IHierarchicalCachedItem;
			if (hierarchicalCachedItem != null && hierarchicalCachedItem.ParentKey != null)
			{
				IHierarchicalCachedItem hierarchicalCachedItem2 = this.cache[hierarchicalCachedItem.ParentKey] as IHierarchicalCachedItem;
				if (hierarchicalCachedItem2 != null)
				{
					hierarchicalCachedItem.MakeDependentOn(hierarchicalCachedItem2);
				}
			}
			item.NotifyItemIsCached();
			return flag;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00059DC8 File Offset: 0x00057FC8
		private void SaveToCache(SortedList dependencyTree)
		{
			if (dependencyTree == null)
			{
				return;
			}
			int count = dependencyTree.Count;
			for (int i = 0; i < count; i++)
			{
				string text = (string)dependencyTree.GetKey(i);
				ICachedItem cachedItem = (ICachedItem)dependencyTree.GetByIndex(i);
				this.SaveToCache(cachedItem);
			}
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00059E10 File Offset: 0x00058010
		private void PrintCachedItems()
		{
			if (RSTrace.CacheTracer.TraceVerbose)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("Cache content:");
				foreach (object obj in HttpRuntime.Cache)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					ICachedItem cachedItem = this.cache[(string)dictionaryEntry.Key] as ICachedItem;
					if (cachedItem != null)
					{
						stringBuilder.AppendFormat("Key: {0}", dictionaryEntry.Key);
						stringBuilder.AppendLine();
						stringBuilder.AppendLine("\tCached item:");
						stringBuilder.AppendFormat("\tType: {0}", cachedItem.GetType().ToString());
						stringBuilder.AppendLine();
						stringBuilder.AppendFormat("\tExpires: {0}\t Estimated Size: {1} KB", cachedItem.ExpirationDate.ToString(), cachedItem.SizeEstimateKb);
						stringBuilder.AppendLine();
						IHierarchicalCachedItem hierarchicalCachedItem = cachedItem as IHierarchicalCachedItem;
						if (hierarchicalCachedItem != null)
						{
							stringBuilder.AppendFormat("\t\tParent key : {0}", hierarchicalCachedItem.ParentKey);
							stringBuilder.AppendLine();
							stringBuilder.AppendLine("\t\tChildren:");
							List<string> childKeys = hierarchicalCachedItem.ChildKeys;
							lock (childKeys)
							{
								foreach (string text in hierarchicalCachedItem.ChildKeys)
								{
									stringBuilder.AppendFormat("\t\t\t Child key: {0}", text);
									stringBuilder.AppendLine();
								}
							}
						}
						stringBuilder.AppendLine();
					}
				}
				string text2 = stringBuilder.ToString();
				RSTrace.CacheTracer.Trace(TraceLevel.Verbose, text2);
			}
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x0005A018 File Offset: 0x00058218
		public bool RemoveFromCache(string key)
		{
			if (key == null)
			{
				return false;
			}
			if ((ICachedItem)this.cache[key] != null)
			{
				this.cache.Remove(key);
				return true;
			}
			return false;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0005A044 File Offset: 0x00058244
		internal void CacheRenderedResult(CatalogItemContext context, StreamManager streamManager, ReportRenderingResult result, DateTime expirationDate, ReportSnapshot reportSnapshot)
		{
			SortedList sortedList = CompositeCacheBuilder.BuildMainStream(context, result, streamManager, expirationDate, reportSnapshot);
			this.SaveToCache(sortedList);
			this.CacheOnlySecondaryStreams(context, streamManager, result, expirationDate, reportSnapshot);
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0005A074 File Offset: 0x00058274
		internal void CacheOnlySecondaryStreams(CatalogItemContext context, StreamManager streamManager, ReportRenderingResult result, DateTime expirationDate, ReportSnapshot reportSnapshot)
		{
			foreach (RSStream rsstream in streamManager.SecondaryCacheableStreams)
			{
				SortedList sortedList = CompositeCacheBuilder.BuildSecondaryStream(context, result, rsstream, expirationDate, reportSnapshot, false);
				this.SaveToCache(sortedList);
				if (sortedList != null)
				{
					rsstream.Close();
				}
			}
			this.PrintCachedItems();
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x0005A0C0 File Offset: 0x000582C0
		internal void CacheSecondaryStream(CatalogItemContext context, RSStream stream, DateTime expirationDate, ReportSnapshot reportSnapshot)
		{
			SortedList sortedList = CompositeCacheBuilder.BuildSecondaryStream(context, null, stream, expirationDate, reportSnapshot);
			this.SaveToCache(sortedList);
			this.PrintCachedItems();
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0005A0E8 File Offset: 0x000582E8
		internal ReportRenderingResult GetCachedResult(CatalogItemContext context, string streamName, ReportSnapshot reportSnapshot)
		{
			string text;
			if (streamName == null)
			{
				text = CompositeCacheBuilder.BuildMainStreamKey(context, reportSnapshot);
			}
			else
			{
				text = CompositeCacheBuilder.BuildSecondaryStreamKey(context, streamName, reportSnapshot);
			}
			if (text == null)
			{
				return null;
			}
			CachedRenderingResult cachedRenderingResult = (CachedRenderingResult)this.cache[text];
			if (cachedRenderingResult != null)
			{
				if (RSTrace.CacheTracer.TraceVerbose)
				{
					RSTrace.CacheTracer.Trace(TraceLevel.Verbose, "Found key in cache: " + text);
				}
				return cachedRenderingResult.GetRenderingResult();
			}
			if (RSTrace.CacheTracer.TraceVerbose)
			{
				RSTrace.CacheTracer.Trace(TraceLevel.Verbose, "Key not found in cache: " + text);
			}
			this.PrintCachedItems();
			return null;
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0005A17C File Offset: 0x0005837C
		internal bool RemoveCachedReport(CatalogItemContext context)
		{
			string text = CompositeCacheBuilder.BuildReportRootKey(context);
			return this.RemoveFromCache(text);
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0005A197 File Offset: 0x00058397
		internal void CacheModel(ModelCatalogItem.ModelStorage.CachedModel cachedModel)
		{
			this.SaveToCache(cachedModel);
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0005A1A1 File Offset: 0x000583A1
		internal ModelCatalogItem.ModelStorage.CachedModel GetCachedModel(string cachedModelKey)
		{
			return this.GetCacheItem(cachedModelKey) as ModelCatalogItem.ModelStorage.CachedModel;
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0005A1AF File Offset: 0x000583AF
		internal bool RemoveCachedModel(string cachedModelKey)
		{
			return this.RemoveFromCache(cachedModelKey);
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x0005A1B8 File Offset: 0x000583B8
		public override long CurrentMemoryUsageKBytes
		{
			get
			{
				return Interlocked.Read(ref this.m_approximateKBytes);
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x0005A1B8 File Offset: 0x000583B8
		public override long CurrentFreeableMemoryKBytes
		{
			get
			{
				return Interlocked.Read(ref this.m_approximateKBytes);
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x000588F4 File Offset: 0x00056AF4
		public override long PendingFreeKBytes
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x0005A1C5 File Offset: 0x000583C5
		public override double FreeOverhead
		{
			get
			{
				return Microsoft.ReportingServices.Diagnostics.FreeOverhead.None;
			}
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0005A1CC File Offset: 0x000583CC
		public override void SetNewMemoryTarget(long memoryTargetKBytes)
		{
			if (RSTrace.CacheTracer.TraceVerbose)
			{
				RSTrace.CacheTracer.Trace(TraceLevel.Verbose, "Adjusting usage of memory cache.  Currently using approximately {0} kbytes, new target = {1} kbytes", new object[] { this.CurrentMemoryUsageKBytes, memoryTargetKBytes });
			}
			this.m_cachePolicy.PerformEviction(delegate(ICachedItem cachedItem)
			{
				if (this.CurrentMemoryUsageKBytes > memoryTargetKBytes)
				{
					this.cache.Remove(cachedItem.Key);
					return true;
				}
				return false;
			}, true);
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x000053DC File Offset: 0x000035DC
		public override bool CanPerformPartialRelease
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400083E RID: 2110
		internal static readonly RSLocalCacheManager Current = new RSLocalCacheManager();

		// Token: 0x0400083F RID: 2111
		private readonly Cache cache = HttpRuntime.Cache;

		// Token: 0x04000840 RID: 2112
		private readonly ICacheRetentionPolicy<string, ICachedItem> m_cachePolicy;

		// Token: 0x04000841 RID: 2113
		private long m_approximateKBytes;
	}
}

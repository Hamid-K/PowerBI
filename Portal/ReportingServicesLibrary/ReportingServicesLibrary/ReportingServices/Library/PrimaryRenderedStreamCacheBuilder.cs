using System;
using System.Diagnostics;
using System.Text;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000275 RID: 629
	internal sealed class PrimaryRenderedStreamCacheBuilder : PrimaryRenderedStreamKeyOnlyCacheBuilder, ICachedItemBuilder
	{
		// Token: 0x0600166F RID: 5743 RVA: 0x00059518 File Offset: 0x00057718
		internal PrimaryRenderedStreamCacheBuilder(CatalogItemContext context, StreamManager streamManager, ReportRenderingResult procResult)
			: base(context)
		{
			RSTrace.CacheTracer.Assert(streamManager != null);
			RSTrace.CacheTracer.Assert(procResult != null);
			this.m_streamManager = streamManager;
			this.m_procResult = procResult;
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0005954C File Offset: 0x0005774C
		public override bool AppendKeyInformation(StringBuilder key)
		{
			RSTrace.CacheTracer.Assert(key != null);
			if (!this.m_streamManager.IsPrimaryStreamCacheable)
			{
				if (RSTrace.CacheTracer.TraceVerbose)
				{
					RSTrace.CacheTracer.Trace(TraceLevel.Verbose, "Item not cacheable - buffered stream: " + base.ItemContext.OriginalItemPath.Value);
				}
				return false;
			}
			return base.AppendKeyInformation(key);
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x000595B0 File Offset: 0x000577B0
		public IHierarchicalCachedItem CreateCachedItem(string itemKey, DateTime expirationDate, IHierarchicalCachedItem parent)
		{
			RSTrace.CacheTracer.Assert(!string.IsNullOrEmpty(itemKey), "!string.IsNullOrEmpty(itemKey)");
			RSTrace.CacheTracer.Assert(parent != null, "parent != null");
			RSTrace.CacheTracer.Assert(this.m_streamManager.IsPrimaryStreamCacheable, "m_streamManager.IsPrimaryStreamCacheable");
			RSTrace.CacheTracer.Assert(this.m_procResult != null, "m_procResult != null");
			CachedRenderingResult cachedRenderingResult = new CachedRenderingResult(this.m_procResult, this.m_streamManager.PrimaryStream);
			cachedRenderingResult.Key = itemKey;
			cachedRenderingResult.MakeDependentOn(parent);
			CachedData.CacheStream cacheStream = new CachedData.CacheStream(this.m_streamManager.PrimaryStream, cachedRenderingResult.Data);
			this.m_streamManager.SetCachedPrimaryStream(cacheStream);
			return cachedRenderingResult;
		}

		// Token: 0x04000830 RID: 2096
		private readonly StreamManager m_streamManager;

		// Token: 0x04000831 RID: 2097
		private readonly ReportRenderingResult m_procResult;
	}
}
